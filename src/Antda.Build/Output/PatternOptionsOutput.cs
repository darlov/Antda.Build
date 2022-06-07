using System.Collections.Generic;
using Antda.Build.Context;
using Microsoft.Extensions.Options;

namespace Antda.Build.Output;

public class PatternOptionsOutput: ILogObjectProvider<PatternOptions>
{
  private readonly PatternOptions _patternOptions;

  public PatternOptionsOutput(IOptions<PatternOptions> patternOptions)
  {
    _patternOptions = patternOptions.Value;
  }

  public IEnumerable<LogObject> GetLogs(PatternOptions target)
  {
    return new LogObject[]
    {
      new(target.Projects),
      new(target.TestProjects),
      new(target.MasterBranch),
      new(target.ReleaseBranch),
      new(target.HotfixBranch),
      new(target.DevelopBranch)
    };
  }

  public string Name => "Patterns";

  public IEnumerable<LogObject> GetLogs() => GetLogs(_patternOptions);
}