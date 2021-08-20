using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevUniverse.Pipelines.Infrastructure.Extensions
{
    /// <summary>
    /// The type extensions.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Resolves the type method.
        /// If the method is not found it throws the <see cref="MissingMethodException" />.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name to match.</param>
        /// <param name="returnType">The return type to match.</param>
        /// <param name="parameterTypes">The parameters to match.</param>
        /// <param name="invariantOnly">Indicates if the only variant types should be matched.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <returns>The method info.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some arguments are null.</exception>
        /// <exception cref="MissingMethodException">The exception when the method is not found.</exception>
        public static MethodInfo ResolveMethod
        (
            this Type type,
            string name,
            Type returnType,
            List<Type> parameterTypes = null,
            bool invariantOnly = false,
            BindingFlags bindingFlags = BindingFlags.Default
        )
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var publicMembers = type.GetMethods(bindingFlags).ToList();

            var results = publicMembers
                .Where(mi => mi.MatchesByName(name))
                .Where(mi => mi.MatchesByReturnType(returnType, invariantOnly))
                .Where(mi => mi.MatchesByParameterTypes(parameterTypes, invariantOnly))
                .ToList();

            if (results.Count == 0)
            {
                throw new MissingMethodException(ErrorMessages.CreateCouldNotResolveMethod(type, name, returnType, parameterTypes));
            }

            return results.First();
        }
    }
}
