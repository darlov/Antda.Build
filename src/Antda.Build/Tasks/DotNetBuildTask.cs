using Antda.Build.BuildProvider;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("DotNet-Build")]
[IsDependentOn(typeof(DotNetRestoreTask))]
public sealed class DotNetBuildTask : FrostingTask<DefaultBuildContext>
{
  public override void Run(DefaultBuildContext context)
  {
    var searchPath = $"{context.Paths.Source}/{context.Patterns.Projects}";
    var projects = context.GetFiles(searchPath);

    foreach (var project in projects)
    {
      context.DotNetBuild(project.FullPath, new DotNetBuildSettings
      {
        Configuration = context.Parameters.Configuration,
        NoRestore = true,
        MSBuildSettings = new DotNetMSBuildSettings
        {
          Version = context.BuildVersion.SemVersion,
          InformationalVersion = context.BuildVersion.InformationalVersion,
          ContinuousIntegrationBuild = !context.BuildProvider.IsLocalBuild()
        }
      });
    }
  }
}