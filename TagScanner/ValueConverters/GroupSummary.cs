namespace TagScanner.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;
    using TagScanner.Models;

    public class GroupSummary : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ReadOnlyObservableCollection<object> group)
            {
                var tracks = new List<Track>();
                AddTracks(tracks, group);
                var summary = new Selection(tracks);
                var trackCount = tracks.Count;
                return string.Format(
                    " ({0:n0} {1}, {2}, {3})",
                    trackCount,
                    trackCount == 1 ? "track" : "tracks",
                    summary.FileSize.AsString(false),
                    summary.Duration.AsString(false));
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;

        private void AddTracks(List<Track> tracks, ReadOnlyObservableCollection<object> group)
        {
            if (group.Any())
                if (group[0] is Track)
                    tracks.AddRange(group.Cast<Track>());
                else
                    foreach (CollectionViewGroup item in group)
                        AddTracks(tracks, item.Items);
        }
    }
}
