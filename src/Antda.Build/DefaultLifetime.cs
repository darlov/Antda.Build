using System;
using System.Linq;
using System.Text.RegularExpressions;
using Antda.Build.BuildProviders;
using Antda.Build.Context;
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
    AnsiConsole.Write(new FigletText(context.Parameters.Title)
      .LeftAligned());

    context.BuildVersion = GetBuildVersion(context);
    context.BranchType = GetBranchType(context);
    context.IsPreReleaseBranch = !context.Parameters.BranchesToRelease.Contains(context.BranchType);
    context.IsMainRepository =  $"{context.Parameters.RepositoryOwner}/{context.Parameters.RepositoryName}".Equals(_buildProvider.Repository.Name, StringComparison.OrdinalIgnoreCase);

    if (context.Parameters.UpdateBuildNumber)
    {
      var version = $"{context.BuildVersion.SemVer}_{_buildProvider.BuildNumber}";
      context.Information("Updating build number to {0}", version);
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
        OutputType = GitVersionOutput.Json,
        NoFetch = true,

      });

      return new BuildVersion(gitVersion.SemVer, gitVersion.InformationalVersion);
    }

    return new BuildVersion("0.1.0-beta.0", "0.1.0-beta.0+Branch.local.Sha.5a030134417cb4ee281bb74aaf61bb046f722272");
  }

  private BranchType GetBranchType(DefaultBuildContext context)
  {
    if (Regex.IsMatch(_buildProvider.Repository.BranchName, context.Patterns.MasterBranch, RegexOptions.IgnoreCase))
    {
      return BranchType.Master;
    }
    
    if (Regex.IsMatch(_buildProvider.Repository.BranchName, context.Patterns.DevelopBranch, RegexOptions.IgnoreCase))
    {
      return BranchType.Develop;
    }
    
    if (Regex.IsMatch(_buildProvider.Repository.BranchName, context.Patterns.ReleaseBranch, RegexOptions.IgnoreCase))
    {
      return BranchType.Release;
    }
    
    if (Regex.IsMatch(_buildProvider.Repository.BranchName, context.Patterns.HotfixBranch, RegexOptions.IgnoreCase))
    {
      return BranchType.Hotfix;
    }

    return string.IsNullOrEmpty(_buildProvider.Repository.BranchName) ? BranchType.None : BranchType.Other;
  }
}