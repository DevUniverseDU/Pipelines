using System;
using System.Collections.Generic;

namespace DevUniverse.Pipelines.Core.BuilderFactories.Shared
{
    /// <summary>
    /// The basic pipeline builder factory.
    /// </summary>
    public interface IPipelineBuilderFactoryBasic
    {
        /// <summary>
        /// Creates the pipeline builder of the specified type using the type variable.
        /// </summary>
        /// <param name="type">The type of the pipeline builder.</param>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <returns>The new instance of the pipeline builder.</returns>
        public object Create(Type type, params object?[]? constructorArgs);

        /// <summary>
        /// Creates the pipeline builder of the specified type using the type variable.
        /// </summary>
        /// <param name="type">The type of the pipeline builder.</param>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <returns>The new instance of the pipeline builder.</returns>
        public object Create(Type type, IEnumerable<object?>? constructorArgs = null);
    }
}
