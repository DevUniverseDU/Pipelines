using System;

using DevUniverse.Pipelines.Core.BuilderFactories;
using DevUniverse.Pipelines.Infrastructure.BuilderFactories;
using DevUniverse.Pipelines.Infrastructure.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Extensions
{
    public class ServiceCollectionExtensionsUnitTests
    {
        #region AddPipelines

        [Fact]
        public void AddPipelines_ArgumentIsNull_ThrowsException()
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage("serviceCollection");

            var sut = default(IServiceCollection);

            // ReSharper disable once ExpressionIsAlwaysNull
            var actualResult = Assert.Throws<ArgumentNullException>(() => sut.AddPipelines());

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Fact]
        public void AddPipelines_ReturnsServiceCollectionWithAddedDependencies()
        {
            var expectedType = typeof(PipelineBuilderFactory);

            var sut = new ServiceCollection();

            var actualResult = sut.AddPipelines();

            var serviceProvider = actualResult.BuildServiceProvider();
            var service = serviceProvider.GetService<IPipelineBuilderFactory>();

            Assert.NotNull(service);
            Assert.Equal(expectedType, service.GetType());
        }

        #endregion AddPipelines
    }
}
