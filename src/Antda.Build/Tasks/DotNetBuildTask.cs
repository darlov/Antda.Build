using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("DotNet-Build")]
[IsDependentOn(typeof(DotNetRestoreTask))]
public sealed class DotNetBuildTask : FrostingTask<DefaultBuildContext>
{
  // Tasks can be asynchronous
  public override void Run(DefaultBuildContext context)
  {
    var projects = context.GetFiles(context.Options.ProjectsPattern);
    foreach (var project in projects)
    {
      context.DotNetBuild(project.FullPath, new DotNetCoreBuildSettings
      {
        Configuration = context.BuildConfiguration,
        NoRestore = true,
        MSBuildSettings = new DotNetMSBuildSettings
        {
          Version = context.BuildVersion.SemVer
        }
      });
    }
  }
}