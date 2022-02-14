using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Steps;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilderAsync{TParam0, TParam1, TResult}" />
    public class PipelineBuilderAsync<TParam0, TParam1, TResult> :
        PipelineBuilderAsyncBase
        <
            Func<TParam0, TParam1, TResult>,
            IPipelineStep<TParam0, TParam1, TResult>,
            Func<TParam0, TParam1, Task<bool>>,
            IPipelineConditionAsync<TParam0, TParam1>,
            IPipelineBuilderAsync<TParam0, TParam1, TResult>
        >,
        IPipelineBuilderAsync<TParam0, TParam1, TResult>
        where TResult : Task

    {
        #region Constructors

        public PipelineBuilderAsync(IServiceProvider? serviceProvider = null) : base(serviceProvider) { }

        #endregion Constructors
    }
}
