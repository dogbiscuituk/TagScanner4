using System.Collections.Generic;
using System.Linq;
using TagScanner.Models;

namespace TagScanner.Models
{
	public class Order
	{
		public Order(string propertyName, bool descending = false)
		{
			PropertyName = propertyName;
			Descending = descending;
		}

		public string PropertyName { get; set; }
		public bool Descending { get; set; }

		public IOrderedEnumerable<Track> ApplyFirst(IEnumerable<Track> files)
		{
			return Descending ? files.OrderByDescending(PropertyName) : files.OrderBy(PropertyName);
		}

		public IOrderedEnumerable<Track> ApplyNext(IOrderedEnumerable<Track> files)
		{
			return Descending ? files.ThenByDescending(PropertyName) : files.ThenBy(PropertyName);
		}
	}
}
