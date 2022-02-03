namespace Common.Extensions {
    public static class Casting {
        /// <summary>
        ///     Uses the AS keyword
        /// </summary>
        /// <typeparam name="T">Type to cast to</typeparam>
        /// <param name="object">Current object</param>
        /// <returns><paramref name="object"/>, cast to the type of <typeparamref name="T"/></returns>
        public static T? As<T>(this object @object) where T : class => @object as T;


        /// <summary>
        ///     "Hard" casts the current object to the specified type. Throws an <see cref="InvalidCastException"/> if the current object is not assignable to that type,
        /// so <see cref="As{T}"/> might be preferable.
        /// </summary>
        /// <typeparam name="T">Type to cast to</typeparam>
        /// <param name="object">Current object</param>
        /// <exception cref="InvalidCastException"></exception>
        /// <returns><paramref name="object"/>, cast to the type of <typeparamref name="T"/>, or an exception is thrown.</returns>
        public static T Cast<T>(this object @object) where T : class => (T)@object;

    }
}
