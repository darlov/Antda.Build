using Cake.Core;
using Cake.Core.IO;
using Microsoft.Extensions.Options;

namespace Antda.Build.Context.Configurations;

public class PathOptionsPostConfigure : IPostConfigureOptions<PathOptions>
{
  private readonly ICakeContext _context;

  public PathOptionsPostConfigure(ICakeContext context)
  {
    _context = context;
  }

  public void PostConfigure(string name, PathOptions options)
  {
    options.Root = (string.IsNullOrEmpty(options.Root) ? _context.Environment.WorkingDirectory : options.Root).MakeAbsolute(_context.Environment).FullPath;
    options.Source = DirectoryPath.FromString(string.IsNullOrEmpty(options.Source) ? options.Root : options.Source).MakeAbsolute(options.Root).FullPath;
    options.ProjectFile = DirectoryPath.FromString(options.Source).CombineWithFilePath(options.ProjectFile).FullPath;
    options.Output = DirectoryPath.FromString(options.Output).MakeAbsolute(options.Root).FullPath;
    options.OutputNugetPackages = DirectoryPath.FromString(options.OutputNugetPackages).MakeAbsolute(options.Output).FullPath;
  }
}