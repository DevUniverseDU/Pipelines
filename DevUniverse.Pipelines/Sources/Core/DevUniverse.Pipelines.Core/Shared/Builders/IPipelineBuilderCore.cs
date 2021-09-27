using System;
using System.Collections.Generic;

namespace DevUniverse.Pipelines.Core.Shared.Builders
{
    /// <summary>
    /// The core pipeline builder.
    /// </summary>
    /// <typeparam name="TDelegate">The delegate type.</typeparam>
    /// <typeparam name="TResult">The result pipeline builder type.</typeparam>
    public interface IPipelineBuilderCore<TDelegate, out TResult> : IPipelineBuilderBasic
        where TDelegate : Delegate
        where TResult : IPipelineBuilderCore<TDelegate, TResult>
    {
        #region Properties

        /// <summary>
        /// The pipeline builder component.
        /// </summary>
        public IReadOnlyCollection<Func<TDelegate, TDelegate>> Components { get; }

        /// <summary>
        /// The target (terminating step) of the pipeline.
        /// </summary>
        public TDelegate? Target { get; }

        #endregion Properties

        #region Methods

        #region Use component

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public TResult Use(Func<TDelegate, TDelegate> component);

        #endregion Use component

        #region UseTarget

        /// <summary>
        /// Sets the pipeline target.
        /// The target is the last (terminating) step of the pipeline.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public TResult UseTarget(TDelegate target);

        #endregion UseTarget

        #region Copy

        /// <summary>
        /// Creates the new instance of the pipeline builder with same configuration (components/steps and target) as the current instance.
        /// </summary>
        /// <returns>The new instance of the pipeline builder.</returns>
        public TResult Copy();

        #endregion Copy

        #region Build

        /// <summary>
        /// Builds the pipeline.
        /// </summary>
        /// <param name="target">The target of the pipeline.</param>
        /// <returns>The pipeline delegate which is the start of the pipeline.</returns>
        public TDelegate Build(TDelegate? target = null);

        #endregion Build

        #endregion Methods
    }
}
