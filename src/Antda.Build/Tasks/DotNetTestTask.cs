using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Test;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("DotNet-Test")]
[IsDependentOn(typeof(DotNetBuildTask))]
public class DotNetTestTask : FrostingTask<DefaultBuildContext>
{
  public override void Run(DefaultBuildContext context)
  {
    var searchPath = $"{context.Paths.Source}/{context.Patterns.TestProjects}";
    var projects = context.GetFiles(searchPath).ToList();

    if (!projects.Any())
    {
      context.Information("The test project files are not found by the pattern {0}", searchPath);
    }
    else
    {
      foreach (var project in projects)
      {
        context.DotNetTest(project.ToString(), new DotNetTestSettings
        {
          Configuration = context.Parameters.Configuration,
          NoBuild = true,
          NoRestore = true
        });
      }
    }
  }
}