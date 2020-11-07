using System;

namespace TagScanner.Models
{
	public static class TrackExtensions
	{
		#region Public Interface

		public static Logical AsLogical(this bool value)
		{
			return value ? Logical.Yes : Logical.No;
		}

		public static string AsOrdinal(this long number)
		{
			return string.Concat(number, GetSuffix(number));
		}

		/// <summary>
		/// Approximate a byte count to three significant figures, using the most suitable prefix as necessary.
		/// 
		/// Examples:
		/// 
		/// 1,023 -> "1023 Bytes"
		/// 1,024 -> "1.00 KB"
		/// 2,345,678 -> "2.24 MB"
		/// 9,012,345,678 -> "8.39 GB"
		/// 
		/// </summary>
		/// <param name="bytes">The exact number of bytes to be represented.</param>
		/// <param name="binary">When true, use the appropriate IEC-preferred binary prefix:
		/// 1 KiB = 1,024 bytes; 1 MiB = 1,048,576 bytes; etc.
		/// When false, use the appropriate SI decimal prefix:
		/// 1 KB = 1,000 bytes; 1 MB = 1,000,000 bytes; etc.</param>
		/// <returns>A string representation of the byte count to three significant figures,
		/// using the most suitable prefix if necessary.</returns>
		public static string AsString(this long bytes, bool binary)
		{
			const string units = "KMGTPE";
			for (int scale = units.Length; scale > 0; scale--)
			{
				var chunk = binary ? 1L << 10*scale : (long) Math.Pow(1000, scale);
				if (bytes >= chunk)
				{
					var value = Decimal.Divide(bytes, chunk);
					return String.Format(
						String.Format(
							"{{0:{0}}} {{1}}{{2}}B",
							value >= 100 ? "#" : value >= 10 ? "#.#" : "#.##"),
							value, units[scale - 1], binary ? "i" : "");
				}
			}
			return String.Format("{0} Bytes", bytes);
		}

		public static string AsString(this TimeSpan t, bool exact)
		{
			var format = @"d\.hh\:mm\:ss".Substring(t.Days > 0 ? 0 : t.Hours > 0 ? 4 : 8);
			if (exact)
				format += @"\.fff";
			return t.ToString(format);
		}

		/// <summary>
		/// Return the first non-void string value from a pair.
		/// </summary>
		/// <param name="a">First string value.</param>
		/// <param name="b">Second string value.</param>
		/// <returns>Value a, if a is non-null, non-empty, and not just whitespace; otherwise, value b.</returns>
		public static string Coalesce(this string a, string b)
		{
			return !string.IsNullOrWhiteSpace(a) ? a : b;
		}

		public static string Format(this object value, string propertyName, Type type, bool exact)
		{
			if (value == null)
				return string.Empty;
			switch (propertyName)
			{
				case "Duration":
					return ((TimeSpan) value).AsString(exact);
				case "FileSize":
                    var fileSize = (long) value;
					return exact ? string.Format("{0:n0}", fileSize) : fileSize.AsString(true);
				case "Year":
					return (int)value == 0 ? string.Empty : value.ToString();
			}
			return
				type == typeof (int?) || type == typeof (uint?) || type == typeof (long?)
					? Convert.ToInt64(value) == 0 ? string.Empty : string.Format("{0:n0}", value)
					: value.ToString();
		}

		public static string GetIndex(this string s)
		{
			return string.IsNullOrWhiteSpace(s) ? " " : (s.ToUpper() + " ").Substring(0, 1);
		}

		#endregion

		#region Private Implementation

		private static string GetSuffix(long number)
		{
			switch (number % 100)
			{
				case 11:
				case 12:
				case 13:
					return "th";
			}
			switch (number % 10)
			{
				case 1:
					return "st";
				case 2:
					return "nd";
				case 3:
					return "rd";
			}
			return "th";
		}

		#endregion
	}
}
