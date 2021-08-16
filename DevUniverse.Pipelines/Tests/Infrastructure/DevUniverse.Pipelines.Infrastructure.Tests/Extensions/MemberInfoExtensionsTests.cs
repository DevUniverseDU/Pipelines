using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using DevUniverse.Pipelines.Infrastructure.Extensions;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Extensions
{
    public class MemberInfoExtensionsUnitTests
    {
        #region MatchesByName

        public static TheoryData<MemberInfo, string, StringComparison, string> MatchesByNameExceptionTestData =>
            new TheoryData<MemberInfo, string, StringComparison, string>()
            {
                { null, null, StringComparison.Ordinal, "memberInfo" },
                { typeof(object).GetMethods(BindingFlags.Instance | BindingFlags.Public).First(), null, StringComparison.Ordinal, "name" }
            };

        [Theory]
        [MemberData(nameof(MemberInfoExtensionsUnitTests.MatchesByNameExceptionTestData))]
        public void MatchesByName_ArgumentIsNull_ThrowsException(MemberInfo memberInfo, string name, StringComparison stringComparison, string parameterName)
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage(parameterName);

            var actualResult = Assert.Throws<ArgumentNullException>(() => memberInfo.MatchesByName(name, stringComparison));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static TheoryData<MemberInfo, string, StringComparison, bool> MatchesByNameTestData =>
            new TheoryData<MemberInfo, string, StringComparison, bool>()
            {
                { typeof(Exception).GetProperty(nameof(Exception.Message)), "Message", StringComparison.Ordinal, true },
                { typeof(Exception).GetProperty(nameof(Exception.Message)), "message", StringComparison.Ordinal, false },
                { typeof(Exception).GetProperty(nameof(Exception.Message)), "message", StringComparison.OrdinalIgnoreCase, true },
                { typeof(Exception).GetMethod(nameof(Exception.GetType)), "GetType", StringComparison.Ordinal, true },
                { typeof(Exception).GetMethod(nameof(Exception.GetType)), "getType", StringComparison.Ordinal, false },
                { typeof(Exception).GetMethod(nameof(Exception.GetType)), "getType", StringComparison.OrdinalIgnoreCase, true }
            };

        [Theory]
        [MemberData(nameof(MemberInfoExtensionsUnitTests.MatchesByNameTestData))]
        public void MatchesByName_ReturnsCorrectResult(MemberInfo memberInfo, string name, StringComparison stringComparison, bool expectedResult)
        {
            var actualResult = memberInfo.MatchesByName(name, stringComparison);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion MatchesByName

        #region MatchesByReturnType

        public static TheoryData<MethodInfo, Type, bool, string> MatchesByReturnTypeExceptionTestData =>
            new TheoryData<MethodInfo, Type, bool, string>()
            {
                { null, null, false, "methodInfo" },
                { typeof(object).GetMethods(BindingFlags.Instance | BindingFlags.Public).First(), null, false, "returnType" }
            };

        [Theory]
        [MemberData(nameof(MemberInfoExtensionsUnitTests.MatchesByReturnTypeExceptionTestData))]
        public void MatchesByReturnType_ArgumentIsNull_ThrowsException(MethodInfo methodInfo, Type returnType, bool invariantOnly, string parameterName)
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage(parameterName);

            var actualResult = Assert.Throws<ArgumentNullException>(() => methodInfo.MatchesByReturnType(returnType, invariantOnly));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static MethodInfo MatchesByMethodInfo => typeof(string).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        public static TheoryData<MethodInfo, Type, bool, bool> MatchesByReturnTypeTestData =>
            new TheoryData<MethodInfo, Type, bool, bool>()
            {
                { MemberInfoExtensionsUnitTests.MatchesByMethodInfo, typeof(string), true, false },
                { MemberInfoExtensionsUnitTests.MatchesByMethodInfo, typeof(string), false, true },
                { MemberInfoExtensionsUnitTests.MatchesByMethodInfo, typeof(object), true, true },
                { MemberInfoExtensionsUnitTests.MatchesByMethodInfo, typeof(object), false, true }
            };

        [Theory]
        [MemberData(nameof(MemberInfoExtensionsUnitTests.MatchesByReturnTypeTestData))]
        public void MatchesByReturnType_ReturnsCorrectResult(MethodInfo methodInfo, Type returnType, bool invariantOnly, bool expectedResult)
        {
            var actualResult = methodInfo.MatchesByReturnType(returnType, invariantOnly);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion MatchesByReturnType

        #region MatchesByParameterTypes

        public static TheoryData<MethodBase, IEnumerable<Type>, bool, string> MatchesByParameterTypesExceptionTestData =>
            new TheoryData<MethodBase, IEnumerable<Type>, bool, string>()
            {
                { null, null, false, "methodBase" }
            };

        [Theory]
        [MemberData(nameof(MemberInfoExtensionsUnitTests.MatchesByParameterTypesExceptionTestData))]
        public void MatchesByParameterTypes_ArgumentIsNull_ThrowsException(MethodBase methodBase, List<Type> parameterTypes, bool invariantOnly, string parameterName)
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage(parameterName);

            var actualResult = Assert.Throws<ArgumentNullException>(() => methodBase.MatchesByParameterTypes(parameterTypes, invariantOnly));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static MethodBase MatchesByParameterTypesMethodBaseConstructorWithoutParameters =>
            typeof(Exception).GetConstructors().First(item => item.GetParameters().Length == 0);

        public static MethodBase MatchesByParameterTypesMethodBaseConstructorWithParameters =>
            typeof(Exception).GetConstructors().First(item => item.GetParameters().Length == 2);

        public static MethodBase MatchesByParameterTypesMethodBaseMethodWithoutParameters => typeof(object).GetMethod(nameof(Object.GetType));

        public static MethodBase MatchesByParameterTypesMethodBaseMethodWithParameters =>
            typeof(object).GetMethod(nameof(Object.Equals), BindingFlags.Instance | BindingFlags.Public);

        public static TheoryData<MethodBase, IEnumerable<Type>, bool, bool> MatchesByParameterTypesTestData =>
            new TheoryData<MethodBase, IEnumerable<Type>, bool, bool>()
            {
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithoutParameters, null, false, true },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithoutParameters, null, true, true },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithoutParameters, new List<Type>(), false, true },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithoutParameters, new List<Type>(), true, true },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithoutParameters, new List<Type>() { typeof(Exception) }, false, false },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithoutParameters, new List<Type>() { typeof(Exception) }, true, false },

                {
                    MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithParameters,
                    new List<Type>() { typeof(string), typeof(Exception) },
                    false,
                    true
                },
                {
                    MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithParameters,
                    new List<Type>() { typeof(string), typeof(Exception) },
                    true,
                    true
                },
                {
                    MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithParameters,
                    new List<Type>() { typeof(string), typeof(NotSupportedException) },
                    false,
                    true
                },
                {
                    MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithParameters,
                    new List<Type>() { typeof(string), typeof(NotSupportedException) },
                    true,
                    false
                },
                {
                    MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithParameters,
                    new List<Type>() { typeof(string), typeof(object) },
                    false,
                    false
                },
                {
                    MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithParameters,
                    new List<Type>() { typeof(string), typeof(object) },
                    true,
                    false
                },
                {
                    MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithParameters,
                    new List<Type>() { typeof(string) },
                    false,
                    false
                },
                {
                    MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseConstructorWithParameters,
                    new List<Type>() { typeof(string), typeof(NotSupportedException), typeof(ArgumentException) },
                    false,
                    false
                },

                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithoutParameters, null, false, true },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithoutParameters, null, true, true },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithoutParameters, new List<Type>(), false, true },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithoutParameters, new List<Type>(), true, true },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithoutParameters, new List<Type>() { typeof(object) }, false, false },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithoutParameters, new List<Type>() { typeof(object) }, true, false },

                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithParameters, new List<Type>() { typeof(object) }, false, true },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithParameters, new List<Type>() { typeof(object) }, true, true },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithParameters, new List<Type>() { typeof(string) }, false, true },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithParameters, new List<Type>() { typeof(string) }, true, false },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithParameters, new List<Type>(), false, false },
                { MemberInfoExtensionsUnitTests.MatchesByParameterTypesMethodBaseMethodWithParameters, new List<Type>() { typeof(string), typeof(object) }, true, false }
            };

        [Theory]
        [MemberData(nameof(MemberInfoExtensionsUnitTests.MatchesByParameterTypesTestData))]
        public void MatchesByParameterTypes_ReturnsCorrectResult(MethodBase methodBase, List<Type> parameterTypes, bool invariantOnly, bool expectedResult)
        {
            var actualResult = methodBase.MatchesByParameterTypes(parameterTypes, invariantOnly);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion MatchesByParameterTypes
    }
}
