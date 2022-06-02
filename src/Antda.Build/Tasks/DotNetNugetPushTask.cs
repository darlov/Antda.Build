using System.Linq;
using Antda.Build.BuildProviders;
using Antda.Build.PackageSources;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.NuGet.Push;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("DotNet-Nuget-Push")]
[IsDependentOn(typeof(DotNetPackTask))]
public class DotNetNugetPushTask: FrostingTask<DefaultBuildContext>
{
  private readonly IPackageSourceProvider _packageSourceProvider;

  public DotNetNugetPushTask(IPackageSourceProvider packageSourceProvider)
  {
    _packageSourceProvider = packageSourceProvider;
  }

  public override bool ShouldRun(DefaultBuildContext context)
  {
    return !context.BuildProvider.IsLocalBuild() || context.Parameters.ForceRun;
  }
  
  public override void Run(DefaultBuildContext context)
  {
    var packages = context.GetFiles(context.Paths.OutputNugetPackages + "/*");
    var packageSources = _packageSourceProvider.GetPackageSources();

    foreach (var source in packageSources.Where(source => source.PreRelease == context.IsPreReleaseBranch))
    {
      if (string.IsNullOrEmpty(source.PushSourceUrl))
      {
        context.Warning("Unable to push NuGet Packages to '{0}' as push source URL haven't been provided", source.PrefixName);
      }
      else if (string.IsNullOrEmpty(source.ApiKey))
      {
        context.Warning("Unable to push NuGet Packages to '{0}' as API key haven't been provided", source.PrefixName);
      }
      else
      {
        foreach (var package in packages)
        {
          context.DotNetNuGetPush(package, new DotNetNuGetPushSettings
          {
            Source = source.PushSourceUrl,
            ApiKey = source.ApiKey,
            SkipDuplicate = true
          });
        }
      }
    }
  }
}