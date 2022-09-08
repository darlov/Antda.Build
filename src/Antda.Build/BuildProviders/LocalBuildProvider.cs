using System.Linq;
using Antda.Build.Context;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.IO;
using Cake.Git;

namespace Antda.Build.BuildProviders;

public class LocalBuildProvider : IBuildProvider
{
  private readonly ICakeContext _context;

  public LocalBuildProvider(ICakeContext context, PathOptions pathOptions)
  {
    _context = context;

    if (!string.IsNullOrEmpty(pathOptions.GitRoot))
    {
      var branch = context.GitBranchCurrent(pathOptions.GitRoot);
      var tags = context.GitTags(pathOptions.GitRoot, true);
      var tag = tags?.FirstOrDefault(m => m.Target.Sha == branch.Tip.Sha);
      var isTag = tag != null;
      var tagName = tag != null ? tag.FriendlyName : null;

      Repository = new Repository("Local", true)
      {
        BranchName = branch.FriendlyName,
        TagName = tagName,
        IsTag = isTag
      };
    }
    else
    {
      Repository = new Repository("Local", false);
    }
  }

  public BuildProviderType Type => BuildProviderType.Local;

  public string BuildNumber => "-1";

  public Repository Repository { get; }

  public void UploadArtifact(FilePath path) => _context.Warning("Unable to upload build artifacts. Path: {0}", path);

  public void UpdateBuildVersion(string buildVersion) => _context.Warning("Unable to update build version. Build Version: {0}", buildVersion);
}