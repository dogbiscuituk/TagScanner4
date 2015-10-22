using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace TagScanner.Models
{
	[Serializable]
	public class Track : ITrack
	{
		#region Public Interface

		#region Constructor

		public Track(string filePath)
		{
			FilePath = filePath;
			Load();
		}

		#endregion

		#region Properties

		private bool FileExists
		{
			get
			{
				return File.Exists(FilePath);
			}
		}

		[NonSerialized]
		private bool _isModified;
		public bool IsModified
		{
			get
			{
				return _isModified;
			}
			set
			{
				_isModified = value;
			}
		}

		[NonSerialized]
		private IList<IObserveTracks> _observers;
        public IList<IObserveTracks> Observers
		{
			get
			{
				return _observers ?? (_observers = new List<IObserveTracks>());
			}
		}

		#endregion

		#region ITrack

		private string _album;
		public string Album
		{
			get
			{
				return _album;
			}
			set
			{
				if (Album != value)
				{
					_album = value;
					OnPropertyChanged("Album");
				}
			}
		}

		private string[] _albumArtists;
		public string[] AlbumArtists
		{
			get
			{
				return _albumArtists;
			}
			set
			{
				if (!Enumerable.SequenceEqual(AlbumArtists, value))
				{
					_albumArtists = value;
					OnPropertyChanged("AlbumArtists");
				}
			}
		}

		private string[] _albumArtistsSort;
		public string[] AlbumArtistsSort
		{
			get
			{
				return _albumArtistsSort;
			}
			set
			{
				if (!Enumerable.SequenceEqual(AlbumArtistsSort, value))
				{
					_albumArtistsSort = value;
					OnPropertyChanged("AlbumArtistsSort");
				}
			}
		}

		public string AlbumIndex
		{
			get
			{
				return AlbumSort.Coalesce(Album).GetIndex();
			}
		}

		private string _albumSort;
		public string AlbumSort
		{
			get
			{
				return _albumSort;
			}
			set
			{
				if (AlbumSort != value)
				{
					_albumSort = value;
					OnPropertyChanged("AlbumSort");
				}
			}
		}

		private string _amazonId;
		public string AmazonId
		{
			get
			{
				return _amazonId;
			}
			set
			{
				if (AmazonId != value)
				{
					_amazonId = value;
					OnPropertyChanged("AmazonId");
				}
			}
		}

		private string[] _artists;
		public string[] Artists
		{
			get
			{
				return _artists;
			}
			set
			{
				if (!Enumerable.SequenceEqual(Artists, value))
				{
					_artists = value;
					OnPropertyChanged("Artists");
				}
			}
		}

		private int _audioBitrate;
		public int AudioBitrate
		{
			get
			{
				return _audioBitrate;
			}
		}

		private int _audioChannels;
		public int AudioChannels
		{
			get
			{
				return _audioChannels;
			}
		}

		private int _audioSampleRate;
		public int AudioSampleRate
		{
			get
			{
				return _audioSampleRate;
			}
		}

		private int _beatsPerMinute;
		public int BeatsPerMinute
		{
			get
			{
				return _beatsPerMinute;
			}
			set
			{
				if (BeatsPerMinute != value)
				{
					_beatsPerMinute = value;
					OnPropertyChanged("BeatsPerMinute");
				}
			}
		}

		private int _bitsPerSample;
		public int BitsPerSample
		{
			get
			{
				return _bitsPerSample;
			}
		}

		public string Century
		{
			get
			{
				return Year > 0 ? ((long)(Year + 99) / 100).AsOrdinal() : string.Empty;
			}
		}

		public string Codecs { get; private set; }

		private string _comment;
		public string Comment
		{
			get
			{
				return _comment;
			}
			set
			{
				if (Comment != value)
				{
					_comment = value;
					OnPropertyChanged("Comment");
				}
			}
		}

		private string[] _composers;
		public string[] Composers
		{
			get
			{
				return _composers;
			}
			set
			{
				if (!Enumerable.SequenceEqual(Composers, value))
				{
					_composers = value;
					OnPropertyChanged("Composers");
				}
			}
		}

		private string[] _composersSort;
		public string[] ComposersSort
		{
			get
			{
				return _composersSort;
			}
			set
			{
				if (!Enumerable.SequenceEqual(ComposersSort, value))
				{
					_composersSort = value;
					OnPropertyChanged("ComposersSort");
				}
			}
		}

		private string _conductor;
		public string Conductor
		{
			get
			{
				return _conductor;
			}
			set
			{
				if (Conductor != value)
				{
					_conductor = value;
					OnPropertyChanged("Conductor");
				}
			}
		}

		private string _copyright;
		public string Copyright
		{
			get
			{
				return _copyright;
			}
			set
			{
				if (Copyright != value)
				{
					_copyright = value;
					OnPropertyChanged("Copyright");
				}
			}
		}

		public string Decade
		{
			get
			{
				return Year > 0 ? string.Format("{0}0s", Year / 10) : string.Empty;
			}
		}

		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
		}

		private int _discCount;
		public int DiscCount
		{
			get
			{
				return _discCount;
			}
			set
			{
				if (DiscCount != value)
				{
					_discCount = value;
					OnPropertyChanged("DiscCount");
				}
			}
		}

		private int _discNumber;
		public int DiscNumber
		{
			get
			{
				return _discNumber;
			}
			set
			{
				if (DiscNumber != value)
				{
					_discNumber = value;
					OnPropertyChanged("DiscNumber");
				}
			}
		}

		public string DiscOf
		{
			get
			{
				return NumberOfTotal("{0}/{1}", DiscNumber, DiscCount);
			}
		}

		public string DiscTrack
		{
			get
			{
				var discOf = DiscOf;
				if (discOf == null)
					return null;
				var trackOf = TrackOf;
				if (trackOf == null)
					return null;
				return string.Format("{0} - {1}", discOf, trackOf);
			}
		}

		private TimeSpan _duration;
		public TimeSpan Duration
		{
			get
			{
				return _duration;
			}
		}

		public FileAttributes FileAttributes { get; private set; }

		private DateTime _fileCreationTime;
		public DateTime FileCreationTime
		{
			get
			{
				return _fileCreationTime;
			}
		}

		private DateTime _fileCreationTimeUtc;
		public DateTime FileCreationTimeUtc
		{
			get
			{
				return _fileCreationTimeUtc;
			}
		}

		public string FileExtension
		{
			get
			{
				return Path.GetExtension(FilePath);
			}
		}

		private DateTime _fileLastAccessTime;
		public DateTime FileLastAccessTime
		{
			get
			{
				return _fileLastAccessTime;
			}
		}

		private DateTime _fileLastAccessTimeUtc;
		public DateTime FileLastAccessTimeUtc
		{
			get
			{
				return _fileLastAccessTimeUtc;
			}
		}

		private DateTime _fileLastWriteTime;
		public DateTime FileLastWriteTime
		{
			get
			{
				return _fileLastWriteTime;
			}
		}

		private DateTime _fileLastWriteTimeUtc;
		public DateTime FileLastWriteTimeUtc
		{
			get
			{
				return _fileLastWriteTimeUtc;
			}
		}

		private long _fileLength;
		public long FileLength
		{
			get
			{
				return _fileLength;
			}
		}

		public string FileName
		{
			get
			{
				return Path.GetFileName(FilePath);
			}
		}

		public string FileNameWithoutExtension
		{
			get
			{
				return Path.GetFileNameWithoutExtension(FilePath);
			}
		}

		public string FilePath { get; private set; }

		private long _fileSize;
		public long FileSize
		{
			get
			{
				return _fileSize;
			}
		}

		public string FirstAlbumArtist { get; private set; }
		public string FirstAlbumArtistSort { get; private set; }
		public string FirstArtist { get; private set; }
		public string FirstComposer { get; private set; }
		public string FirstComposerSort { get; private set; }
		public string FirstGenre { get; private set; }
		public string FirstPerformer { get; private set; }
		public string FirstPerformerSort { get; private set; }

		private string[] _genres;
		public string[] Genres
		{
			get
			{
				return _genres;
			}
			set
			{
				if (!Enumerable.SequenceEqual(Genres, value))
				{
					_genres = value;
					OnPropertyChanged("Genres");
				}
			}
		}

		private string _grouping;
		public string Grouping
		{
			get
			{
				return _grouping;
			}
			set
			{
				if (Grouping != value)
				{
					_grouping = value;
					OnPropertyChanged("Grouping");
				}
			}
		}

		private long _invariantEndPosition;
		public long InvariantEndPosition
		{
			get
			{
				return _invariantEndPosition;
			}
		}

		private long _invariantStartPosition;
		public long InvariantStartPosition
		{
			get
			{
				return _invariantStartPosition;
			}
		}

		public Logical IsClassical
		{
			get
			{
				return (FirstGenre == "Classical").AsLogical();
			}
		}

		private bool _isEmpty;
		public Logical IsEmpty
		{
			get
			{
				return _isEmpty.AsLogical();
			}
		}

		public string JoinedAlbumArtists { get; private set; }
		public string JoinedArtists { get; private set; }
		public string JoinedComposers { get; private set; }
		public string JoinedGenres { get; private set; }
		public string JoinedPerformers { get; private set; }

		public string JoinedPerformersIndex
		{
			get
			{
				return JoinedPerformersSort.Coalesce(JoinedPerformers).GetIndex();
			}
		}

		public string JoinedPerformersSort { get; private set; }

		private long _length;
		public long Length
		{
			get
			{
				return _length;
			}
		}

		private string _lyrics;
		public string Lyrics
		{
			get
			{
				return _lyrics;
			}
			set
			{
				if (Lyrics != value)
				{
					_lyrics = value;
					OnPropertyChanged("Lyrics");
				}
			}
		}

		private TagLib.MediaTypes _mediaTypes;
		public TagLib.MediaTypes MediaTypes
		{
			get
			{
				return _mediaTypes;
			}
		}

		public string Millennium
		{
			get
			{
				return Year > 0 ? ((long)(Year + 999) / 1000).AsOrdinal() : string.Empty;
			}
		}

		public string MimeType { get; private set; }

		private string _musicBrainzArtistId;
		public string MusicBrainzArtistId
		{
			get
			{
				return _musicBrainzArtistId;
			}
			set
			{
				if (MusicBrainzArtistId != value)
				{
					_musicBrainzArtistId = value;
					OnPropertyChanged("MusicBrainzArtistId");
				}
			}
		}

		private string _musicBrainzDiscId;
		public string MusicBrainzDiscId
		{
			get
			{
				return _musicBrainzDiscId;
			}
			set
			{
				if (MusicBrainzArtistId != value)
				{
					MusicBrainzArtistId = value;
					OnPropertyChanged("MusicBrainzDiscId");
				}
			}
		}

		private string _musicBrainzReleaseArtistId;
		public string MusicBrainzReleaseArtistId
		{
			get
			{
				return _musicBrainzReleaseArtistId;
			}
			set
			{
				if (MusicBrainzReleaseArtistId != value)
				{
					_musicBrainzReleaseArtistId = value;
					OnPropertyChanged("MusicBrainzReleaseArtistId");
				}
			}
		}

		private string _musicBrainzReleaseCountry;
		public string MusicBrainzReleaseCountry
		{
			get
			{
				return _musicBrainzReleaseCountry;
			}
			set
			{
				if (MusicBrainzReleaseCountry != value)
				{
					_musicBrainzReleaseCountry = value;
					OnPropertyChanged("MusicBrainzReleaseCountry");
				}
			}
		}

		private string _musicBrainzReleaseId;
		public string MusicBrainzReleaseId
		{
			get
			{
				return _musicBrainzReleaseId;
			}
			set
			{
				if (MusicBrainzReleaseId != value)
				{
					_musicBrainzReleaseId = value;
					OnPropertyChanged("MusicBrainzReleaseId");
				}
			}
		}

		private string _musicBrainzReleaseStatus;
		public string MusicBrainzReleaseStatus
		{
			get
			{
				return _musicBrainzReleaseStatus;
			}
			set
			{
				if (MusicBrainzReleaseStatus != value)
				{
					_musicBrainzReleaseStatus = value;
					OnPropertyChanged("MusicBrainzReleaseStatus");
				}
			}
		}

		private string _musicBrainzReleaseType;
		public string MusicBrainzReleaseType
		{
			get
			{
				return _musicBrainzReleaseType;
			}
			set
			{
				if (MusicBrainzReleaseType != value)
				{
					_musicBrainzReleaseType = value;
					OnPropertyChanged("MusicBrainzReleaseType");
				}
			}
		}

		private string _musicBrainzTrackId;
		public string MusicBrainzTrackId
		{
			get
			{
				return _musicBrainzTrackId;
			}
			set
			{
				if (MusicBrainzTrackId != value)
				{
					_musicBrainzTrackId = value;
					OnPropertyChanged("MusicBrainzTrackId");
				}
			}
		}

		private string _musicIpId;
		public string MusicIpId
		{
			get
			{
				return _musicIpId;
			}
			set
			{
				if (MusicIpId != value)
				{
					_musicIpId = value;
					OnPropertyChanged("MusicIpId");
				}
			}
		}

		public string Name { get; private set; }

		private string[] _performers;
		public string[] Performers
		{
			get
			{
				return _performers;
			}
			set
			{
				if (!Enumerable.SequenceEqual(Performers, value))
				{
					_performers = value;
					OnPropertyChanged("Performers");
				}
			}
		}

		private string[] _performersSort;
		public string[] PerformersSort
		{
			get
			{
				return _performersSort;
			}
			set
			{
				if (!Enumerable.SequenceEqual(PerformersSort, value))
				{
					_performersSort = value;
					OnPropertyChanged("PerformersSort");
				}
			}
		}

		private int _photoHeight;
		public int PhotoHeight
		{
			get
			{
				return _photoHeight;
			}
		}

		private int _photoQuality;
		public int PhotoQuality
		{
			get
			{
				return _photoQuality;
			}
		}

		private int _photoWidth;
		public int PhotoWidth
		{
			get
			{
				return _photoWidth;
			}
		}

		private int _pictureCount;
		public int PictureCount
		{
			get
			{
				return _pictureCount;
			}
		}

		private Picture[] _pictures;
		public Picture[] Pictures
		{
			get
			{
				return _pictures;
			}
		}

		private bool _possiblyCorrupt;
		public Logical PossiblyCorrupt
		{
			get
			{
				return _possiblyCorrupt.AsLogical();
			}
		}

		public TrackStatus Status
		{
			get
			{
				if (!FileExists)
					return TrackStatus.Deleted;
				if (IsModified)
					return TrackStatus.Pending;
				var elapsedTime = FileLastWriteTimeUtc - File.GetLastWriteTimeUtc(FilePath);
				switch (Math.Sign(elapsedTime.Ticks))
				{
					case +1:
						return TrackStatus.Pending;
					case -1:
						return TrackStatus.Updated;
				}
				return TrackStatus.Current;
			}
		}

		private TagLib.TagTypes _tagTypes;
		public TagLib.TagTypes TagTypes
		{
			get
			{
				return _tagTypes;
			}
		}

		private TagLib.TagTypes _tagTypesOnDisk;
		public TagLib.TagTypes TagTypesOnDisk
		{
			get
			{
				return _tagTypesOnDisk;
			}
		}

		private string _title;
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				if (Title != value)
				{
					_title = value;
					OnPropertyChanged("Title");
				}
			}
		}

		public string TitleIndex
		{
			get
			{
				return TitleSort.Coalesce(Title).GetIndex();
			}
		}

		private string _titleSort;
		public string TitleSort
		{
			get
			{
				return _titleSort;
			}
			set
			{
				if (TitleSort != value)
				{
					_titleSort = value;
					OnPropertyChanged("TitleSort");
				}
			}
		}

		private int _trackCount;
		public int TrackCount
		{
			get
			{
				return _trackCount;
			}
			set
			{
				if (TrackCount != value)
				{
					_trackCount = value;
					OnPropertyChanged("TrackCount");
				}
			}
		}

		private int _trackNumber;
		public int TrackNumber
		{
			get
			{
				return _trackNumber;
			}
			set
			{
				if (TrackNumber != value)
				{
					_trackNumber = value;
					OnPropertyChanged("TrackNumber");
				}
			}
		}

		public string TrackOf
		{
			get
			{
				return NumberOfTotal("{0:D2}/{1:D2}", TrackNumber, TrackCount);
			}
		}

		private int _videoHeight;
		public int VideoHeight
		{
			get
			{
				return _videoHeight;
			}
		}

		private int _videoWidth;
		public int VideoWidth
		{
			get
			{
				return _videoWidth;
			}
		}

		private int _year;

		public int Year
		{
			get
			{
				return _year;
			}
			set
			{
				if (Year != value)
				{
					_year = value;
					OnPropertyChanged("Year");
				}
			}
		}

		public string YearAlbum
		{
			get
			{
				return string.Format("{0} - {1}", Year, Album);
			}
		}

		#endregion

		#region Methods

		public void Load()
		{
			ReadMetadata();
			using (var file = GetTagLibFile())
				ReadFile(file);
		}

		public void Save()
		{
			using (var file = GetTagLibFile())
			{
				WriteTag(file.Tag);
				file.Save();
			}
			Load();
			IsModified = false;
		}

		public override string ToString()
		{
			return string.Format(
				"{0} | {1} | {2} {3} ({4}) {5}",
				JoinedPerformers,
				YearAlbum,
				TrackOf,
				Title,
				Duration.AsString(false),
				FileSize.AsString(true));
		}

		#endregion

		#endregion

		#region Private Implementation

		private TagLib.File GetTagLibFile()
		{
			return TagLib.File.Create(FilePath);
		}

		private static string NumberOfTotal(string format, int number, int total)
		{
			number = Math.Max(number, 1);
			return string.Format(format, number, Math.Max(total, number));
		}

		private void OnPropertyChanged(string propertyName)
		{
			IsModified = true;
			if (_observers != null)
				foreach (var observer in Observers)
					observer.TrackPropertyChanged(this, propertyName);
		}

		private void ReadFile(TagLib.File file)
		{
			if (file == null)
				return;
			_fileLength = file.Length;
			_invariantEndPosition = file.InvariantEndPosition;
			_invariantStartPosition = file.InvariantStartPosition;
			_length = file.Length;
			MimeType = file.MimeType;
			Name = file.Name;
			_possiblyCorrupt = file.PossiblyCorrupt;
			_tagTypes = file.TagTypes;
			_tagTypesOnDisk = file.TagTypesOnDisk;
			ReadProperties(file.Properties);
			ReadTag(file.Tag);
		}

		private void ReadMetadata()
		{
			_fileSize = new FileInfo(FilePath).Length;
			FileAttributes = File.GetAttributes(FilePath);
			_fileCreationTime = File.GetCreationTimeUtc(FilePath);
			_fileLastWriteTime = File.GetLastWriteTimeUtc(FilePath);
			_fileLastAccessTime = File.GetLastAccessTimeUtc(FilePath);
			_fileCreationTimeUtc = File.GetCreationTimeUtc(FilePath);
			_fileLastWriteTimeUtc = File.GetLastWriteTimeUtc(FilePath);
			_fileLastAccessTimeUtc = File.GetLastAccessTimeUtc(FilePath);
		}

		private void ReadProperties(TagLib.Properties properties)
		{
			if (properties == null)
				return;
			_audioBitrate = properties.AudioBitrate;
			_audioChannels = properties.AudioChannels;
			_audioSampleRate = properties.AudioSampleRate;
			_bitsPerSample = properties.BitsPerSample;
			Codecs = properties.Codecs.ToString();
			_description = properties.Description;
			_duration = properties.Duration;
			_mediaTypes = properties.MediaTypes;
			_photoHeight = properties.PhotoHeight;
			_photoQuality = properties.PhotoQuality;
			_photoWidth = properties.PhotoWidth;
			_videoHeight = properties.VideoHeight;
			_videoWidth = properties.VideoWidth;
		}

		private void ReadTag(TagLib.Tag tag)
		{
			if (tag == null)
				return;
			_album = tag.Album;
			_albumArtists = tag.AlbumArtists;
			_albumArtistsSort = tag.AlbumArtistsSort;
			_albumSort = tag.AlbumSort;
			_amazonId = tag.AmazonId;
#pragma warning disable 612,618
			_artists = tag.Artists;
#pragma warning restore 612, 618
			_beatsPerMinute = (int)tag.BeatsPerMinute;
			_comment = tag.Comment;
			_composers = tag.Composers;
			_composersSort = tag.ComposersSort;
			_conductor = tag.Conductor;
			_copyright = tag.Copyright;
			_discNumber = (int)tag.Disc;
			_discCount = (int)tag.DiscCount;
			FirstAlbumArtist = tag.FirstAlbumArtist;
			FirstAlbumArtistSort = tag.FirstAlbumArtistSort;
#pragma warning disable 612,618
			FirstArtist = tag.FirstArtist;
#pragma warning restore 612,618
			FirstComposer = tag.FirstComposer;
			FirstComposerSort = tag.FirstComposerSort;
			FirstGenre = tag.FirstGenre;
			FirstPerformer = tag.FirstPerformer;
			FirstPerformerSort = tag.FirstPerformerSort;
			_genres = tag.Genres;
			_grouping = tag.Grouping;
			_isEmpty = tag.IsEmpty;
			JoinedAlbumArtists = tag.JoinedAlbumArtists;
#pragma warning disable 612,618
			JoinedArtists = tag.JoinedArtists;
#pragma warning restore 612,618
			JoinedComposers = tag.JoinedComposers;
			JoinedGenres = tag.JoinedGenres;
			JoinedPerformers = tag.JoinedPerformers;
			JoinedPerformersSort = tag.JoinedPerformersSort;
			_lyrics = tag.Lyrics;
			_musicBrainzArtistId = tag.MusicBrainzArtistId;
			_musicBrainzDiscId = tag.MusicBrainzDiscId;
			_musicBrainzReleaseArtistId = tag.MusicBrainzReleaseArtistId;
			_musicBrainzReleaseCountry = tag.MusicBrainzReleaseCountry;
			_musicBrainzReleaseId = tag.MusicBrainzReleaseId;
			_musicBrainzReleaseStatus = tag.MusicBrainzReleaseStatus;
			_musicBrainzReleaseType = tag.MusicBrainzReleaseType;
			_musicBrainzTrackId = tag.MusicBrainzTrackId;
			_musicIpId = tag.MusicIpId;
			_performers = tag.Performers;
			_performersSort = tag.PerformersSort;
			_pictureCount = tag.Pictures.Length;
			var pictureIndex = 0;
			_pictures = tag.Pictures.Select(q => new Picture(FilePath, pictureIndex++, q)).ToArray();
			_title = tag.Title;
			_titleSort = tag.TitleSort;
			_trackNumber = (int)tag.Track;
			_trackCount = (int)tag.TrackCount;
			_year = (int)tag.Year;
		}

		private void WriteTag(TagLib.Tag tag)
		{
			if (tag == null)
				return;
			tag.Album = _album;
			tag.AlbumArtists = _albumArtists;
			tag.AlbumArtistsSort = _albumArtistsSort;
			tag.AlbumSort = _albumSort;
			tag.AmazonId = _amazonId;
#pragma warning disable 612, 618
			tag.Artists = _artists;
#pragma warning restore 612, 618
			tag.BeatsPerMinute = (uint)_beatsPerMinute;
			tag.Comment = _comment;
			tag.Composers = _composers;
			tag.ComposersSort = _composersSort;
			tag.Conductor = _conductor;
			tag.Copyright = _copyright;
			tag.Disc = (uint)_discNumber;
			tag.DiscCount = (uint)_discCount;
			tag.Genres = _genres;
			tag.Grouping = _grouping;
			tag.Lyrics = _lyrics;
			tag.MusicBrainzArtistId = _musicBrainzArtistId;
			tag.MusicBrainzDiscId = _musicBrainzDiscId;
			tag.MusicBrainzReleaseArtistId = _musicBrainzReleaseArtistId;
			tag.MusicBrainzReleaseCountry = _musicBrainzReleaseCountry;
			tag.MusicBrainzReleaseId = _musicBrainzReleaseId;
			tag.MusicBrainzReleaseStatus = _musicBrainzReleaseStatus;
			tag.MusicBrainzReleaseType = _musicBrainzReleaseType;
			tag.MusicBrainzTrackId = _musicBrainzTrackId;
			tag.MusicIpId = _musicIpId;
			tag.Performers = _performers;
			tag.PerformersSort = _performersSort;
			tag.Title = _title;
			tag.TitleSort = _titleSort;
			tag.Track = (uint)_trackNumber;
			tag.TrackCount = (uint)_trackCount;
			tag.Year = (uint)_year;
		}

		#endregion
	}
}
