using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Infrastructure.Shared;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Shared
{
    public class ErrorMessagesUnitTests
    {
        #region CreateNoTargetSetErrorMessage

        [Fact]
        public void CreateNoTargetSetErrorMessage_ArgumentIsNull_ThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => ErrorMessages.CreateNoTargetSetErrorMessage(null!));

        public static TheoryData<Type> TestDataCreateNoTargetSetErrorMessage =>
            new TheoryData<Type>()
            {
                { typeof(object) },
                { typeof(Exception) },
                { typeof(string) }
            };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.TestDataCreateNoTargetSetErrorMessage))]
        public void CreateNoTargetSetErrorMessage_ReturnsCorrectErrorMessage(Type type)
        {
            var expectedResult = $"The {type} does not have the target.";

            var actualResult = ErrorMessages.CreateNoTargetSetErrorMessage(type);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion CreateNoTargetSetErrorMessage

        #region CreateCouldNotResolveMethodErrorMessage

        public static TheoryData<Type?, string?, Type?, IEnumerable<Type>?> TestDataCreateCouldNotResolveMethodErrorMessageException =>
            new TheoryData<Type?, string?, Type?, IEnumerable<Type>?>()
            {
                { null, null, null, null },
                { typeof(string), null, null, null },
                { typeof(string), "Name", null, null }
            };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.TestDataCreateCouldNotResolveMethodErrorMessageException))]
        public void CreateCouldNotResolveMethodErrorMessage_ArgumentIsNull_ThrowsException
        (
            Type? type,
            string? name,
            Type? returnType,
            IEnumerable<Type>? parameterTypes
        ) =>
            Assert.Throws<ArgumentNullException>(() => ErrorMessages.CreateCouldNotResolveMethodErrorMessage(type!, name!, returnType!, parameterTypes));

        public static TheoryData<Type, string, Type, IEnumerable<Type>?> TestDataCreateCouldNotResolveMethodErrorMessage =>
            new TheoryData<Type, string, Type, IEnumerable<Type>?>()
            {
                { typeof(object), "ObjectMethod", typeof(object), null },
                { typeof(object), "TestMethod", typeof(object), new List<Type>() },
                { typeof(object), "MethodToResolve", typeof(object), new List<Type>() { typeof(object) } }
            };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.TestDataCreateCouldNotResolveMethodErrorMessage))]
        public void CreateCouldNotResolveMethodErrorMessage_ReturnsCorrectResult(Type type, string name, Type returnType, List<Type>? parameterTypes)
        {
            var expectedResult = $"The type {type} does not have matching method {returnType} {name} ({String.Join(", ", parameterTypes ?? new List<Type>(0))}).";

            var actualResult = ErrorMessages.CreateCouldNotResolveMethodErrorMessage(type, name, returnType, parameterTypes);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion CreateCouldNotResolveMethodErrorMessage

        #region CreateInvalidPipelineStepTypeErrorMessage

        public static TheoryData<Type?, string?> TestDataCreateInvalidPipelineStepTypeErrorMessageException => new TheoryData<Type?, string?>()
        {
            { null, null },
            { typeof(object), null }
        };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.TestDataCreateInvalidPipelineStepTypeErrorMessageException))]
        public void CreateInvalidPipelineStepTypeErrorMessage_ArgumentIsNull_ThrowsException(Type? type, string? interfaceName) =>
            Assert.Throws<ArgumentNullException>(() => ErrorMessages.CreateInvalidPipelineStepTypeErrorMessage(type!, interfaceName!));

        public static TheoryData<Type, string> TestDataCreateInvalidPipelineStepTypeErrorMessage => new TheoryData<Type, string>()
        {
            { typeof(string), "IString" },
            { typeof(object), "ITest" },
            { typeof(Task), "ITask" }
        };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.TestDataCreateInvalidPipelineStepTypeErrorMessage))]
        public void CreateInvalidPipelineStepTypeErrorMessage_ReturnsCorrectResult(Type type, string interfaceName)
        {
            var expectedResult = $"The {type} should implement {interfaceName} interface.";

            var actualResult = ErrorMessages.CreateInvalidPipelineStepTypeErrorMessage(type, interfaceName);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion CreateInvalidPipelineStepTypeErrorMessage

        #region CreateNoServiceProviderErrorMessage

        [Fact]
        public void CreateNoServiceProviderErrorMessage_ArgumentIsNull_ThrowsException() =>
            Assert.Throws<ArgumentNullException>(() => ErrorMessages.CreateNoServiceProviderErrorMessage(null!));

        public static TheoryData<Type> TestDataCreateNoServiceProviderErrorMessage =>
            new TheoryData<Type>()
            {
                { typeof(object) },
                { typeof(Exception) },
                { typeof(Task) }
            };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.TestDataCreateNoServiceProviderErrorMessage))]
        public void CreateNoServiceProviderErrorMessage_ReturnsCorrectResult(Type type)
        {
            var expectedResult = $"The service provider is not set for {type}.";

            var actualResult = ErrorMessages.CreateNoServiceProviderErrorMessage(type);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion CreateNoServiceProviderErrorMessage
    }
}
