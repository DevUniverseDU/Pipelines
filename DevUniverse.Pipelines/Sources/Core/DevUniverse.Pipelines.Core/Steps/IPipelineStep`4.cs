using System;

namespace DevUniverse.Pipelines.Core.Steps
{
    /// <summary>
    /// The pipeline step with 3 input parameters which returns the result.
    /// </summary>
    /// <typeparam name="T0">The type of the 1st parameter.</typeparam>
    /// <typeparam name="T1">The type of the 2nd parameter.</typeparam>
    /// <typeparam name="T2">The type of the 3rd parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineStep<T0, T1, T2, TResult> : IPipelineStep
    {
        /// <summary>
        /// Executes the logic of the step.
        /// </summary>
        /// <param name="param0">The 1st parameter.</param>
        /// <param name="param1">The 2nd parameter.</param>
        /// <param name="param2">The 3rd parameter.</param>
        /// <param name="next">The next step in the pipeline which can be executed after this one.</param>
        /// <returns>The result of the step execution.</returns>
        public TResult Invoke(T0 param0, T1 param1, T2 param2, Func<T0, T1, T2, TResult> next);
    }
}
