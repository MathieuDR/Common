namespace MathieuDR.Common.Parsers; 

public static class NumberParser {
	public static decimal ParseToDecimal(string query) {
		decimal result = 0;
		// removes spaces in input
		query = query.Replace(" ", "");
		// lower input
		query = query.ToLower();
		
		//	parse number
		if (query.Contains("k")) {
			// remove k
			query = query.Replace("k", "");
			// parse number
			result = decimal.Parse(query) * 1000;
		} else if (query.Contains("m")) {
			// remove m
			query = query.Replace("m", "");
			// parse number
			result = decimal.Parse(query) * 1000000;
		} else {
			// parse number
			result = decimal.Parse(query);
		}
		
		// convert decimal to long
		return result;
	}

	public static long ParseToLong(string query) => (long)ParseToDecimal(query);
}
