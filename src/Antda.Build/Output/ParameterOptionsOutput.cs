using System.Collections.Generic;
using Antda.Build.Context;
using Microsoft.Extensions.Options;

namespace Antda.Build.Output;

public class ParameterOptionsOutput : ILogObjectProvider<ParameterOptions>
{
  private readonly IOptions<ParameterOptions> _options;
  
  public ParameterOptionsOutput(IOptions<ParameterOptions> options)
  {
    _options = options;
  }

  public IEnumerable<LogObject> GetLogs(ParameterOptions target)
  {
    return new LogObject[]
    {
      new(target.Title),
      new(target.Configuration),
      new(target.Target),
      new(target.ForceRun),
      new(target.RepositoryName),
      new(target.RepositoryOwner),
      new(string.Join(", ", target.BranchesToRelease), nameof(target.BranchesToRelease))
    };
  }

  public string Name => "Parameters";
  
  public IEnumerable<LogObject> GetLogs() => GetLogs(_options.Value);
}