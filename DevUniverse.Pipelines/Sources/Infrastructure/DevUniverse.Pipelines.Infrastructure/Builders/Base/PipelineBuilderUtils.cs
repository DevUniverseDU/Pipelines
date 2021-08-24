using System;
using System.Collections.Generic;
using System.Reflection;

using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Infrastructure.Extensions;

namespace DevUniverse.Pipelines.Infrastructure.Builders.Base
{
    /// <inheritdoc />
    public abstract partial class PipelineBuilderBase<TDelegate> where TDelegate : Delegate
    {
        protected virtual TResult UseTargetItem<TResult>(TResult pipelineBuilderInstance, TDelegate target)
        {
            const string methodName = nameof(IPipelineBuilder<object>.UseTarget);

            var pipelineBuilderType = pipelineBuilderInstance.GetType();
            var parameterTypes = new List<Type>() { target.GetType() };

            var methodInfo = pipelineBuilderType.ResolveMethod(methodName, pipelineBuilderType, parameterTypes, false, BindingFlags.Instance | BindingFlags.Public);

            methodInfo.Invoke(pipelineBuilderInstance, new object[] { target });

            return pipelineBuilderInstance;
        }

        protected virtual TDelegate BuildDelegate(object pipelineBuilderInstance)
        {
            const string methodName = nameof(IPipelineBuilder<object>.Build);

            var pipelineBuilderType = pipelineBuilderInstance.GetType();
            var delegateType = typeof(TDelegate);

            var methodInfo = pipelineBuilderType.ResolveMethod(methodName, delegateType, new List<Type>(), false, BindingFlags.Instance | BindingFlags.Public);

            var result = methodInfo.Invoke(pipelineBuilderInstance, null);

            return (TDelegate)result;
        }
    }
}

