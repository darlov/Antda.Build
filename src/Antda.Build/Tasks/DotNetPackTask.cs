using System.Linq;
using Antda.Build.BuildProvider;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Common.Tools.DotNet.Pack;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("DotNet-Pack")]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(DotNetTestTask))]
public class DotNetPackTask : FrostingTask<DefaultBuildContext>
{
  public override bool ShouldRun(DefaultBuildContext context)
    => !context.BuildProvider.IsLocalBuild() || context.Parameters.ForceRun;

  public override void Run(DefaultBuildContext context)
  {
    var projectFiles = context.Paths.ProjectFiles ?? context.GetFiles(context.Patterns.Projects).Select(p => p.FullPath).ToList();

    if (projectFiles.Any())
    {
      foreach (var projectFile in projectFiles)
      {
        context.DotNetPack(projectFile, new DotNetPackSettings
        {
          Configuration = context.Parameters.Configuration,
          OutputDirectory = context.Paths.OutputNugetPackages,
          NoRestore = true,
          NoBuild = true,
          IncludeSymbols = true,
          SymbolPackageFormat = "snupkg",
          MSBuildSettings = new DotNetMSBuildSettings
          {
            Version = context.BuildVersion.SemVersion,
            InformationalVersion = context.BuildVersion.InformationalVersion,
            ContinuousIntegrationBuild = !context.BuildProvider.IsLocalBuild(),
          }.WithProperty("Deterministic", context.BuildProvider.IsLocalBuild() ? bool.FalseString : bool.TrueString)
        });
      }
    }
    else
    {
      context.Warning("No project files found to pack.");
    }
  }
}