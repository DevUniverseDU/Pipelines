using System;

using DevUniverse.Pipelines.Core.Shared.Steps;

namespace DevUniverse.Pipelines.Core.Shared.Builders.StepInterface
{
    /// <summary>
    /// The pipeline builder with the possibility to add the pipeline step using the <see cref="IPipelineStepBasic"/>> implementation.
    /// </summary>
    /// <typeparam name="TDelegate">The delegate type.</typeparam>
    /// <typeparam name="TPipelineStep">The pipeline step type.</typeparam>
    /// <typeparam name="TResult">The result pipeline builder type.</typeparam>
    public interface IPipelineBuilderStepInterfaceWithServiceProvider<TDelegate, in TPipelineStep, out TResult> : IPipelineBuilderWithServiceProvider<TDelegate, TResult>
        where TDelegate : Delegate
        where TPipelineStep : IPipelineStepBasic
        where TResult : IPipelineBuilderStepInterfaceWithServiceProvider<TDelegate, TPipelineStep, TResult>
    {
        /// <summary>
        /// Add the component from the pipeline step interface implementation.
        /// Requires the service provider to be set.
        /// </summary>
        /// <param name="pipelineStepFactory">The factory which provides the pipeline step instance.</param>
        /// <typeparam name="TStep">The pipeline step type.</typeparam>
        /// <returns></returns>
        public TResult Use<TStep>(Func<IServiceProvider, TStep>? pipelineStepFactory = null) where TStep : TPipelineStep;
    }
}
