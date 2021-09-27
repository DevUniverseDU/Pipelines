using DevUniverse.Pipelines.Core.Shared.Conditions;

namespace DevUniverse.Pipelines.Core.Conditions
{
    /// <summary>
    /// The pipeline condition with 1 parameter.
    /// </summary>
    /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
    public interface IPipelineCondition<in TParam0> : IPipelineConditionBasic
    {
        /// <summary>
        /// Executes the logic of the condition.
        /// </summary>
        /// <param name="param0">The 1st parameter.</param>
        /// <returns><see langword="true"/> if the condition is met, otherwise <see langword="false"/>.</returns>
        public bool Invoke(TParam0 param0);
    }
}
