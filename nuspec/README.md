
`Antda.Build` is a set of tasks based on [Cake.Frosting](https://cakebuild.net/docs/running-builds/runners/cake-frosting) library. This library simplify the building, deployment and release processes for `.Net` projects hosted on GitHub. 

[![License](http://img.shields.io/:license-mit-blue.svg)](https://github.com/darlov/Antda.Build/blob/main/LICENSE)

### Features
- Supported any `.Net` library projects
- Supported publish packages to [GitHub NuGet register](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry) and [NuGet.org](https://www.nuget.org/)
- Automatically create release notes for [GitHub release](https://docs.github.com/en/repositories/releasing-projects-on-github/about-releases)
- Run unit tests
- Upload build artifacts to CI build
- Version incrementing by [GitVersion](https://github.com/GitTools/GitVersion)

### Supported build platforms
| Build platform |   Status    |
|:--------------:|:-----------:|
|    AppVeyor    | ✔️Supported |
| GitHub Actions | ✔️Supported |


## Getting Started
- Create `build` folder in the repository root.
- Create a new console application inside. See [Create a simple C# console app](https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-console?view=vs-2022).
- Then replace content of `Program.cs` by following code: 
  ```csharp 
  using Antda.Build;
  using Antda.Build.Extensions;

  return BuildHostBuilder
    .CreateDefault()
    .WithProjects("ProjectFolder/ProjectName.csproj")
    .WithSource("src")
    .WithTitle("ProjectName")
    .WithRepository("GitRepositoryName", "GitRepositoryOwner")
    .UseGithubPackageSource()
    .UseNugetPackageSource()
    .Build()
    .Run(args);
  ```
- Add build script in the repository root
  -  For Windows add `build.ps1` powreshell script with content:
  ```
  dotnet run --project ./build/Build.csproj --configuration Release -- $args
  exit $LASTEXITCODE;
  ```
  - For Linux add `build.sh` bash script with content:
  ```bash
  dotnet run --project ./build/Build.csproj --configuration Release -- "$@"
  ```
For more documentation, follow the link there [Antda.Build](https://github.com/darlov/Antda.Build)

