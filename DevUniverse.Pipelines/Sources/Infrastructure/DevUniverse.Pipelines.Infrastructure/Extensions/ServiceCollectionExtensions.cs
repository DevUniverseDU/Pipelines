using System;

using DevUniverse.Pipelines.Core.BuilderFactories;
using DevUniverse.Pipelines.Infrastructure.BuilderFactories;

using Microsoft.Extensions.DependencyInjection;

namespace DevUniverse.Pipelines.Infrastructure.Extensions
{
    /// <summary>
    /// The service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds service dependencies needed for the pipelines.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The current instance of the <see cref="IServiceCollection"/>.</returns>
        /// <exception cref="ArgumentNullException">The exception when the some of the arguments are null.</exception>
        public static IServiceCollection AddPipelines(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            serviceCollection.AddSingleton<IPipelineBuilderFactory, PipelineBuilderFactory>();

            return serviceCollection;
        }
    }
}
