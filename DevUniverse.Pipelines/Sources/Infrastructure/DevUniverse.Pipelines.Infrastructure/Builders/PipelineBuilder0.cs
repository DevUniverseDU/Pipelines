using System;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Steps;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilder{TResult}" />
    public class PipelineBuilder<TResult> :
        PipelineBuilderBase
        <
            Func<TResult>,
            IPipelineStep<TResult>,
            Func<bool>,
            IPipelineCondition,
            IPipelineBuilder<TResult>
        >,
        IPipelineBuilder<TResult>
    {
        #region Constructors

        public PipelineBuilder(IServiceProvider? serviceProvider = null) : base(serviceProvider) { }

        #endregion Constructors
    }
}
