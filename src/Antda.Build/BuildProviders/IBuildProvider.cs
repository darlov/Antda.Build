using Cake.Core.IO;

namespace Antda.Build.BuildProviders;

public interface IBuildProvider
{
  BuildProviderType Type { get; } 
  
  string BuildNumber { get; }
  
  bool IsPullRequest { get; }
  
  public string BranchName { get; }

  public string RepositoryName { get; }
  
  void UploadArtifact(FilePath path);

  void UpdateBuildVersion(string buildVersion);
}