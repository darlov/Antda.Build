using Antda.Build.BuildProviders;
using Cake.Common.IO;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Publish-Artifact")]
[IsDependentOn(typeof(DotNetPackTask))]
public class PublishArtifactTask : FrostingTask<DefaultBuildContext>
{
  private readonly IBuildProvider _buildProvider;

  public PublishArtifactTask(IBuildProvider buildProvider)
  {
    _buildProvider = buildProvider;
  }

  // public override bool ShouldRun(DefaultBuildContext context)
  // {
  //   return !context.IsLocalBuild;
  // }

  public override void Run(DefaultBuildContext context)
  {
    var packages = context.GetFiles(context.Options.OutputNugetPackagesDirectoryPath + "/*");

    foreach (var package in packages)
    {
      _buildProvider.UploadArtifact(package);
    }
  }
}