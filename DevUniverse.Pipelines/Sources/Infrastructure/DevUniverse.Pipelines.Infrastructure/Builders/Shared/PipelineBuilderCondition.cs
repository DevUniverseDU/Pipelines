using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using DevUniverse.Pipelines.Core.BuilderFactories;
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
        protected abstract string ConditionInterfaceMethodName { get; }
        protected abstract Type ConditionReturnType { get; }

        #region UseIf

        /// <inheritdoc />
        public virtual TResult UseIf
        (
            TPredicate predicate,
            Action<TResult> branchBuilderConfiguration,
            Func<TResult> branchBuilderFactory
        ) => this.UseConditionallyFromDelegate(predicate, branchBuilderConfiguration, branchBuilderFactory, true);

        /// <inheritdoc />
        public virtual TResult UseIf
        (
            Func<TPipelineCondition> conditionFactory,
            Action<TResult> branchBuilderConfiguration,
            Func<TResult> branchBuilderFactory
        ) => this.UseConditionallyFromInterface(conditionFactory, branchBuilderConfiguration, branchBuilderFactory, true);

        /// <inheritdoc />
        public virtual TResult UseIf
        (
            TPredicate predicate,
            Action<TResult> branchBuilderConfiguration,
            Func<IServiceProvider, TResult>? branchBuilderFactory = null
        ) => this.UseConditionallyFromDelegate(predicate, branchBuilderConfiguration, branchBuilderFactory, true);

        /// <inheritdoc />
        public virtual TResult UseIf<TCustomCondition>
        (
            Func<IServiceProvider, TCustomCondition>? conditionFactory,
            Action<TResult> branchBuilderConfiguration,
            Func<IServiceProvider, TResult>? branchBuilderFactory = null
        ) where TCustomCondition : TPipelineCondition =>
            this.UseConditionallyFromInterface(conditionFactory, branchBuilderConfiguration, branchBuilderFactory, true);

        #endregion UseIf

        #region UseBranchIf

        /// <inheritdoc />
        public virtual TResult UseBranchIf
        (
            TPredicate predicate,
            Action<TResult> branchBuilderConfiguration,
            Func<TResult> branchBuilderFactory
        ) => this.UseConditionallyFromDelegate(predicate, branchBuilderConfiguration, branchBuilderFactory, false);

        /// <inheritdoc />
        public virtual TResult UseBranchIf
        (
            Func<TPipelineCondition> conditionFactory,
            Action<TResult> branchBuilderConfiguration,
            Func<TResult> branchBuilderFactory
        ) => this.UseConditionallyFromInterface(conditionFactory, branchBuilderConfiguration, branchBuilderFactory, false);

        /// <inheritdoc />
        public virtual TResult UseBranchIf
        (
            TPredicate predicate,
            Action<TResult> branchBuilderConfiguration,
            Func<IServiceProvider, TResult>? branchBuilderFactory = null
        ) => this.UseConditionallyFromDelegate(predicate, branchBuilderConfiguration, branchBuilderFactory, false);

        /// <inheritdoc />
        public virtual TResult UseBranchIf<TCustomCondition>
        (
            Func<IServiceProvider, TCustomCondition>? conditionFactory,
            Action<TResult> branchBuilderConfiguration,
            Func<IServiceProvider, TResult>? branchBuilderFactory = null
        ) where TCustomCondition : TPipelineCondition =>
            this.UseConditionallyFromInterface(conditionFactory, branchBuilderConfiguration, branchBuilderFactory, false);

        #endregion UseBranchIf

        #region CreatePredicateFromInterface

        protected virtual Delegate CreatePredicateFromInterface(TPipelineCondition pipelineCondition)
        {
            if ((object)pipelineCondition == null)
            {
                throw new ArgumentNullException(nameof(pipelineCondition));
            }

            var parameterTypes = this.DelegateMethodInfo.GetParameters().Select(item => item.ParameterType).ToList();

            var interfaceMethodInfo = pipelineCondition.GetType().ResolveMethod
            (
                this.ConditionInterfaceMethodName,
                this.ConditionReturnType,
                parameterTypes,
                false,
                BindingFlags.Instance | BindingFlags.Public
            );

            var expressionParameters = parameterTypes
                .Select((item, index) => Expression.Parameter(item, $"{Constants.ParamPrefix}{index}"))
                .ToList();

            #region Interface method Delegate

            var expressionCallInterfaceMethod = Expression.Call(Expression.Constant(pipelineCondition), interfaceMethodInfo, expressionParameters);
            var expressionLambdaInterfaceMethod = Expression.Lambda(expressionCallInterfaceMethod, expressionParameters);
            var interfaceMethodDelegate = expressionLambdaInterfaceMethod.Compile();

            #endregion Interface method Delegate

            #region Result Delegate

            var expressionInvokeInterfaceMethodDelegate = Expression.Invoke(Expression.Constant(interfaceMethodDelegate), expressionParameters);
            var expressionLambdaResultDelegate = Expression.Lambda(expressionInvokeInterfaceMethodDelegate, expressionParameters);
            var resultDelegate = expressionLambdaResultDelegate.Compile();

            #endregion Result Delegate

            return resultDelegate;
        }

        #endregion CreatePredicateFromInterface

        #region UseConditionally

        protected virtual TResult UseConditionallyFromInterface
        (
            Func<TPipelineCondition> pipelineConditionFactory,
            Action<TResult> branchBuilderConfiguration,
            Func<TResult> branchBuilderFactory,
            bool withRejoining
        )
        {
            var pipelineCondition = this.GetPipelineCondition(pipelineConditionFactory);
            var predicateFromPipelineCondition = this.CreatePredicateFromInterface(pipelineCondition);

            return this.UseConditionallyFromDelegate(predicateFromPipelineCondition, branchBuilderConfiguration, branchBuilderFactory, withRejoining);
        }

        protected virtual TResult UseConditionallyFromInterface<TCustomCondition>
        (
            Func<IServiceProvider, TCustomCondition>? pipelineConditionFactory,
            Action<TResult> branchBuilderConfiguration,
            Func<IServiceProvider, TResult>? branchBuilderFactory,
            bool withRejoining
        ) where TCustomCondition : TPipelineCondition
        {
            var pipelineCondition = this.GetPipelineCondition(pipelineConditionFactory);
            var predicateFromPipelineCondition = this.CreatePredicateFromInterface(pipelineCondition);

            return this.UseConditionallyFromDelegate(predicateFromPipelineCondition, branchBuilderConfiguration, branchBuilderFactory, withRejoining);
        }

        protected virtual TResult UseConditionallyFromDelegate
        (
            Delegate predicate,
            Action<TResult> branchBuilderConfiguration,
            Func<TResult> branchBuilderFactory,
            bool withRejoining
        )
        {
            var branchBuilder = this.GetBranchBuilder(branchBuilderFactory);

            return this.UseConditionallyFromDelegate(predicate, branchBuilderConfiguration, branchBuilder, withRejoining);
        }

        protected virtual TResult UseConditionallyFromDelegate
        (
            Delegate predicate,
            Action<TResult> branchBuilderConfiguration,
            Func<IServiceProvider, TResult>? branchBuilderFactory,
            bool withRejoining
        )
        {
            var branchBuilder = this.GetBranchBuilder(branchBuilderFactory);

            return this.UseConditionallyFromDelegate(predicate, branchBuilderConfiguration, branchBuilder, withRejoining);
        }

        protected virtual TResult UseConditionallyFromDelegate
        (
            Delegate predicate,
            Action<TResult> branchBuilderConfiguration,
            TResult branchBuilder,
            bool withRejoining
        )
        {
            ExceptionUtils.Process(predicate, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(predicate)));
            ExceptionUtils.Process(branchBuilderConfiguration, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(branchBuilderConfiguration)));
            ExceptionUtils.Process((object)branchBuilder, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(branchBuilder)));

            branchBuilderConfiguration.Invoke(branchBuilder);

            Func<TDelegate, TDelegate> component;

            if (withRejoining)
            {
                component = next =>
                {
                    var branch = branchBuilder.Build(next);

                    return this.CreateDelegateForCondition(predicate, branch, next);
                };
            }
            else
            {
                var branch = branchBuilder.Build();

                component = next => this.CreateDelegateForCondition(predicate, branch, next);
            }

            return this.Use(component);
        }

        #endregion UseConditionally

        #region CreateDelegateForCondition

        protected virtual TDelegate CreateDelegateForCondition(Delegate predicate, TDelegate ifTrue, TDelegate ifFalse)
        {
            var parameterTypes = this.DelegateMethodInfo.GetParameters().Select(item => item.ParameterType).ToList();

            var expressionParameters = parameterTypes
                .Select((item, index) => Expression.Parameter(item, $"{Constants.ParamPrefix}{index}"))
                .ToList();

            var expressionInvokePredicate = Expression.Invoke(Expression.Constant(predicate), expressionParameters);
            var expressionInvokeIfTrue = Expression.Invoke(Expression.Constant(ifTrue), expressionParameters);
            var expressionInvokeIfFalse = Expression.Invoke(Expression.Constant(ifFalse), expressionParameters);

            var expressionCondition = Expression.Condition(expressionInvokePredicate, expressionInvokeIfTrue, expressionInvokeIfFalse);
            var expressionLambdaResultDelegate = Expression.Lambda(expressionCondition, expressionParameters);

            var resultDelegate = (TDelegate)expressionLambdaResultDelegate.Compile();

            return resultDelegate;
        }

        #endregion CreateDelegateForCondition

        #region GetBranchBuilder

        protected virtual TResult GetBranchBuilder(Func<TResult> branchBuilderFactory)
        {
            ExceptionUtils.Process(branchBuilderFactory, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(branchBuilderFactory)));

            var branchBuilder = branchBuilderFactory.Invoke();

            return branchBuilder;
        }

        protected virtual TResult GetBranchBuilder(Func<IServiceProvider, TResult>? branchBuilderFactory = null)
        {
            ExceptionUtils.Process
            (
                this.ServiceProvider,
                ExceptionUtils.CheckIfNull,
                () => new InvalidOperationException(ErrorMessages.CreateNoServiceProviderErrorMessage(this.GetType()))
            );

            var branchBuilder = branchBuilderFactory == null ? this.GetBranchBuilderUsingPipelineBuilderFactory() : branchBuilderFactory.Invoke(this.ServiceProvider!);

            return branchBuilder;
        }

        protected virtual TResult GetBranchBuilderUsingPipelineBuilderFactory()
        {
            var factory = this.ServiceProvider!.GetRequiredService<IPipelineBuilderFactory>();

            var result = (TResult)factory.Create(this.GetType(), new List<object>() { this.ServiceProvider! });

            return result;
        }

        #endregion GetBranchBuilder

        #region GetPipelineCondition

        protected virtual TPipelineCondition GetPipelineCondition(Func<TPipelineCondition> pipelineConditionFactory)
        {
            ExceptionUtils.Process(pipelineConditionFactory, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(pipelineConditionFactory)));

            var pipelineCondition = pipelineConditionFactory.Invoke();

            return pipelineCondition;
        }

        protected virtual TPipelineCondition GetPipelineCondition<TCustomCondition>(Func<IServiceProvider, TCustomCondition>? pipelineConditionFactory)
            where TCustomCondition : TPipelineCondition
        {
            ExceptionUtils.Process
            (
                this.ServiceProvider,
                ExceptionUtils.CheckIfNull,
                () => new InvalidOperationException(ErrorMessages.CreateNoServiceProviderErrorMessage(this.GetType()))
            );

            var pipelineCondition = pipelineConditionFactory == null
                ? this.ServiceProvider!.GetRequiredService<TCustomCondition>()
                : pipelineConditionFactory.Invoke(this.ServiceProvider!);

            return pipelineCondition;
        }

        #endregion GetPipelineCondition
    }
}
