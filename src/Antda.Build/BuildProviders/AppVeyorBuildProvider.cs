using Antda.Build.Context;
using Cake.Common.Build;
using Cake.Common.Build.AppVeyor;
using Cake.Core;
using Cake.Core.IO;

namespace Antda.Build.BuildProviders;

public class AppVeyorBuildProvider : IBuildProvider
{
  private readonly IAppVeyorProvider _appVeyorProvider;
  
  public AppVeyorBuildProvider(IAppVeyorProvider appVeyorProvider)
  {
    _appVeyorProvider = appVeyorProvider;
    BuildNumber = _appVeyorProvider.Environment.Build.Number.ToString();
    BranchName = _appVeyorProvider.Environment.Repository.Branch;
    RepositoryName = _appVeyorProvider.Environment.Repository.Name;
    IsPullRequest = _appVeyorProvider.Environment.PullRequest.IsPullRequest;
  }

  public BuildProviderType Type => BuildProviderType.AppVeyor;
  public string BuildNumber { get; }
  public bool IsPullRequest { get; }
  public string BranchName { get; }
  public string RepositoryName { get; }
  
  public void UploadArtifact(FilePath path) => _appVeyorProvider.UploadArtifact(path);

  public void UpdateBuildVersion(string buildVersion) => _appVeyorProvider.UpdateBuildVersion(buildVersion);
}