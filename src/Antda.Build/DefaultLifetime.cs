using System;
using System.Collections.Generic;
using System.Linq;
using Antda.Build.BuildProviders;
using Antda.Build.Context;
using Antda.Build.Types;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.GitVersion;
using Cake.Core;
using Cake.Frosting;
using Spectre.Console;

namespace Antda.Build;

public class DefaultLifetime : FrostingLifetime<DefaultBuildContext>
{
  private readonly IBuildProvider _buildProvider;

  public DefaultLifetime(IBuildProvider buildProvider)
  {
    _buildProvider = buildProvider;
  }

  public override void Setup(DefaultBuildContext context)
  {
    if (!string.IsNullOrEmpty(context.Parameters.Title))
    {
      AnsiConsole.Write(new FigletText(context.Parameters.Title).LeftAligned());
      AnsiConsole.WriteLine();
    }

    context.BuildVersion = GetBuildVersion(context);
    context.BranchType = GetBranchType(context);
    context.IsMainRepository = $"{context.Parameters.RepositoryOwner}/{context.Parameters.RepositoryName}".Equals(_buildProvider.Repository.Name, StringComparison.OrdinalIgnoreCase);

    if (context.IsMainRepository && !context.BuildProvider.Repository.IsPullRequest)
    {
      if (context.Parameters.PreReleaseBranches.Contains(context.BranchType))
      {
        context.PublishType = PublishType.PreRelease;
      }
      else if (context.Parameters.ReleaseBranches.Contains(context.BranchType) && context.BuildProvider.Repository.IsTag && !string.IsNullOrEmpty(context.BuildProvider.Repository.TagName))
      {
        context.PublishType = PublishType.Release;
      }
    }

    if (context.Parameters.UpdateBuildNumber && !context.BuildProvider.IsLocalBuild())
    {
      var version = $"{context.BuildVersion.SemVersion}_{_buildProvider.BuildNumber}";
      context.Information("Updating build version to {0}", version);
      _buildProvider.UpdateBuildVersion(version);
    }
  }

  public override void Teardown(DefaultBuildContext context, ITeardownContext info)
  {
  }

  private BuildVersion GetBuildVersion(DefaultBuildContext context)
  {
    if (context.BuildProvider.Repository.Exist)
    {
      var gitVersion = context.GitVersion(new GitVersionSettings
      {
        OutputType = GitVersionOutput.Json
      });

      var milestone = context.Parameters.UsePreRelease ? gitVersion.SemVer : gitVersion.MajorMinorPatch;

      return new BuildVersion(milestone, gitVersion.MajorMinorPatch, gitVersion.SemVer, gitVersion.InformationalVersion);
    }

    return new BuildVersion("0.1.0", "0.1.0", "0.1.0-beta.0", "0.1.0-beta.0+Branch.local.Sha.5a030134417cb4ee281bb74aaf61bb046f722272");
  }

  private BranchType GetBranchType(DefaultBuildContext context)
  {
    var mapping = new List<(string Pattern, BranchType type)>
    {
      (context.Patterns.MasterBranch, BranchType.Master),
      (context.Patterns.DevelopBranch, BranchType.Develop),
      (context.Patterns.ReleaseBranch, BranchType.Release),
      (context.Patterns.HotfixBranch, BranchType.Hotfix)
    };

    foreach (var (pattern, type) in mapping)
    {
      if (pattern.StartsWith(_buildProvider.Repository.BranchName, StringComparison.OrdinalIgnoreCase))
      {
        return type;
      }
    }

    return string.IsNullOrEmpty(_buildProvider.Repository.BranchName) || _buildProvider.Repository.BranchName == StringNone.Value ? BranchType.None : BranchType.Other;
  }
}