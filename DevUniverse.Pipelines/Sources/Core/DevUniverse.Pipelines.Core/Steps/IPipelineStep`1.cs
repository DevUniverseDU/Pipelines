﻿using System;

namespace DevUniverse.Pipelines.Core.Steps
{
    /// <summary>
    /// The pipeline step without input parameters which returns the result.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineStep<TResult> : IPipelineStep
    {
        /// <summary>
        /// Executes the logic of the step.
        /// </summary>
        /// <param name="next">The next step in the pipeline which can be executed after this one.</param>
        /// <returns>The result of the step execution.</returns>
        public TResult Invoke(Func<TResult> next);
    }
}
