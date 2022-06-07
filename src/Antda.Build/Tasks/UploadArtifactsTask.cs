using Antda.Build.BuildProviders;
using Cake.Common.IO;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Upload-Artifacts")]
[IsDependentOn(typeof(DotNetPackTask))]
public class UploadArtifactsTask : FrostingTask<DefaultBuildContext>
{
  public override bool ShouldRun(DefaultBuildContext context) => !context.BuildProvider.IsLocalBuild() || context.Parameters.ForceRun;

  public override void Run(DefaultBuildContext context)
  {
    var packages = context.GetFiles(context.Paths.OutputNugetPackages + "/*");

    foreach (var package in packages)
    {
      context.BuildProvider.UploadArtifact(package);
    }
  }
}