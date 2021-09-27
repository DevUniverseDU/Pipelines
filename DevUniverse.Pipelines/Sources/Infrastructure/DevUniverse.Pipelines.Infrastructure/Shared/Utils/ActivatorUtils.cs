using System;
using System.Collections.Generic;
using System.Linq;

namespace DevUniverse.Pipelines.Infrastructure.Shared.Utils
{
    /// <summary>
    /// The activator utils.
    /// </summary>
    public static class ActivatorUtils
    {
        /// <summary>
        /// Creates the instance of the specified type.
        /// </summary>
        /// <param name="type">The type of the instance.</param>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <returns>The created instance.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some arguments are <see langword="null"/>.</exception>
        public static object Create(Type type, IEnumerable<object?>? constructorArgs = null)
        {
            ExceptionUtils.Process(type, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(type)));

            return Activator.CreateInstance(type, (constructorArgs ?? new List<object?>()).ToArray())!;
        }
    }
}
