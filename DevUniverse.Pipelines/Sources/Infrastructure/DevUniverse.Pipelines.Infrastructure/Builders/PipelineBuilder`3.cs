using System;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Steps;
using DevUniverse.Pipelines.Infrastructure.Builders.Base;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilder{T0, T1, TResult}" />
    public class PipelineBuilder<T0, T1, TResult> : PipelineBuilderBase<Func<T0, T1, TResult>>, IPipelineBuilder<T0, T1, TResult>
    {
        #region Constructors

        public PipelineBuilder(IServiceProvider serviceProvider = null) : base(serviceProvider) { }

        #endregion Constructors

        #region Methods

        #region Use

        #region Use component

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> Use(Func<Func<T0, T1, TResult>, Func<T0, T1, TResult>> component) =>
            this.UseComponent<PipelineBuilder<T0, T1, TResult>>(component);

        #endregion Use component

        #region Use handler

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> Use(Func<T0, T1, Func<T0, T1, TResult>, TResult> handler) =>
            this.UseComponentFromHandler<PipelineBuilder<T0, T1, TResult>>(handler);

        #endregion Use handler

        #region Use step

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> Use<TPipelineStep>() where TPipelineStep : IPipelineStep<T0, T1, TResult> =>
            this.UseInterface<TPipelineStep, IPipelineBuilder<T0, T1, TResult>>();

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> Use(Func<IPipelineStep<T0, T1, TResult>> factory) =>
            this.UseInterface<IPipelineBuilder<T0, T1, TResult>>(factory);

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> Use(Func<IServiceProvider, IPipelineStep<T0, T1, TResult>> factory) =>
            this.UseInterface<IPipelineBuilder<T0, T1, TResult>>(factory);

        #endregion Use step

        #endregion Use

        #region UseIf

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> UseIf
        (
            Func<T0, T1, bool> predicate,
            Action<IPipelineBuilder<T0, T1, TResult>> configuration
        ) =>
            this.UseComponentIf<PipelineBuilder<T0, T1, TResult>>(predicate, configuration);

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> UseIf
        (
            Func<T0, T1, bool> predicate,
            Action<IPipelineBuilder<T0, T1, TResult>> configuration,
            Func<IPipelineBuilder<T0, T1, TResult>> factory
        ) =>
            this.UseComponentIf(predicate, configuration, factory);

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> UseIf
        (
            Func<T0, T1, bool> predicate,
            Action<IPipelineBuilder<T0, T1, TResult>> configuration,
            Func<IServiceProvider, IPipelineBuilder<T0, T1, TResult>> factory
        ) =>
            this.UseComponentIf(predicate, configuration, factory);

        #endregion UseIf

        #region UseBranchIf

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> UseBranchIf
        (
            Func<T0, T1, bool> predicate,
            Action<IPipelineBuilder<T0, T1, TResult>> configuration
        ) =>
            this.UseBranchComponentIf<PipelineBuilder<T0, T1, TResult>>(predicate, configuration);

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> UseBranchIf
        (
            Func<T0, T1, bool> predicate,
            Action<IPipelineBuilder<T0, T1, TResult>> configuration,
            Func<IPipelineBuilder<T0, T1, TResult>> factory
        ) =>
            this.UseBranchComponentIf(predicate, configuration, factory);

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> UseBranchIf
        (
            Func<T0, T1, bool> predicate,
            Action<IPipelineBuilder<T0, T1, TResult>> configuration,
            Func<IServiceProvider, IPipelineBuilder<T0, T1, TResult>> factory
        ) =>
            this.UseBranchComponentIf(predicate, configuration, factory);

        #endregion UseBranchIf

        #region UseTarget

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> UseTarget(Func<T0, T1, TResult> target) =>
            this.UseTargetItem<IPipelineBuilder<T0, T1, TResult>>(target);

        #endregion UseTarget

        #region Copy

        public IPipelineBuilder<T0, T1, TResult> Copy() => this.Copy<IPipelineBuilder<T0, T1, TResult>>();

        #endregion Copy

        #endregion Methods
    }
}
