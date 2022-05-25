using Antda.Build.Context;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.IO;
using Cake.Git;

namespace Antda.Build.BuildProviders;

public class LocalBuildProvider : IBuildProvider
{
  private readonly ICakeContext _context;
  public LocalBuildProvider(ICakeContext context, BuildOptions buildOptions)
  {
    _context = context;

    var gitRoot = context.GitFindRootFromPath(buildOptions.RootDirectoryPath);
    var branch = context.GitBranchCurrent(gitRoot);

    BranchName = branch.FriendlyName;
  }

  public BuildProviderType Type => BuildProviderType.Local;

  public string BuildNumber => "-1";

  public bool IsPullRequest => false;

  public string BranchName { get; }

  public string RepositoryName => "Local";
  
  public void UploadArtifact(FilePath path)
  {
    _context.Warning("Unable to upload build artifacts. Path: {0}", path);
  }

  public void UpdateBuildVersion(string buildVersion)
  {
    _context.Warning("Unable to update build number. Build Version: {0}", buildVersion);
  }
}