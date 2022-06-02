using System.Collections.Generic;
using System.Linq;
using Antda.Build.Output;
using Cake.Common.Diagnostics;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using Humanizer;

namespace Antda.Build.Tasks;

[TaskName("Setup-Info")]
public class SetupInfoTask : FrostingTask<DefaultBuildContext>
{
  private readonly IEnumerable<ILogObjectProvider> _printProviders;

  public SetupInfoTask(IEnumerable<ILogObjectProvider> printProviders)
  {
    _printProviders = printProviders;
  }

  public override void Run(DefaultBuildContext context)
  {
    foreach (var printProvider in _printProviders)
    {
      LogOutputHelper.LogGroup(context, printProvider.Name);
      foreach (var item in printProvider.GetLogs())
      {
        LogOutputHelper.Log(context, item);
      }
    }
  }
}