using System;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Steps;
using DevUniverse.Pipelines.Infrastructure.Builders.Base;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilder{TResult}" />
    public class PipelineBuilder<TResult> : PipelineBuilderBase<Func<TResult>>, IPipelineBuilder<TResult>
    {
        #region Constructors

        public PipelineBuilder(IServiceProvider serviceProvider = null) : base(serviceProvider) { }

        #endregion Constructors

        #region Methods

        #region Use

        #region Use component

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> Use(Func<Func<TResult>, Func<TResult>> component) =>
            this.UseComponent<PipelineBuilder<TResult>>(component);

        #endregion Use component

        #region Use handler

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> Use(Func<Func<TResult>, TResult> handler) =>
            this.UseComponentFromHandler<PipelineBuilder<TResult>>(handler);

        #endregion Use handler

        #region Use step

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> Use<TPipelineStep>() where TPipelineStep : IPipelineStep<TResult> =>
            this.UseInterface<TPipelineStep, IPipelineBuilder<TResult>>();

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> Use(Func<IPipelineStep<TResult>> factory) =>
            this.UseInterface<IPipelineBuilder<TResult>>(factory);

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> Use(Func<IServiceProvider, IPipelineStep<TResult>> factory) =>
            this.UseInterface<IPipelineBuilder<TResult>>(factory);

        #endregion Use step

        #endregion Use

        #region UseIf

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> UseIf
        (
            Func<bool> predicate,
            Action<IPipelineBuilder<TResult>> configuration
        ) =>
            this.UseComponentIf<PipelineBuilder<TResult>>(predicate, configuration);

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> UseIf
        (
            Func<bool> predicate,
            Action<IPipelineBuilder<TResult>> configuration,
            Func<IPipelineBuilder<TResult>> factory
        ) =>
            this.UseComponentIf(predicate, configuration, factory);

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> UseIf
        (
            Func<bool> predicate,
            Action<IPipelineBuilder<TResult>> configuration,
            Func<IServiceProvider, IPipelineBuilder<TResult>> factory
        ) =>
            this.UseComponentIf(predicate, configuration, factory);

        #endregion UseIf

        #region UseBranchIf

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> UseBranchIf
        (
            Func<bool> predicate,
            Action<IPipelineBuilder<TResult>> configuration
        ) =>
            this.UseBranchComponentIf<PipelineBuilder<TResult>>(predicate, configuration);

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> UseBranchIf
        (
            Func<bool> predicate,
            Action<IPipelineBuilder<TResult>> configuration,
            Func<IPipelineBuilder<TResult>> factory
        ) =>
            this.UseBranchComponentIf(predicate, configuration, factory);

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> UseBranchIf
        (
            Func<bool> predicate,
            Action<IPipelineBuilder<TResult>> configuration,
            Func<IServiceProvider, IPipelineBuilder<TResult>> factory
        ) =>
            this.UseBranchComponentIf(predicate, configuration, factory);

        #endregion UseBranchIf

        #region UseTarget

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> UseTarget(Func<TResult> target) =>
            this.UseTargetItem<IPipelineBuilder<TResult>>(target);

        #endregion UseTarget

        #region Copy

        public IPipelineBuilder<TResult> Copy() => this.Copy<IPipelineBuilder<TResult>>();

        #endregion Copy

        #endregion Methods
    }
}
