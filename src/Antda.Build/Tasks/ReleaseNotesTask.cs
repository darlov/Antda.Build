using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Release-Notes")]
[IsDependentOn(typeof(GitCreateReleaseNotesTask))]
public class ReleaseNotesTask : FrostingTask
{
}