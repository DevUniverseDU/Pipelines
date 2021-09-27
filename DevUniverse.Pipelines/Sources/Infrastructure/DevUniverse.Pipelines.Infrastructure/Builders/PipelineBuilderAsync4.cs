using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Steps;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilderAsync{TParam0, TParam1, TParam2, TParam3, TResult}" />
    public class PipelineBuilderAsync<TParam0, TParam1, TParam2, TParam3, TResult> :
        PipelineBuilderAsyncBasic
        <
            Func<TParam0, TParam1, TParam2, TParam3, TResult>,
            IPipelineStep<TParam0, TParam1, TParam2, TParam3, TResult>,
            Func<TParam0, TParam1, TParam2, TParam3, Task<bool>>,
            IPipelineConditionAsync<TParam0, TParam1, TParam2, TParam3>,
            IPipelineBuilderAsync<TParam0, TParam1, TParam2, TParam3, TResult>
        >,
        IPipelineBuilderAsync<TParam0, TParam1, TParam2, TParam3, TResult>
        where TResult : Task

    {
        #region Constructors

        public PipelineBuilderAsync(IServiceProvider? serviceProvider = null) : base(serviceProvider) { }

        #endregion Constructors
    }
}
