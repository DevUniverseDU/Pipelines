using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using DevUniverse.Pipelines.Core.Steps;
using DevUniverse.Pipelines.Infrastructure.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace DevUniverse.Pipelines.Infrastructure.Builders.Base
{
    /// <inheritdoc />
    public abstract partial class PipelineBuilderBase<TDelegate> where TDelegate : Delegate
    {
        #region UseInterface

        protected virtual TResult UseInterface<TStep, TResult>()
        {
            var pipelineStepType = typeof(TStep);

            if (this.ServiceProvider == null)
            {
                throw new InvalidOperationException(ErrorMessages.CreateNoServiceProviderErrorMessage(this.GetType()));
            }

            var pipelineStepInstance = this.ServiceProvider.GetRequiredService(pipelineStepType);

            return this.UseInterface<TResult>(pipelineStepInstance);
        }

        protected virtual TResult UseInterface<TResult>(Func<object> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            var instance = factory.Invoke();

            return this.UseInterface<TResult>(instance);
        }

        protected virtual TResult UseInterface<TResult>(Func<IServiceProvider, object> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (this.ServiceProvider == null)
            {
                throw new InvalidOperationException(ErrorMessages.CreateNoServiceProviderErrorMessage(this.GetType()));
            }

            var instance = factory.Invoke(this.ServiceProvider);

            return this.UseInterface<TResult>(instance);
        }

        protected virtual TResult UseInterface<TResult>(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            Func<TDelegate, TDelegate> component = next => this.CreateComponentForInterface(instance, next);

            return this.UseComponent<TResult>(component);
        }

        #endregion UseInterface

        #region CreateComponentForInterface

        protected virtual TDelegate CreateComponentForInterface(object target, TDelegate next)
        {
            var delegateType = typeof(TDelegate);
            var delegateTypeGenericArguments = delegateType.GetGenericArguments().ToList();

            var delegateParameterTypes = delegateTypeGenericArguments
                .Take(delegateTypeGenericArguments.Count - 1).ToList();

            var delegateReturnType = delegateTypeGenericArguments.Last();

            var interfaceMethodParameterTypes = new List<Type>(delegateParameterTypes) { delegateType };

            var methodInfo = target.GetType().ResolveMethod
            (
                nameof(IPipelineStep<object>.Invoke),
                delegateReturnType,
                interfaceMethodParameterTypes,
                false,
                BindingFlags.Instance | BindingFlags.Public
            );

            var expressionParameters = delegateParameterTypes
                .Select((item, index) => Expression.Parameter(item, $"arg{index}"))
                .ToList();

            #region Interface method Delegate

            var expressionParametersInterfaceMethod = new List<ParameterExpression>(expressionParameters) { Expression.Parameter(delegateType, nameof(next)) };

            var expressionCall = Expression.Call(Expression.Constant(target), methodInfo, expressionParametersInterfaceMethod);
            var expressionLambdaInterfaceMethod = Expression.Lambda(expressionCall, expressionParametersInterfaceMethod);
            var interfaceMethodDelegate = expressionLambdaInterfaceMethod.Compile();

            #endregion Interface method Delegate

            #region Result Delegate

            var expressionArgumentsInterfaceMethod = new List<Expression>(expressionParameters) { Expression.Constant(next) };

            var expressionInvokeInterfaceMethodDelegate = Expression.Invoke(Expression.Constant(interfaceMethodDelegate), expressionArgumentsInterfaceMethod);
            var expressionLambdaResultDelegate = Expression.Lambda(expressionInvokeInterfaceMethodDelegate, expressionParameters);
            var resultDelegate = expressionLambdaResultDelegate.Compile();

            #endregion Result Delegate

            return (TDelegate) resultDelegate;
        }

        #endregion CreateComponentForInterface
    }
}
