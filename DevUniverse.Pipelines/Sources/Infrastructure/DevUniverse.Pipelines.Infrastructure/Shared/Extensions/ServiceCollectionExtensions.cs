using System;

using DevUniverse.Pipelines.Core.BuilderFactories;
using DevUniverse.Pipelines.Infrastructure.BuilderFactories;
using DevUniverse.Pipelines.Infrastructure.Shared.Utils;

using Microsoft.Extensions.DependencyInjection;

namespace DevUniverse.Pipelines.Infrastructure.Shared.Extensions
{
    /// <summary>
    /// The service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the dependencies needed for the pipelines.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The passed instance of the <see cref="IServiceCollection"/> with the added dependencies.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some arguments are <see langword="null"/>.</exception>
        public static IServiceCollection AddPipelines(this IServiceCollection serviceCollection)
        {
            ExceptionUtils.Process(serviceCollection, ExceptionUtils.CheckIfNull, () => new ArgumentNullException(nameof(serviceCollection)));

            serviceCollection.AddSingleton<IPipelineBuilderFactory, PipelineBuilderFactory>();
            serviceCollection.AddSingleton<IPipelineBuilderAsyncFactory, PipelineBuilderAsyncFactory>();

            return serviceCollection;
        }
    }
}
