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

	private static string FormatUnsigned(decimal number) {
		var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
		nfi.NumberGroupSeparator = " ";
		var absValue = Math.Abs(number);

		if (absValue >= 100000000) {
			return (number / 1000000).ToString("#,0M", nfi);
		}

		if (absValue >= 10000000) {
			return (number / 1000000).ToString("0.#", nfi) + "M";
		}

		if (absValue >= 100000) {
			return (number / 1000).ToString("#,0K", nfi);
		}

		if (absValue >= 10000) {
			return (number / 1000).ToString("0.#", nfi) + "K";
		}

		return number.ToString("0", nfi);
	}
	
	private static string FormatSigned(decimal number) {
		var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
		nfi.NumberGroupSeparator = " ";
		var absValue = Math.Abs(number);

		if (absValue >= 100000000) {
			return (number / 1000000).ToString("+#,0M;-#,0M", nfi);
		}

		if (absValue >= 10000000) {
			return (number / 1000000).ToString("+0.#;-0.#", nfi) + "M";
		}

		if (absValue >= 100000) {
			return (number / 1000).ToString("+#,0K;-#,0K", nfi);
		}

		if (absValue >= 10000) {
			return (number / 1000).ToString("+0.#;-0.#", nfi) + "K";
		}

		return number.ToString("+#;-#;0", nfi);
	}
}
