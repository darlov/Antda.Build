using System.Collections.Generic;
using Antda.Build.PackageSources;

namespace Antda.Build.Output;

public class PackageSourcesOutput(IPackageSourceProvider packageSourceProvider) : ILogObjectProvider<IEnumerable<PackageSource>>
{
  public IEnumerable<LogObject> GetLogs(IEnumerable<PackageSource> targets)
  {
    foreach (var source in targets)
    {
      yield return new(source.PrefixName);
      yield return new(source.PushSourceUrl);
      yield return new(source.PreRelease);
    }
  }

  public string Name => "Package Sources";

  public IEnumerable<LogObject> GetLogs() => GetLogs(packageSourceProvider.GetPackageSources());
}