using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.BuilderFactories;
using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Core.Conditions;
using DevUniverse.Pipelines.Core.Shared.Builders;
using DevUniverse.Pipelines.Core.Shared.Steps;
using DevUniverse.Pipelines.Infrastructure.Shared;
using DevUniverse.Pipelines.Infrastructure.Shared.Builders;

using Microsoft.Extensions.DependencyInjection;

namespace DevUniverse.Pipelines.Infrastructure.Builders
{
    /// <inheritdoc cref="IPipelineBuilderFull{TDelegate, TPipelineStep, TPredicate, TPipelineConditionAsync, TResult}"/>/>
    public abstract class PipelineBuilderAsyncBasic
    <
        TDelegate,
        TPipelineStep,
        TPredicate,
        TPipelineConditionAsync,
        TResult
    > :
        PipelineBuilderBasic<
            TDelegate,
            TPipelineStep,
            TPredicate,
            TPipelineConditionAsync,
            TResult
        >,
        IPipelineBuilderAsync
        where TDelegate : Delegate
        where TPipelineStep : IPipelineStepBasic
        where TPredicate : Delegate
        where TPipelineConditionAsync : IPipelineConditionAsyncBasic
        where TResult : IPipelineBuilderFull
        <
            TDelegate,
            TPipelineStep,
            TPredicate,
            TPipelineConditionAsync,
            TResult
        >
    {
        #region Properties

        #region Async

        protected MethodInfo MethodInfoContinueWith { get; set; }
        protected MethodInfo MethodInfoUnwrap { get; set; }
        protected MethodInfo MethodInfoTaskSchedulerFromCurrentSynchronizationContext { get; set; }
        protected MethodInfo MethodInfoTaskSchedulerCurrentPropertyGetter { get; set; }
        protected MethodInfo MethodInfoSynchronizationContextCurrentPropertyGetter { get; set; }

        #endregion Async

        #region Condition

        protected sealed override string ConditionInterfaceMethodName => nameof(IPipelineConditionAsync.InvokeAsync);
        protected sealed override Type ConditionReturnType { get; } = typeof(Task<bool>);

        protected MethodInfo MethodInfoConditionResultPropertyGetter { get; set; }

        #endregion Condition

        #endregion Properties

        #region Constructors

#pragma warning disable 8618

        // properties are always initialized by method call in constructor
        protected PipelineBuilderAsyncBasic(IServiceProvider? serviceProvider = null) : base(serviceProvider) => this.InitializeProperties();

#pragma warning restore 8618

        #endregion Constructors

        #region Methods

        #region InitializeProperties

        protected void InitializeProperties()
        {
            this.InitializePropertiesForDelegate();
            this.InitializePropertiesForCondition();
            this.InitializePropertiesForSynchronizationContext();
        }

        #region Delegate

        protected void InitializePropertiesForDelegate()
        {
            this.MethodInfoContinueWith = this.ConditionReturnType
                .GetMethods()
                .Where(mi => mi.Name == nameof(Task.ContinueWith))
                .Where(mi => mi.IsGenericMethod)
                .Where(mi => mi.GetGenericMethodDefinition().GetGenericArguments().Length == 1)
                .First
                (
                    mi =>
                    {
                        var parameters = mi.GetParameters();

                        return parameters.Length == 2 && parameters.Last().ParameterType == typeof(TaskScheduler);
                    }
                )
                .MakeGenericMethod(this.DelegateMethodInfo.ReturnType);

            var taskResultPropertyInfo = this.DelegateMethodInfo.ReturnType.GetProperty(nameof(Task<object>.Result));
            var delegateAsyncResultType = taskResultPropertyInfo == null ? typeof(void) : taskResultPropertyInfo.PropertyType;
            var isVoidReturnType = taskResultPropertyInfo == null;

            this.MethodInfoUnwrap = typeof(TaskExtensions)
                .GetMethods()
                .Where(mi => mi.Name == nameof(TaskExtensions.Unwrap))
                .Where(mi => mi.IsGenericMethod || isVoidReturnType)
                .First(mi => isVoidReturnType ? !mi.IsGenericMethod : mi.GetGenericMethodDefinition().GetGenericArguments().Length == 1);

            if (!isVoidReturnType)
            {
                this.MethodInfoUnwrap = this.MethodInfoUnwrap.MakeGenericMethod(delegateAsyncResultType);
            }
        }

        #endregion Delegate

        #region Condition

        protected void InitializePropertiesForCondition() =>
            this.MethodInfoConditionResultPropertyGetter = this.ConditionReturnType.GetProperty(nameof(Task<object>.Result))!.GetGetMethod()!;

        #endregion Condition

        #region Synchronization context

        protected void InitializePropertiesForSynchronizationContext()
        {
            this.MethodInfoSynchronizationContextCurrentPropertyGetter = typeof(SynchronizationContext).GetProperty(nameof(SynchronizationContext.Current))!.GetGetMethod()!;
            this.MethodInfoTaskSchedulerCurrentPropertyGetter = typeof(TaskScheduler).GetProperty(nameof(TaskScheduler.Current))!.GetGetMethod()!;

            this.MethodInfoTaskSchedulerFromCurrentSynchronizationContext = typeof(TaskScheduler)
                .GetMethods()
                .Where(mi => mi.Name == nameof(TaskScheduler.FromCurrentSynchronizationContext))
                .First(mi => mi.GetParameters().Length == 0);
        }

        #endregion Synchronization context

        #endregion InitializeProperties

        #region CreateDelegateForCondition

        protected override TDelegate CreateDelegateForCondition(Delegate predicate, TDelegate ifTrue, TDelegate ifFalse)
        {
            var parameterTypes = this.DelegateMethodInfo.GetParameters().Select(item => item.ParameterType).ToList();

            var expressionParameters = parameterTypes
                .Select((item, index) => Expression.Parameter(item, $"{Constants.ParamPrefix}{index}"))
                .ToList();

            var expressionInvokePredicate = Expression.Invoke(Expression.Constant(predicate), expressionParameters);
            var expressionInvokeIfTrue = Expression.Invoke(Expression.Constant(ifTrue), expressionParameters);
            var expressionInvokeIfFalse = Expression.Invoke(Expression.Constant(ifFalse), expressionParameters);

            #region Synchronization context

            var expressionCallSynchronizationContextCurrent = Expression.Call(null, this.MethodInfoSynchronizationContextCurrentPropertyGetter);
            var expressionCallTaskSchedulerCurrent = Expression.Call(null, this.MethodInfoTaskSchedulerCurrentPropertyGetter);
            var expressionCallTaskSchedulerFromSynchronizationContext = Expression.Call(null, this.MethodInfoTaskSchedulerFromCurrentSynchronizationContext);

            var expressionEqualSynchronizationContextNullCheck = Expression.Equal(expressionCallSynchronizationContextCurrent, Expression.Constant(null));

            var expressionConditionSynchronizationContext = Expression.Condition
            (
                expressionEqualSynchronizationContextNullCheck,
                expressionCallTaskSchedulerCurrent,
                expressionCallTaskSchedulerFromSynchronizationContext
            );

            #endregion Synchronization context

            #region Continuation delegate

            var expressionContinuationParameter = Expression.Parameter(this.ConditionReturnType, "predicateTask");
            var expressionCallPredicateResult = Expression.Call(expressionContinuationParameter, this.MethodInfoConditionResultPropertyGetter);
            var expressionConditionCondition = Expression.Condition(expressionCallPredicateResult, expressionInvokeIfTrue, expressionInvokeIfFalse);
            var expressionLambdaContinuationDelegate = Expression.Lambda(expressionConditionCondition, expressionContinuationParameter);

            #endregion Continuation delegate

            #region Result delegate

            var continuationParameters = new List<Expression>()
            {
                expressionLambdaContinuationDelegate,
                expressionConditionSynchronizationContext
            };

            var expressionCallContinueWith = Expression.Call(expressionInvokePredicate, this.MethodInfoContinueWith, continuationParameters);
            var expressionCallUnwrap = Expression.Call(null, this.MethodInfoUnwrap, expressionCallContinueWith);
            var expressionLambdaResultDelegate = Expression.Lambda(expressionCallUnwrap, expressionParameters);
            var resultDelegate = (TDelegate)expressionLambdaResultDelegate.Compile();

            #endregion Result delegate

            return resultDelegate;
        }

        #endregion CreateDelegateForCondition

        #region GetBranchBuilder

        protected override TResult GetBranchBuilderUsingPipelineBuilderFactory()
        {
            var factory = this.ServiceProvider!.GetRequiredService<IPipelineBuilderAsyncFactory>();

            var result = (TResult)factory.Create(this.GetType(), new List<object?>() { this.ServiceProvider! });

            return result;
        }

        #endregion GetBranchBuilder

        #endregion Methods
    }
}
