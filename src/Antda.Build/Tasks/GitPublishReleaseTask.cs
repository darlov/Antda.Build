using System.Linq;
using Antda.Build.BuildProvider;
using Antda.Build.Context;
using Cake.Common.IO;
using Cake.Common.Tools.GitReleaseManager;
using Cake.Common.Tools.GitReleaseManager.AddAssets;
using Cake.Common.Tools.GitReleaseManager.Close;
using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Git-Publish-Release")]
[IsDependentOn(typeof(DotNetPackTask))]
public class GitPublishReleaseTask : FrostingTask<DefaultBuildContext>
{
  public override bool ShouldRun(DefaultBuildContext context) => !context.BuildProvider.IsLocalBuild() && context.PublishType == PublishType.Release;

  public override void Run(DefaultBuildContext context)
  {
    var addSetting = new GitReleaseManagerAddAssetsSettings
    {
      NoLogo = true
    };
    
    var packages = context.GetFiles(context.Paths.OutputNugetPackages + "/*")
      .OrderBy(m => m.FullPath);

    foreach (var package in packages)
    {
      context.GitReleaseManagerAddAssets(context.Github.GithubToken, context.Github.RepositoryOwner, context.Github.RepositoryName, context.BuildVersion.Milestone, package.FullPath, addSetting);
    }

    var closeSetting = new GitReleaseManagerCloseMilestoneSettings
    {
      NoLogo = true,
      Debug = context.Log.Verbosity == Verbosity.Verbose,
      Verbose = context.Log.Verbosity == Verbosity.Diagnostic
    };

    context.GitReleaseManagerClose(context.Github.GithubToken, context.Github.RepositoryOwner, context.Github.RepositoryName, context.BuildVersion.Milestone, closeSetting);
  }
}