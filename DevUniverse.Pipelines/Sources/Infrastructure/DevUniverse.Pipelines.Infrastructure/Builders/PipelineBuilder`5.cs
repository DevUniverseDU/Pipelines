using System;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Steps;
using DevUniverse.Pipelines.Infrastructure.Builders.Base;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilder{T0, T1, T2, T3, TResult}" />
    public class PipelineBuilder<T0, T1, T2, T3, TResult> : PipelineBuilderBase<Func<T0, T1, T2, T3, TResult>>, IPipelineBuilder<T0, T1, T2, T3, TResult>
    {
        #region Constructors

        public PipelineBuilder(IServiceProvider serviceProvider = null) : base(serviceProvider) { }

        #endregion Constructors

        #region Methods

        #region Use

        #region Use component

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> Use(Func<Func<T0, T1, T2, T3, TResult>, Func<T0, T1, T2, T3, TResult>> component) =>
            this.UseComponent<PipelineBuilder<T0, T1, T2, T3, TResult>>(component);

        #endregion Use component

        #region Use handler

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> Use(Func<T0, T1, T2, T3, Func<T0, T1, T2, T3, TResult>, TResult> handler) =>
            this.UseComponentFromHandler<PipelineBuilder<T0, T1, T2, T3, TResult>>(handler);

        #endregion Use handler

        #region Use step

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> Use<TPipelineStep>() where TPipelineStep : IPipelineStep<T0, T1, T2, T3, TResult> =>
            this.UseInterface<TPipelineStep, IPipelineBuilder<T0, T1, T2, T3, TResult>>();

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> Use(Func<IPipelineStep<T0, T1, T2, T3, TResult>> factory) =>
            this.UseInterface<IPipelineBuilder<T0, T1, T2, T3, TResult>>(factory);

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> Use(Func<IServiceProvider, IPipelineStep<T0, T1, T2, T3, TResult>> factory) =>
            this.UseInterface<IPipelineBuilder<T0, T1, T2, T3, TResult>>(factory);

        #endregion Use step

        #endregion Use

        #region UseIf

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> UseIf
        (
            Func<T0, T1, T2, T3, bool> predicate,
            Action<IPipelineBuilder<T0, T1, T2, T3, TResult>> configuration
        ) =>
            this.UseComponentIf<PipelineBuilder<T0, T1, T2, T3, TResult>>(predicate, configuration);

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> UseIf
        (
            Func<T0, T1, T2, T3, bool> predicate,
            Action<IPipelineBuilder<T0, T1, T2, T3, TResult>> configuration,
            Func<IPipelineBuilder<T0, T1, T2, T3, TResult>> factory
        ) =>
            this.UseComponentIf(predicate, configuration, factory);

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> UseIf
        (
            Func<T0, T1, T2, T3, bool> predicate,
            Action<IPipelineBuilder<T0, T1, T2, T3, TResult>> configuration,
            Func<IServiceProvider, IPipelineBuilder<T0, T1, T2, T3, TResult>> factory
        ) =>
            this.UseComponentIf(predicate, configuration, factory);

        #endregion UseIf

        #region UseBranchIf

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> UseBranchIf
        (
            Func<T0, T1, T2, T3, bool> predicate,
            Action<IPipelineBuilder<T0, T1, T2, T3, TResult>> configuration
        ) =>
            this.UseBranchComponentIf<PipelineBuilder<T0, T1, T2, T3, TResult>>(predicate, configuration);

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> UseBranchIf
        (
            Func<T0, T1, T2, T3, bool> predicate,
            Action<IPipelineBuilder<T0, T1, T2, T3, TResult>> configuration,
            Func<IPipelineBuilder<T0, T1, T2, T3, TResult>> factory
        ) =>
            this.UseBranchComponentIf(predicate, configuration, factory);

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> UseBranchIf
        (
            Func<T0, T1, T2, T3, bool> predicate,
            Action<IPipelineBuilder<T0, T1, T2, T3, TResult>> configuration,
            Func<IServiceProvider, IPipelineBuilder<T0, T1, T2, T3, TResult>> factory
        ) =>
            this.UseBranchComponentIf(predicate, configuration, factory);

        #endregion UseBranchIf

        #region UseTarget

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> UseTarget(Func<T0, T1, T2, T3, TResult> target) =>
            this.UseTargetItem<IPipelineBuilder<T0, T1, T2, T3, TResult>>(target);

        #endregion UseTarget

        #endregion Methods
    }
}
