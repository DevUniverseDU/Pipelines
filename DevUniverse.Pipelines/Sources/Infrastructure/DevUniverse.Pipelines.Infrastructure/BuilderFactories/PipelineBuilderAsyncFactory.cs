using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.BuilderFactories;
using DevUniverse.Pipelines.Core.Builders;
using DevUniverse.Pipelines.Infrastructure.Builders;
using DevUniverse.Pipelines.Infrastructure.Shared.BuilderFactories;

namespace DevUniverse.Pipelines.Infrastructure.BuilderFactories
{
    /// <inheritdoc cref="IPipelineBuilderAsyncFactory"/>
    public class PipelineBuilderAsyncFactory : PipelineBuilderFactoryBasic, IPipelineBuilderAsyncFactory
    {
        /// <inheritdoc />
        public virtual IPipelineBuilderAsync<TResult> Create<TResult>(params object?[]? constructorArgs) where TResult : Task =>
            this.Create<TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilderAsync<TResult> Create<TResult>(IEnumerable<object?>? constructorArgs = null) where TResult : Task =>
            this.CreateGeneric<PipelineBuilderAsync<TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilderAsync<TParam0, TResult> Create<TParam0, TResult>(params object?[]? constructorArgs) where TResult : Task =>
            this.Create<TParam0, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilderAsync<TParam0, TResult> Create<TParam0, TResult>(IEnumerable<object?>? constructorArgs = null) where TResult : Task =>
            this.CreateGeneric<PipelineBuilderAsync<TParam0, TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilderAsync<TParam0, TParam1, TResult> Create<TParam0, TParam1, TResult>(params object?[]? constructorArgs) where TResult : Task =>
            this.Create<TParam0, TParam1, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilderAsync<TParam0, TParam1, TResult> Create<TParam0, TParam1, TResult>(IEnumerable<object?>? constructorArgs = null) where TResult : Task =>
            this.CreateGeneric<PipelineBuilderAsync<TParam0, TParam1, TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilderAsync<TParam0, TParam1, TParam2, TResult> Create<TParam0, TParam1, TParam2, TResult>(params object?[]? constructorArgs)
            where TResult : Task =>
            this.Create<TParam0, TParam1, TParam2, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilderAsync<TParam0, TParam1, TParam2, TResult> Create<TParam0, TParam1, TParam2, TResult>(IEnumerable<object?>? constructorArgs = null)
            where TResult : Task =>
            this.CreateGeneric<PipelineBuilderAsync<TParam0, TParam1, TParam2, TResult>>(constructorArgs);


        /// <inheritdoc />
        public virtual IPipelineBuilderAsync<TParam0, TParam1, TParam2, TParam3, TResult> Create<TParam0, TParam1, TParam2, TParam3, TResult>
            (params object?[]? constructorArgs) where TResult : Task =>
            this.Create<TParam0, TParam1, TParam2, TParam3, TResult>(constructorArgs?.ToList());

        /// <inheritdoc />
        public virtual IPipelineBuilderAsync<TParam0, TParam1, TParam2, TParam3, TResult> Create<TParam0, TParam1, TParam2, TParam3, TResult>
            (IEnumerable<object?>? constructorArgs = null) where TResult : Task =>
            this.CreateGeneric<PipelineBuilderAsync<TParam0, TParam1, TParam2, TParam3, TResult>>(constructorArgs);
    }
}
