using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace TagScanner.ValueConverters
{
	public class StringsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is IEnumerable<string>)
			{
				var strings = (IEnumerable<string>)value;
				return strings.Any() ? strings.Aggregate((x, y) => string.Concat(x, '\n', y)) : string.Empty;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
