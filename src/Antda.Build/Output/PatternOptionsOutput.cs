using System.Collections.Generic;
using Antda.Build.Context;
using Microsoft.Extensions.Options;

namespace Antda.Build.Output;

public class PatternOptionsOutput(IOptions<PatternOptions> patternOptions) : ILogObjectProvider<PatternOptions>
{
  public IEnumerable<LogObject> GetLogs(PatternOptions target) =>
  [
    new(target.Projects),
    new(target.TestProjects),
    new(target.MasterBranch),
    new(target.ReleaseBranch),
    new(target.HotfixBranch),
    new(target.DevelopBranch)
  ];

  public string Name => "Patterns";

  public IEnumerable<LogObject> GetLogs() => GetLogs(patternOptions.Value);
}