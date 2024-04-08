using Antda.Build.BuildProvider;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("Local-Coverage-Report")]
[IsDependentOn(typeof(DotNetTestTask))]
public class LocalCoverageReportTask : FrostingTask<DefaultBuildContext>
{
  public override bool ShouldRun(DefaultBuildContext context)
    => context.Parameters.CollectCoverage && context.BuildProvider.IsLocalBuild();

  public override void Run(DefaultBuildContext context)
  {
    context.ReportGenerator(new GlobPattern($"{context.Paths.OutputTestCoverage}/{context.Patterns.CoverageResult}"), context.Paths.OutputTestCoverage, new ReportGeneratorSettings
    {
      ReportTypes = new[] { ReportGeneratorReportType.HtmlInline },
      ArgumentCustomization = b => b.AppendQuoted("-tag:{0}", context.BuildVersion.Version),
    });
  }
}