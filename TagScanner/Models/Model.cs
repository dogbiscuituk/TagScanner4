using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TagScanner.Models
{
	[Serializable]
	public class Model
	{
		#region Public Interface

		private Library _library = new Library();
		public Library Library
		{
			get
			{
				return _library;
			}
			set
			{
				_library = value;
				OnTracksChanged();
			}
		}

		public List<string> Folders
		{
			get
			{
				return Library.Folders;
			}
		}

		public List<Track> Tracks
		{
			get
			{
				return Library.Tracks;
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

		public int AddFiles(string[] filePaths, IProgress<ProgressEventArgs> progress)
		{
			return ReadTracks(p => p.AddTracks(filePaths), progress);
		}

		public int AddFolder(string folderPath, string filter, IProgress<ProgressEventArgs> progress)
		{
			var folder = string.Concat(folderPath, '|', filter);
			if (!Folders.Contains(folder))
				Folders.Add(folder);
			return ReadTracks(p => p.AddFolder(folderPath, filter.Split(';')), progress);
		}

		public void Clear()
		{
			Library.Clear();
		}

		public bool ProcessTrack(Track track)
		{
			switch (track.Status)
			{
				case TrackStatus.New:
					return AddTrack(track);
				case TrackStatus.Updated:
					return LoadTrack(track);
				case TrackStatus.Pending:
					return SaveTrack(track);
				case TrackStatus.Deleted:
					return DropTrack(track);
			}
			return false;
		}

		public void Track_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			Modified = true;
		}

		public event EventHandler ModifiedChanged;
		public event EventHandler TracksChanged;

		#endregion

		#region Private Implementation

		private bool AddTrack(Track track)
		{
			track.IsNew = false;
			return true;
		}

		private bool DropTrack(Track track)
		{
			return Tracks.Remove(track);
		}

		private bool LoadTrack(Track track)
		{
			track.Load();
			return true;
		}

		protected virtual void OnModifiedChanged()
		{
			var modifiedChanged = ModifiedChanged;
			if (modifiedChanged != null)
				modifiedChanged(this, EventArgs.Empty);
		}

		protected virtual void OnTracksChanged()
		{
			var tracksChanged = TracksChanged;
			if (tracksChanged != null)
				TracksChanged(this, EventArgs.Empty);
		}

		private int ReadTracks(Action<Reader> action, IProgress<ProgressEventArgs> progress)
		{
			var existingFilePaths = Tracks.Select(t => t.FilePath).ToList();
			var reader = new Reader(existingFilePaths, progress);
			action(reader);
			var tracks = reader.Tracks;
			Tracks.AddRange(tracks);
			OnTracksChanged();
			return tracks.Count;
		}

		private bool SaveTrack(Track track)
		{
			track.Save();
			return true;
		}

		#endregion
	}
}
