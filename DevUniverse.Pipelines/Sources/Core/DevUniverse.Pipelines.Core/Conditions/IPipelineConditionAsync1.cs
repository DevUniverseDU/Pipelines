using System.Threading.Tasks;

namespace DevUniverse.Pipelines.Core.Conditions
{
    /// <summary>
    /// The async pipeline condition with 1 parameter.
    /// </summary>
    /// <typeparam name="TParam0">The type of the 1st parameter.</typeparam>
    public interface IPipelineConditionAsync<in TParam0> : IPipelineConditionAsyncBasic
    {
        /// <summary>
        /// Executes the logic of the condition.
        /// </summary>
        /// <param name="param0">The 1st parameter.</param>
        /// <returns><see langword="true"/> if the condition is met, otherwise <see langword="false"/>.</returns>
        public Task<bool> InvokeAsync(TParam0 param0);
    }
}
