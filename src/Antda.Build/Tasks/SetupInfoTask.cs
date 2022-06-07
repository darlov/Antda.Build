using System.Collections.Generic;
using System.Linq;
using Antda.Build.Output;
using Cake.Frosting;

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
    var groups = _printProviders.Select(m => new LogObjectGroup(m.Name, m.GetLogs().ToList()));
    LogOutputHelper.Log(context, groups);
  }
}