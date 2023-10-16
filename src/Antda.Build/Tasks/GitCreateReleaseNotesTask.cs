using Cake.Common.Tools.GitReleaseManager;
using Cake.Common.Tools.GitReleaseManager.Create;
using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Git-Create-Release-Notes")]
public class GitCreateReleaseNotesTask : FrostingTask<DefaultBuildContext>
{
  public override void Run(DefaultBuildContext context)
  {
    var settings = new GitReleaseManagerCreateSettings
    {
      Milestone = context.BuildVersion.Milestone,
      Name = context.BuildVersion.Milestone,
      TargetCommitish = context.Parameters.UsePreRelease ? context.BuildProvider.Repository.BranchName : context.Patterns.MasterBranch,
      Prerelease = context.Parameters.UsePreRelease,
      NoLogo = true,
      Debug = context.Log.Verbosity == Verbosity.Verbose,
      Verbose = context.Log.Verbosity == Verbosity.Diagnostic
    };

    context.GitReleaseManagerCreate(context.Github.GithubToken, context.Github.RepositoryOwner, context.Github.RepositoryName, settings);
  }
}