using System.Collections.Generic;
using System.Threading.Tasks;
using Cake.Common.Build;
using Cake.Common.Build.AppVeyor;
using Cake.Core;
using Cake.Core.IO;

namespace Antda.Build.BuildProvider.Agents;

public class AppVeyorBuildProvider : BaseBuildProvider
{
  private readonly IAppVeyorProvider _appVeyorProvider;

  public AppVeyorBuildProvider(ICakeContext context)
  {
    _appVeyorProvider = context.AppVeyor();
    BuildNumber = _appVeyorProvider.Environment.Build.Number.ToString();
    var repositoryName = _appVeyorProvider.Environment.Repository.Name;

    Repository = new Repository(repositoryName, true)
    {
      BranchName = _appVeyorProvider.Environment.Repository.Branch,
      IsPullRequest = _appVeyorProvider.Environment.PullRequest.IsPullRequest,
      IsTag = _appVeyorProvider.Environment.Repository.Tag.IsTag,
      TagName = _appVeyorProvider.Environment.Repository.Tag.Name
    };
  }

  public override BuildProviderType Type => BuildProviderType.AppVeyor;
  public override string BuildNumber { get; }

  public override Repository Repository { get; }

  public override Task UploadArtifactAsync(FilePath path)
  {
    _appVeyorProvider.UploadArtifact(path);
    return Task.CompletedTask;
  }

  public override void UpdateBuildVersion(string buildVersion) => _appVeyorProvider.UpdateBuildVersion(buildVersion);

  public override IReadOnlyCollection<string> Variables => new[]
  {
    "APPVEYOR_API_URL",
    "APPVEYOR_BUILD_FOLDER",
    "APPVEYOR_BUILD_ID",
    "APPVEYOR_BUILD_NUMBER",
    "APPVEYOR_BUILD_VERSION",
    "APPVEYOR_FORCED_BUILD",
    "APPVEYOR_JOB_ID",
    "APPVEYOR_PROJECT_ID",
    "APPVEYOR_PROJECT_NAME",
    "APPVEYOR_PROJECT_SLUG",
    "APPVEYOR_PULL_REQUEST_NUMBER",
    "APPVEYOR_PULL_REQUEST_TITLE",
    "APPVEYOR_RE_BUILD",
    "APPVEYOR_REPO_COMMIT_AUTHOR",
    "APPVEYOR_REPO_COMMIT_TIMESTAMP",
    "APPVEYOR_REPO_NAME",
    "APPVEYOR_REPO_PROVIDER",
    "APPVEYOR_REPO_SCM",
    "APPVEYOR_REPO_TAG_NAME",
    "APPVEYOR_REPO_TAG",
    "APPVEYOR_SCHEDULED_BUILD",
    "CI",
    "CONFIGURATION",
    "PLATFORM",
  };
}