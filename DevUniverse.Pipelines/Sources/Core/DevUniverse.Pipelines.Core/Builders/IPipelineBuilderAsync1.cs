using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Builders;
using DevUniverse.Pipelines.Core.Shared.Steps;

namespace DevUniverse.Pipelines.Core.Builders
{
    /// <summary>
    /// The pipeline builder with 1 parameter which returns the result asynchronously.
    /// </summary>
    /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineBuilderAsync<TParam0, TResult> :
        IPipelineBuilderAsync,
        IPipelineBuilderFull
        <
            Func<TParam0, TResult>,
            IPipelineStep<TParam0, TResult>,
            Func<TParam0, Task<bool>>,
            IPipelineConditionAsync<TParam0>,
            IPipelineBuilderAsync<TParam0, TResult>
        > where TResult : Task { }
}
