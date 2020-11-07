namespace TagScanner.ValueConverters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;
	using TagScanner.Models;

	public class FileSizeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is long longValue)
				return longValue.AsString(false);
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
