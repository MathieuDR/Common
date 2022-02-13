using Common.Formatters;
using FluentAssertions;
using Xunit;

namespace Common.tests.Formatters; 

public class NumberFormatterTests {
	
	[Theory]
	[InlineData(0, "-")]
	[InlineData(10.2, "10.2")]
	[InlineData(1000, "1.00K")]
	[InlineData(999, "999")]
	[InlineData(10000, "10.0K")]
	[InlineData(100000, "100K")]
	[InlineData(1000000, "1.00M")]
	[InlineData(10000000, "10.0M")]
	[InlineData(10100000, "10.1M")]
	[InlineData(100000000, "100.0M")]
	[InlineData(100120000, "100.1M")]
	[InlineData(139200000, "139.2M")]
	[InlineData(1001200000, "1.00B")]
	[InlineData(1039200000, "1.04B")]
	[InlineData(10039200000, "10.04B")]
	[InlineData(100039200000, "100.04B")]
	[InlineData(1000039200000, "1000.04B")]
	public void FormatDecimal_ShouldFormat_WhenGivenNormalNumbersAndNotSigned(decimal number, string expected) {
		NumberFormatter.FormatDecimal(number).Should().Be(expected);
	}
	
	[Theory]
	[InlineData(0, "-")]
	[InlineData(10.2, "+10.2")]
	[InlineData(1000, "+1.00K")]
	[InlineData(10100000, "+10.1M")]
	[InlineData(100000000, "+100.0M")]
	public void FormatDecimal_ShouldPositiveSign_WhenGivenPositiveNumberAndSignParameter(decimal number, string expected) {
		NumberFormatter.FormatDecimal(number, true).Should().Be(expected);
	}
	
	[Theory]
	[InlineData(-0, "-")]
	[InlineData(-10.2, "-10.2")]
	[InlineData(-1000, "-1.00K")]
	[InlineData(-10100000, "-10.1M")]
	[InlineData(-100000000, "-100.0M")]
	public void FormatDecimal_ShouldNegativeSign_WhenGivenNegativeNumberAndSignParameter(decimal number, string expected) {
		NumberFormatter.FormatDecimal(number, true).Should().Be(expected);
	}
	
}
