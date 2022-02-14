using System;

namespace DevUniverse.Pipelines.Core.Builders.Shared.Condition.ConditionDelegate
{
    /// <summary>
    /// The pipeline builder with the possibility to add the pipeline step conditionally.
    /// </summary>
    /// <typeparam name="TDelegate">The delegate type.</typeparam>
    /// <typeparam name="TPredicate">The predicate type.</typeparam>
    /// <typeparam name="TResult">The result pipeline builder type.</typeparam>
    public interface IPipelineBuilderConditionDelegate<TDelegate, in TPredicate, TResult> : IPipelineBuilderCore<TDelegate, TResult>
        where TDelegate : Delegate
        where TPredicate : Delegate
        where TResult : IPipelineBuilderConditionDelegate<TDelegate, TPredicate, TResult>
    {
        /// <summary>
        /// Adds the pipeline component to the pipeline.
        /// If the condition is met the configuration is executed and it is rejoined to the main pipeline branch, so the next step of the main branch is executed after this configuration.
        /// If the condition is NOT met the configuration is just skipped and next step of the main branch is executed.
        /// </summary>
        /// <param name="predicate">The predicate which determines if the added pipeline component should be executed.</param>
        /// <param name="branchBuilderConfiguration">The configuration of the branch pipeline builder.</param>
        /// <param name="branchBuilderFactory">The factory which provides the branch builder instance.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public TResult UseIf
        (
            TPredicate predicate,
            Action<TResult> branchBuilderConfiguration,
            Func<TResult> branchBuilderFactory
        );

        /// <summary>
        /// Adds the pipeline component to the pipeline.
        /// If the condition is met the configuration is executed and it is NOT rejoined to the main pipeline branch, so the next component of the main branch is NOT executed after this configuration.
        /// If the condition is NOT met the configuration is just skipped and next step of the main branch is executed.
        /// </summary>
        /// <param name="predicate">The predicate which determines if the added pipeline component should be executed.</param>
        /// <param name="branchBuilderConfiguration">The configuration of the branch pipeline builder.</param>
        /// <param name="branchBuilderFactory">The factory which provides the branch builder instance.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public TResult UseBranchIf
        (
            TPredicate predicate,
            Action<TResult> branchBuilderConfiguration,
            Func<TResult> branchBuilderFactory
        );
    }
}
