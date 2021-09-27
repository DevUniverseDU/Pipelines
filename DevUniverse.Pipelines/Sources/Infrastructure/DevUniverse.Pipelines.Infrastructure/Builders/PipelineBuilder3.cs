using System;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Steps;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilder{TParam0, TParam1, TParam2, TResult}" />
    public class PipelineBuilder<TParam0, TParam1, TParam2, TResult> :
        PipelineBuilderBase
        <
            Func<TParam0, TParam1, TParam2, TResult>,
            IPipelineStep<TParam0, TParam1, TParam2, TResult>,
            Func<TParam0, TParam1, TParam2, bool>,
            IPipelineCondition<TParam0, TParam1, TParam2>,
            IPipelineBuilder<TParam0, TParam1, TParam2, TResult>
        >,
        IPipelineBuilder<TParam0, TParam1, TParam2, TResult>
    {
        #region Constructors

        public PipelineBuilder(IServiceProvider? serviceProvider = null) : base(serviceProvider) { }

        #endregion Constructors
    }
}
