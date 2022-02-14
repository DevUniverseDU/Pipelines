using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using DevUniverse.Pipelines.Infrastructure.Shared.Utils;

namespace DevUniverse.Pipelines.Infrastructure.Shared.Extensions
{
    /// <summary>
    /// The member info extensions.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Checks if the member matches by the name.
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        /// <param name="name">The name to match.</param>
        /// <param name="stringComparison">The string comparison rule.</param>
        /// <returns><see langword="true"/> in case of the match, otherwise <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some arguments are <see langword="null"/>.</exception>
        public static bool MatchesByName(this MemberInfo memberInfo, string name, StringComparison stringComparison = StringComparison.Ordinal)
        {
            ExceptionUtils.Process(memberInfo, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(memberInfo)));
            ExceptionUtils.Process(name, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(name)));

            return String.Equals(memberInfo.Name, name, stringComparison);
        }

        /// <summary>
        /// Checks if the method matches by the return type.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        /// <param name="returnType">The return type to check.</param>
        /// <param name="invariantOnly">Indicates if the only invariant types should match.</param>
        /// <returns><see langword="true"/> in case of the match, otherwise <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some arguments are <see langword="null"/>.</exception>
        public static bool MatchesByReturnType(this MethodInfo methodInfo, Type returnType, bool invariantOnly = false)
        {
            ExceptionUtils.Process(methodInfo, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(methodInfo)));
            ExceptionUtils.Process(returnType, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(returnType)));

            return invariantOnly ? returnType == methodInfo.ReturnType : methodInfo.ReturnType.GetTypeInfo().IsAssignableFrom(returnType);
        }

        /// <summary>
        /// Checks if the method base matches by the parameter types.
        /// Parameter types should be in the same order as in the declaration.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="invariantOnly">Indicates if the only invariant types should match.</param>
        /// <returns><see langword="true"/> in case of the match, otherwise <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some arguments are <see langword="null"/>.</exception>
        public static bool MatchesByParameterTypes(this MethodBase methodBase, List<Type>? parameterTypes = null, bool invariantOnly = false)
        {
            ExceptionUtils.Process(methodBase, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(methodBase)));

            parameterTypes ??= new List<Type>();

            var methodInfoParameterTypes = methodBase.GetParameters().Select(item => item.ParameterType).ToList();

            if (parameterTypes.Count != methodInfoParameterTypes.Count)
            {
                return false;
            }

            for (var a = 0; a < methodInfoParameterTypes.Count; a++)
            {
                var methodBaseParameterType = methodInfoParameterTypes[a];
                var parameterType = parameterTypes[a];

                if (invariantOnly)
                {
                    if (parameterType != methodBaseParameterType)
                    {
                        return false;
                    }
                }
                else
                {
                    if (!methodBaseParameterType.GetTypeInfo().IsAssignableFrom(parameterType))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
