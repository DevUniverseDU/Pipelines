using System;

namespace DevUniverse.Pipelines.Core.Shared.Steps
{
    /// <summary>
    /// The pipeline step with 1 parameter which returns the result.
    /// </summary>
    /// <typeparam name="TParam0">The type of the parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineStep<TParam0, TResult> : IPipelineStepBasic
    {
        /// <summary>
        /// Executes the logic of the step.
        /// </summary>
        /// <param name="param0">The parameter.</param>
        /// <param name="next">The next step in the pipeline which can be executed after this one.</param>
        /// <returns>The result of the step execution.</returns>
        public TResult Invoke(TParam0 param0, Func<TParam0, TResult> next);
    }
}
