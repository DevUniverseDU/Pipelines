using System;
using System.Collections.Generic;

using DevUniverse.Pipelines.Infrastructure.Shared.Utils;

namespace DevUniverse.Pipelines.Infrastructure.Shared
{
    /// <summary>
    /// The error messages.
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        /// Creates the error message when the pipeline target is not set.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The error message.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some arguments are <see langword="null"/>.</exception>
        public static string CreateNoTargetSetErrorMessage(Type type)
        {
            ExceptionUtils.Process(type, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(type)));

            return $"The {type} does not have the target.";
        }

        /// <summary>
        /// Creates the error message when the method for the type is not resolved.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="returnType">The return type.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The error message.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some arguments are <see langword="null"/>.</exception>
        public static string CreateCouldNotResolveMethodErrorMessage(Type type, string name, Type returnType, IEnumerable<Type>? parameterTypes)
        {
            ExceptionUtils.Process(type, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(type)));
            ExceptionUtils.Process(name, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(name)));
            ExceptionUtils.Process(returnType, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(returnType)));

            return $"The type {type} does not have matching method {returnType} {name} ({String.Join(", ", parameterTypes ?? new List<Type>(0))}).";
        }

        /// <summary>
        /// Creates the error message when trying to create the new pipeline builder of the invalid type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="interfaceName">The interface name.</param>
        /// <returns>The error message.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some arguments are <see langword="null"/>.</exception>
        public static string CreateInvalidPipelineStepTypeErrorMessage(Type type, string interfaceName)
        {
            ExceptionUtils.Process(type, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(type)));
            ExceptionUtils.Process(interfaceName, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(interfaceName)));

            return $"The {type} should implement {interfaceName} interface.";
        }

        /// <summary>
        /// Creates the error message when the service provider is not set.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The error message.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some arguments are <see langword="null"/>.</exception>
        public static string CreateNoServiceProviderErrorMessage(Type type)
        {
            ExceptionUtils.Process(type, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(type)));

            return $"The service provider is not set for {type}.";
        }
    }
}
