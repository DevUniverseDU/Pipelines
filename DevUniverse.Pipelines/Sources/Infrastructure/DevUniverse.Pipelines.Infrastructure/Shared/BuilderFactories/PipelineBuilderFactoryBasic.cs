using System;
using System.Collections.Generic;
using System.Linq;

using DevUniverse.Pipelines.Core.Shared.BuilderFactories;
using DevUniverse.Pipelines.Core.Shared.Builders;
using DevUniverse.Pipelines.Infrastructure.Shared.Utils;

namespace DevUniverse.Pipelines.Infrastructure.Shared.BuilderFactories
{
    /// <inheritdoc />
    public class PipelineBuilderFactoryBasic : IPipelineBuilderFactoryBasic
    {
        /// <inheritdoc />
        public virtual object Create(Type type, params object?[]? constructorArgs) =>
            this.Create(type, constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual object Create(Type type, IEnumerable<object?>? constructorArgs = null)
        {
            ExceptionUtils.Process
            (
                type,
                param => !typeof(IPipelineBuilderBasic).IsAssignableFrom(param),
                () => new InvalidOperationException(ErrorMessages.CreateInvalidPipelineStepTypeErrorMessage(type, nameof(IPipelineBuilderBasic)))
            );

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

        protected virtual TResult CreateGeneric<TResult>(IEnumerable<object?>? constructorArgs = null) where TResult : IPipelineBuilderBasic =>
            (TResult)this.Create(typeof(TResult), constructorArgs);
    }
}
