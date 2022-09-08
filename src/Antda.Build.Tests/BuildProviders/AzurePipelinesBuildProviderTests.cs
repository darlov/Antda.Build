using Antda.Build.BuildProviders;
using Antda.Build.Context;
using Cake.Common.Build.AzurePipelines;
using Cake.Core;
using Moq;
using NUnit.Framework;

namespace TestProject1.BuildProviders;

public class AzurePipelinesBuildProviderTests
{
  [Test]
  public void Test1()
  {
    var providerMock = new Mock<IAzurePipelinesProvider>();
    var contextMock = new Mock<ICakeContext>();

    var t = new AzurePipelinesBuildProvider(providerMock.Object, contextMock.Object, new PathOptions());
    Assert.Pass();
  }
}