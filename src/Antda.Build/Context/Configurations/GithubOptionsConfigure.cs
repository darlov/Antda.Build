using Cake.Core;
using Microsoft.Extensions.Options;

namespace Antda.Build.Context.Configurations;

public class GithubOptionsConfigure(ICakeContext context, IOptions<ParameterOptions> parameterOptions, IOptions<VariableOptions> variableOptions)
  : IConfigureOptions<GithubOptions>
{
  private readonly ParameterOptions _parameterOptions = parameterOptions.Value;
  private readonly VariableOptions _variableOptions = variableOptions.Value;

  public void Configure(GithubOptions options)
  {
    options.RepositoryName = _parameterOptions.RepositoryName;
    options.RepositoryOwner = _parameterOptions.RepositoryOwner;

    if (!string.IsNullOrEmpty(_variableOptions.GithubToken))
    {
      options.GithubToken = context.Environment.GetEnvironmentVariable(_variableOptions.GithubToken);
    }
  }
}