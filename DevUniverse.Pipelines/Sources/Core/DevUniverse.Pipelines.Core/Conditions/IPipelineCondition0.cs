using DevUniverse.Pipelines.Core.Shared.Conditions;

namespace DevUniverse.Pipelines.Core.Conditions
{
    /// <summary>
    /// The pipeline condition with no parameters.
    /// </summary>
    public interface IPipelineCondition : IPipelineConditionBasic
    {
        /// <summary>
        /// Executes the logic of the condition.
        /// </summary>
        /// <returns><see langword="true"/> if the condition is met, otherwise <see langword="false"/>.</returns>
        public bool Invoke();
    }
}
