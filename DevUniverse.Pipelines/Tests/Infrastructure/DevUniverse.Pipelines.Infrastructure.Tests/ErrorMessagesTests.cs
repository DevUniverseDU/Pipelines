using System;
using System.Collections.Generic;

using DevUniverse.Pipelines.Core.Builders;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests
{
    public class ErrorMessagesUnitTests
    {
        public static TheoryData<Type> TypesToCheck =>
            new TheoryData<Type>()
            {
                { typeof(object) },
                { typeof(Exception) },
                { typeof(int) },
                { typeof(string) }
            };

        #region CreateNoTargetSetErrorMessage

        public static TheoryData<Type, string> CreateNoTargetSetErrorMessageExceptionTestData =>
            new TheoryData<Type, string>()
            {
                { null, "type" }
            };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.CreateNoTargetSetErrorMessageExceptionTestData))]
        public void CreateNoTargetSetErrorMessage_ArgumentIsNull_ThrowsException(Type type, string parameterName)
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage(parameterName);

            var actualResult = Assert.Throws<ArgumentNullException>(() => ErrorMessages.CreateNoTargetSetErrorMessage(type));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.TypesToCheck))]
        public void CreateNoTargetSetErrorMessage_ReturnsCorrectErrorMessage(Type type)
        {
            var expectedResult = $"The {type} does not have the target. Set the target using {nameof(IPipelineBuilder<object, object, object>.UseTarget)}.";

            var actualResult = ErrorMessages.CreateNoTargetSetErrorMessage(type);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion CreateNoTargetSetErrorMessage

        #region CreateInvalidPipelineStepTypeErrorMessage

        public static TheoryData<Type, string> CreateInvalidPipelineStepTypeErrorMessageExceptionTestData =>
            new TheoryData<Type, string>()
            {
                { null, "type" }
            };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.CreateInvalidPipelineStepTypeErrorMessageExceptionTestData))]
        public void CreateInvalidPipelineStepTypeErrorMessage_ArgumentIsNull_ThrowsException(Type type, string parameterName)
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage(parameterName);

            var actualResult = Assert.Throws<ArgumentNullException>(() => ErrorMessages.CreateInvalidPipelineStepTypeErrorMessage(type));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.TypesToCheck))]
        public void CreateInvalidPipelineStepTypeErrorMessage_ReturnsCorrectResult(Type type)
        {
            var expectedResult = $"The pipeline builder {type} should implement {nameof(IPipelineBuilder)} interface.";

            var actualResult = ErrorMessages.CreateInvalidPipelineStepTypeErrorMessage(type);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion CreateInvalidPipelineStepTypeErrorMessage

        #region CreateCouldNotResolveMethod

        public static TheoryData<Type, string, Type, IEnumerable<Type>, string> CreateCouldNotResolveMethodExceptionTestData =>
            new TheoryData<Type, string, Type, IEnumerable<Type>, string>()
            {
                { null, null, null, null, "type" },
                { typeof(string), null, null, null, "name" },
                { typeof(string), "Name", null, null, "returnType" }
            };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.CreateCouldNotResolveMethodExceptionTestData))]
        public void CreateCouldNotResolveMethod_ArgumentIsNull_ThrowsException
        (
            Type type,
            string name,
            Type returnType,
            IEnumerable<Type> parameterTypes,
            string parameterName
        )
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage(parameterName);

            var actualResult = Assert.Throws<ArgumentNullException>(() => ErrorMessages.CreateCouldNotResolveMethod(type, name, returnType, parameterTypes));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static TheoryData<Type, string, Type, IEnumerable<Type>> CreateCouldNotResolveMethodTestData =>
            new TheoryData<Type, string, Type, IEnumerable<Type>>()
            {
                { typeof(object), "ObjectMethod", typeof(object), null },
                { typeof(object), "TestMethod", typeof(object), new List<Type>() },
                { typeof(object), "MethodToResolve", typeof(object), new List<Type>() { typeof(object) } }
            };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.CreateCouldNotResolveMethodTestData))]
        public void CreateCouldNotResolveMethod_ReturnsCorrectResult(Type type, string name, Type returnType, List<Type> parameterTypes)
        {
            var expectedResult =
                $"The type {type} does not have matching method {returnType} {name} ({String.Join(", ", parameterTypes ?? new List<Type>(0))}).";

            var actualResult = ErrorMessages.CreateCouldNotResolveMethod(type, name, returnType, parameterTypes);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion CreateCouldNotResolveMethod

        #region CreateNoServiceProviderErrorMessage

        public static TheoryData<Type, string> CreateNoServiceProviderErrorMessageExceptionTestData =>
            new TheoryData<Type, string>()
            {
                { null, "type" }
            };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.CreateNoServiceProviderErrorMessageExceptionTestData))]
        public void CreateNoServiceProviderErrorMessage_ArgumentIsNull_ThrowsException(Type type, string parameterName)
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage(parameterName);

            var actualResult = Assert.Throws<ArgumentNullException>(() => ErrorMessages.CreateNoServiceProviderErrorMessage(type));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static TheoryData<Type> CreateNoServiceProviderErrorMessageTestData =>
            new TheoryData<Type>()
            {
                { typeof(object) },
                { typeof(Exception) }
            };

        [Theory]
        [MemberData(nameof(ErrorMessagesUnitTests.CreateNoServiceProviderErrorMessageTestData))]
        public void CreateNoServiceProviderErrorMessage_ReturnsCorrectResult(Type type)
        {
            var expectedResult =
                $"The service provider is not set for pipeline builder {type}.";

            var actualResult = ErrorMessages.CreateNoServiceProviderErrorMessage(type);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion CreateNoServiceProviderErrorMessage
    }
}
