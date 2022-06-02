using Antda.Build;
using Antda.Build.Extensions;

return BuildHostBuilder
  .CreateDefault("Antda.Build/Antda.Build.csproj")
  .WithSource("src")
  //.WithTitle("Antda Build")
  .WithRepository("Antda.Build", "darlov")
  .UseGithubPackageSource()
  .UseNugetPackageSource()
  .Build()
  .Run(args);