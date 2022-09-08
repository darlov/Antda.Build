using System.Linq;
using Antda.Build.Context;
using Antda.Build.Types;
using Cake.Common.Build.AzurePipelines;
using Cake.Core;
using Cake.Core.IO;
using Cake.Git;
using LibGit2Sharp;

namespace Antda.Build.BuildProviders;

public class AzurePipelinesBuildProvider : IBuildProvider
{
  private readonly IAzurePipelinesProvider _azurePipelinesProvider;

  public AzurePipelinesBuildProvider(IAzurePipelinesProvider azurePipelinesProvider, ICakeContext context, PathOptions pathOptions)
  {
    _azurePipelinesProvider = azurePipelinesProvider;
    BuildNumber = _azurePipelinesProvider.Environment.Build.Number;
    var repositoryName = _azurePipelinesProvider.Environment.Repository.RepoName;

    var (branchName, isTag) = GetBranchInfo(context, pathOptions);

    Repository = new Repository(repositoryName, true)
    {
      BranchName = branchName ?? StringNone.Value,
      IsPullRequest = _azurePipelinesProvider.Environment.PullRequest.IsPullRequest,
      IsTag = isTag,
      TagName = isTag ? branchName : null
    };
  }
  
  public BuildProviderType Type => BuildProviderType.AzurePipelines;

  public string BuildNumber { get; }

  public Repository Repository { get; }

  public void UploadArtifact(FilePath path)
  {
    throw new System.NotImplementedException();
  }

  public void UpdateBuildVersion(string buildVersion)
  {
    throw new System.NotImplementedException();
  }

  private (string? Name, bool IsTag) GetBranchInfo(ICakeContext context, PathOptions pathOptions)
  {
    const string headBranchPrefix = "refs/heads/";
    const string tagBranchPrefix = "refs/tags/";
    
    var fullBranch = _azurePipelinesProvider.Environment.Repository.SourceBranch;

    if (!string.IsNullOrEmpty(fullBranch))
    {
      if (fullBranch.StartsWith(headBranchPrefix))
      {
        return (fullBranch[headBranchPrefix.Length..], false);
      }
      
      if (fullBranch.StartsWith(tagBranchPrefix))
      {
        if (!string.IsNullOrEmpty(pathOptions.GitRoot))
        {
          var lastCommit = context.GitLog(pathOptions.GitRoot, 1).First();

          var currentBranch = context.GitBranchCurrent(pathOptions.GitRoot);

          var repo = new LibGit2Sharp.Repository(pathOptions.GitRoot);

          var b = repo.Refs.Where(b => b.IsRemoteTrackingBranch);
          foreach (var branch in  context.GitBranches(pathOptions.GitRoot))
          {
            
          }
        }
        return (fullBranch[tagBranchPrefix.Length..], true);
      }
    }

    return (null, false);
  }
}