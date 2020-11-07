namespace TagScanner.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is DateTime t ? t.ToString("g") : value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
