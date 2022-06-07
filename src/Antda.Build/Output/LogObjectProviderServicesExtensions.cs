using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Build.Output;

public static class LogObjectProviderServicesExtensions
{
  public static IServiceCollection AddLogObjectProvider<T>(this IServiceCollection services)
    where T : class, ILogObjectProvider
  {
    var type = typeof(T);
    var genericInterface = typeof(ILogObjectProvider<>);

    var definition = type.GetInterfaces().FirstOrDefault(m => m.IsGenericType && m.GetGenericTypeDefinition() == genericInterface);
    if (definition != null)
    {
      services.AddSingleton(definition, typeof(T));
      services.AddSingleton(s => (ILogObjectProvider)s.GetRequiredService(definition));
    }
    else
    {
      services.AddSingleton<ILogObjectProvider, T>();
    }

    return services;
  }
}