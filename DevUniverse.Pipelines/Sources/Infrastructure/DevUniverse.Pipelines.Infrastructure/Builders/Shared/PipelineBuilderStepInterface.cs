using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using DevUniverse.Pipelines.Core.Steps;
using DevUniverse.Pipelines.Infrastructure.Shared;
using DevUniverse.Pipelines.Infrastructure.Shared.Extensions;
using DevUniverse.Pipelines.Infrastructure.Shared.Utils;

using Microsoft.Extensions.DependencyInjection;

namespace DevUniverse.Pipelines.Infrastructure.Builders.Shared
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
    {
        #region UseInterface

        /// <inheritdoc />
        public virtual TResult Use(Func<TPipelineStep> pipelineStepFactory)
        {
            ExceptionUtils.Process(pipelineStepFactory, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(pipelineStepFactory)));

            var instance = pipelineStepFactory.Invoke();

            return this.UseStep(instance);
        }

        /// <inheritdoc />
        public virtual TResult Use<TStep>(Func<IServiceProvider, TStep>? pipelineStepFactory = null) where TStep : TPipelineStep
        {
            ExceptionUtils.Process
            (
                this.ServiceProvider,
                ExceptionUtils.CheckIfNull,
                () => new InvalidOperationException(ErrorMessages.CreateNoServiceProviderErrorMessage(this.GetType()))
            );

            var pipelineStepInstance = pipelineStepFactory == null
                ? this.ServiceProvider!.GetRequiredService<TStep>()
                : pipelineStepFactory.Invoke(this.ServiceProvider!);

            return this.UseStep(pipelineStepInstance);
        }

        protected virtual TResult UseStep(TPipelineStep pipelineStepInstance)
        {
            ExceptionUtils.Process((object)pipelineStepInstance, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(pipelineStepInstance)));

            Func<TDelegate, TDelegate> component = next => this.CreateComponentFromStep(pipelineStepInstance, next);

            return this.Use(component);
        }

        #endregion UseInterface

        #region CreateComponentForInterface

        protected virtual TDelegate CreateComponentFromStep(TPipelineStep target, TDelegate next)
        {
            var parameterTypes = this.DelegateMethodInfo.GetParameters().Select(item => item.ParameterType).ToList();

            var interfaceMethodParameterTypes = new List<Type>(parameterTypes) { this.DelegateType };

            var interfaceMethodInfo = target.GetType().ResolveMethod
            (
                nameof(IPipelineStep<object>.Invoke),
                this.DelegateMethodInfo.ReturnType,
                interfaceMethodParameterTypes,
                false,
                BindingFlags.Instance | BindingFlags.Public
            );

            var expressionParameters = parameterTypes
                .Select((item, index) => Expression.Parameter(item, $"{Constants.ParamPrefix}{index}"))
                .ToList();

            #region Interface method Delegate

            var expressionParametersForInterfaceMethod = new List<ParameterExpression>(expressionParameters) { Expression.Parameter(this.DelegateType, nameof(next)) };

            var expressionCallInterfaceMethod = Expression.Call(Expression.Constant(target), interfaceMethodInfo, expressionParametersForInterfaceMethod);
            var expressionLambdaInterfaceMethod = Expression.Lambda(expressionCallInterfaceMethod, expressionParametersForInterfaceMethod);
            var interfaceMethodDelegate = expressionLambdaInterfaceMethod.Compile();

            #endregion Interface method Delegate

            #region Result Delegate

            var expressionParametersInterfaceMethod = new List<Expression>(expressionParameters) { Expression.Constant(next, this.DelegateType) };

            var expressionInvokeInterfaceMethodDelegate = Expression.Invoke(Expression.Constant(interfaceMethodDelegate), expressionParametersInterfaceMethod);
            var expressionLambdaResultDelegate = Expression.Lambda(expressionInvokeInterfaceMethodDelegate, expressionParameters);
            var resultDelegate = (TDelegate)expressionLambdaResultDelegate.Compile();

            #endregion Result Delegate

            return resultDelegate;
        }

        #endregion CreateComponentForInterface
    }
}
