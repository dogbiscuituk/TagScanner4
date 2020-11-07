namespace TagScanner.ValueConverters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;
	using TagScanner.Models;

	public class TimeSpanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is TimeSpan timeSpan)
				return timeSpan.AsString(false);
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
