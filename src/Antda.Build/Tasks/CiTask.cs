using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("CI")]
[IsDependentOn(typeof(SetupInfoTask))]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(DotNetRestoreTask))]
[IsDependentOn(typeof(DotNetBuildTask))]
[IsDependentOn(typeof(DotNetPackTask))]
[IsDependentOn(typeof(UploadArtifactsTask))]
[IsDependentOn(typeof(GitReleaseTask))]
[IsDependentOn(typeof(DotNetNugetPushTask))]
public class CiTask : FrostingTask
{
}