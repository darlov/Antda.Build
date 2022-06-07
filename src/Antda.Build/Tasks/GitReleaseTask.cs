using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Git-Release")]
[IsDependentOn(typeof(UploadArtifactsTask))]
public class GitReleaseTask : FrostingTask<DefaultBuildContext>
{
  public override void Run(DefaultBuildContext context)
  {
    //context.GitReleaseManagerAddAssets();
  }
}