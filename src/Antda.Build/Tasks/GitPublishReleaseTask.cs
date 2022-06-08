using Antda.Build.BuildProviders;
using Antda.Build.Context;
using Cake.Common.IO;
using Cake.Common.Tools.GitReleaseManager;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Git-Publish-Release")]
[IsDependentOn(typeof(DotNetPackTask))]
public class GitPublishReleaseTask : FrostingTask<DefaultBuildContext>
{
  public override bool ShouldRun(DefaultBuildContext context) => !context.BuildProvider.IsLocalBuild() && context.PublishType == PublishType.Release;

  public override void Run(DefaultBuildContext context)
  {
    var packages = context.GetFiles(context.Paths.OutputNugetPackages + "/*");
    foreach (var package in packages)
    {
      context.GitReleaseManagerAddAssets(context.Github.GithubToken, context.Github.RepositoryOwner, context.Github.RepositoryName, context.BuildVersion.Milestone, package.FullPath);
    }

    context.GitReleaseManagerClose(context.Github.GithubToken, context.Github.RepositoryOwner, context.Github.RepositoryName, context.BuildVersion.Milestone);
  }
}