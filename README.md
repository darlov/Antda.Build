# Antda.Build

`Antda.Build` is a set of build tasks based on [Cake.Frosting](https://cakebuild.net/docs/running-builds/runners/cake-frosting) library.

[![License](http://img.shields.io/:license-mit-blue.svg)](https://github.com/darlov/Antda.Build/blob/main/LICENSE)

| Stable | Pre-release |
|:--:|:--:|
|[![Nuget](https://img.shields.io/nuget/v/Antda.Build.svg)](https://www.nuget.org/packages/Antda.Build)|[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Antda.Build)](https://www.nuget.org/packages/Antda.Build)|

## Build status

| develop | main |
|:--:|:--:|
|[![Build status](https://ci.appveyor.com/api/projects/status/p94fjg2f2nyx066d/branch/develop?svg=true)](https://ci.appveyor.com/project/darlov/antda-build/branch/develop)|[![Build status](https://ci.appveyor.com/api/projects/status/p94fjg2f2nyx066d/branch/main?svg=true)](https://ci.appveyor.com/project/darlov/antda-build/branch/main)|

## Requirements
Supported `net6.0` and `net7.0` as target framework.

### Build platforms
| Build platform      | Status          |
|---------------------|-----------------|
| AppVeyor            | ✔️Supported     |
| GitHub Actions      | ✔️Supported     |
| Azure Pipelines     | ❌️Not Supported |
| Bamboo              | ❌️Not Supported |
| Bitbucket Pipelines | ❌️Not Supported |
| Bitrise             | ❌️Not Supported |
| Continua CI         | ❌️Not Supported |
| GitLab CI           | ❌️Not Supported |
| GoCD                | ❌️Not Supported |
| Jenkins             | ❌️Not Supported |
| MyGet               | ❌️Not Supported | 
| TeamCity            | ❌️Not Supported |
| TravisCI            | ❌️Not Supported |


## Getting Started
Create a new console application. See [Create a simple C# console app](https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-console?view=vs-2022). Then replace content of `Program.cs` by following code:

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


### Create and publish new release
**TODO**

### Build parameters
**TODO**

### Environment variables
**TODO**

### Tasks
Here a list of all avaliable tasks to run:
- `CI`
- `Setup-Info`
- `Clean`
- `DotNet-Restore`
- `DotNet-Build`
- `DotNet-Test`
- `DotNet-Pack`
- `Upload-Artifacts`
- `DotNet-Nuget-Push`
- `Git-Publish-Release`
- `Local`
- `Release-Notes`

#### Task dependencies 
**TODO**
