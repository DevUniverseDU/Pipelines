using System;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Steps;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilderAsync{TParam0, TResult}" />
    public class PipelineBuilderAsync<TParam0, TResult> :
        PipelineBuilderAsyncBase
        <
            Func<TParam0, TResult>,
            IPipelineStep<TParam0, TResult>,
            Func<TParam0, Task<bool>>,
            IPipelineConditionAsync<TParam0>,
            IPipelineBuilderAsync<TParam0, TResult>
        >,
        IPipelineBuilderAsync<TParam0, TResult>
        where TResult : Task

    {
        #region Constructors

        public PipelineBuilderAsync(IServiceProvider? serviceProvider = null) : base(serviceProvider) { }

        #endregion Constructors
    }
}
