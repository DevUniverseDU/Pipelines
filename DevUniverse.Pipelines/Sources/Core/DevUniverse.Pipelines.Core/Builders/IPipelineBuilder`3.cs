﻿using System;

using DevUniverse.Pipelines.Core.Steps;

namespace DevUniverse.Pipelines.Core.Builders
{
    /// <summary>
    /// The pipeline builder with 2 input parameters which returns the result.
    /// </summary>
    /// <typeparam name="T0">The type of the 1st parameter.</typeparam>
    /// <typeparam name="T1">The type of the 2nd parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IPipelineBuilder<T0, T1, TResult> : IPipelineBuilder
    {
        #region Properties

        /// <summary>
        /// The target (terminating step) of the pipeline.
        /// </summary>
        public Func<T0, T1, TResult> Target { get; }

        #endregion Properties

        #region Methods

        #region Use

        #region Use component

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> Use(Func<Func<T0, T1, TResult>, Func<T0, T1, TResult>> component);

        #endregion Use component

        #region Use handler

        /// <summary>
        /// Adds the component created from the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> Use(Func<T0, T1, Func<T0, T1, TResult>, TResult> handler);

        #endregion Use handler

        #region Use step

        /// <summary>
        /// Add the component from the pipeline step interface implementation.
        /// Requires the service provider to be set.
        /// </summary>
        /// <typeparam name="TPipelineStep">The type of the pipeline step.</typeparam>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> Use<TPipelineStep>() where TPipelineStep : IPipelineStep<T0, T1, TResult>;

        /// <summary>
        /// Add the component from the pipeline step interface implementation.
        /// </summary>
        /// <param name="factory">The factory which provides the pipeline step instance.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> Use(Func<IPipelineStep<T0, T1, TResult>> factory);

        /// <summary>
        /// Add the component from the pipeline step interface implementation.
        /// Requires the service provider to be set.
        /// </summary>
        /// <param name="factory">The factory which provides the pipeline step instance.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> Use(Func<IServiceProvider, IPipelineStep<T0, T1, TResult>> factory);

        #endregion Use step

        #endregion Use

        #region UseIf

        /// <summary>
        /// Adds the pipeline component to the pipeline.
        /// If condition is met the configuration is executed and it is rejoined to the main pipeline branch, so the next step of the main branch is executed after this configuration.
        /// If condition is NOT met the configuration is just skipped and next step of the main branch is executed.
        /// Requires the service provider to be set.
        /// </summary>
        /// <param name="predicate">The predicate which determines if the added pipeline component should be executed.</param>
        /// <param name="configuration">The configuration of the branch pipeline builder.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> UseIf
        (
            Func<T0, T1, bool> predicate,
            Action<IPipelineBuilder<T0, T1, TResult>> configuration
        );

        /// <summary>
        /// Adds the pipeline component to the pipeline.
        /// If condition is met the configuration is executed and it is rejoined to the main pipeline branch, so the next step of the main branch is executed after this configuration.
        /// If condition is NOT met the configuration is just skipped and next step of the main branch is executed.
        /// </summary>
        /// <param name="predicate">The predicate which determines if the added pipeline component should be executed.</param>
        /// <param name="configuration">The configuration of the branch pipeline builder.</param>
        /// <param name="factory">The factory which provides the branch builder instance.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> UseIf
        (
            Func<T0, T1, bool> predicate,
            Action<IPipelineBuilder<T0, T1, TResult>> configuration,
            Func<IPipelineBuilder<T0, T1, TResult>> factory
        );

        /// <summary>
        /// Adds the pipeline component to the pipeline.
        /// If condition is met the configuration is executed and it is rejoined to the main pipeline branch, so the next step of the main branch is executed after this configuration.
        /// If condition is NOT met the configuration is just skipped and next step of the main branch is executed.
        /// Requires the service provider to be set.
        /// </summary>
        /// <param name="predicate">The predicate which determines if the added pipeline component should be executed.</param>
        /// <param name="configuration">The configuration of the branch pipeline builder.</param>
        /// <param name="factory">The factory which provides the branch builder instance.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> UseIf
        (
            Func<T0, T1, bool> predicate,
            Action<IPipelineBuilder<T0, T1, TResult>> configuration,
            Func<IServiceProvider, IPipelineBuilder<T0, T1, TResult>> factory
        );

        #endregion UseIf

        #region UseBranchIf

        /// <summary>
        /// Adds the pipeline component to the pipeline.
        /// If condition is met the configuration is executed and it is NOT rejoined to the main pipeline branch, so the next component of the main branch is NOT executed after this configuration.
        /// If condition is NOT met the configuration is just skipped and next step of the main branch is executed.
        /// Requires the service provider to be set.
        /// </summary>
        /// <param name="predicate">The predicate which determines if the added pipeline component should be executed.</param>
        /// <param name="configuration">The configuration of the branch pipeline builder.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> UseBranchIf(Func<T0, T1, bool> predicate, Action<IPipelineBuilder<T0, T1, TResult>> configuration);

        /// <summary>
        /// Adds the pipeline component to the pipeline.
        /// If condition is met the configuration is executed and it is NOT rejoined to the main pipeline branch, so the next component of the main branch is NOT executed after this configuration.
        /// If condition is NOT met the configuration is just skipped and next step of the main branch is executed.
        /// </summary>
        /// <param name="predicate">The predicate which determines if the added pipeline component should be executed.</param>
        /// <param name="configuration">The configuration of the branch pipeline builder.</param>
        /// <param name="factory">The factory which provides the branch builder instance.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> UseBranchIf
        (
            Func<T0, T1, bool> predicate,
            Action<IPipelineBuilder<T0, T1, TResult>> configuration,
            Func<IPipelineBuilder<T0, T1, TResult>> factory
        );

        /// <summary>
        /// Adds the pipeline component to the pipeline.
        /// If condition is met the configuration is executed and it is NOT rejoined to the main pipeline branch, so the next component of the main branch is NOT executed after this configuration.
        /// If condition is NOT met the configuration is just skipped and next step of the main branch is executed.
        /// Requires the service provider to be set.
        /// </summary>
        /// <param name="predicate">The predicate which determines if the added pipeline component should be executed.</param>
        /// <param name="configuration">The configuration of the branch pipeline builder.</param>
        /// <param name="factory">The factory which provides the branch builder instance.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> UseBranchIf
        (
            Func<T0, T1, bool> predicate,
            Action<IPipelineBuilder<T0, T1, TResult>> configuration,
            Func<IServiceProvider, IPipelineBuilder<T0, T1, TResult>> factory
        );

        #endregion UseBranchIf

        #region UseTarget

        /// <summary>
        /// Sets the pipeline target.
        /// The target is the last (terminating) step of the pipeline.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>The current instance of the pipeline builder.</returns>
        public IPipelineBuilder<T0, T1, TResult> UseTarget(Func<T0, T1, TResult> target);

        #endregion UseTarget

        #region Build

        /// <summary>
        /// Builds the pipeline.
        /// </summary>
        /// <returns>The pipeline delegate which is the start of the pipeline.</returns>
        public Func<T0, T1, TResult> Build();

        #endregion Build

        #endregion Methods
    }
}
