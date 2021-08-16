using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DevUniverse.Pipelines.Infrastructure.Tests")]
namespace DevUniverse.Pipelines.Infrastructure.Utils
{
    /// <summary>
    /// The activator utils.
    /// </summary>
    internal static class ActivatorUtils
    {
        /// <summary>
        /// Creates the instance of the specified type.
        /// </summary>
        /// <param name="type">The type of the instance.</param>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <returns>The created instance.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some of the arguments are null.</exception>
        public static object Create(Type type, IEnumerable<object> constructorArgs = null)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Activator.CreateInstance(type, (constructorArgs ?? new List<object>()).ToArray());
        }
    }
}
