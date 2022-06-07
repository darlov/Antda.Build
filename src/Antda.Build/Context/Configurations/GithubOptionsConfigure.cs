using Cake.Core;
using Microsoft.Extensions.Options;

namespace Antda.Build.Context.Configurations;

public class GithubOptionsConfigure : IConfigureOptions<GithubOptions>
{
  private readonly ICakeContext _context;
  private readonly ParameterOptions _parameterOptions;
  private readonly VariableOptions _variableOptions;

  public GithubOptionsConfigure(ICakeContext context, IOptions<ParameterOptions> parameterOptions, IOptions<VariableOptions> variableOptions)
  {
    _context = context;
    _variableOptions = variableOptions.Value;
    _parameterOptions = parameterOptions.Value;
  }

  public void Configure(GithubOptions options)
  {
    options.RepositoryName = _parameterOptions.RepositoryName;
    options.RepositoryOwner = _parameterOptions.RepositoryOwner;

    if (!string.IsNullOrEmpty(_variableOptions.GithubToken))
    {
      options.GithubToken = _context.Environment.GetEnvironmentVariable(_variableOptions.GithubToken);
    }
  }
}