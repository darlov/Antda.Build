using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Local")]
[IsDependentOn(typeof(SetupInfoTask))]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(DotNetRestoreTask))]
[IsDependentOn(typeof(DotNetBuildTask))]
[IsDependentOn(typeof(DotNetTestTask))]
[IsDependentOn(typeof(CoverageReportTask))]
public class LocalTask : FrostingTask;