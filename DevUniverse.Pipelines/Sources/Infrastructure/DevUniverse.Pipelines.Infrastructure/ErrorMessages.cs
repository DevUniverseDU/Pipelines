using System;
using System.Collections.Generic;

using DevUniverse.Pipelines.Core.Builders;

namespace DevUniverse.Pipelines.Infrastructure
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
        /// <exception cref="ArgumentNullException">The exception when the some of the arguments are null.</exception>
        public static string CreateNoTargetSetErrorMessage(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return $"The {type} does not have the target. Set the target using {nameof(IPipelineBuilder<object>.UseTarget)}.";
        }

        /// <summary>
        /// Creates the error message when the method for the type is not resolved.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="returnType">The return type.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The error message.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some of the arguments are null.</exception>
        public static string CreateCouldNotResolveMethod(Type type, string name, Type returnType, IEnumerable<Type> parameterTypes)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (returnType == null)
            {
                throw new ArgumentNullException(nameof(returnType));
            }

            return
                $"The type {type} does not have matching method {returnType} {name} ({String.Join(", ", parameterTypes ?? new List<Type>(0))}).";
        }

        /// <summary>
        /// Creates the error message when trying to create the new pipeline builder of the invalid type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The error message.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some of the arguments are null.</exception>
        public static string CreateInvalidPipelineStepTypeErrorMessage(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return $"The pipeline builder {type} should implement {nameof(IPipelineBuilder)} interface.";
        }

        /// <summary>
        /// Creates the error message when the service provider is not set.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The error message.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some of the arguments are null.</exception>
        public static string CreateNoServiceProviderErrorMessage(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return $"The service provider is not set for pipeline builder {type}.";
        }
    }
}
