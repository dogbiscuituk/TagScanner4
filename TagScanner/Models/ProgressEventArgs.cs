using System;

namespace TagScanner
{
	public class ProgressEventArgs : EventArgs
	{
		public bool Continue { get; set; }
		public int Count { get; set; }
		public int Index { get; set; }
		public string Path { get; set; }
		public bool Success { get; set; }

		public ProgressEventArgs(int index, int count, string path, bool success)
		{
			Continue = true;
			Count = count;
			Index = index;
			Path = path;
			Success = success;
		}
	}
}
