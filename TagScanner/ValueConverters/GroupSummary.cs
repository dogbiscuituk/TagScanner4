using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using TagScanner.Models;

namespace TagScanner.ValueConverters
{
	public class GroupSummary : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var group = value as ReadOnlyObservableCollection<object>;
			if (group == null)
				return string.Empty;
			var tracks = new List<ITrack>();
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

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}

		private void AddTracks(List<ITrack> tracks, ReadOnlyObservableCollection<object> group)
		{
			if (group.Any())
				if (group[0] is ITrack)
					tracks.AddRange(group.Cast<ITrack>());
				else
					foreach (CollectionViewGroup item in group)
						AddTracks(tracks, item.Items);
		}
	}
}
