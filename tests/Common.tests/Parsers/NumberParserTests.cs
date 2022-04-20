using MathieuDR.Common.Parsers;
using Xunit;

namespace MathieuDR.Common.tests.Parsers; 

public class NumberParserTests {
	[Theory]
	[InlineData("1", 1)]
	[InlineData("100", 100)]
	[InlineData("100.22", 100.22)]
	[InlineData("100K", 100*1000)]
	[InlineData("100.5K", 100.5*1000)]
	[InlineData("100M", 100*1000000)]
	[InlineData("100.5M", 100.5*1000000)]
	
	[InlineData("100k", 100*1000)]
	[InlineData("100.5k", 100.5*1000)]
	[InlineData("100m", 100*1000000)]
	[InlineData("100.5m", 100.5*1000000)]
	
	[InlineData("100 k", 100*1000)]
	[InlineData("100.5 k", 100.5*1000)]
	[InlineData("100 m", 100*1000000)]
	[InlineData("100.5 m", 100.5*1000000)]
	
	[InlineData("100 000", 100*1000)]
	[InlineData("-100", -100)]
	[InlineData("-100m", -100 * 1000000)]
	
	[InlineData("10.932m", 10.932 * 1000000)]
	public void Parse_ValidNumber_ReturnsNumber(string input, decimal expected) {
		var result = NumberParser.ParseToDecimal(input);
		Assert.Equal(expected, result);
	}
}
