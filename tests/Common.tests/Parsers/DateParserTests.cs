using System;
using Common.Parsers;
using FluentAssertions;
using Microsoft.Recognizers.Text;
using Xunit;

namespace Common.tests.Parsers {
    public class DateParserTests {
        [Theory]
        [InlineData("8:30", "2021-12-01 08:30:00")]
        [InlineData("8h30m", "2021-12-01 08:30:00")]
        [InlineData("8h30", "2021-12-01 08:30:00")]
        [InlineData("8pm today", "2021-12-01 20:00:00")]
        [InlineData("today 8pm", "2021-12-01 20:00:00")]
        [InlineData("1 october", "2022-10-01 00:00:00")]
        [InlineData("first of october", "2022-10-01 00:00:00")]
        [InlineData("first of october 2023", "2023-10-01 00:00:00")]
        [InlineData("1st of october 2022", "2022-10-01 00:00:00")]
        [InlineData("at 6pm today", "2021-12-01 18:00:00")]
        [InlineData("2nd january", "2022-01-02 00:00:00")]
        [InlineData("2nd january 9h", "2022-01-02 09:00:00")]
        [InlineData("10th dec midnight", "2021-12-10 00:00:00")]
        [InlineData("10th december 10am", "2021-12-10 10:00:00")]
        [InlineData("2nd january 15h", "2022-01-02 15:00:00")]
        [InlineData("2nd january 15h30", "2022-01-02 15:30:00")]
        [InlineData("8pm", "2021-12-01 20:00:00")]
        [InlineData("15h", "2021-12-01 15:00:00")]
        [InlineData("at 15h", "2021-12-01 15:00:00")]
        [InlineData("I'll go back 8pm today", "2021-12-01 20:00:00")]
        [InlineData("14/12/2021 18:00", "2021-12-14 18:00:00")]
        [InlineData("22th dec 23:00", "2021-12-22 23:00:00")]
        public void CanParseFutureDates(string dateString, string parseAbleDate) {
            DateTime reference = new DateTime(2021, 12, 1, 4, 10, 0);
            var futureDateResult = dateString.ToFutureDate(referenceDate: reference);

            futureDateResult.IsSuccess.Should().BeTrue();
            futureDateResult.Value.Should().Be(DateTime.Parse(parseAbleDate));
        }
        
        [Theory]
        [InlineData("22th november 23:00", "2021-11-22 23:00:00", "2021-11-21 14:12:30")]
        public void CanParseFutureDatesWithReference(string dateString, string parseAbleDate, string parseAbleReferenceDate) {
            DateTime reference = DateTime.Parse(parseAbleReferenceDate);
            var futureDateResult = dateString.ToFutureDate(referenceDate: reference);

            futureDateResult.IsSuccess.Should().BeTrue();
            futureDateResult.Value.Should().Be(DateTime.Parse(parseAbleDate));
        }

        [Theory]
        [InlineData("8:30", "2021-12-01 08:30:00")]
        [InlineData("8h30m", "2021-12-01 08:30:00")]
        [InlineData("8h30", "2021-12-01 08:30:00")]
        [InlineData("8pm today", "2021-12-01 20:00:00")]
        [InlineData("today 8pm", "2021-12-01 20:00:00")]
        [InlineData("1 october", "2021-10-01 00:00:00")]
        [InlineData("first of october", "2021-10-01 00:00:00")]
        [InlineData("first of october 2023", "2023-10-01 00:00:00")]
        [InlineData("1st of october 2022", "2022-10-01 00:00:00")]
        [InlineData("at 6pm today", "2021-12-01 18:00:00")]
        [InlineData("2nd january", "2021-01-02 00:00:00")]
        [InlineData("2nd january 9h", "2021-01-02 09:00:00")]
        [InlineData("10th dec midnight", "2021-12-10 00:00:00")]
        [InlineData("10th december 10am", "2021-12-10 10:00:00")]
        [InlineData("2nd january 15h", "2021-01-02 15:00:00")]
        [InlineData("2nd january 15h30", "2021-01-02 15:30:00")]
        [InlineData("8pm", "2021-12-01 20:00:00")]
        [InlineData("15h", "2021-12-01 15:00:00")]
        [InlineData("at 15h", "2021-12-01 15:00:00")]
        [InlineData("I'll go back 8pm today", "2021-12-01 20:00:00")]
        [InlineData("14/12/2021 18:00", "2021-12-14 18:00:00")]
        public void CanParseDates(string dateString, string parseAbleDate) {
            DateTime reference = new DateTime(2021, 12, 1, 4, 10, 0);
            var futureDateResult = dateString.ToDate(referenceDate: reference);

            futureDateResult.IsSuccess.Should().BeTrue();
            futureDateResult.Value.Should().Be(DateTime.Parse(parseAbleDate));
        }
        
        [Theory]
        [InlineData("8:30", "2021-12-01 08:30:00")]
        [InlineData("8h30m", "2021-12-01 08:30:00")]
        [InlineData("8h30", "2021-12-01 08:30:00")]
        [InlineData("8pm vandaag", "2021-12-01 20:00:00")]
        [InlineData("vandaag 8 uur", "2021-12-01 20:00:00")]
        [InlineData("vandaag 8 uur in de ochtend", "2021-12-01 08:00:00")]
        [InlineData("vandaag 8 uur 's avonds", "2021-12-01 20:00:00")]
        [InlineData("1 oktober", "2022-10-01 00:00:00")]
        [InlineData("eerste oktober", "2022-10-01 00:00:00")]
        [InlineData("eerste oktober 2023", "2023-10-01 00:00:00")]
        [InlineData("1ste oktober 2022", "2022-10-01 00:00:00")]
        [InlineData("om 6 uur 's avonds vandaag", "2021-12-01 18:00:00")]
        [InlineData("om 18 uur vandaag", "2021-12-01 18:00:00")]
        [InlineData("2e januari", "2022-01-02 00:00:00")]
        [InlineData("2e januari 9h in de ochtend", "2022-01-02 09:00:00")]
        [InlineData("10e dec middernacht", "2021-12-10 12:00:00")] // stupid spec?
        [InlineData("10e december 10am", "2021-12-10 10:00:00")]
        [InlineData("2e januari 15h", "2022-01-02 15:00:00")]
        [InlineData("2e januari 15h30", "2022-01-02 15:30:00")]
        [InlineData("8pm", "2021-12-01 20:00:00")]
        [InlineData("15h", "2021-12-01 15:00:00")]
        [InlineData("at 15h", "2021-12-01 15:00:00")]
        [InlineData("Ik kom terug om 8:00 uur in de avond vandaag", "2021-12-01 20:00:00")]
        [InlineData("14/12/2021 18:00", "2021-12-14 18:00:00")]
        public void CanParseFutureDatesInDutch(string dateString, string parseAbleDate) {
            DateTime reference = new DateTime(2021, 12, 1, 4, 10, 0);
            var futureDateResult = dateString.ToFutureDate(referenceDate: reference, culture: Culture.Dutch);

            futureDateResult.IsSuccess.Should().BeTrue();
            futureDateResult.Value.Should().Be(DateTime.Parse(parseAbleDate));
        }
        
        [Theory]
        [InlineData("8:30", "2021-12-01 08:30:00")]
        [InlineData("8h30m", "2021-12-01 08:30:00")]
        [InlineData("8h30", "2021-12-01 08:30:00")]
        [InlineData("8pm vandaag", "2021-12-01 20:00:00")]
        [InlineData("vandaag 8 uur 's morgens", "2021-12-01 08:00:00")]
        [InlineData("vandaag 8 uur 's avonds", "2021-12-01 20:00:00")]
        [InlineData("1 oktober", "2021-10-01 00:00:00")]
        [InlineData("eerste oktober", "2021-10-01 00:00:00")]
        [InlineData("eerste oktober 2023", "2023-10-01 00:00:00")]
        [InlineData("1ste oktober 2022", "2022-10-01 00:00:00")]
        [InlineData("om 6 uur 's avonds vandaag", "2021-12-01 18:00:00")]
        [InlineData("om 18 uur vandaag", "2021-12-01 18:00:00")]
        [InlineData("2e januari", "2021-01-02 00:00:00")]
        [InlineData("2e januari 9h in de ochtend", "2021-01-02 09:00:00")]
        [InlineData("10e dec middernacht", "2021-12-10 12:00:00")] // stupid spec?
        [InlineData("10e december 10am", "2021-12-10 10:00:00")]
        [InlineData("2e januari 15h", "2021-01-02 15:00:00")]
        [InlineData("2e januari 15h30", "2021-01-02 15:30:00")]
        [InlineData("8pm", "2021-12-01 20:00:00")]
        [InlineData("15h", "2021-12-01 15:00:00")]
        [InlineData("at 15h", "2021-12-01 15:00:00")]
        [InlineData("Ik kom terug om 8:00 uur in de avond vandaag", "2021-12-01 20:00:00")]
        [InlineData("14/12/2021 18:00", "2021-12-14 18:00:00")]
        public void CanParseDatesInDutch(string dateString, string parseAbleDate) {
            DateTime reference = new DateTime(2021, 12, 1, 4, 10, 0);
            var futureDateResult = dateString.ToDate(referenceDate: reference, culture: Culture.Dutch);

            futureDateResult.IsSuccess.Should().BeTrue();
            futureDateResult.Value.Should().Be(DateTime.Parse(parseAbleDate));
        }
    }
}
