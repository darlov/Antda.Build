using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antda.Build.Parsers;
using Cake.Common;
using Cake.Common.Build;
using Cake.Common.Build.GitHubActions;
using Cake.Common.Build.GitHubActions.Data;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

namespace Antda.Build.BuildProvider.Agents;

public class GitHubActionsBuildProvider : BaseBuildProvider
{
    private const string RefsTags = "refs/tags/";
    private const string RefsPull = "refs/pull/";
    private readonly IGitHubActionsProvider _gitHubActionsProvider;

    public GitHubActionsBuildProvider(ICakeContext context) : base(context)
    {
        _gitHubActionsProvider = context.GitHubActions();
        this.BuildNumber = _gitHubActionsProvider.Environment.Workflow.RunNumber.ToString();

        var repositoryName = _gitHubActionsProvider.Environment.Workflow.Repository;
        var tagName = GetTagName(_gitHubActionsProvider.Environment.Workflow);

        Repository = new Repository(repositoryName, true)
        {
            BranchName = GetBranchName(_gitHubActionsProvider.Environment.Workflow, context),
            IsPullRequest = GetIsPullRequest(_gitHubActionsProvider.Environment.Workflow),
            IsTag = tagName != null,
            TagName = tagName
        };
    }

    public override BuildProviderType Type => BuildProviderType.GitHubActions;
    public override string BuildNumber { get; }
    public override Repository Repository { get; }

    public override async Task UploadArtifactAsync(FilePath path)
    {
        await _gitHubActionsProvider.Commands.UploadArtifact(path, path.GetFilename().ToString());
    }

    public override void UpdateBuildVersion(string buildVersion)
    {
        _gitHubActionsProvider.Commands.SetStepSummary(buildVersion);
    }

    public override IReadOnlyCollection<string> Variables => new[]
    {
        "CI",
        "HOME",
        "GITHUB_WORKFLOW",
        "GITHUB_RUN_ID",
        "GITHUB_RUN_NUMBER",
        "GITHUB_ACTION",
        "GITHUB_ACTIONS",
        "GITHUB_ACTOR",
        "GITHUB_REPOSITORY",
        "GITHUB_EVENT_NAME",
        "GITHUB_EVENT_PATH",
        "GITHUB_WORKSPACE",
        "GITHUB_SHA",
        "GITHUB_REF",
        "GITHUB_HEAD_REF",
        "GITHUB_BASE_REF",
        "RUNNER_OS",
        "RUNNER_ARCH",
        "RUNNER_NAME"
    };

    private bool GetIsPullRequest(GitHubActionsWorkflowInfo workflow) => workflow.Ref.Contains(RefsPull, StringComparison.OrdinalIgnoreCase);

    private string? GetTagName(GitHubActionsWorkflowInfo workflow)
    {
        return workflow.Ref.Contains(RefsTags, StringComparison.OrdinalIgnoreCase) 
            ? workflow.Ref[(workflow.Ref.LastIndexOf('/') + 1)..] 
            : null;
    }

    private string GetBranchName(GitHubActionsWorkflowInfo workflow, ICakeContext context)
    {
        const string refsHeads = "refs/heads/";
        var branchHeadRef = workflow.HeadRef;
        
        if (!string.IsNullOrEmpty(branchHeadRef))
        {
            return branchHeadRef;
        }

        var branchRef = workflow.Ref;
        
        if (branchRef.StartsWith(refsHeads, StringComparison.OrdinalIgnoreCase))
        {
            return branchRef[refsHeads.Length..];
        }
        
        if (branchRef.StartsWith(RefsTags, StringComparison.OrdinalIgnoreCase))
        {
           var gitTool = context.Tools.Resolve("git") ?? context.Tools.Resolve("git.exe");

            if (gitTool != null)
            {
                var exitCode = context.StartProcess(
                    gitTool,
                    new ProcessSettings
                    {
                        Arguments = "branch -r --contains " + branchRef,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                    },
                    out IEnumerable<string> redirectedStandardOutput,
                    out IEnumerable<string> redirectedError
                );

                if (exitCode == 0)
                {
                    var line = redirectedStandardOutput.FirstOrDefault();
                    if (line != null)
                    {
                        var branchName = line.TrimStart(' ', '*').Replace("origin/", string.Empty);
                        return branchName;
                    }
                }
                else
                {
                    context.Log.Warning("Unable to get branch name from git. See error {0}", string.Join('\n', redirectedError));
                }
            }

            return branchRef;
        }
        
        return branchRef.Contains('/') ? branchRef[(branchRef.LastIndexOf('/') + 1)..] : branchRef;
    }
}