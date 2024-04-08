using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Coverage-Report")]
[IsDependentOn(typeof(LocalCoverageReportTask))]
public class CoverageReportTask : FrostingTask;