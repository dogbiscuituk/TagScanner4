namespace TagScanner.ValueConverters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;

	public class LogicalConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch (value.ToString())
			{
				case "Yes":
					return true;
				case "No":
					return false;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
