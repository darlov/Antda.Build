using System.Threading.Tasks;
using Antda.Build.BuildProvider;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Upload-Artifacts")]
[IsDependentOn(typeof(DotNetPackTask))]
public class UploadArtifactsTask : AsyncFrostingTask<DefaultBuildContext>
{
  public override bool ShouldRun(DefaultBuildContext context) => !context.BuildProvider.IsLocalBuild() || context.Parameters.ForceRun;

  public override async Task RunAsync(DefaultBuildContext context)
  {
    var packages = context.GetFiles(context.Paths.OutputNugetPackages + "/*");

    foreach (var package in packages)
    {
      await context.BuildProvider.UploadArtifactAsync(package);
      context.Information("Uploaded artifact file {0}", package);
    }
  }
}