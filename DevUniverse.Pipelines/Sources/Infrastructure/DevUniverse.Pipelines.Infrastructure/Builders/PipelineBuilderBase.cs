using System;

using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Builders;
using DevUniverse.Pipelines.Core.Shared.Conditions;
using DevUniverse.Pipelines.Core.Shared.Steps;
using DevUniverse.Pipelines.Infrastructure.Shared.Builders;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilderFull{TDelegate, TPipelineStep, TPredicate, TPipelineCondition, TResult}"/>/>
    public class PipelineBuilderBase
    <
        TDelegate,
        TPipelineStep,
        TPredicate,
        TPipelineCondition,
        TResult
    > :
        PipelineBuilderBasic<
            TDelegate,
            TPipelineStep,
            TPredicate,
            TPipelineCondition,
            TResult
        >
        where TDelegate : Delegate
        where TPipelineStep : IPipelineStepBasic
        where TPredicate : Delegate
        where TPipelineCondition : IPipelineConditionBasic
        where TResult : IPipelineBuilderFull
        <
            TDelegate,
            TPipelineStep,
            TPredicate,
            TPipelineCondition,
            TResult
        >
    {
        #region Condition

        protected sealed override string ConditionInterfaceMethodName => nameof(IPipelineCondition.Invoke);
        protected sealed override Type ConditionReturnType { get; } = typeof(bool);

        #endregion Condition

        protected PipelineBuilderBase(IServiceProvider? serviceProvider = null) : base(serviceProvider) { }
    }
}
