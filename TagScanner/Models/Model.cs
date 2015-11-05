using System;
using System.Collections.Generic;
using System.Linq;

namespace TagScanner.Models
{
	[Serializable]
	public class Model
	{
		#region Public Interface

		public List<Track> Tracks
		{
			get
			{
				return _tracks;
			}
			set
			{
				_tracks = value;
				OnTracksChanged();
			}
		}

		private bool _modified;
		public bool Modified
		{
			get
			{
				return _modified;
			}
			set
			{
				if (Modified != value)
				{
					_modified = value;
					OnModifiedChanged();
				}
			}
		}

		protected virtual void OnModifiedChanged()
		{
			var modifiedChanged = ModifiedChanged;
			if (modifiedChanged != null)
				modifiedChanged(this, EventArgs.Empty);
        }

		public int AddFiles(string[] filePaths, IProgress<ProgressEventArgs> progress)
		{
			return ReadTracks(p => p.AddTracks(filePaths), progress);
		}

		public int AddFolder(string folderPath, string filter, IProgress<ProgressEventArgs> progress)
		{
			return ReadTracks(p => p.AddFolder(folderPath, filter.Split(';')), progress);
		}

		public event EventHandler ModifiedChanged;
		public event EventHandler TracksChanged;

		#endregion

		#region Private Implementation

		private List<Track> _tracks = new List<Track>();

		protected virtual void OnTracksChanged()
		{
			var tracksChanged = TracksChanged;
			if (tracksChanged != null)
				TracksChanged(this, EventArgs.Empty);
		}

		private int ReadTracks(Action<Reader> run, IProgress<ProgressEventArgs> progress)
		{
			var reader = new Reader(progress);
			run(reader);
			var tracks = reader.Tracks;
			_tracks.AddRange(tracks);
			OnTracksChanged();
			return tracks.Count;
		}

		#endregion
	}
}
