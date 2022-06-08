using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Local")]
[IsDependentOn(typeof(SetupInfoTask))]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(DotNetRestoreTask))]
[IsDependentOn(typeof(DotNetBuildTask))]
public class LocalTask : FrostingTask
{
}