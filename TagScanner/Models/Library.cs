namespace TagScanner.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class Library
    {
        private List<string> _folders = new List<string>();
        public List<string> Folders
        {
            get => _folders;
            set => _folders = value;
        }

        private List<Track> _tracks = new List<Track>();
        public List<Track> Tracks
        {
            get => _tracks;
            set => _tracks = value;
        }

        public void Clear()
        {
            Folders.Clear();
            Tracks.Clear();
        }
    }
}
