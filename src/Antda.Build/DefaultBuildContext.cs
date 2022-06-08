using Antda.Build.BuildProviders;
using Antda.Build.Context;
using Cake.Core;
using Cake.Frosting;
using Microsoft.Extensions.Options;

namespace Antda.Build;

public class DefaultBuildContext : FrostingContext
{
  public DefaultBuildContext(
    ICakeContext context,
    IBuildProvider buildProvider,
    IOptions<GithubOptions> githubOptions,
    IOptions<PatternOptions> patterns,
    IOptions<PathOptions> paths,
    IOptions<ParameterOptions> parameters,
    BuildPlatform buildPlatform)
    : base(context)
  {
    BuildProvider = buildProvider;
    BuildPlatform = buildPlatform;
    Patterns = patterns.Value;
    Paths = paths.Value;
    Parameters = parameters.Value;
    Github = githubOptions.Value;
  }

  public PatternOptions Patterns { get; }

  public PathOptions Paths { get; }

  public ParameterOptions Parameters { get; }

  public GithubOptions Github { get; }

  public IBuildProvider BuildProvider { get; }

  public BuildVersion BuildVersion { get; set; } = null!;

  public BuildPlatform BuildPlatform { get; }

  public BranchType BranchType { get; set; }
  public bool IsMainRepository { get; set; }

  public PublishType PublishType { get; set; } = PublishType.None;
}