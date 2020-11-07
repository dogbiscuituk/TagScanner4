namespace TagScanner.Controllers
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using TagScanner.ValueConverters;

    public abstract class GridController
    {
        protected abstract DataGrid DataGrid { get; }

        protected virtual DataGridBoundColumn GetColumn(PropertyInfo propertyInfo)
        {
            var propertyTypeName = propertyInfo.PropertyType.Name;
            var column = GetColumn(propertyTypeName);
            if (column != null)
            {
                var propertyName = propertyInfo.Name;
                var binding = new Binding(propertyName)
                {
                    Mode = propertyInfo.CanWrite ? BindingMode.TwoWay : BindingMode.OneWay,
                    Converter = GetConverter(propertyInfo)
                };
                column.Binding = binding;
                column.CellStyle = GetColumnStyle(propertyInfo);
                column.Header = propertyName;
            }
            return column;
        }

        protected virtual DataGridBoundColumn GetColumn(string propertyTypeName)
        {
            switch (propertyTypeName)
            {
                case "DateTime":
                case "Double":
                case "Int32":
                case "Int64":
                case "TimeSpan":
                    return GetTextBoxColumn(StringAlignment.Far);
                case "Boolean":
                case "Logical":
                    return GetCheckBoxColumn();
            }
            return GetTextBoxColumn(StringAlignment.Near);
        }

        protected virtual IValueConverter GetConverter(PropertyInfo propertyInfo)
        {
            switch (propertyInfo.PropertyType.Name)
            {
                case "DateTime":
                    return new DateTimeConverter();
                case "Logical":
                    return new LogicalConverter();
                case "String[]":
                    return new StringsConverter();
                case "TimeSpan":
                    return new TimeSpanConverter();
            }
            return null;
        }

        protected abstract PropertyInfo[] GetPropertyInfos();

        protected void InitColumns()
        {
            DataGrid.Columns.Clear();
            foreach (var column in GetColumns())
                DataGrid.Columns.Add(column);
            DataGrid.GridLinesVisibility = DataGridGridLinesVisibility.Vertical;
        }

        private IEnumerable<DataGridBoundColumn> GetColumns() => GetPropertyInfos().Select(GetColumn).Where(c => c != null);

        private static Style _rightAlignStyle;
        private static Style RightAlignStyle
        {
            get
            {
                if (_rightAlignStyle == null)
                {
                    _rightAlignStyle = new Style(typeof(DataGridCell));
                    _rightAlignStyle.Setters.Add(new Setter
                    {
                        Property = FrameworkElement.HorizontalAlignmentProperty,
                        Value = HorizontalAlignment.Right
                    });
                }
                return _rightAlignStyle;
            }
        }

        private static DataGridBoundColumn GetCheckBoxColumn()
        {
            var column = new DataGridCheckBoxColumn
            {
                Width = 80
            };
            return column;
        }

        private static Style GetColumnStyle(PropertyInfo propertyInfo)
        {
            switch (propertyInfo.PropertyType.Name)
            {
                case "Int32":
                case "Int64":
                case "TimeSpan":
                    return RightAlignStyle;
            }
            switch (propertyInfo.Name)
            {
                case "DiscOf":
                case "DiscTrack":
                case "TrackOf":
                    return RightAlignStyle;
            }
            return null;
        }

        private static DataGridBoundColumn GetTextBoxColumn(StringAlignment alignment)
        {
            var column = new DataGridTextColumn
            {
                Width = alignment == StringAlignment.Near ? 160 : 80
            };
            return column;
        }
    }
}
