using System;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Steps;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilder{TParam0, TResult}" />
    public class PipelineBuilder<TParam0, TResult> :
        PipelineBuilderBase
        <
            Func<TParam0, TResult>,
            IPipelineStep<TParam0, TResult>,
            Func<TParam0, bool>,
            IPipelineCondition<TParam0>,
            IPipelineBuilder<TParam0, TResult>
        >,
        IPipelineBuilder<TParam0, TResult>
    {
        #region Constructors

        public PipelineBuilder(IServiceProvider? serviceProvider = null) : base(serviceProvider) { }

        #endregion Constructors
    }
}
