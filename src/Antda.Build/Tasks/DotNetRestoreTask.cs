using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("DotNet-Restore")]
public class DotNetRestoreTask : FrostingTask<DefaultBuildContext>
{
  public override void Run(DefaultBuildContext context)
  {
    var projects = context.GetFiles(context.Options.ProjectsPattern).ToList();

    if (!projects.Any())
    {
      context.Warning("The project files is not found in {0}/{1}", context.Environment.WorkingDirectory, context.Options.ProjectsPattern);
    }

    foreach (var project in projects)
    {
      context.DotNetRestore(project.ToString());
    }
  }
}