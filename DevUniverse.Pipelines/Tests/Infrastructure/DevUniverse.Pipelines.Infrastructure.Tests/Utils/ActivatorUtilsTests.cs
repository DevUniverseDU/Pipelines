using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Infrastructure.Builders;
using DevUniverse.Pipelines.Infrastructure.Utils;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Utils
{
    public class ActivatorUtilsUnitTests
    {
        public static IServiceProvider ServiceProvider => new ServiceCollection().BuildServiceProvider();

        #region Create

        public static TheoryData<Type, string> CreateNonGenericExceptionTestData =>
            new TheoryData<Type, string>()
            {
                { null, "type" }
            };

        [Theory]
        [MemberData(nameof(ActivatorUtilsUnitTests.CreateNonGenericExceptionTestData))]
        public void Create_ArgumentIsNull_ThrowsException(Type type, string parameterName)
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage(parameterName);

            var actualResult = Assert.Throws<ArgumentNullException>(() => ActivatorUtils.Create(type));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static TheoryData<Type, IEnumerable<object>> CreateNonGenericTestData =>
            new TheoryData<Type, IEnumerable<object>>()
            {
                { typeof(object), null },
                { typeof(object), new List<object>() },
                { typeof(PipelineBuilder<string, Task>), new List<object>() { null } },
                { typeof(PipelineBuilder<string, Task>), new List<object>() { ActivatorUtilsUnitTests.ServiceProvider } },
                { typeof(Exception), new List<object>() { "Message" } }
            };

        [Theory]
        [MemberData(nameof(ActivatorUtilsUnitTests.CreateNonGenericTestData))]
        public void Create_NonGeneric_ReturnsCorrectResult(Type expectedResultType, List<object> constructorArguments)
        {
            var actualResult = ActivatorUtils.Create(expectedResultType, constructorArguments);

            Assert.NotNull(actualResult);
            Assert.Equal(expectedResultType, actualResult.GetType());
        }

        #endregion Create
    }
}
