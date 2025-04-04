using System.Collections.Generic;

namespace Antda.Build.Output;

public class LogObjectGroup(string name, IReadOnlyCollection<LogObject> items)
{
  public string Name => name;

  public IReadOnlyCollection<LogObject> Items => items;
}