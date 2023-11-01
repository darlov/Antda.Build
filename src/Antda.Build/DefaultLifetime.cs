using System;
using System.Collections.Generic;
using System.Linq;
using Antda.Build.BuildProvider;
using Antda.Build.Context;
using Antda.Build.Types;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.GitVersion;
using Cake.Core;
using Cake.Frosting;
using Cake.Git;
using Spectre.Console;

namespace Antda.Build;

public class DefaultLifetime : FrostingLifetime<DefaultBuildContext>
{
  private readonly IBuildProvider _buildProvider;

  public DefaultLifetime(IBuildProvider buildProvider)
  {
    _buildProvider = buildProvider;
  }

  public override void Setup(DefaultBuildContext context, ISetupContext info)
  {
    if (!string.IsNullOrEmpty(context.Parameters.Title))
    {
      AnsiConsole.Write(new FigletText(context.Parameters.Title).LeftJustified());
      AnsiConsole.WriteLine();
    }

    context.BuildVersion = GetBuildVersion(context);
    context.BranchType = GetBranchType(context);
    context.IsMainRepository = $"{context.Parameters.RepositoryOwner}/{context.Parameters.RepositoryName}".Equals(_buildProvider.Repository.Name, StringComparison.OrdinalIgnoreCase);

    if (context is { IsMainRepository: true, BuildProvider.Repository.IsPullRequest: false })
    {
      if (context.Parameters.PreReleaseBranches.Contains(context.BranchType))
      {
        context.PublishType = PublishType.PreRelease;
      }
      else if (context.Parameters.ReleaseBranches.Contains(context.BranchType) && context.BuildProvider.Repository.IsTagged)
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
      context.GitVersion(new GitVersionSettings
      {
        OutputType = GitVersionOutput.BuildServer,
        NoFetch = true,
        
      });

      var version = context.GitVersion(new GitVersionSettings
      {
        OutputType = GitVersionOutput.Json,
        NoFetch = true
      });

      var milestone = context.Parameters.UsePreRelease ? version.SemVer : version.MajorMinorPatch;
      return new BuildVersion(milestone, version.MajorMinorPatch, version.SemVer, version.InformationalVersion);
    }

    return new BuildVersion("0.1.0", "0.1.0", "0.1.0-beta.0", "0.1.0-beta.0+Branch.local.Sha.5a030134417cb4ee281bb74aaf61bb046f722272");
  }

  private BranchType GetBranchType(DefaultBuildContext context)
  {
    BranchType? branchType;

    if (context.BuildProvider.Repository.IsTagged)
    {
      var branches = context.GitBranches(context.Paths.GitRoot);
      branchType = GetBranchType(context, branches.Select(m => m.FriendlyName));
    }
    else
    {
      branchType = GetBranchType(context, _buildProvider.Repository.BranchName);
    }

    if (branchType == null)
    {
      return string.IsNullOrEmpty(_buildProvider.Repository.BranchName) || _buildProvider.Repository.BranchName == StringNone.Value ? BranchType.None : BranchType.Other;
    }

    return branchType.Value;
  }

  private BranchType? GetBranchType(DefaultBuildContext context, IEnumerable<string> branches)
  {
    var mappings = new List<(string Pattern, BranchType type)>
    {
      (context.Patterns.MasterBranch, BranchType.Master),
      (context.Patterns.DevelopBranch, BranchType.Develop),
      (context.Patterns.ReleaseBranch, BranchType.Release),
      (context.Patterns.HotfixBranch, BranchType.Hotfix)
    };

    var branchNames = branches.ToList();
    foreach (var mapping in mappings)
    {
      foreach (var branchName in branchNames)
      {
        if (branchName.StartsWith(mapping.Pattern, StringComparison.OrdinalIgnoreCase))
        {
          return mapping.type;
        }
      }
    }

    return null;
  }

  private BranchType? GetBranchType(DefaultBuildContext context, params string[] branches) => GetBranchType(context, (IEnumerable<string>)branches);
}