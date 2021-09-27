using System;

using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Builders;
using DevUniverse.Pipelines.Core.Shared.Steps;

namespace DevUniverse.Pipelines.Core.Builders
{
    /// <summary>
    /// The pipeline builder with no parameters which returns the result.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineBuilder<TResult> :
        IPipelineBuilderFull
        <
            Func<TResult>,
            IPipelineStep<TResult>,
            Func<bool>,
            IPipelineCondition,
            IPipelineBuilder<TResult>
        > { }
}
