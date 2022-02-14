using System;

using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Steps;

namespace DevUniverse.Pipelines.Core.Builders
{
    /// <summary>
    /// The pipeline builder with 3 parameters which returns the result.
    /// </summary>
    /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
    /// <typeparam name="TParam1">The type of the 2nd parameter.</typeparam>
    /// <typeparam name="TParam2">The type of the 3rd parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineBuilder<TParam0, TParam1, TParam2, TResult> : Shared.IPipelineBuilder
        <
            Func<TParam0, TParam1, TParam2, TResult>,
            IPipelineStep<TParam0, TParam1, TParam2, TResult>,
            Func<TParam0, TParam1, TParam2, bool>,
            IPipelineCondition<TParam0, TParam1, TParam2>,
            IPipelineBuilder<TParam0, TParam1, TParam2, TResult>
        > { }
}
