using System.Collections.Generic;
using Antda.Build.Context;
using Microsoft.Extensions.Options;

namespace Antda.Build.Output;

public class ParameterOptionsOutput(IOptions<ParameterOptions> options) : ILogObjectProvider<ParameterOptions>
{
  public IEnumerable<LogObject> GetLogs(ParameterOptions target)
  {
    return
    [
      new(target.Title),
      new(target.Configuration),
      new(target.Target),
      new(target.ForceRun),
      new(target.RepositoryName),
      new(target.RepositoryOwner),
      new(target.ReleaseBranches),
      new(target.PreReleaseBranches)
    ];
  }

  public string Name => "Parameters";

  public IEnumerable<LogObject> GetLogs() => GetLogs(options.Value);
}