using System;
using System.Collections.Generic;

namespace MathieuDR.Common.Extensions {
    public static class CollectionExtensions {
        /// <summary>
        ///     Performs the specified <paramref name="action" /> on each element in the current <paramref name="enumerable" />;
        ///     passing the current element as a parameter to the action.
        /// </summary>
        /// <param name="enumerable">The current enumerable.</param>
        /// <param name="action">The action to perform.</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
            foreach (var item in enumerable) {
                action(item);
            }
        }
    }
}
