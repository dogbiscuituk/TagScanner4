using System;
using TagScanner.Models;

namespace TagScanner
{
	public class ProgressEventArgs : EventArgs
	{
		public bool Continue { get; set; }
		public int Count { get; set; }
		public int Index { get; set; }
		public string Path { get; set; }
		public bool Skip { get; set; }
		public Track Track { get; set; }

		public ProgressEventArgs(int index, int count, string path, Track track)
		{
			Count = count;
			Index = index;
			Path = path;
			Track = track;
			Continue = true;
			Skip = false;
		}
	}
}
