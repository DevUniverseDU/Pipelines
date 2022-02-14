using System;

namespace DevUniverse.Pipelines.Core.Builders.Shared
{
    /// <summary>
    /// The pipeline builder with service provider.
    /// </summary>
    /// <typeparam name="TDelegate">The delegate type.</typeparam>
    /// <typeparam name="TResult">The result pipeline builder type.</typeparam>
    public interface IPipelineBuilderWithServiceProvider<TDelegate, out TResult> : IPipelineBuilderCore<TDelegate, TResult>
        where TDelegate : Delegate
        where TResult : IPipelineBuilderWithServiceProvider<TDelegate, TResult>
    {
        /// <summary>
        /// The service provider.
        /// </summary>
        public IServiceProvider? ServiceProvider { get; }
    }
}
