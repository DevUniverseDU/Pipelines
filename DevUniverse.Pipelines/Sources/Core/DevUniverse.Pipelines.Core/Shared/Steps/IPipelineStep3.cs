using System;

namespace DevUniverse.Pipelines.Core.Shared.Steps
{
    /// <summary>
    /// The pipeline step with 3 parameters which returns the result.
    /// </summary>
    /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
    /// <typeparam name="TParam1">The type of the 2nd parameter.</typeparam>
    /// <typeparam name="TParam2">The type of the 3rd parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineStep<TParam0, TParam1, TParam2, TResult> : IPipelineStepBasic
    {
        /// <summary>
        /// Executes the logic of the step.
        /// </summary>
        /// <param name="param0">The 1st parameter.</param>
        /// <param name="param1">The 2nd parameter.</param>
        /// <param name="param2">The 3rd parameter.</param>
        /// <param name="next">The next step in the pipeline which can be executed after this one.</param>
        /// <returns>The result of the step execution.</returns>
        public TResult Invoke(TParam0 param0, TParam1 param1, TParam2 param2, Func<TParam0, TParam1, TParam2, TResult> next);
    }
}
