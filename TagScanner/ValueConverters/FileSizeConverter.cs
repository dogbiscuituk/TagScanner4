namespace TagScanner.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using TagScanner.Models;

    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is long longValue ? longValue.AsString(false) : value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
