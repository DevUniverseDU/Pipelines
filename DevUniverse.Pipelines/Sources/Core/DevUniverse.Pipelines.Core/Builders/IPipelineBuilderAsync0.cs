using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Builders;
using DevUniverse.Pipelines.Core.Shared.Steps;

namespace DevUniverse.Pipelines.Core.Builders
{
    /// <summary>
    /// The pipeline builder with no parameters which returns the result asynchronously.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineBuilderAsync<TResult> :
        IPipelineBuilderAsync,
        IPipelineBuilderFull
        <
            Func<TResult>,
            IPipelineStep<TResult>,
            Func<Task<bool>>,
            IPipelineConditionAsync,
            IPipelineBuilderAsync<TResult>
        > where TResult : Task { }
}
