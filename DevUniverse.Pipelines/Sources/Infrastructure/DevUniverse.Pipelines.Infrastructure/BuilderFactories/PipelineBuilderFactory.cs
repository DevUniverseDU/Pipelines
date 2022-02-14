using System.Collections.Generic;
using System.Linq;

using DevUniverse.Pipelines.Core.BuilderFactories;
using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Infrastructure.BuilderFactories.Shared;
using DevUniverse.Pipelines.Infrastructure.Builders;

namespace DevUniverse.Pipelines.Infrastructure.BuilderFactories
{
    /// <inheritdoc cref="IPipelineBuilderFactory"/>
    public class PipelineBuilderFactory : PipelineBuilderFactoryBasic, IPipelineBuilderFactory
    {
        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> Create<TResult>(params object?[]? constructorArgs) =>
            this.Create<TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilder<TResult> Create<TResult>(IEnumerable<object?>? constructorArgs = null) =>
            this.CreateGeneric<PipelineBuilder<TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilder<TParam0, TResult> Create<TParam0, TResult>(params object?[]? constructorArgs) =>
            this.Create<TParam0, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilder<TParam0, TResult> Create<TParam0, TResult>(IEnumerable<object?>? constructorArgs = null) =>
            this.CreateGeneric<PipelineBuilder<TParam0, TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilder<TParam0, TParam1, TResult> Create<TParam0, TParam1, TResult>(params object?[]? constructorArgs) =>
            this.Create<TParam0, TParam1, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilder<TParam0, TParam1, TResult> Create<TParam0, TParam1, TResult>(IEnumerable<object?>? constructorArgs = null) =>
            this.CreateGeneric<PipelineBuilder<TParam0, TParam1, TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilder<TParam0, TParam1, TParam2, TResult> Create<TParam0, TParam1, TParam2, TResult>(params object?[]? constructorArgs) =>
            this.Create<TParam0, TParam1, TParam2, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilder<TParam0, TParam1, TParam2, TResult> Create<TParam0, TParam1, TParam2, TResult>(IEnumerable<object?>? constructorArgs = null) =>
            this.CreateGeneric<PipelineBuilder<TParam0, TParam1, TParam2, TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilder<TParam0, TParam1, TParam2, TParam3, TResult> Create<TParam0, TParam1, TParam2, TParam3, TResult>(params object?[]? constructorArgs) =>
            this.Create<TParam0, TParam1, TParam2, TParam3, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilder<TParam0, TParam1, TParam2, TParam3, TResult> Create<TParam0, TParam1, TParam2, TParam3, TResult>
            (IEnumerable<object?>? constructorArgs = null) =>
            this.CreateGeneric<PipelineBuilder<TParam0, TParam1, TParam2, TParam3, TResult>>(constructorArgs);
    }
}
