using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("CI-Test")]
[IsDependentOn(typeof(SetupInfoTask))]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(DotNetRestoreTask))]
[IsDependentOn(typeof(DotNetBuildTask))]
[IsDependentOn(typeof(DotNetTestTask))]
[IsDependentOn(typeof(DotNetPackTask))]
[IsDependentOn(typeof(UploadArtifactsTask))]
public class CiTestTask : FrostingTask
{
}