using System;

namespace MathieuDR.Common.Extensions {
    public static class StringExtensions {
        /// <summary>
        ///     Prepends <paramref name="toPrepend" /> to <paramref name="base" />.
        /// </summary>
        /// <param name="base">Base string</param>
        /// <param name="toPrepend">The value to prepend</param>
        /// <returns><paramref name="toPrepend" /> added to <paramref name="base" />.</returns>
        public static string Prepend(this string @base, string toPrepend) {
            return @base.Insert(0, toPrepend);
        }

        /// <summary>
        ///     Reverses the string
        /// </summary>
        /// <param name="base">Base string</param>
        /// <returns>Reverse of  <paramref name="base" /></returns>
        public static string Reverse(this string @base) {
            return new(@base.ToCharArray().Apply(Array.Reverse));
        }
    }
}
