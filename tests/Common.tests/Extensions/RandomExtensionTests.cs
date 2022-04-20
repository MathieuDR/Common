using System;
using System.Linq;
using FluentAssertions;
using MathieuDR.Common.Extensions;
using Xunit;

namespace MathieuDR.Common.tests.Extensions; 

public class RandomExtensionTests {
	public enum MyEnum {
		A,
		B,
		C,
		D,
		E,
		F,
		G,
		H
	}
	
	[Fact]
	public void NextEnum_ShouldBeAValue_WhenGivenEnum() {
		var random = new Random();
		var result = random.NextEnum<MyEnum>();
		result.Should().BeOneOf(MyEnum.A, MyEnum.B, MyEnum.C, MyEnum.D, MyEnum.E, MyEnum.F, MyEnum.G, MyEnum.H);
	}
	
	[Fact]
	public void NextEnum_ShouldHaveAllValues_WhenCallingItMultipleTimes() {
		var random = new Random();
		
		var results = new MyEnum[1000];
		for (var i = 0; i < results.Length; i++) {
			results[i] = random.NextEnum<MyEnum>();
		}
		
		var distinctResults = results.Distinct().ToList();
		distinctResults.Count.Should().Be(8);
	}
}
