using System;

namespace MathieuDR.Common.Extensions;

public static class RandomExtensions {
	public static T NextEnum<T>(this Random random) where T: System.Enum{
		var values = Enum.GetValues(typeof(T));
		return (T)values.GetValue(random.Next(values.Length));
	}
}
