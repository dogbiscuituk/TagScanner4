namespace TagScanner.Models
{
	public interface IObserveTracks
	{
		void TrackPropertyChanged(Track sender, string propertyName);
	}
}
