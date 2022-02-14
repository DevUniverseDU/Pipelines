using System;

namespace DevUniverse.Pipelines.Infrastructure.Builders.Shared
{
    /// <inheritdoc />
    public abstract partial class PipelineBuilderBasic
    <
        TDelegate,
        TPipelineStep,
        TPredicate,
        TPipelineCondition,
        TResult
    >
    {
        /// <inheritdoc />
        public IServiceProvider? ServiceProvider { get; }
    }
}
