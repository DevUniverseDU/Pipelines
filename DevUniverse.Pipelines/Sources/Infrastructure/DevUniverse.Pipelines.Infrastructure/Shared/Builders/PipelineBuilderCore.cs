using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

using DevUniverse.Pipelines.Core.Shared.Builders;
using DevUniverse.Pipelines.Core.Shared.Conditions;
using DevUniverse.Pipelines.Core.Shared.Steps;

namespace DevUniverse.Pipelines.Infrastructure.Shared.Builders
{
    /// <inheritdoc />
    public abstract partial class PipelineBuilderBasic
        <
            TDelegate,
            TPipelineStep,
            TPredicate,
            TPipelineCondition,
            TResult
        >
        : IPipelineBuilderFull
        <
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
        #region Properties

        protected List<Func<TDelegate, TDelegate>> ComponentsList { get; set; } = new List<Func<TDelegate, TDelegate>>();

        protected Type DelegateType { get; }
        protected MethodInfo DelegateMethodInfo { get; }

        /// <inheritdoc />
        public IReadOnlyCollection<Func<TDelegate, TDelegate>> Components { get; protected set; }

        /// <inheritdoc />
        public TDelegate? Target { get; protected set; }

        #endregion Properties

        #region Constructors

        protected PipelineBuilderBasic(IServiceProvider? serviceProvider = null)
        {
            this.DelegateType = typeof(TDelegate);
            this.DelegateMethodInfo = this.DelegateType.GetMethod(Constants.DelegateMethodName)!;

            this.Components = new ReadOnlyCollection<Func<TDelegate, TDelegate>>(this.ComponentsList);
            this.ServiceProvider = serviceProvider;
        }

        #endregion Constructors

        #region Methods

        #region Use

        /// <inheritdoc />
        public virtual TResult Use(Func<TDelegate, TDelegate> component)
        {
            ExceptionUtils.Process(component, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(component)));

            this.ComponentsList.Add(component);

            return (TResult)(object)this;
        }

        #endregion Use

        #region UseTarget

        /// <inheritdoc />
        public virtual TResult UseTarget(TDelegate target)
        {
            ExceptionUtils.Process(target, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(target)));

            this.Target = target;

            return (TResult)(object)this;
        }

        #endregion UseTarget

        #region Copy

        /// <inheritdoc />
        public virtual TResult Copy()
        {
            var copiedInstance = (PipelineBuilderBasic<TDelegate, TPipelineStep, TPredicate, TPipelineCondition, TResult>)this.MemberwiseClone();
            copiedInstance.ComponentsList = this.Components.Select(item => (Func<TDelegate, TDelegate>)item.Clone()).ToList();
            copiedInstance.Components = new ReadOnlyCollection<Func<TDelegate, TDelegate>>(copiedInstance.ComponentsList);

            if (this.Target != null)
            {
                copiedInstance.Target = (TDelegate)this.Target.Clone();
            }

            return (TResult)(object)copiedInstance;
        }

        #endregion Copy

        #region Build

        /// <inheritdoc />
        public virtual TDelegate Build(TDelegate? target = null)
        {
            if (target != null)
            {
                this.UseTarget(target);
            }

            ExceptionUtils.Process
            (
                this.Target,
                ExceptionUtils.CheckIfNull,
                () => new InvalidOperationException(ErrorMessages.CreateNoTargetSetErrorMessage(this.GetType()))
            );

            var next = this.Target!;

            for (var a = this.Components.Count - 1; a >= 0; a--)
            {
                next = this.ComponentsList[a].Invoke(next);
            }

            return next;
        }

        #endregion Build

        #endregion Methods
    }
}
