using System;

using DevUniverse.Pipelines.Core.Conditions.Shared;

namespace DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionInterface
{
    /// <summary>
    /// The pipeline builder with the possibility to add the pipeline step conditionally.
    /// </summary>
    /// <typeparam name="TDelegate">The delegate type.</typeparam>
    /// <typeparam name="TCondition">The condition type.</typeparam>
    /// <typeparam name="TResult">The result pipeline builder type.</typeparam>
    public interface IPipelineBuilderConditionInterfaceWithServiceProvider<TDelegate, in TCondition, TResult> : IPipelineBuilderCore<TDelegate, TResult>
        where TDelegate : Delegate
        where TCondition : IPipelineConditionBasic
        where TResult : IPipelineBuilderConditionInterfaceWithServiceProvider<TDelegate, TCondition, TResult>
    {
        /// <summary>
        /// Adds the pipeline component to the pipeline.
        /// If the condition is met the configuration is executed and it is rejoined to the main pipeline branch, so the next step of the main branch is executed after this configuration.
        /// If the condition is NOT met the configuration is just skipped and next step of the main branch is executed.
        /// </summary>
        /// <param name="conditionFactory">The condition factory which provides the condition instance. Set to null if standard logic should be used to get condition instance.</param>
        /// <param name="branchBuilderConfiguration">The configuration of the branch pipeline builder.</param>
        /// <param name="branchBuilderFactory">The factory which provides the branch builder instance.</param>
        /// <typeparam name="TCustomCondition">The condition type.</typeparam>
        /// <returns>The current instance of the pipeline builder.</returns>
        public TResult UseIf<TCustomCondition>
        (
            Func<IServiceProvider, TCustomCondition>? conditionFactory,
            Action<TResult> branchBuilderConfiguration,
            Func<IServiceProvider, TResult>? branchBuilderFactory = null
        ) where TCustomCondition : TCondition;

        /// <summary>
        /// Adds the pipeline component to the pipeline.
        /// If the condition is met the configuration is executed and it is NOT rejoined to the main pipeline branch, so the next component of the main branch is NOT executed after this configuration.
        /// If the condition is NOT met the configuration is just skipped and next step of the main branch is executed.
        /// </summary>
        /// <param name="conditionFactory">The condition factory which provides the condition instance. Set to null if standard logic should be used to get condition instance.</param>
        /// <param name="branchBuilderConfiguration">The configuration of the branch pipeline builder.</param>
        /// <param name="branchBuilderFactory">The factory which provides the branch builder instance.</param>
        /// <typeparam name="TCustomCondition">The condition type.</typeparam>
        /// <returns>The current instance of the pipeline builder.</returns>
        public TResult UseBranchIf<TCustomCondition>
        (
            Func<IServiceProvider, TCustomCondition>? conditionFactory,
            Action<TResult> branchBuilderConfiguration,
            Func<IServiceProvider, TResult>? branchBuilderFactory = null
        ) where TCustomCondition : TCondition;
    }
}
