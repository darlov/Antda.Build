using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Build;

public interface IHostStartup
{
  void Configure(IServiceCollection services, IConfiguration configuration);
}