using System.Collections.Generic;
using System.Threading.Tasks;
using Cake.Core.IO;

namespace Antda.Build.BuildProvider;

public interface IBuildProvider
{
  BuildProviderType Type { get; }

  string BuildNumber { get; }

  Repository Repository { get; }

  Task UploadArtifactAsync(FilePath path);

  void UpdateBuildVersion(string buildVersion);

  string GetEnvironmentVariable(string name);

  IReadOnlyCollection<string> Variables { get; }
}