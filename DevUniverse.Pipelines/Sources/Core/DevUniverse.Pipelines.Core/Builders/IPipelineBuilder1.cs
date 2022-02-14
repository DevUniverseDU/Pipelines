using System;

using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Steps;

namespace DevUniverse.Pipelines.Core.Builders
{
    /// <summary>
    /// The pipeline builder with 1 parameter which returns the result.
    /// </summary>
    /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineBuilder<TParam0, TResult> : Shared.IPipelineBuilder
        <
            Func<TParam0, TResult>,
            IPipelineStep<TParam0, TResult>,
            Func<TParam0, bool>,
            IPipelineCondition<TParam0>,
            IPipelineBuilder<TParam0, TResult>
        > { }
}
