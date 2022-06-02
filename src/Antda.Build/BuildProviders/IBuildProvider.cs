using Cake.Core.IO;

namespace Antda.Build.BuildProviders;

public interface IBuildProvider
{
  BuildProviderType Type { get; } 
  
  string BuildNumber { get; }
  
  Repository Repository { get; }
  
  void UploadArtifact(FilePath path);

  void UpdateBuildVersion(string buildVersion);
}