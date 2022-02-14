using System.Collections.Generic;
using System.Threading.Tasks;

using DevUniverse.Pipelines.Core.BuilderFactories.Shared;
using DevUniverse.Pipelines.Core.Builders;

namespace DevUniverse.Pipelines.Core.BuilderFactories
{
    /// <summary>
    /// The pipeline builder factory.
    /// Creates the new instances of the pipeline builders.
    /// </summary>
    public interface IPipelineBuilderAsyncFactory : IPipelineBuilderFactoryBasic
    {
        /// <summary>
        /// Creates the pipeline builder without the parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilderAsync{TResult}"/>.</returns>
        public IPipelineBuilderAsync<TResult> Create<TResult>(params object?[]? constructorArgs) where TResult : Task;

        /// <summary>
        /// Creates the pipeline builder without the parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilderAsync{TResult}"/>.</returns>
        public IPipelineBuilderAsync<TResult> Create<TResult>(IEnumerable<object?>? constructorArgs = null) where TResult : Task;


        /// <summary>
        /// Creates the pipeline builder with 1 parameter which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TParam0">The type of the parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilderAsync{TParam0, TResult}"/>.</returns>
        public IPipelineBuilderAsync<TParam0, TResult> Create<TParam0, TResult>(params object?[]? constructorArgs) where TResult : Task;

        /// <summary>
        /// Creates the pipeline builder with 1 parameter which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TParam0">The type of the parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilderAsync{TParam0, TResult}"/>.</returns>
        public IPipelineBuilderAsync<TParam0, TResult> Create<TParam0, TResult>(IEnumerable<object?>? constructorArgs = null) where TResult : Task;


        /// <summary>
        /// Creates the pipeline builder with 2 parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="TParam1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilderAsync{TParam0, TParam1, TResult}"/>.</returns>
        public IPipelineBuilderAsync<TParam0, TParam1, TResult> Create<TParam0, TParam1, TResult>(params object?[]? constructorArgs) where TResult : Task;

        /// <summary>
        /// Creates the pipeline builder with 2 parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="TParam1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilderAsync{TParam0, TParam1, TResult}"/>.</returns>
        public IPipelineBuilderAsync<TParam0, TParam1, TResult> Create<TParam0, TParam1, TResult>(IEnumerable<object?>? constructorArgs = null) where TResult : Task;


        /// <summary>
        /// Creates the pipeline builder with 3 parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="TParam1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="TParam2">The type of the 3rd parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilderAsync{TParam0, TParam1, TParam2, TResult}"/>.</returns>
        public IPipelineBuilderAsync<TParam0, TParam1, TParam2, TResult> Create<TParam0, TParam1, TParam2, TResult>(params object?[]? constructorArgs) where TResult : Task;

        /// <summary>
        /// Creates the pipeline builder with 3 parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="TParam1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="TParam2">The type of the 3rd parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilderAsync{TParam0, TParam1, TParam2, TResult}"/>.</returns>
        public IPipelineBuilderAsync<TParam0, TParam1, TParam2, TResult> Create<TParam0, TParam1, TParam2, TResult>(IEnumerable<object?>? constructorArgs = null)
            where TResult : Task;


        /// <summary>
        /// Creates the pipeline builder with 4 parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="TParam1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="TParam2">The type of the 3rd parameter.</typeparam>
        /// <typeparam name="TParam3">The type of the 4th parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilderAsync{TParam0, TParam1, TParam2, TParam3, TResult}"/>.</returns>
        public IPipelineBuilderAsync<TParam0, TParam1, TParam2, TParam3, TResult> Create<TParam0, TParam1, TParam2, TParam3, TResult>(params object?[]? constructorArgs)
            where TResult : Task;

        /// <summary>
        /// Creates the pipeline builder with 4 parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="TParam1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="TParam2">The type of the 3rd parameter.</typeparam>
        /// <typeparam name="TParam3">The type of the 4th parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilderAsync{TParam0, TParam1, TParam2, TParam3, TResult}"/>.</returns>
        public IPipelineBuilderAsync<TParam0, TParam1, TParam2, TParam3, TResult> Create<TParam0, TParam1, TParam2, TParam3, TResult>
            (IEnumerable<object?>? constructorArgs = null) where TResult : Task;
    }
}
