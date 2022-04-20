using System;
using System.Collections.Generic;
using FluentResults;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;

namespace MathieuDR.Common.Parsers {
    public static class DateParsers {
        public static Result<DateTime> ToFutureDate(this string query, DateTime? referenceDate = null,
            string culture = Culture.English) {
            var reference = referenceDate ?? DateTime.Now;
            var result = query.ToDate(reference, culture);

            if (result.IsFailed) {
                return result;
            }

            // Return if its in the future
            if (result.Value > reference) {
                return result;
            }

            // Add a year
            return Result.Ok(result.Value.AddYears(1));
        }

        public static Result<DateTime> ToDate(this string query, DateTime? referenceDate = null,
            string culture = Culture.English) {
            var reference = referenceDate ?? DateTime.Now;
            var recognized = DateTimeRecognizer.RecognizeDateTime(query, culture, refTime: reference);

            DateTime? resultDate = null;
            string durationString = null;

            // Loop through options
            foreach (var modelResult in recognized) {
                var values = modelResult.Resolution["values"] as List<Dictionary<string, string>> ??
                             new List<Dictionary<string, string>>();

                foreach (var dict in values) {
                    var type = dict["type"];

                    switch (type) {
                        case "time":
                            resultDate ??= reference.Date.Add(TimeSpan.Parse(dict["value"]));
                            break;
                        case "datetime":
                        case "date":
                            var tempDate = DateTime.Parse(dict["value"]);
                            if (tempDate.Year == reference.Year) {
                                resultDate = tempDate;
                            }

                            resultDate ??= tempDate;
                            break;
                        case "datetimerange":
                        case "daterange":
                        case "timerange":
                            var tempDateRange = DateTime.Parse(dict["start"]);
                            if (tempDateRange.Year == reference.Year) {
                                resultDate = tempDateRange;
                            }

                            resultDate ??= tempDateRange;
                            break;
                        case "duration":
                            durationString ??= dict["value"];
                            break;
                    }
                }
            }

            // Add time of duration string
            if (!string.IsNullOrEmpty(durationString)) {
                resultDate ??= reference.Date;
                resultDate = resultDate.Value.Add(TimeSpan.FromSeconds(long.Parse(durationString)));
            }

            // return error if we cannot find one
            if (!resultDate.HasValue) {
                return Result.Fail("Could not find date");
            }

            return Result.Ok(resultDate.Value);
        }
    }
}
