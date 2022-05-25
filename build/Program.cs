using Antda.Build;

return BuildHostBuilder
  .CreateDefault("src/Antda.Build/Antda.Build.csproj")
  .WithTitle("Antda Build")
  .WithRepository("Antda.Build", "darlov")
  .Build()
  .Run(args);