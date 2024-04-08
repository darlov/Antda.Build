using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antda.Build.Context;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.IO;
using Cake.Git;
using Microsoft.Extensions.Options;

namespace Antda.Build.BuildProvider.Agents;

public class LocalBuildProvider : BaseBuildProvider
{
  private readonly ICakeContext _context;

  public LocalBuildProvider(ICakeContext context, IOptions<PathOptions> buildOptions)
  {
    _context = context;

    if (!string.IsNullOrEmpty(buildOptions.Value.GitRoot))
    {
      var branch = context.GitBranchCurrent(buildOptions.Value.GitRoot);
      var tags = context.GitTags(buildOptions.Value.GitRoot, true);
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

  public override BuildProviderType Type => BuildProviderType.Local;

  public override string BuildNumber => "-1";

  public override Repository Repository { get; }

  public override Task UploadArtifactAsync(FilePath path)
  {
    _context.Warning("Unable to upload build artifacts. Path: {0}", path);
    return Task.CompletedTask;
  }

  public override void UpdateBuildVersion(string buildVersion) => _context.Warning("Unable to update build version. Build Version: {0}", buildVersion);
  public override IReadOnlyCollection<string> Variables => ArraySegment<string>.Empty;
}