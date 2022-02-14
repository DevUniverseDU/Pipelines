using DevUniverse.Pipelines.Core.Conditions.Shared;

namespace DevUniverse.Pipelines.Core.Conditions
{
    /// <summary>
    /// The pipeline condition with 2 parameters.
    /// </summary>
    /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
    /// <typeparam name="TParam1">The type of the 2nd parameter.</typeparam>
    public interface IPipelineCondition<in TParam0, in TParam1> : IPipelineConditionBasic
    {
        /// <summary>
        /// Executes the logic of the condition.
        /// </summary>
        /// <param name="param0">The 1st parameter.</param>
        /// <param name="param1">The 2nd parameter.</param>
        /// <returns><see langword="true"/> if the condition is met, otherwise <see langword="false"/>.</returns>
        public bool Invoke(TParam0 param0, TParam1 param1);
    }
}
