using System.Threading.Tasks;

namespace DevUniverse.Pipelines.Core.Conditions
{
    /// <summary>
    /// The async pipeline condition with no parameters.
    /// </summary>
    public interface IPipelineConditionAsync : IPipelineConditionAsyncBasic
    {
        /// <summary>
        /// Executes the logic of the condition.
        /// </summary>
        /// <returns><see langword="true"/> if the condition is met, otherwise <see langword="false"/>.</returns>
        public Task<bool> InvokeAsync();
    }
}
