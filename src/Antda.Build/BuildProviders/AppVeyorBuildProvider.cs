using Cake.Common.Build.AppVeyor;
using Cake.Core.IO;

namespace Antda.Build.BuildProviders;

public class AppVeyorBuildProvider : IBuildProvider
{
  private readonly IAppVeyorProvider _appVeyorProvider;

  public AppVeyorBuildProvider(IAppVeyorProvider appVeyorProvider)
  {
    _appVeyorProvider = appVeyorProvider;
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

  public BuildProviderType Type => BuildProviderType.AppVeyor;
  
  public string BuildNumber { get; }

  public Repository Repository { get; }

  public void UploadArtifact(FilePath path) => _appVeyorProvider.UploadArtifact(path);

  public void UpdateBuildVersion(string buildVersion) => _appVeyorProvider.UpdateBuildVersion(buildVersion);
}