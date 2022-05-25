using System;
using Cake.Core.IO;

namespace Antda.Build.Context;

public class BuildOptions
{
  public string? Title { get; set; }

  public string? RepositoryName { get; set; }

  public string? RepositoryOwner { get; set; }

  public DirectoryPath? RootDirectoryPath { get; set; } = null!;

  public DirectoryPath? SourceDirectoryPath { get; set; } = null!;
  
  public FilePath ProjectFile { get; set;} = null!;

  public string ProjectsPattern { get; set;} = null!;

  public string TestProjectsPattern { get; set;} = null!;

  public DirectoryPath OutputDirectoryPath { get; set;} = null!;

  public DirectoryPath OutputNugetPackagesDirectoryPath { get; set;} = null!;
}