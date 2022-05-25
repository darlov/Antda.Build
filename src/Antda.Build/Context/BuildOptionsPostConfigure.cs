using Cake.Core;
using Microsoft.Extensions.Options;

namespace Antda.Build.Context;

public class BuildOptionsPostConfigure : IPostConfigureOptions<BuildOptions>
{
  private readonly ICakeContext _cakeContext;

  public BuildOptionsPostConfigure(ICakeContext cakeContext)
  {
    _cakeContext = cakeContext;
  }

  public void PostConfigure(string name, BuildOptions options)
  {
    options.RootDirectoryPath ??= _cakeContext.Environment.WorkingDirectory;
  }
}