using System;
using System.Linq;
using Cake.Common;
using Cake.Core;
using Humanizer;
using Microsoft.Extensions.Options;

namespace Antda.Build.Context.Configurations;

public class ParameterOptionsPostConfigure : IPostConfigureOptions<ParameterOptions>
{
  private readonly ICakeContext _context;
  private readonly PathOptions _pathOptions;

  public ParameterOptionsPostConfigure(ICakeContext context, IOptions<PathOptions> pathOptions)
  {
    _context = context;
    _pathOptions = pathOptions.Value;
  }

  public void PostConfigure(string name, ParameterOptions options)
  {
    options.ForceRun = _context.Argument("forceRun", options.ForceRun);
    
    options.Target = _context.Argument("target", "Default");
    options.Configuration = _context.Argument("configuration", "Release");
    
    options.Title = string.IsNullOrEmpty(options.Title) 
      ? System.IO.Path.GetFileNameWithoutExtension(_pathOptions.ProjectFile).Humanize(LetterCasing.Title)
      : options.Title;
    
    options.BranchesToRelease = options.ReleaseBranches
      .Split(";", StringSplitOptions.RemoveEmptyEntries)
      .Select(m => Enum.Parse<BranchType>(m, true))
      .ToList();
  }
}