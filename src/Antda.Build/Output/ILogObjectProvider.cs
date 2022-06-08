using System.Collections.Generic;

namespace Antda.Build.Output;

public interface ILogObjectProvider
{
  string Name { get; }

  IEnumerable<LogObject> GetLogs();
}

public interface ILogObjectProvider<in T> : ILogObjectProvider
{
  IEnumerable<LogObject> GetLogs(T target);
}