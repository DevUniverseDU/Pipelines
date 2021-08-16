using System;
using System.Linq;
using System.Linq.Expressions;

namespace DevUniverse.Pipelines.Infrastructure.Builders.Base
{
    /// <inheritdoc />
    public abstract partial class PipelineBuilderBase<TDelegate> where TDelegate : Delegate
    {
        #region CreateDelegateForCondition

        protected virtual TDelegate CreateDelegateForCondition<TPredicate>(TPredicate predicate, TDelegate ifTrue, TDelegate ifFalse) where TPredicate : Delegate
        {
            var delegateType = typeof(TDelegate);
            var delegateTypeGenericArguments = delegateType.GetGenericArguments().ToList();

            var delegateParameterTypes = delegateTypeGenericArguments
                .Take(delegateTypeGenericArguments.Count - 1)
                .ToList();

            var expressionParameters = delegateParameterTypes
                .Select((item, index) => Expression.Parameter(item, $"arg{index}"))
                .ToList();

            var expressionPredicate = Expression.Invoke(Expression.Constant(predicate), expressionParameters);
            var expressionIfTrue = Expression.Invoke(Expression.Constant(ifTrue), expressionParameters);
            var expressionIfFalse = Expression.Invoke(Expression.Constant(ifFalse), expressionParameters);

            var expressionCondition = Expression.Condition(expressionPredicate, expressionIfTrue, expressionIfFalse);
            var expressionLambda = Expression.Lambda(expressionCondition, expressionParameters);

            var resultDelegate = expressionLambda.Compile();

            return (TDelegate) resultDelegate;
        }

        #endregion CreateDelegateForCondition
    }
}
