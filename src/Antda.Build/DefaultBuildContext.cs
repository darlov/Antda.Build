using Antda.Build.Context;
using Cake.Common;
using Cake.Common.Build;
using Cake.Common.Build.AppVeyor;
using Cake.Core;
using Cake.Frosting;
using Microsoft.Extensions.Options;

namespace Antda.Build;

public class DefaultBuildContext : FrostingContext
{
  public DefaultBuildContext(ICakeContext context, IOptions<BuildOptions> buildOptions)
    : base(context)
  {
    BuildSystem = this.BuildSystem();
    VariableNames = new EnvironmentVariableNames();
    Options = buildOptions.Value;
    
    BuildConfiguration = context.Argument("configuration", "Release");
  }

  public BuildOptions Options { get; }

  public BuildSystem BuildSystem { get; }

  public bool IsLocalBuild => BuildSystem.IsLocalBuild;

  public bool IsAppVeyor => AppVeyor.IsRunningOnAppVeyor;

  public IAppVeyorProvider AppVeyor => BuildSystem.AppVeyor;
  
  public BuildVersion BuildVersion { get; set; } = null!;

  public EnvironmentVariableNames VariableNames { get; }

  public string BuildConfiguration { get; }
}