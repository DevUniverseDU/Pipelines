using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Steps;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilderAsync{TResult}" />
    public class PipelineBuilderAsync<TResult> :
        PipelineBuilderAsyncBase
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
