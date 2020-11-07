using System;
using System.Collections.Generic;

namespace TagScanner.Models
{
	[Serializable]
	public class Library
	{
		private List<string> _folders = new List<string>();
		public List<string> Folders
		{
			get
			{
				return _folders;
			}
			set
			{
				_folders = value;
			}
		}

		private List<Track> _tracks = new List<Track>();
		public List<Track> Tracks
		{
			get
			{
				return _tracks;
			}
			set
			{
				_tracks = value;
			}
		}

		public void Clear()
		{
			Folders.Clear();
			Tracks.Clear();
		}
	}
}
