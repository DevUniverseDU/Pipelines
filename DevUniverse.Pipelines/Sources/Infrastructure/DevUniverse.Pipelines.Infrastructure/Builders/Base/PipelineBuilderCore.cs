using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using DevUniverse.Pipelines.Core.Builders;

namespace DevUniverse.Pipelines.Infrastructure.Builders.Base
{
    /// <inheritdoc />
    public abstract partial class PipelineBuilderBase<TDelegate> : IPipelineBuilder where TDelegate : Delegate
    {
        #region Properties

        protected List<Func<TDelegate, TDelegate>> Components { get; } = new List<Func<TDelegate, TDelegate>>();
        public TDelegate Target { get; protected set; }

        public IServiceProvider ServiceProvider { get; }

        #endregion Properties

        #region Constructors

        protected PipelineBuilderBase(IServiceProvider serviceProvider = null) => this.ServiceProvider = serviceProvider;

        #endregion Constructors

        #region Methods

        #region UseComponent

        protected virtual TResult UseComponent<TResult>(Func<TDelegate, TDelegate> component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            this.Components.Add(component);

            return (TResult) (object) this;
        }

        #endregion UseComponent

        #region UseComponentFromHandler

        protected virtual TResult UseComponentFromHandler<TResult>(Delegate handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var component = this.CreateComponentFromHandler(handler);

            return this.UseComponent<TResult>(component);
        }

        #endregion UseComponentFromHandler

        #region UseTargetItem

        protected virtual TResult UseTargetItem<TResult>(TDelegate target)
        {
            this.Target = target ?? throw new ArgumentNullException(nameof(target));

            return (TResult) (object) this;
        }

        #endregion UseTargetItem

        #region BuildDelegate

        public virtual TDelegate Build()
        {
            if (this.Target == null)
            {
                throw new InvalidOperationException(ErrorMessages.CreateNoTargetSetErrorMessage(this.GetType()));
            }

            var next = this.Target;

            for (var a = this.Components.Count - 1; a >= 0; a--)
            {
                next = this.Components[a].Invoke(next);
            }

            return next;
        }

        #endregion BuildDelegate

        #region CreateComponentFromHandler

        protected virtual Func<TDelegate, TDelegate> CreateComponentFromHandler<THandler>(THandler handler) where THandler : Delegate
        {
            Func<TDelegate, TDelegate> component = next =>
            {
                var delegateType = typeof(TDelegate);
                var delegateTypeGenericArguments = delegateType.GetGenericArguments().ToList();

                var delegateParameterTypes = delegateTypeGenericArguments
                    .Take(delegateTypeGenericArguments.Count - 1)
                    .ToList();

                var expressionParameters = delegateParameterTypes
                    .Select((item, index) => Expression.Parameter(item, $"arg{index}"))
                    .ToList();

                var resultDelegateParameters = new List<Expression>(expressionParameters)
                {
                    Expression.Constant(next)
                };

                var expressionHandlerInvoke = Expression.Invoke(Expression.Constant(handler), resultDelegateParameters);
                var resultDelegate = Expression.Lambda(delegateType, expressionHandlerInvoke, expressionParameters).Compile();

                return (TDelegate) resultDelegate;
            };

            return component;
        }

        #endregion CreateComponentFromHandler

        #endregion Methods
    }
}
