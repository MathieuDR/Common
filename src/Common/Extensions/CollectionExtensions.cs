using System;
using System.Collections.Generic;
using Common.Helpers.Internal;

namespace Common.Extensions {
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

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize) {
            if (chunksize < 1) {
                throw new InvalidOperationException();
            }

            var wrapper = new EnumeratorWrapper<T>(source);
            var currentPos = 0;

            try {
                wrapper.AddRef();
                while (wrapper.Get(currentPos, out _)) {
                    yield return new ChunkedEnumerable<T>(wrapper, chunksize, currentPos);
                    currentPos += chunksize;
                }
            }
            finally {
                wrapper.RemoveRef();
            }
        }
    }
}
