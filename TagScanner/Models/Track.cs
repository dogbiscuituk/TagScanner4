﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace TagScanner.Models
{
	[Serializable]
	public class Track : ITrack, INotifyPropertyChanged
	{
		#region Public Interface

		#region Constructors

		public Track() { }

		public Track(string filePath) : this()
		{
			FilePath = filePath;
			Load();
			IsNew = true;
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
		[XmlIgnore]
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
		[XmlIgnore]
		public bool IsNew;

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
				if (!SequenceEqual(AlbumArtists, value))
				{
					_albumArtists = value;
					OnPropertyChanged("AlbumArtists");
				}
			}
		}

		public int AlbumArtistsCount
		{
			get
			{
				return AlbumArtists.Length;
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
				if (!SequenceEqual(AlbumArtistsSort, value))
				{
					_albumArtistsSort = value;
					OnPropertyChanged("AlbumArtistsSort");
				}
			}
		}

		public int AlbumArtistsSortCount
		{
			get
			{
				return AlbumArtistsSort.Length;
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
				if (!SequenceEqual(Artists, value))
				{
					_artists = value;
					OnPropertyChanged("Artists");
				}
			}
		}

		public int ArtistsCount
		{
			get
			{
				return Artists.Length;
			}
		}

		public int AudioBitrate { get; set; }
		public int AudioChannels { get; set; }
		public int AudioSampleRate { get; set; }

		private int _beatsPerMinute;
		[DefaultValue(0)]
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

		public int BitsPerSample { get; set; }

		public string Century
		{
			get
			{
				return Year > 0 ? ((long)(Year + 99) / 100).AsOrdinal() : string.Empty;
			}
		}

		public string Codecs { get; set; }

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
				if (!SequenceEqual(Composers, value))
				{
					_composers = value;
					OnPropertyChanged("Composers");
				}
			}
		}

		public int ComposersCount
		{
			get
			{
				return Composers.Length;
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
				if (!SequenceEqual(ComposersSort, value))
				{
					_composersSort = value;
					OnPropertyChanged("ComposersSort");
				}
			}
		}

		public int ComposersSortCount
		{
			get
			{
				return ComposersSort.Length;
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

		public string Description { get; set; }

		[DefaultValue(0)]
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

		[DefaultValue(0)]
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
				return NumberOfTotal(DiscNumber, DiscCount, 1);
			}
		}

		public string DiscTrack
		{
			get
			{
				return string.Format("{0} - {1}", DiscOf, TrackOf);
			}
		}

		[XmlIgnore]
		public TimeSpan Duration { get; set; }

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[XmlElement(DataType ="duration", ElementName ="Duration")]
		public string DurationString
		{
			get
			{
				return XmlConvert.ToString(Duration);
			}
			set
			{
				Duration = string.IsNullOrWhiteSpace(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
			}
		}

		public string FileAttributes { get; set; }

		public DateTime FileCreationTime { get; set; }
		public DateTime FileCreationTimeUtc { get; set; }

		public string FileExtension
		{
			get
			{
				return Path.GetExtension(FilePath);
			}
		}

		public DateTime FileLastAccessTime { get; set; }

		public DateTime FileLastAccessTimeUtc { get; set; }

		public DateTime FileLastWriteTime { get; set; }

		public DateTime FileLastWriteTimeUtc { get; set; }

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

		public string FilePath { get; set; }

		public long FileSize { get; set; }

		public string FirstAlbumArtist { get; set; }
		public string FirstAlbumArtistSort { get; set; }
		public string FirstArtist { get; set; }
		public string FirstComposer { get; set; }
		public string FirstComposerSort { get; set; }
		public string FirstGenre { get; set; }
		public string FirstPerformer { get; set; }
		public string FirstPerformerSort { get; set; }

		private string[] _genres;
		public string[] Genres
		{
			get
			{
				return _genres;
			}
			set
			{
				if (!SequenceEqual(Genres, value))
				{
					_genres = value;
					OnPropertyChanged("Genres");
				}
			}
		}

		public int GenresCount
		{
			get
			{
				return Genres.Length;
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

		[DefaultValue(0)]
		public double ImageAltitude { get; set; }

		public string ImageCreator { get; set; }

		public DateTime ImageDateTime { get; set; }

		[DefaultValue(0)]
		public double ImageExposureTime { get; set; }

		[DefaultValue(0)]
		public double ImageFNumber { get; set; }

		[DefaultValue(0)]
		public double ImageFocalLength { get; set; }

		[DefaultValue(0)]
		public int ImageFocalLengthIn35mmFilm { get; set; }

		[DefaultValue(0)]
		public int ImageISOSpeedRatings { get; set; }

		public string[] ImageKeywords { get; set; }

		[DefaultValue(0)]
		public double ImageLatitude { get; set; }

		[DefaultValue(0)]
		public double ImageLongitude { get; set; }

		public string ImageMake { get; set; }

		public string ImageModel { get; set; }

		[DefaultValue(TagLib.Image.ImageOrientation.None)]
		public TagLib.Image.ImageOrientation ImageOrientation { get; set; }

		[DefaultValue(0)]
		public int ImageRating { get; set; }

		public string ImageSoftware { get; set; }

		public long InvariantEndPosition { get; set; }
		public long InvariantStartPosition { get; set; }

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

		public string JoinedAlbumArtists { get; set; }
		public string JoinedArtists { get; set; }
		public string JoinedComposers { get; set; }
		public string JoinedGenres { get; set; }
		public string JoinedPerformers { get; set; }
		public string JoinedPerformersSort { get; set; }

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

		public TagLib.MediaTypes MediaTypes { get; set; }

		public string Millennium
		{
			get
			{
				return Year > 0 ? ((long)(Year + 999) / 1000).AsOrdinal() : string.Empty;
			}
		}

		public string MimeType { get; set; }

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

		public string Name { get; set; }

		private string[] _performers;
		public string[] Performers
		{
			get
			{
				return _performers;
			}
			set
			{
				if (!SequenceEqual(Performers, value))
				{
					_performers = value;
					OnPropertyChanged("Performers");
				}
			}
		}

		public int PerformersCount
		{
			get
			{
				return Performers.Length;
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
				if (!SequenceEqual(PerformersSort, value))
				{
					_performersSort = value;
					OnPropertyChanged("PerformersSort");
				}
			}
		}

		public int PerformersSortCount
		{
			get
			{
				return PerformersSort.Length;
			}
		}

		[DefaultValue(0)]
		public int PhotoHeight { get; set; }

		[DefaultValue(0)]
		public int PhotoQuality { get; set; }

		[DefaultValue(0)]
		public int PhotoWidth { get; set; }

		public Picture[] Pictures { get; set; }

		private int _picturesCount;
		public int PicturesCount
		{
			get
			{
				return _picturesCount;
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
				if (IsNew)
					return IsModified ? TrackStatus.New | TrackStatus.Pending : TrackStatus.New;
				if (IsModified)
					return TrackStatus.Pending;
				// TODO: check for resolution inaccuracies & daylight saving time transitions.
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

		public TagLib.TagTypes TagTypes { get; set; }
		public TagLib.TagTypes TagTypesOnDisk { get; set; }

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
		[DefaultValue(0)]
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
		[DefaultValue(0)]
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
				return NumberOfTotal(TrackNumber, TrackCount, 2);
			}
		}

		[DefaultValue(0)]
		public int VideoHeight { get; set; }
		[DefaultValue(0)]
		public int VideoWidth { get; set; }

		private int _year;
		[DefaultValue(0)]
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

		#region INotifyPropertyChanged

		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Methods

		public object GetPropertyValue(string propertyName)
		{
			return GetPropertyInfo(propertyName).GetValue(this);
		}

		public void Load()
		{
			ReadMetadata();
			using (var file = GetTagLibFile())
				ReadFile(file);
			IsModified = false;
		}

		public void Save()
		{
			using (var file = GetTagLibFile())
			{
				WriteTag(file.Tag);
				file.Save();
			}
			Load();
		}

		public void SetPropertyValue(string propertyName, object value)
		{
			GetPropertyInfo(propertyName).SetValue(this, value);
			OnPropertyChanged(propertyName);
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

		private static PropertyInfo GetPropertyInfo(string propertyName)
		{
			return typeof(Track).GetProperty(propertyName);
		}

		private TagLib.File GetTagLibFile()
		{
			return TagLib.File.Create(FilePath);
		}

		private void InvokeHandler(PropertyChangedEventHandler propertyChanged, string propertyName)
		{
			propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			foreach (var dependentPropertyName in Metadata.GetDependentPropertyNames(propertyName))
				InvokeHandler(propertyChanged, dependentPropertyName);
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var propertyChanged = PropertyChanged;
			if (propertyChanged == null) // Are we just now streaming in, using XML?
				return; // Yes: then property accessors should have no side effects.
			IsModified = true;
			InvokeHandler(propertyChanged, propertyName);
			InvokeHandler(propertyChanged, "Status");
		}

		private void ReadFile(TagLib.File file)
		{
			if (file == null)
				return;
			InvariantEndPosition = file.InvariantEndPosition;
			InvariantStartPosition = file.InvariantStartPosition;
			MimeType = file.MimeType;
			Name = file.Name;
			_possiblyCorrupt = file.PossiblyCorrupt;
			TagTypes = file.TagTypes;
			TagTypesOnDisk = file.TagTypesOnDisk;
			ReadProperties(file.Properties);
			ReadTag(file.Tag);
		}

		private void ReadMetadata()
		{
			FileSize = new FileInfo(FilePath).Length;
			FileAttributes = File.GetAttributes(FilePath).ToString();
			FileCreationTime = File.GetCreationTimeUtc(FilePath);
			FileLastWriteTime = File.GetLastWriteTimeUtc(FilePath);
			FileLastAccessTime = File.GetLastAccessTimeUtc(FilePath);
			FileCreationTimeUtc = File.GetCreationTimeUtc(FilePath);
			FileLastWriteTimeUtc = File.GetLastWriteTimeUtc(FilePath);
			FileLastAccessTimeUtc = File.GetLastAccessTimeUtc(FilePath);
		}

		private void ReadProperties(TagLib.Properties properties)
		{
			if (properties == null)
				return;
			AudioBitrate = properties.AudioBitrate;
			AudioChannels = properties.AudioChannels;
			AudioSampleRate = properties.AudioSampleRate;
			BitsPerSample = properties.BitsPerSample;
			Codecs = properties.Codecs
				.Where(c => c != null)
				.Select(c => string.Format("{0} ({1} - {2:g})", c.MediaTypes, c.Description, c.Duration))
				.Aggregate((s, t) => s + "; " + t);
			Description = properties.Description;
			Duration = properties.Duration;
			MediaTypes = properties.MediaTypes; // 0 = None, 1 = Audio, 2 = Video, 4 = Photo, 8 = Text.
			PhotoHeight = properties.PhotoHeight;
			PhotoQuality = properties.PhotoQuality;
			PhotoWidth = properties.PhotoWidth;
			VideoHeight = properties.VideoHeight;
			VideoWidth = properties.VideoWidth;
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
			_picturesCount = tag.Pictures.Length;
			var pictureIndex = 0;
			Pictures = tag.Pictures.Select(p => new Picture(FilePath, pictureIndex++, p)).ToArray();
			_title = tag.Title;
			_titleSort = tag.TitleSort;
			_trackNumber = (int)tag.Track;
			_trackCount = (int)tag.TrackCount;
			_year = (int)tag.Year;
			var imageTag = tag as TagLib.Image.ImageTag;
            if (imageTag != null)
			{
				ImageAltitude = imageTag.Altitude ?? 0;
				ImageCreator = imageTag.Creator;
				ImageDateTime = imageTag.DateTime ?? DateTime.MinValue;
				ImageExposureTime = imageTag.ExposureTime ?? 0;
				ImageFNumber = imageTag.FNumber ?? 0;
				ImageFocalLength = imageTag.FocalLength ?? 0;
				ImageFocalLengthIn35mmFilm = (int)(imageTag.FocalLengthIn35mmFilm ?? 0);
				ImageISOSpeedRatings = (int)(imageTag.ISOSpeedRatings ?? 0);
				ImageKeywords = imageTag.Keywords;
				ImageLatitude = imageTag.Latitude ?? 0;
				ImageLongitude = imageTag.Longitude ?? 0;
				ImageMake = imageTag.Make;
				ImageModel = imageTag.Model;
				ImageOrientation = imageTag.Orientation;
				ImageRating = (int)(imageTag.Rating ?? 0);
				ImageSoftware = imageTag.Software;
			}
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
			var imageTag = tag as TagLib.Image.ImageTag;
			if (imageTag != null)
			{
				imageTag.Altitude = ImageAltitude;
                imageTag.Creator = ImageCreator;
				imageTag.DateTime = ImageDateTime;
				imageTag.ExposureTime = ImageExposureTime;
				imageTag.FNumber = ImageFNumber;
				imageTag.FocalLength = ImageFocalLength;
				imageTag.FocalLengthIn35mmFilm = (uint)ImageFocalLengthIn35mmFilm;
				imageTag.ISOSpeedRatings = (uint)ImageISOSpeedRatings;
                imageTag.Keywords = ImageKeywords;
				imageTag.Latitude = ImageLatitude;
				imageTag.Longitude = ImageLongitude;
				imageTag.Make = ImageMake;
				imageTag.Model = ImageModel;
				imageTag.Orientation = ImageOrientation;
				imageTag.Rating = (uint)ImageRating;
				imageTag.Software = ImageSoftware;
			}
		}

		private static string NumberOfTotal(int number, int total, int digits)
		{
			number = Math.Max(number, 1);
			total = Math.Max(number, total);
			digits = Math.Max(digits, total.ToString().Length);
            return string.Format(string.Format("{{0:D{0}}}/{{1:D{0}}}", digits), number, total);
		}

		private static bool SequenceEqual(IEnumerable<string> x, IEnumerable<string> y)
		{
			return x == null ? y == null : y != null && Enumerable.SequenceEqual(x, y);
		}

		#endregion
	}
}
