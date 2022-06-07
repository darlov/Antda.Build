using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("DotNet-Restore")]
public class DotNetRestoreTask : FrostingTask<DefaultBuildContext>
{
  public override void Run(DefaultBuildContext context)
  {
    var searchPath = $"{context.Paths.Source}/{context.Patterns.Projects}";
    var projects = context.GetFiles(searchPath).ToList();

    if (!projects.Any())
    {
      context.Warning("The project files are not found by the pattern {0}", searchPath);
    }
    else
    {
      foreach (var project in projects)
      {
        context.DotNetRestore(project.ToString());
      }
    }
  }
}