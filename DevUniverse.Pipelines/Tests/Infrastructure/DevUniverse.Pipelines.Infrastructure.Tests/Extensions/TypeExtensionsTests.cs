﻿using System;
using System.Collections.Generic;
using System.Reflection;

using DevUniverse.Pipelines.Infrastructure.Extensions;

using Xunit;

namespace DevUniverse.Pipelines.Infrastructure.Tests.Extensions
{
    public class TypeExtensionsUnitTests
    {
        #region ResolveMethod

        public static TheoryData<Type, string, Type, IEnumerable<Type>, bool, BindingFlags, string> ResolveMethodExceptionTestData =>
            new TheoryData<Type, string, Type, IEnumerable<Type>, bool, BindingFlags, string>()
            {
                { null, null, null, null, false, BindingFlags.Default, "type" }
            };

        [Theory]
        [MemberData(nameof(TypeExtensionsUnitTests.ResolveMethodExceptionTestData))]
        public void ResolveMethod_ArgumentIsNull_ThrowsException
        (
            Type type,
            string name,
            Type returnType,
            List<Type> parameterTypes,
            bool invariantOnly,
            BindingFlags bindingFlags,
            string parameterName
        )
        {
            var expectedResultMessage = TestUtils.GetArgumentNullExceptionMessage(parameterName);

            var actualResult = Assert.Throws<ArgumentNullException>(() => type.ResolveMethod(name, returnType, parameterTypes, invariantOnly, bindingFlags));

            Assert.Equal(expectedResultMessage, actualResult.Message);
        }

        public static Type ResolveMethodType => typeof(object);
        public static MethodInfo ResolveMethodMethodInfoStatic => TypeExtensionsUnitTests.ResolveMethodType.GetMethod(nameof(Object.ReferenceEquals));
        public static MethodInfo ResolveMethodMethodInfoNonStatic => TypeExtensionsUnitTests.ResolveMethodType.GetMethod(nameof(Object.ToString));

        public static TheoryData<Type, string, Type, IEnumerable<Type>, bool, BindingFlags, bool, MethodInfo> ResolveMethodTestData =>
            new TheoryData<Type, string, Type, IEnumerable<Type>, bool, BindingFlags, bool, MethodInfo>()
            {
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ReferenceEquals),
                    typeof(bool),
                    new List<Type>() { typeof(object), typeof(object) },
                    false,
                    BindingFlags.Default,
                    true,
                    null
                },
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ReferenceEquals),
                    typeof(bool),
                    new List<Type>() { typeof(object), typeof(object) },
                    false,
                    BindingFlags.Public | BindingFlags.Static,
                    false,
                    TypeExtensionsUnitTests.ResolveMethodMethodInfoStatic
                },
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ReferenceEquals),
                    typeof(bool),
                    new List<Type>() { typeof(string), typeof(string) },
                    false,
                    BindingFlags.Public | BindingFlags.Static,
                    false,
                    TypeExtensionsUnitTests.ResolveMethodMethodInfoStatic
                },
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ReferenceEquals),
                    typeof(bool),
                    new List<Type>() { typeof(string), typeof(string) },
                    true,
                    BindingFlags.Public | BindingFlags.Static,
                    true,
                    null
                },
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ReferenceEquals),
                    typeof(object),
                    new List<Type>() { typeof(object), typeof(object) },
                    false,
                    BindingFlags.Public | BindingFlags.Static,
                    true,
                    null
                },
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ReferenceEquals),
                    typeof(bool),
                    new List<Type>() { typeof(object) },
                    false,
                    BindingFlags.Public | BindingFlags.Static,
                    true,
                    null
                },
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ReferenceEquals),
                    typeof(bool),
                    new List<Type>() { typeof(object), typeof(object), typeof(object) },
                    false,
                    BindingFlags.Public | BindingFlags.Static,
                    true,
                    null
                },

                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ToString),
                    typeof(string),
                    null,
                    false,
                    BindingFlags.Public | BindingFlags.Instance,
                    false,
                    TypeExtensionsUnitTests.ResolveMethodMethodInfoNonStatic
                },
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ToString),
                    typeof(string),
                    new List<Type>(),
                    false,
                    BindingFlags.Public | BindingFlags.Instance,
                    false,
                    TypeExtensionsUnitTests.ResolveMethodMethodInfoNonStatic
                },
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ToString),
                    typeof(string),
                    null,
                    false,
                    BindingFlags.Default,
                    true,
                    null
                },
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ToString),
                    typeof(string),
                    new List<Type>(),
                    false,
                    BindingFlags.Static | BindingFlags.Public,
                    true,
                    null
                },
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ToString),
                    typeof(object),
                    new List<Type>(),
                    false,
                    BindingFlags.Default,
                    true,
                    null
                },
                {
                    TypeExtensionsUnitTests.ResolveMethodType,
                    nameof(Object.ToString),
                    typeof(string),
                    new List<Type>() { typeof(object) },
                    false,
                    BindingFlags.Default,
                    true,
                    null
                }
            };

        [Theory]
        [MemberData(nameof(TypeExtensionsUnitTests.ResolveMethodTestData))]
        public void ResolveMethod_ReturnsCorrectResult
        (
            Type type,
            string name,
            Type returnType,
            List<Type> parameterTypes,
            bool invariantOnly,
            BindingFlags bindingFlags,
            bool isException,
            MethodInfo expectedResult
        )
        {
            if (isException)
            {
                var actualResult = Assert.Throws<MissingMethodException>(() => type.ResolveMethod(name, returnType, parameterTypes, invariantOnly, bindingFlags));

                Assert.NotNull(actualResult);
            }
            else
            {
                var actualResult = type.ResolveMethod(name, returnType, parameterTypes, invariantOnly, bindingFlags);

                Assert.Equal(expectedResult, actualResult);
            }
        }

        #endregion ResolveMethod
    }
}
