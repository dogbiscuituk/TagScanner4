namespace TagScanner.Models
{
    using System;

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
            Continue = true;
            Count = count;
            Index = index;
            Path = path;
            Skip = false;
            Track = track;
        }
    }
}
