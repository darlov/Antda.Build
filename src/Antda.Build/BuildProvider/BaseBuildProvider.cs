using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.IO;

namespace Antda.Build.BuildProvider;

public abstract class BaseBuildProvider : IBuildProvider
{
  private readonly ICakeContext _context;

  protected BaseBuildProvider(ICakeContext context)
  {
    ArgumentNullException.ThrowIfNull(context);
    _context = context;
  }

  public abstract BuildProviderType Type { get; }
  public abstract string BuildNumber { get; }
  public abstract Repository Repository { get; }
  public abstract Task UploadArtifactAsync(FilePath path);

  public abstract void UpdateBuildVersion(string buildVersion);
  
  public abstract IReadOnlyCollection<string> Variables { get; }
}