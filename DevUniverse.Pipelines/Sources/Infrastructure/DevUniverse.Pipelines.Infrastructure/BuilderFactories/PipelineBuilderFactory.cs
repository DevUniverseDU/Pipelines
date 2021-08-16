using System;
using System.Collections.Generic;
using System.Linq;

using DevUniverse.Pipelines.Core.BuilderFactories;
using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Infrastructure.Builders;
using DevUniverse.Pipelines.Infrastructure.Utils;

namespace DevUniverse.Pipelines.Infrastructure.BuilderFactories
{
    /// <inheritdoc />
    public class PipelineBuilderFactory : IPipelineBuilderFactory
    {
        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> Create<TResult>(params object[] constructorArgs) =>
            this.Create<TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> Create<TResult>(IEnumerable<object> constructorArgs = null) =>
            this.CreateGeneric<PipelineBuilder<TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, TResult> Create<T0, TResult>(params object[] constructorArgs) =>
            this.Create<T0, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, TResult> Create<T0, TResult>(IEnumerable<object> constructorArgs = null) =>
            this.CreateGeneric<PipelineBuilder<T0, TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> Create<T0, T1, TResult>(params object[] constructorArgs) =>
            this.Create<T0, T1, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, TResult> Create<T0, T1, TResult>(IEnumerable<object> constructorArgs = null) =>
            this.CreateGeneric<PipelineBuilder<T0, T1, TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, TResult> Create<T0, T1, T2, TResult>(params object[] constructorArgs) =>
            this.Create<T0, T1, T2, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, TResult> Create<T0, T1, T2, TResult>(IEnumerable<object> constructorArgs = null) =>
            this.CreateGeneric<PipelineBuilder<T0, T1, T2, TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> Create<T0, T1, T2, T3, TResult>(params object[] constructorArgs) =>
            this.Create<T0, T1, T2, T3, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilder<T0, T1, T2, T3, TResult> Create<T0, T1, T2, T3, TResult>(IEnumerable<object> constructorArgs = null) =>
            this.CreateGeneric<PipelineBuilder<T0, T1, T2, T3, TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual object Create(Type type, params object[] constructorArgs) =>
            this.Create(type, constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual object Create(Type type, IEnumerable<object> constructorArgs = null)
        {
            if (!typeof(IPipelineBuilder).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(ErrorMessages.CreateInvalidPipelineStepTypeErrorMessage(type));
            }

            if (constructorArgs == null)
            {
                var constructors = type.GetConstructors().ToList();
                var constructorWithMinParams = constructors.FirstOrDefault(ci => ci.GetParameters().Length == constructors.Min(item => item.GetParameters().Length));

                if (constructorWithMinParams != null)
                {
                    var @params = constructorWithMinParams.GetParameters().ToList();

                    constructorArgs = @params.Select(item => item.ParameterType.IsValueType ? ActivatorUtils.Create(item.ParameterType) : null).ToList();
                }
            }

            return ActivatorUtils.Create(type, constructorArgs);
        }


        protected virtual TResult CreateGeneric<TResult>(IEnumerable<object> constructorArgs = null) where TResult : IPipelineBuilder =>
            (TResult) this.Create(typeof(TResult), constructorArgs);
    }
}
