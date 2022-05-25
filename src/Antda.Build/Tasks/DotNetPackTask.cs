using System;
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
  // public override bool ShouldRun(DefaultBuildContext context)
  // {
  //   return !context.IsLocalBuild;
  // }

  public override void Run(DefaultBuildContext context)
  {
    context.DotNetPack(context.Options.ProjectFile.FullPath, new DotNetPackSettings
    {
      Configuration = context.BuildConfiguration,
      OutputDirectory = context.Options.OutputNugetPackagesDirectoryPath,
      NoRestore = true,
      NoBuild = true,
      IncludeSymbols = true,
      SymbolPackageFormat = "snupkg",
      MSBuildSettings = new DotNetMSBuildSettings
      {
        Version =  context.BuildVersion.SemVer
      }
    });
  }
}