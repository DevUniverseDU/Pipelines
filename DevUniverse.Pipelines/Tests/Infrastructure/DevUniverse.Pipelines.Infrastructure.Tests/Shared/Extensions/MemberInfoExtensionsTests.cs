using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using DevUniverse.Pipelines.Infrastructure.Shared.Extensions;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Shared.Extensions
{
    public class MemberInfoExtensionsUnitTests
    {
        #region MatchesByName

        public static TheoryData<MemberInfo?, string?, StringComparison> TestDataMatchesByNameException =>
            new TheoryData<MemberInfo?, string?, StringComparison>()
            {
                { null, null, StringComparison.Ordinal },
                { typeof(object).GetMethods(BindingFlags.Instance | BindingFlags.Public).First(), null, StringComparison.Ordinal }
            };

        [Theory]
        [MemberData(nameof(MemberInfoExtensionsUnitTests.TestDataMatchesByNameException))]
        public void MatchesByName_ArgumentIsNull_ThrowsException(MemberInfo? memberInfo, string? name, StringComparison stringComparison) =>
            Assert.Throws<ArgumentNullException>(() => memberInfo!.MatchesByName(name!, stringComparison));

        public static TheoryData<MemberInfo, string, StringComparison, bool> TestDataMatchesByName =>
            new TheoryData<MemberInfo, string, StringComparison, bool>()
            {
                { typeof(Exception).GetProperty(nameof(Exception.Message))!, "Message", StringComparison.Ordinal, true },
                { typeof(Exception).GetProperty(nameof(Exception.Message))!, "message", StringComparison.Ordinal, false },
                { typeof(Exception).GetProperty(nameof(Exception.Message))!, "message", StringComparison.OrdinalIgnoreCase, true },
                { typeof(Exception).GetMethod(nameof(Exception.GetType))!, "GetType", StringComparison.Ordinal, true },
                { typeof(Exception).GetMethod(nameof(Exception.GetType))!, "getType", StringComparison.Ordinal, false },
                { typeof(Exception).GetMethod(nameof(Exception.GetType))!, "getType", StringComparison.OrdinalIgnoreCase, true }
            };

        [Theory]
        [MemberData(nameof(MemberInfoExtensionsUnitTests.TestDataMatchesByName))]
        public void MatchesByName_ReturnsCorrectResult(MemberInfo memberInfo, string name, StringComparison stringComparison, bool expectedResult)
        {
            var actualResult = memberInfo.MatchesByName(name, stringComparison);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion MatchesByName

        #region MatchesByReturnType

        public static TheoryData<MethodInfo?, Type?, bool> TestDataMatchesByReturnTypeException =>
            new TheoryData<MethodInfo?, Type?, bool>()
            {
                { null, null, false },
                { typeof(object).GetMethods(BindingFlags.Instance | BindingFlags.Public).First(), null, false }
            };

        [Theory]
        [MemberData(nameof(MemberInfoExtensionsUnitTests.TestDataMatchesByReturnTypeException))]
        public void MatchesByReturnType_ArgumentIsNull_ThrowsException(MethodInfo? methodInfo, Type? returnType, bool invariantOnly) =>
            Assert.Throws<ArgumentNullException>(() => methodInfo!.MatchesByReturnType(returnType!, invariantOnly));

        public static MethodInfo MatchesByMethodInfo => typeof(string).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!;

        public static TheoryData<MethodInfo, Type, bool, bool> TestDataMatchesByReturnType =>
            new TheoryData<MethodInfo, Type, bool, bool>()
            {
                { MemberInfoExtensionsUnitTests.MatchesByMethodInfo, typeof(string), true, false },
                { MemberInfoExtensionsUnitTests.MatchesByMethodInfo, typeof(string), false, true },
                { MemberInfoExtensionsUnitTests.MatchesByMethodInfo, typeof(object), true, true },
                { MemberInfoExtensionsUnitTests.MatchesByMethodInfo, typeof(object), false, true }
            };

        [Theory]
        [MemberData(nameof(MemberInfoExtensionsUnitTests.TestDataMatchesByReturnType))]
        public void MatchesByReturnType_ReturnsCorrectResult(MethodInfo methodInfo, Type returnType, bool invariantOnly, bool expectedResult)
        {
            var actualResult = methodInfo.MatchesByReturnType(returnType, invariantOnly);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion MatchesByReturnType

        #region MatchesByParameterTypes

        [Fact]
        public void MatchesByParameterTypes_ArgumentIsNull_ThrowsException()
        {
            var methodBase = (MethodBase)null!;

            Assert.Throws<ArgumentNullException>(() => methodBase.MatchesByParameterTypes(null!, false));
        }

        public static MethodBase MatchesByParameterTypesMethodBaseConstructorWithoutParameters =>
            typeof(Exception).GetConstructors().First(item => item.GetParameters().Length == 0);

        public static MethodBase MatchesByParameterTypesMethodBaseConstructorWithParameters =>
            typeof(Exception).GetConstructors().First(item => item.GetParameters().Length == 2);

        public static MethodBase MatchesByParameterTypesMethodBaseMethodWithoutParameters => typeof(object).GetMethod(nameof(Object.GetType))!;

        public static MethodBase MatchesByParameterTypesMethodBaseMethodWithParameters =>
            typeof(object).GetMethod(nameof(Object.Equals), BindingFlags.Instance | BindingFlags.Public)!;

        public static TheoryData<MethodBase, IEnumerable<Type>?, bool, bool> TestDataMatchesByParameterTypes =>
            new TheoryData<MethodBase, IEnumerable<Type>?, bool, bool>()
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
        [MemberData(nameof(MemberInfoExtensionsUnitTests.TestDataMatchesByParameterTypes))]
        public void MatchesByParameterTypes_ReturnsCorrectResult(MethodBase methodBase, List<Type> parameterTypes, bool invariantOnly, bool expectedResult)
        {
            var actualResult = methodBase.MatchesByParameterTypes(parameterTypes, invariantOnly);

            Assert.Equal(expectedResult, actualResult);
        }

        #endregion MatchesByParameterTypes
    }
}
