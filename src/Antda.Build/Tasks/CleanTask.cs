using Cake.Common.IO;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Clean")]
public class CleanTask : FrostingTask<DefaultBuildContext>
{
  public override void Run(DefaultBuildContext context)
  {
    if (context.DirectoryExists(context.Options.OutputNugetPackagesDirectoryPath))
    {
      context.DeleteDirectory(context.Options.OutputNugetPackagesDirectoryPath, new DeleteDirectorySettings
      {
        Recursive = true,
        Force = true
      });
    }
  }
}