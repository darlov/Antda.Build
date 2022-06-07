using System.Collections.Generic;

namespace Antda.Build.Output;

public class LogObjectGroup
{
  public LogObjectGroup(string name, IReadOnlyCollection<LogObject> items)
  {
    Name = name;
    Items = items;
  }

  public string Name { get; }
  
  public IReadOnlyCollection<LogObject> Items { get; }
}