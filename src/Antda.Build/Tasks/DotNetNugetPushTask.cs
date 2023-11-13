using System;
using System.Collections.Generic;
using System.Linq;
using Antda.Build.BuildProvider;
using Antda.Build.Context;
using Antda.Build.PackageSources;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.NuGet.Push;
using Cake.Core.IO;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("DotNet-Nuget-Push")]
[IsDependentOn(typeof(DotNetPackTask))]
public class DotNetNugetPushTask : FrostingTask<DefaultBuildContext>
{
  private readonly IPackageSourceProvider _packageSourceProvider;

  public DotNetNugetPushTask(IPackageSourceProvider packageSourceProvider)
  {
    _packageSourceProvider = packageSourceProvider;
  }
  
  public override bool ShouldRun(DefaultBuildContext context)
  {
    return context.Parameters.ForceRun || !context.BuildProvider.IsLocalBuild() && context.PublishType is PublishType.Release or PublishType.PreRelease;
  }

  public override void Run(DefaultBuildContext context)
  {
    var packages = context.GetFiles(context.Paths.OutputNugetPackages + "/*.nupkg")
      .OrderBy(m => m.FullPath)
      .ToList();
    
    var packageSources = GetPackageSources(context.PublishType);

    foreach (var source in packageSources)
    {
      PushNuget(context, source, packages);
    }
  }

  private void PushNuget(DefaultBuildContext context, PackageSource source, IReadOnlyCollection<FilePath> packages)
  {
    if (string.IsNullOrEmpty(source.PushSourceUrl))
    {
      context.Warning("Unable to push NuGet Packages to '{0}' as push source URL haven't been provided. Env Name: {0}__{1}", source.PrefixName, nameof(source.PushSourceUrl));
    }
    else if (string.IsNullOrEmpty(source.ApiKey))
    {
      context.Warning("Unable to push NuGet Packages to '{0}' as API key haven't been provided. Env Name: {0}__{1}", source.PrefixName, nameof(source.ApiKey));
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

  private IEnumerable<PackageSource> GetPackageSources(PublishType publishType)
  {
    var packageSources = _packageSourceProvider.GetPackageSources();
    return publishType switch
    {
      PublishType.Release => packageSources.Where(source => !source.PreRelease),
      PublishType.PreRelease => packageSources.Where(source => source.PreRelease),
      _ => throw new ArgumentOutOfRangeException(nameof(publishType), publishType, null)
    };
  }
}