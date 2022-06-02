﻿using Cake.Common.IO;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Clean")]
public class CleanTask : FrostingTask<DefaultBuildContext>
{
  public override void Run(DefaultBuildContext context)
  {
    if (context.DirectoryExists(context.Paths.OutputNugetPackages))
    {
      context.DeleteDirectory(context.Paths.OutputNugetPackages, new DeleteDirectorySettings
      {
        Recursive = true,
        Force = true
      });
    }
  }
}