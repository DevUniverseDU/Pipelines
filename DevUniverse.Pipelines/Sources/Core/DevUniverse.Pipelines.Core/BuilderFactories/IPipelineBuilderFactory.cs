using System;
using System.Collections.Generic;

using DevUniverse.Pipelines.Core.Builders;

namespace DevUniverse.Pipelines.Core.BuilderFactories
{
    /// <summary>
    /// The pipeline builder factory.
    /// Creates the new instances of the pipeline builders.
    /// </summary>
    public interface IPipelineBuilderFactory
    {
        /// <summary>
        /// Creates the pipeline builder without input parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilder{TResult}"/>.</returns>
        public IPipelineBuilder<TResult> Create<TResult>(params object[] constructorArgs);

        /// <summary>
        /// Creates the pipeline builder without input parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilder{TResult}"/>.</returns>
        public IPipelineBuilder<TResult> Create<TResult>(IEnumerable<object> constructorArgs = null);


        /// <summary>
        /// Creates the pipeline builder with 1 input parameter which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="T0">The type of the parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilder{T0, TResult}"/>.</returns>
        public IPipelineBuilder<T0, TResult> Create<T0, TResult>(params object[] constructorArgs);

        /// <summary>
        /// Creates the pipeline builder with 1 input parameter which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="T0">The type of the parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilder{T0, TResult}"/>.</returns>
        public IPipelineBuilder<T0, TResult> Create<T0, TResult>(IEnumerable<object> constructorArgs = null);


        /// <summary>
        /// Creates the pipeline builder with 2 input parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="T0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="T1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilder{T0, T1, TResult}"/>.</returns>
        public IPipelineBuilder<T0, T1, TResult> Create<T0, T1, TResult>(params object[] constructorArgs);

        /// <summary>
        /// Creates the pipeline builder with 2 input parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="T0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="T1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilder{T0, T1, TResult}"/>.</returns>
        public IPipelineBuilder<T0, T1, TResult> Create<T0, T1, TResult>(IEnumerable<object> constructorArgs = null);


        /// <summary>
        /// Creates the pipeline builder with 3 input parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="T0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="T1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="T2">The type of the 3rd parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilder{T0, T1, T2, TResult}"/>.</returns>
        public IPipelineBuilder<T0, T1, T2, TResult> Create<T0, T1, T2, TResult>(params object[] constructorArgs);

        /// <summary>
        /// Creates the pipeline builder with 3 input parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="T0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="T1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="T2">The type of the 3rd parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilder{T0, T1, T2, TResult}"/>.</returns>
        public IPipelineBuilder<T0, T1, T2, TResult> Create<T0, T1, T2, TResult>(IEnumerable<object> constructorArgs = null);


        /// <summary>
        /// Creates the pipeline builder with 4 input parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="T0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="T1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="T2">The type of the 3rd parameter.</typeparam>
        /// <typeparam name="T3">The type of the 4th parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilder{T0, T1, T2, T3, TResult}"/>.</returns>
        public IPipelineBuilder<T0, T1, T2, T3, TResult> Create<T0, T1, T2, T3, TResult>(params object[] constructorArgs);

        /// <summary>
        /// Creates the pipeline builder with 4 input parameters which returns the result.
        /// </summary>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <typeparam name="T0">The type of the 1st parameter.</typeparam>
        /// <typeparam name="T1">The type of the 2nd parameter.</typeparam>
        /// <typeparam name="T2">The type of the 3rd parameter.</typeparam>
        /// <typeparam name="T3">The type of the 4th parameter.</typeparam>
        /// <typeparam name="TResult">The return type of the pipeline builder.</typeparam>
        /// <returns>The new instance of the <see cref="IPipelineBuilder{T0, T1, T2, T3, TResult}"/>.</returns>
        public IPipelineBuilder<T0, T1, T2, T3, TResult> Create<T0, T1, T2, T3, TResult>(IEnumerable<object> constructorArgs = null);


        /// <summary>
        /// Creates the pipeline builder of the specified type using the type variable.
        /// </summary>
        /// <param name="type">The type of the pipeline builder.</param>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <returns>The new instance of the pipeline builder.</returns>
        public object Create(Type type, params object[] constructorArgs);

        /// <summary>
        /// Creates the pipeline builder of the specified type using the type variable.
        /// </summary>
        /// <param name="type">The type of the pipeline builder.</param>
        /// <param name="constructorArgs">The constructor arguments.</param>
        /// <returns>The new instance of the pipeline builder.</returns>
        public object Create(Type type, IEnumerable<object> constructorArgs = null);
    }
}
