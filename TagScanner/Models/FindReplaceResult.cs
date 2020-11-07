namespace TagScanner.Models
{
    using System.Linq;

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

        private string GetSort(object value) => value == null
                ? string.Empty
                : value is string stringValue
                ? stringValue
                : !(value is string[] strings) || !strings.Any()
                ? string.Empty
                : ((string[])value).Aggregate((s, t) => string.Concat(s, ' ', t));
    }
}
