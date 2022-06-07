using System.Collections.Generic;
using Antda.Build.Context;
using Microsoft.Extensions.Options;

namespace Antda.Build.Output;

public class PathOptionsOutput : ILogObjectProvider<PathOptions>
{
  private readonly PathOptions _pathOptions;

  public PathOptionsOutput(IOptions<PathOptions> pathOptions)
  {
    _pathOptions = pathOptions.Value;
  }

  public IEnumerable<LogObject> GetLogs(PathOptions target)
  {
    return new LogObject[]
    {
      new(target.Root),
      new(target.Source),
      new(target.ProjectFiles),
      new(target.Output),
      new(target.OutputNugetPackages)
    };
  }

  public string Name => "Paths";

  public IEnumerable<LogObject> GetLogs() => GetLogs(_pathOptions);
}