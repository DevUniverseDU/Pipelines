using System;

using DevUniverse.Pipelines.Core.Steps.Shared;

namespace DevUniverse.Pipelines.Core.Builders.Shared.StepInterface
{
    /// <summary>
    /// The pipeline builder with the possibility to add the pipeline step using the <see cref="IPipelineStepBasic"/>> implementation.
    /// </summary>
    /// <typeparam name="TDelegate">The delegate type.</typeparam>
    /// <typeparam name="TPipelineStep">The pipeline step type.</typeparam>
    /// <typeparam name="TResult">The result pipeline builder type.</typeparam>
    public interface IPipelineBuilderStepInterface<TDelegate, in TPipelineStep, out TResult> : IPipelineBuilderCore<TDelegate, TResult>
        where TDelegate : Delegate
        where TPipelineStep : IPipelineStepBasic
        where TResult : IPipelineBuilderStepInterface<TDelegate, TPipelineStep, TResult>
    {
        /// <summary>
        /// Add the component from the pipeline step interface implementation.
        /// </summary>
        /// <param name="pipelineStepFactory">The factory which provides the pipeline step instance.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public TResult Use(Func<TPipelineStep> pipelineStepFactory);
    }
}
