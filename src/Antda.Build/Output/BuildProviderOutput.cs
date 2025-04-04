using System.Collections.Generic;
using Antda.Build.BuildProvider;
using Cake.Core;

namespace Antda.Build.Output;

public class BuildProviderOutput(IBuildProvider buildProvider, ICakeContext context) : ILogObjectProvider<IBuildProvider>
{
  public IEnumerable<LogObject> GetLogs(IBuildProvider target)
  {
    yield return new(target.Type);
    yield return new(target.Repository.Name);
    yield return new(target.Repository.BranchName);
    yield return new(target.Repository.IsPullRequest);
    yield return new(target.Repository.IsTag);
    yield return new(target.Repository.TagName);
    yield return new(target.BuildNumber);
    yield return new(target.IsLocalBuild());

    foreach (var variable in target.Variables)
    {
      yield return new(context.Environment.GetEnvironmentVariable(variable), variable, false);
    }
  }

  public string Name => "Build Provider";

  public IEnumerable<LogObject> GetLogs() => GetLogs(buildProvider);
}