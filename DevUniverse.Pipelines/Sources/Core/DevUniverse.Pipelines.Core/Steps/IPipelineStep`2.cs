using System;

namespace DevUniverse.Pipelines.Core.Steps
{
    /// <summary>
    /// The pipeline step with 1 input parameter which returns the result.
    /// </summary>
    /// <typeparam name="T0">The type of the parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineStep<T0, TResult> : IPipelineStep
    {
        /// <summary>
        /// Executes the logic of the step.
        /// </summary>
        /// <param name="param0">The parameter.</param>
        /// <param name="next">The next step in the pipeline which can be executed after this one.</param>
        /// <returns>The result of the step execution.</returns>
        public TResult Invoke(T0 param0, Func<T0, TResult> next);
    }
}
