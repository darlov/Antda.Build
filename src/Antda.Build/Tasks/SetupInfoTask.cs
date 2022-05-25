using System;
using Antda.Build.BuildProviders;
using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Setup-Info")]
public class SetupInfoTask : FrostingTask<DefaultBuildContext>
{
  private readonly IBuildProvider _buildProvider;

  public SetupInfoTask(IBuildProvider buildProvider)
  {
    _buildProvider = buildProvider;
  }

  public override void Run(DefaultBuildContext context)
  {
    context.Log.Information("Informational Version  : {0}",  context.BuildVersion.InformationalVersion);
    context.Log.Information("SemVer Version         : {0}",  context.BuildVersion.SemVer);
    context.Log.Information("IsLocalBuild           : {0}", context.IsLocalBuild);
    context.Log.Information("Branch                 : {0}", _buildProvider.BranchName);
    context.Log.Information("Configuration          : {0}", context.BuildConfiguration);
  }
}