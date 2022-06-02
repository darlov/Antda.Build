using System.Collections.Generic;
using Cake.Frosting;

namespace Antda.Build.Output;

public class DefaultBuildContextOutput : ILogObjectProvider<DefaultBuildContext>
{
  private readonly DefaultBuildContext _defaultBuildContext;

  public DefaultBuildContextOutput(IFrostingContext defaultBuildContext)
  {
    _defaultBuildContext = (DefaultBuildContext)defaultBuildContext;
  }

  public IEnumerable<LogObject> GetLogs(DefaultBuildContext target)
  {
    return new LogObject[]
    {
      new(target.IsMainRepository),
      new(target.IsPreReleaseBranch),
      new(target.BranchType),
      new(target.BuildVersion.InformationalVersion),
      new(target.BuildVersion.SemVer)
    };
  }

  public string Name => "Build Context";

  public IEnumerable<LogObject> GetLogs() => GetLogs(_defaultBuildContext);

}