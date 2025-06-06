﻿using Cake.Frosting;

namespace Antda.Build.Tasks;

[TaskName("CI")]
[IsDependentOn(typeof(SetupInfoTask))]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(DotNetRestoreTask))]
[IsDependentOn(typeof(DotNetBuildTask))]
[IsDependentOn(typeof(DotNetTestTask))]
[IsDependentOn(typeof(DotNetPackTask))]
[IsDependentOn(typeof(UploadArtifactsTask))]
[IsDependentOn(typeof(DotNetNugetPushTask))]
[IsDependentOn(typeof(GitPublishReleaseTask))]
public class CiTask : FrostingTask;