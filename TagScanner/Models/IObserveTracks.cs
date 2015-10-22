using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagScanner.Models
{
	public interface IObserveTracks
	{
		void TrackPropertyChanged(Track sender, string propertyName);
	}
}
