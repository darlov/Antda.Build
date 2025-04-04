using System.Collections.Generic;
using Antda.Build.Context;
using Microsoft.Extensions.Options;

namespace Antda.Build.Output;

public class PathOptionsOutput(IOptions<PathOptions> pathOptions) : ILogObjectProvider<PathOptions>
{
  public IEnumerable<LogObject> GetLogs(PathOptions target) =>
  [
    new(target.Root),
    new(target.Source),
    new(target.ProjectFiles),
    new(target.Output),
    new(target.OutputNugetPackages)
  ];

  public string Name => "Paths";

  public IEnumerable<LogObject> GetLogs() => GetLogs(pathOptions.Value);
}