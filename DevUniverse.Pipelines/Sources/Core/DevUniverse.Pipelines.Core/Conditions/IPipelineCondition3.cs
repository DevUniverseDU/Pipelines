using DevUniverse.Pipelines.Core.Conditions.Shared;

namespace DevUniverse.Pipelines.Core.Conditions
{
    /// <summary>
    /// The pipeline condition with 3 parameters.
    /// </summary>
    /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
    /// <typeparam name="TParam1">The type of the 2nd parameter.</typeparam>
    /// <typeparam name="TParam2">The type of the 3rd parameter.</typeparam>
    public interface IPipelineCondition<in TParam0, in TParam1, in TParam2> : IPipelineConditionBasic
    {
        /// <summary>
        /// Executes the logic of the condition.
        /// </summary>
        /// <param name="param0">The 1st parameter.</param>
        /// <param name="param1">The 2nd parameter.</param>
        /// <param name="param2">The 3rd parameter.</param>
        /// <returns><see langword="true"/> if the condition is met, otherwise <see langword="false"/>.</returns>
        public bool Invoke(TParam0 param0, TParam1 param1, TParam2 param2);
    }
}
