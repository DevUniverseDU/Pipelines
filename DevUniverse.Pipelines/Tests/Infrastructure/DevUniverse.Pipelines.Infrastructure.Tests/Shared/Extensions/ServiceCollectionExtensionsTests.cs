using System;

using DevUniverse.Pipelines.Core.BuilderFactories;
using DevUniverse.Pipelines.Infrastructure.BuilderFactories;
using DevUniverse.Pipelines.Infrastructure.Shared.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Shared.Extensions
{
    public class ServiceCollectionExtensionsUnitTests
    {
        #region AddPipelines

        [Fact]
        public void AddPipelines_ArgumentIsNull_ThrowsException()
        {
            var sut = default(IServiceCollection);

            Assert.Throws<ArgumentNullException>(() => sut!.AddPipelines());
        }

        [Fact]
        public void AddPipelines_ReturnsCorrectResult()
        {
            var expectedType = typeof(PipelineBuilderFactory);
            var expectedTypeAsync = typeof(PipelineBuilderAsyncFactory);

            var sut = new ServiceCollection();

            var actualResult = sut.AddPipelines();

            var serviceProvider = actualResult.BuildServiceProvider();

            var factory = serviceProvider.GetRequiredService<IPipelineBuilderFactory>();

            Assert.NotNull(factory);
            Assert.Equal(expectedType, factory.GetType());

            var factoryAsync = serviceProvider.GetRequiredService<IPipelineBuilderAsyncFactory>();

            Assert.NotNull(factoryAsync);
            Assert.Equal(expectedTypeAsync, factoryAsync.GetType());
        }

        #endregion AddPipelines
    }
}
