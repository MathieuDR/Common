using System;
using System.Collections.Generic;
using System.Linq;
using FluentResults;

namespace Common.Extensions {
    public static class FluentResultExtensions {
        private static readonly string DefaultSeparator = Environment.NewLine;
        
        /// <summary>
        /// Combines all the reasons of the result in one single string
        /// The default separator is <see cref="Environment.NewLine"/>
        /// </summary>
        /// <param name="result">Result with reasons</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>One string with all the reasons, concatenated with the default separator</returns>
        public static string CombineMessage<T>(this Result<T> result) {
            return Combine(result.Successes, DefaultSeparator);
        }
        
        /// <summary>
        /// Combines all the reasons of the result in one single string
        /// The default separator is <see cref="Environment.NewLine"/>
        /// </summary>
        /// <param name="result">Result with reasons</param>
        /// <returns>One string with all the reasons, concatenated with the default separator</returns>
        public static string CombineMessage(this Result result) {
            return Combine(result.Reasons, DefaultSeparator);
        }
        
        /// <summary>
        /// Combines all the reasons of the result in one single string
        /// </summary>
        /// <param name="result">Result with reasons</param>
        /// <param name="separator">Separator to concatenate all the results</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>One string with all the reasons</returns>
        public static string CombineMessage<T>(this Result<T> result, string separator) {
            return Combine(result.Reasons, separator);
        }
        
        /// <summary>
        /// Combines all the reasons of the result in one single string
        /// </summary>
        /// <param name="result">Result with reasons</param>
        /// <param name="separator">Separator to concatenate all the results</param>
        /// <returns>One string with all the reasons</returns>
        public static string CombineMessage(this Result result, string separator) {
            return Combine(result.Reasons, separator);
        }

        private static string Combine(List<Reason> reasons, string separator) {
            return reasons.Select(r => r.Message).Aggregate((a, b) => a + separator + b);
        }
        
        private static string Combine(List<Success> successes, string separator) {
            return successes.Select(r => r.Message).Aggregate((a, b) => a + separator + b);
        }
    }

}