namespace TagScanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using TagScanner.Logging;

    public class Reader
    {
        public Reader(List<string> existingFilePaths, IProgress<ProgressEventArgs> progress)
        {
            ExistingFilePaths = existingFilePaths;
            Progress = progress;
        }

        public void AddFolder(string folderPath, IEnumerable<string> searchPatterns)
        {
            if (!Directory.Exists(folderPath))
                return;
            var filePathLists = new List<IEnumerable<string>>();
            foreach (var searchPattern in searchPatterns)
            {
                var filePathList = Directory.EnumerateFiles(folderPath, searchPattern, SearchOption.AllDirectories);
                TrackCount += filePathList.Count();
                filePathLists.Add(filePathList);
            }
            foreach (var filePathList in filePathLists)
                if (!DoAddTracks(filePathList))
                    break;
        }

        public void AddTracks(IEnumerable<string> filePathList)
        {
            TrackCount += filePathList.Count();
            DoAddTracks(filePathList);
        }

        #region State

        public readonly List<string> ExistingFilePaths;
        public readonly List<Track> Tracks = new List<Track>();
        private int TrackIndex, TrackCount;
        private readonly IProgress<ProgressEventArgs> Progress;

        #endregion

        private bool DoAddTrack(string filePath)
        {
            Track track = null;
            try
            {
                if (!ExistingFilePaths.Contains(filePath))
                {
                    ExistingFilePaths.Add(filePath);
                    track = new Track(filePath);
                    Tracks.Add(track);
                }
                TrackIndex++;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, filePath);
                TrackCount--;
            }
            var progressEventArgs = new ProgressEventArgs(TrackIndex, TrackCount, filePath, track);
            Progress.Report(progressEventArgs);
            return progressEventArgs.Continue;
        }

        private bool DoAddTracks(IEnumerable<string> filePathList) => filePathList.FirstOrDefault(p => !DoAddTrack(p)) == null;
    }
}
