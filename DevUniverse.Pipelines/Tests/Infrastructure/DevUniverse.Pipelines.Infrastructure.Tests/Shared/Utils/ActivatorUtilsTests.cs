using System;
using System.Collections.Generic;

using DevUniverse.Pipelines.Infrastructure.Shared.Utils;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Shared.Utils
{
    public class ActivatorUtilsUnitTests
    {
        public static IServiceProvider ServiceProvider => new ServiceCollection().BuildServiceProvider();

        #region Create

        [Fact]
        public void Create_ArgumentIsNull_ThrowsException() => Assert.Throws<ArgumentNullException>(() => ActivatorUtils.Create(null!));

        public static TheoryData<Type, IEnumerable<object?>?> TestDataCreateNonGeneric =>
            new TheoryData<Type, IEnumerable<object?>?>()
            {
                { typeof(object), null },
                { typeof(object), new List<object>() },
                { typeof(Exception), new List<object>() { "Message" } }
            };

        [Theory]
        [MemberData(nameof(ActivatorUtilsUnitTests.TestDataCreateNonGeneric))]
        public void Create_NonGeneric_ReturnsCorrectResult(Type expectedResultType, List<object?>? constructorArguments)
        {
            var actualResult = ActivatorUtils.Create(expectedResultType, constructorArguments);

            Assert.NotNull(actualResult);
            Assert.Equal(expectedResultType, actualResult.GetType());
        }

        #endregion Create
    }
}
