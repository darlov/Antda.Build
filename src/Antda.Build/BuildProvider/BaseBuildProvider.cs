using System.Collections.Generic;
using System.Threading.Tasks;
using Cake.Core.IO;

namespace Antda.Build.BuildProvider;

public abstract class BaseBuildProvider : IBuildProvider
{
  public abstract BuildProviderType Type { get; }
  public abstract string BuildNumber { get; }
  public abstract Repository Repository { get; }
  public abstract Task UploadArtifactAsync(FilePath path);

  public abstract void UpdateBuildVersion(string buildVersion);
  
  public abstract IReadOnlyCollection<string> Variables { get; }
}