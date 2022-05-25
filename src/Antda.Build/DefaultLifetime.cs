using Antda.Build.BuildProviders;
using Antda.Build.Context;
using Cake.Common.Tools.GitVersion;
using Cake.Core;
using Cake.Frosting;

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
    context.BuildVersion = GetBuildVersion(context);

    _buildProvider.UpdateBuildVersion($"{context.BuildVersion.SemVer}_{_buildProvider.BuildNumber}");
  }

  private BuildVersion GetBuildVersion(ICakeContext context)
  {
    var gitVersion = context.GitVersion(new GitVersionSettings
    {
      OutputType = GitVersionOutput.Json,
      NoFetch = true,
      Verbosity = GitVersionVerbosity.Normal
    });

    return new BuildVersion(gitVersion.SemVer, gitVersion.InformationalVersion);
  }

  public override void Teardown(DefaultBuildContext context, ITeardownContext info)
  {
  }
}