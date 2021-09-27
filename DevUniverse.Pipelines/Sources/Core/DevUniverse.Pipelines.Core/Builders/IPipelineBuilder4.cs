using System;

using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Builders;
using DevUniverse.Pipelines.Core.Shared.Steps;

namespace DevUniverse.Pipelines.Core.Builders
{
    /// <summary>
    /// The pipeline builder with 4 parameters which returns the result.
    /// </summary>
    /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
    /// <typeparam name="TParam1">The type of the 2nd parameter.</typeparam>
    /// <typeparam name="TParam2">The type of the 3rd parameter.</typeparam>
    /// <typeparam name="TParam3">The type of the 4th parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineBuilder<TParam0, TParam1, TParam2, TParam3, TResult> :
        IPipelineBuilderFull
        <
            Func<TParam0, TParam1, TParam2, TParam3, TResult>,
            IPipelineStep<TParam0, TParam1, TParam2, TParam3, TResult>,
            Func<TParam0, TParam1, TParam2, TParam3, bool>,
            IPipelineCondition<TParam0, TParam1, TParam2, TParam3>,
            IPipelineBuilder<TParam0, TParam1, TParam2, TParam3, TResult>
        > { }
}
