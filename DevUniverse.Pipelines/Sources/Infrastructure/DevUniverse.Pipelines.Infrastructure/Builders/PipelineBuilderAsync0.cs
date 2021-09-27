using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Steps;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilderAsync{TResult}" />
    public class PipelineBuilderAsync<TResult> :
        PipelineBuilderAsyncBasic
        <
            Func<TResult>,
            IPipelineStep<TResult>,
            Func<Task<bool>>,
            IPipelineConditionAsync,
            IPipelineBuilderAsync<TResult>
        >,
        IPipelineBuilderAsync<TResult>
        where TResult : Task

    {
        #region Constructors

        public PipelineBuilderAsync(IServiceProvider? serviceProvider = null) : base(serviceProvider) { }

        #endregion Constructors
    }
}
