using System;

namespace DevUniverse.Pipelines.Core.Builders
{
    /// <summary>
    /// The basic pipeline builder.
    /// </summary>
    public interface IPipelineBuilder
    {
        #region Properties

        /// <summary>
        /// The service provider.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        #endregion Properties
    }
}
