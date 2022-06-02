using System;
using Antda.Build.BuildProviders;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Common.Tools.DotNet.Pack;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("DotNet-Pack")]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(DotNetBuildTask))]
public class DotNetPackTask : FrostingTask<DefaultBuildContext>
{
  public override bool ShouldRun(DefaultBuildContext context)
  {
    return !context.BuildProvider.IsLocalBuild() ||  context.Parameters.ForceRun;
  }

  public override void Run(DefaultBuildContext context)
  {
    context.DotNetPack(context.Paths.ProjectFile, new DotNetPackSettings
    {
      Configuration = context.Parameters.Configuration,
      OutputDirectory = context.Paths.OutputNugetPackages,
      NoRestore = true,
      NoBuild = true,
      IncludeSymbols = true,
      SymbolPackageFormat = "snupkg",
      MSBuildSettings = new DotNetMSBuildSettings
      {
        Version =  context.BuildVersion.SemVer,
        InformationalVersion = context.BuildVersion.InformationalVersion
      }
    });
  }
}