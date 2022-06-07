using System.Collections.Generic;
using Antda.Build.BuildProviders;

namespace Antda.Build.Output;

public class BuildProviderOutput : ILogObjectProvider<IBuildProvider>
{
  private readonly IBuildProvider _buildProvider;

  public BuildProviderOutput(IBuildProvider buildProvider)
  {
    _buildProvider = buildProvider;
  }

  public IEnumerable<LogObject> GetLogs(IBuildProvider target)
  {
    return new LogObject[]
    {
      new(target.Type),
      new(target.Repository.Name),
      new(target.Repository.BranchName),
      new(target.Repository.IsPullRequest),
      new(target.Repository.IsTag),
      new(target.Repository.TagName),
      new(target.BuildNumber),
      new(target.IsLocalBuild())
    };
  }

  public string Name => "Build Provider";

  public IEnumerable<LogObject> GetLogs() => GetLogs(_buildProvider);
}