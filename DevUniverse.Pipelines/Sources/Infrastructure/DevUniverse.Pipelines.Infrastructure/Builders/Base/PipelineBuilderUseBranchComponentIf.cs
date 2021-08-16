using System;
using System.Collections.Generic;

using DevUniverse.Pipelines.Core.BuilderFactories;

using Microsoft.Extensions.DependencyInjection;

namespace DevUniverse.Pipelines.Infrastructure.Builders.Base
{
    /// <inheritdoc />
    public abstract partial class PipelineBuilderBase<TDelegate> where TDelegate : Delegate
    {
        #region UseBranchComponentIf

        protected virtual TResult UseBranchComponentIf<TResult>(Delegate predicate, Action<TResult> configuration) =>
            this.UseBranchComponentIf
            (
                predicate,
                configuration,
                serviceProvider =>
                {
                    var factory = serviceProvider.GetRequiredService<IPipelineBuilderFactory>();

                    return (TResult)factory.Create(typeof(TResult), new List<object>() { serviceProvider });
                }
            );

        protected virtual TResult UseBranchComponentIf<TResult>(Delegate predicate, Action<TResult> configuration, Func<TResult> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            var branchBuilder = factory.Invoke();

            return this.UseBranchComponentIf(predicate, configuration, branchBuilder);
        }

        protected virtual TResult UseBranchComponentIf<TResult>(Delegate predicate, Action<TResult> configuration, Func<IServiceProvider, TResult> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (this.ServiceProvider == null)
            {
                throw new InvalidOperationException(ErrorMessages.CreateNoServiceProviderErrorMessage(this.GetType()));
            }

            var branchBuilder = factory.Invoke(this.ServiceProvider);

            return this.UseBranchComponentIf(predicate, configuration, branchBuilder);
        }

        protected virtual TResult UseBranchComponentIf<TResult>(Delegate predicate, Action<TResult> configuration, TResult branchBuilder)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            // ReSharper disable once CompareNonConstrainedGenericWithNull
            if (branchBuilder == null)
            {
                throw new ArgumentNullException(nameof(branchBuilder));
            }

            configuration(branchBuilder);
            var branch = this.BuildDelegate(branchBuilder);

            Func<TDelegate, TDelegate> component = next => this.CreateDelegateForCondition(predicate, branch, next);

            return this.UseComponent<TResult>(component);
        }

        #endregion UseBranchComponentIf
    }
}
