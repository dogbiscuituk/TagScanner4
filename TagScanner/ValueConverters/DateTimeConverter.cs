using System;
using System.Globalization;
using System.Windows.Data;
using TagScanner.Models;

namespace TagScanner.ValueConverters
{
	public class DateTimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is DateTime)
				return ((DateTime)value).ToString("g");
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
