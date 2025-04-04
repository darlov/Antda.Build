using System.Collections.Generic;
using System.Linq;
using Antda.Build.Output;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Setup-Info")]
public class SetupInfoTask(IEnumerable<ILogObjectProvider> printProviders) : FrostingTask<DefaultBuildContext>
{
  public override void Run(DefaultBuildContext context)
  {
    var groups = printProviders.Select(m => new LogObjectGroup(m.Name, m.GetLogs().ToList()));
    LogOutputHelper.Log(context, groups);
  }
}