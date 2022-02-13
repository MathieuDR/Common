using System;
using System.Globalization;

namespace Common.Formatters; 

public static class NumberFormatter {
	public static string FormatDecimal(decimal number, bool? positiveSigned = null, bool zeroAsStripe = true) {
		if(zeroAsStripe && number == 0) {
			return "-";
		}
		
		var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
		nfi.NumberGroupSeparator = "";
		var absValue = Math.Abs(number);

		var result = absValue switch {
			>= 1000000000 => (number / 1000000000).ToString("#,0.00B", nfi),
			>= 10000000 =>  (number / 1000000).ToString("#,0.0M", nfi),
			>= 1000000 =>  (number / 1000000).ToString("#,0.00M", nfi),
			>= 100000 =>  (number / 1000).ToString("#,0K", nfi),
			>= 10000 =>  (number / 1000).ToString("#,0.0K", nfi),
			>= 1000 =>  (number / 1000).ToString("#,0.00K", nfi),
			>= 100 =>  number.ToString("#,0", nfi),
			>= 10 =>  number.ToString("#,0.0", nfi),
			>= 1 => number.ToString("#,0.00", nfi),
			_ => number.ToString("#,0.000", nfi)
		};


		if (positiveSigned is not null && positiveSigned.Value && number > 0) {
			return $"+{result}";
		}

		return result;
	}
}
