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
		public Track Track { get; }
		public string Tag { get; }
		public object OldValue { get; }
		public object NewValue { get; }

		private string _oldValueSort;
		public string OldValueSort { get { return _oldValueSort ?? (_oldValueSort = GetSort(OldValue)); } }

		private string _newValueSort;
		public string NewValueSort { get { return _newValueSort ?? (_newValueSort = GetSort(NewValue)); } }

		private string GetSort(object value)
		{
			if (value == null)
				return string.Empty;
			if (value is string)
				return (string)value;
			var strings = value as string[];
			if (strings == null || !strings.Any())
				return string.Empty;
			return ((string[])value).Aggregate((s, t) => string.Concat(s, ' ', t));
		}
	}
}
