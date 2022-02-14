using System;

using DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate;
using DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionInterface;
using DevUniverse.Pipelines.Core.Builders.Shared.StepInterface;
using DevUniverse.Pipelines.Core.Conditions.Shared;
using DevUniverse.Pipelines.Core.Steps.Shared;

namespace DevUniverse.Pipelines.Core.Builders.Shared
{
    /// <summary>
    /// The pipeline builder implementing all available pipeline builder parts.
    /// </summary>
    /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
    /// <typeparam name="TPipelineStep">The type of the step.</typeparam>
    /// <typeparam name="TPredicate">The type of the predicate.</typeparam>
    /// <typeparam name="TPipelineCondition">The type of the condition.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineBuilder
    <
        TDelegate,
        in TPipelineStep,
        in TPredicate,
        in TPipelineCondition,
        TResult
    > : IPipelineBuilderStepInterface
        <
            TDelegate,
            TPipelineStep,
            TResult
        >,
        IPipelineBuilderStepInterfaceWithServiceProvider
        <
            TDelegate,
            TPipelineStep,
            TResult
        >,
        IPipelineBuilderConditionDelegate
        <
            TDelegate,
            TPredicate,
            TResult
        >,
        IPipelineBuilderConditionInterface
        <
            TDelegate,
            TPipelineCondition,
            TResult
        >,
        IPipelineBuilderConditionDelegateWithServiceProvider
        <
            TDelegate,
            TPredicate,
            TResult
        >,
        IPipelineBuilderConditionInterfaceWithServiceProvider
        <
            TDelegate,
            TPipelineCondition,
            TResult
        >
        where TDelegate : Delegate
        where TPipelineStep : IPipelineStepBasic
        where TPredicate : Delegate
        where TPipelineCondition : IPipelineConditionBasic
        where TResult : IPipelineBuilder
        <
            TDelegate,
            TPipelineStep,
            TPredicate,
            TPipelineCondition,
            TResult
        > { }
}
