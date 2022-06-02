using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Default")]
[IsDependentOn(typeof(SetupInfoTask))]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(DotNetRestoreTask))]
[IsDependentOn(typeof(DotNetBuildTask))]
[IsDependentOn(typeof(DotNetPackTask))]
[IsDependentOn(typeof(PublishArtifactTask))]
[IsDependentOn(typeof(DotNetNugetPushTask))]
[IsDependentOn(typeof(GitReleaseTask))]
public class DefaultTask : FrostingTask
{
}