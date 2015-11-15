using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagScanner.Models
{
	public class Reader
	{
		public Reader(IProgress<ProgressEventArgs> progress)
		{
			Progress = progress;
		}

		#region State

		public readonly List<Track> Tracks = new List<Track>();
		private int TrackIndex, TrackCount;
		private IProgress<ProgressEventArgs> Progress;

		#endregion

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

		private bool AddTrack(string filePath)
		{
			var success = false;
			try
			{
				var tagFile = new Track(filePath);
				Tracks.Add(tagFile);
				TrackIndex++;
				success = true;
			}
			catch (TagLib.CorruptFileException ex)
			{
				LogException(ex, filePath);
				TrackCount--;
			}
			catch (TagLib.UnsupportedFormatException ex)
			{
				LogException(ex, filePath);
				TrackCount--;
			}
			var progressEventArgs = new ProgressEventArgs(TrackIndex, TrackCount, filePath, success);
			Progress.Report(progressEventArgs);
			return progressEventArgs.Continue;
		}

		private bool DoAddTracks(IEnumerable<string> filePathList)
		{
			return filePathList.FirstOrDefault(p => !AddTrack(p)) == null;
		}

		private static void LogException(Exception ex, string filePath)
		{
			System.Diagnostics.Debug.WriteLine("{0} - {1} - {2}", ex.GetType(), ex.Message, filePath);
        }
	}
}
