using System;

namespace DevUniverse.Pipelines.Infrastructure.Shared.Utils
{
    /// <summary>
    /// The exception utils.
    /// </summary>
    public static class ExceptionUtils
    {
        /// <summary>
        /// Checks if the argument is <see langword="null"/>.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <typeparam name="TParam">The type of the <see cref="param"/>.</typeparam>
        /// <returns><see langword="true"/> if the argument is <see langword="null"/>, otherwise <see langword="false"/>.</returns>
        public static bool CheckIfNull<TParam>(TParam? param) where TParam : class => param == null;

        /// <summary>
        /// Throws exception using <see cref="exceptionFactory"/> when condition is met using <see cref="predicate"/> on <see cref="param"/>.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="exceptionFactory">The exception factory.</param>
        /// <typeparam name="TParam">The type of the <see cref="param"/>.</typeparam>
        /// <exception cref="ArgumentNullException">The exception when the some arguments are <see langword="null"/>.</exception>
        /// <exception cref="Exception">The type of the exception for <see cref="exceptionFactory"/>.</exception>
        public static void Process<TParam>(TParam param, Func<TParam, bool> predicate, Func<Exception> exceptionFactory)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (exceptionFactory == null)
            {
                throw new ArgumentNullException(nameof(exceptionFactory));
            }

            if (predicate.Invoke(param))
            {
                throw exceptionFactory.Invoke();
            }
        }
    }
}
