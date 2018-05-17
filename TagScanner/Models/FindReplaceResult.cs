using System.Linq;

namespace TagScanner.Models
{
	public class FindReplaceResult
	{
		public FindReplaceResult(Track track, string tag, object oldValue, object newValue)
		{
			Replace = true;
			Track = track;
			Tag = tag;
			OldValue = oldValue;
			NewValue = newValue;
		}

		public bool Replace { get; set; }
		public Track Track { get; set; }
		public string Tag { get; set; }
		public object OldValue { get; set; }
		public object NewValue { get; set; }

		private string _oldValueSort;
		public string OldValueSort => _oldValueSort ?? (_oldValueSort = GetSort(OldValue));

		private string _newValueSort;
		public string NewValueSort => _newValueSort ?? (_newValueSort = GetSort(NewValue));

		private string GetSort(object value)
		{
			if (value == null)
				return string.Empty;
			if (value is string stringValue)
				return stringValue;
			var strings = value as string[];
			if (strings == null || !strings.Any())
				return string.Empty;
			return ((string[])value).Aggregate((s, t) => string.Concat(s, ' ', t));
		}
	}
}
