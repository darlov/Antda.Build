using Antda.Build;
using Antda.Build.Extensions;

return BuildHostBuilder
  .CreateDefault()
  .WithProjects("Antda.Build/Antda.Build.csproj")
  .WithSource("src")
  .WithTitle("Antda.Build")
  .WithRepository("Antda.Build", "darlov")
  .CollectCoverage()
  .UseGithubPackageSource()
  .UseNugetPackageSource()
  .Build()
  .Run(args);