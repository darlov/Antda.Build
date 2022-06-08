using System;
using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.IO;
using Cake.Git;
using LibGit2Sharp;
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
    options.Output = DirectoryPath.FromString(options.Output).MakeAbsolute(options.Root).FullPath;
    options.OutputNugetPackages = DirectoryPath.FromString(options.OutputNugetPackages).MakeAbsolute(options.Output).FullPath;

    try
    {
      options.GitRoot = _context.GitFindRootFromPath(options.Root).FullPath;
    }
    catch (RepositoryNotFoundException)
    {
      _context.Warning("Unable to find git repository.");
    }

    if (options.ProjectFiles is { Count: > 0 })
    {
      var source = DirectoryPath.FromString(options.Source);

      options.ProjectFiles = options.ProjectFiles
        .Select(p => source.CombineWithFilePath(p).FullPath)
        .ToList()
        .AsReadOnly();
    }
  }
}