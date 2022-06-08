using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Default")]
[IsDependentOn(typeof(LocalTask))]
public class DefaultTask : FrostingTask
{
}