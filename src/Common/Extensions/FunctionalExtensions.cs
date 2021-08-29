using System;

namespace Common.Extensions {
    public static class FunctionalExtensions {
        /// <summary>
        ///     Run the <see cref="Action" /> with <paramref name="parameter" /> passed in as a parameter.
        /// </summary>
        /// <param name="parameter">The object to pass along.</param>
        /// <param name="actionToApply">The action to perform.</param>
        /// <typeparam name="T">The type of the current parameter.</typeparam>
        /// <returns><paramref name="parameter" /> after <paramref name="actionToApply" /> runs on it.</returns>
        public static T Apply<T>(this T parameter, Action<T> actionToApply) {
            actionToApply(parameter);
            return parameter;
        }
    }
}
