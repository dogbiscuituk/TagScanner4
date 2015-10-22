using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TagScanner.Models;

namespace TagScanner
{
	[DefaultProperty("Title")]
	public class Selection : ITrack
	{
		#region Lifetime Management

		public Selection(IEnumerable<ITrack> files)
		{
			Tracks = files;
		}

		#endregion

		#region Properties

		public readonly IEnumerable<ITrack> Tracks;

		[Category("Selection")]
		[Description("The total number of tracks in the current selection.")]
		public int TotalTracks
		{
			get
			{
				return Tracks.Count();
			}
		}

		[Category("Selection")]
		[Description("The number of unique album titles in the current selection.")]
		public int UniqueAlbums
		{
			get
			{
				return Tracks.Select(f => f.Album).Distinct().Count();
			}
		}

		[Category("Selection")]
		[Description("The number of unique artists in the current selection.")]
		public int UniqueArtists
		{
			get
			{
				return Tracks.Select(f => f.JoinedPerformers).Distinct().Count();
			}
		}

		[Category("Selection")]
		[Description("The number of unique genres in the current selection.")]
		public int UniqueGenres
		{
			get
			{
				return Tracks.SelectMany(f => f.Genres).Distinct().Count();
			}
		}

		#endregion

		#region ITrack

		private string _album;
		[Category("Details")]
		[Description("A string containing the album of the media represented by the selected item(s), or null if no value is present.")]
		public string Album
		{
			get { return GetString(p => p.Album, ref _album); }
			set
			{
				SetValue(p => p.Album = value);
				_album = null;
				_yearAlbum = null;
			}
		}

		private string[] _albumArtists;
		[Category("Personnel")]
		[Description("A string array containing the band or artist who is credited in the creation of the entire album or collection containing the media described by the selected item(s), or an empty array if no value is present.")]
		public string[] AlbumArtists
		{
			get { return GetStringArray(p => p.AlbumArtists, ref _albumArtists); }
			set
			{
				SetValue(p => p.AlbumArtists = value);
				_albumArtists = null;
				_firstAlbumArtist = null;
				_joinedAlbumArtists = null;
			}
		}

		private string[] _albumArtistsSort;
		[Category("Personnel")]
		[Description("A string array containing the sort names for the band or artist who is credited in the creation of the entire album or collection containing the media described by the selected item(s), or an empty array if no value is present.")]
		public string[] AlbumArtistsSort
		{
			get { return GetStringArray(p => p.AlbumArtistsSort, ref _albumArtistsSort); }
			set
			{
				SetValue(p => p.AlbumArtistsSort = value);
				_albumArtistsSort = null;
				_firstAlbumArtistSort = null;
			}
		}

		[Browsable(false)]
		public string AlbumIndex
		{
			get { return AlbumSort.Coalesce(Album).GetIndex(); }
		}

		private string _albumSort;
		[Category("Details")]
		[Description("A string containing the sort names for the Album Title in the media described by the selected item(s), or null if no value is present.")]
		public string AlbumSort
		{
			get { return GetString(p => p.AlbumSort, ref _albumSort); }
			set
			{
				SetValue(p => p.AlbumSort = value);
				_albumSort = null;
			}
		}

		private string _amazonId;
		[Description("A string containing the AmazonID for the media described by the selected item(s), or null if no value is present.")]
		public string AmazonId
		{
			get { return GetString(p => p.AmazonId, ref _amazonId); }
			set
			{
				SetValue(p => p.AmazonId = value);
				_amazonId = null;
			}
		}

		private string[] _artists;
		[Category("Personnel")]
		[Description("A string array containing the sort names for the performers or artists who performed in the media described by the selected item(s), or an empty array if no value is present. (Obsolete. For album artists, use AlbumArtists. For track artists, use Performers.)")]
		public string[] Artists
		{
			get { return GetStringArray(p => p.Artists, ref _artists); }
			set
			{
				SetValue(p => p.Artists = value);
				_artists = null;
				_firstArtist = null;
				_joinedArtists = null;
			}
		}

		private int _audioBitrate = int.MaxValue;
		[Category("Format")]
		[DefaultValue(0)]
		[Description("An integer containing the bitrate of the audio represented by the selected item(s). This value is equal to the first non-zero audio bitrate.")]
		public int AudioBitrate
		{
			get { return GetInt(p => p.AudioBitrate, ref _audioBitrate); }
		}

		private int _audioChannels = int.MaxValue;
		[Category("Format")]
		[DefaultValue(0)]
		[Description("An integer containing the number of channels in the audio represented by the selected item(s). This value is equal to the first non-zero audio channel count.")]
		public int AudioChannels
		{
			get { return GetInt(p => p.AudioChannels, ref _audioChannels); }
		}

		private int _audioSampleRate = int.MaxValue;
		[Category("Format")]
		[DefaultValue(0)]
		[Description("An integer containing the sample rate of the audio represented by the selected item(s). This value is equal to the first non-zero audio sample rate.")]
		public int AudioSampleRate
		{
			get { return GetInt(p => p.AudioSampleRate, ref _audioSampleRate); }
		}

		private int _beatsPerMinute = int.MaxValue;
		/*[Category("Details")]*/
		[DefaultValue(0)]
		[Description("An unsigned integer containing the number of beats per minute in the audio of the media represented by the selected item(s), or zero if not specified.")]
		public int BeatsPerMinute
		{
			get { return GetInt(p => p.BeatsPerMinute, ref _beatsPerMinute); }
			set
			{
				SetValue(p => p.BeatsPerMinute = value);
				_beatsPerMinute = value;
			}
		}

		private int _bitsPerSample = int.MaxValue;
		[Category("Format")]
		[DefaultValue(0)]
		[Description("An integer value containing the number of bits per sample in the audio represented by the selected item(s). This value is equal to the first non-zero quantization.")]
		public int BitsPerSample
		{
			get { return GetInt(p => p.BitsPerSample, ref _bitsPerSample); }
		}

		private string _century;
		[Browsable(false)]
		[Category("Details")]
		[Description("A string containing the century that the media represented by the selected item(s) was created, or zero if no value is present.")]
		public string Century
		{
			get { return GetString(p => p.Century, ref _century); }
		}

		private string _comment;
		[Description("A string containing user comments on the media represented by the selected item(s), or null if no value is present.")]
		public string Comment
		{
			get { return GetString(p => p.Comment, ref _comment); }
			set
			{
				SetValue(p => p.Comment = value);
				_comment = null;
			}
		}

		private string[] _composers;
		[Category("Personnel")]
		[Description("A string array containing the composers of the media represented by the selected item(s), or an empty array if no value is present.")]
		public string[] Composers
		{
			get { return GetStringArray(p => p.Composers, ref _composers); }
			set
			{
				SetValue(p => p.Composers = value);
				_composers = null;
				_firstComposer = null;
				_joinedComposers = null;
			}
		}

		private string[] _composersSort;
		[Category("Personnel")]
		[Description("A string array containing the sort names for the Composers in the media described by the selected item(s), or an empty array if no value is present.")]
		public string[] ComposersSort
		{
			get { return GetStringArray(p => p.ComposersSort, ref _composersSort); }
			set
			{
				SetValue(p => p.ComposersSort = value);
				_composersSort = null;
				_firstComposerSort = null;
			}
		}

		private string _conductor;
		[Category("Personnel")]
		[Description("A string containing the conductor or director of the media represented by the selected item(s), or null if no value is present.")]
		public string Conductor
		{
			get { return GetString(p => p.Conductor, ref _conductor); }
			set
			{
				SetValue(p => p.Conductor = value);
				_conductor = null;
			}
		}

		private string _copyright;
		[Description("A string containing the copyright information for the media represented by the selected item(s), or null if no value is present.")]
		public string Copyright
		{
			get { return GetString(p => p.Copyright, ref _copyright); }
			set
			{
				SetValue(p => p.Copyright = value);
				_copyright = null;
			}
		}

		private string _decade;
		[Browsable(false)]
		[Category("Details")]
		[Description("A string containing the decade that the media represented by the selected item(s) was created, or zero if no value is present. Following popular usage, years ending in '0' are treated as the start of a decade.")]
		public string Decade
		{
			get { return GetString(p => p.Decade, ref _decade); }
		}

		private string _description;
		[Category("Format")]
		[Description("A string containing a description of the media represented by the selected item(s). The value contains the descriptions of the codecs joined by colons.")]
		public string Description
		{
			get { return GetString(p => p.Description, ref _description); }
		}

		private int _discCount = int.MaxValue;
		[Category("Media")]
		[DefaultValue(0)]
		[Description("An unsigned integer containing the number of discs in the boxed set containing the media represented by the selected item(s), or zero if not specified.")]
		public int DiscCount
		{
			get { return GetInt(p => p.DiscCount, ref _discCount); }
			set
			{
				SetValue(p => p.DiscCount = value);
				_discCount = value;
			}
		}

		private int _discNumber = int.MaxValue;
		[Category("Media")]
		[DefaultValue(0)]
		[Description("An unsigned integer containing the number of the disc containing the media represented by the selected item(s) in the boxed set.")]
		public int DiscNumber
		{
			get { return GetInt(p => p.DiscNumber, ref _discNumber); }
			set
			{
				SetValue(p => p.DiscNumber = value);
				_discNumber = value;
			}
		}

		private string _discOf;
		[Browsable(false)]
		[Category("Media")]
		[Description("A string containing both the number of the disc, and the total number of discs in the boxed set, containing the media represented by the selected item(s).")]
		public string DiscOf
		{
			get { return GetString(p => p.DiscOf, ref _discOf); }
		}

		private string _discTrack;
		[Browsable(false)]
		[Category("Media")]
		[Description("A string containing the track number, the total number of tracks, the disc number, and the total number of discs in the boxed set, containing the media represented by the selected item(s).")]
		public string DiscTrack
		{
			get { return GetString(p => p.DiscTrack, ref _discTrack); }
		}

		private TimeSpan _duration = TimeSpan.MaxValue;
		[Category("Details")]
		[Description("A TimeSpan containing the duration of the media represented by the selected item(s). If the duration was set in the constructor, that value is returned. Otherwise, the longest codec duration is used.")]
		public TimeSpan Duration
		{
			get { return GetTimeSpan(p => p.Duration, ref _duration); }
		}

		private string _fileExtension;
		[Browsable(false)]
		[Category("Details")]
		[Description("A string containing just the file extension portion of the full path to the media file in the filesystem.")]
		public string FileExtension
		{
			get { return GetString(p => p.FileExtension, ref _fileExtension); }
		}

		private long _fileLength = long.MaxValue;
		[Browsable(false)]
		[Category("Details")]
		[DefaultValue(0)]
		[Description("A long integer containing the byte length of the media file in the filesystem.")]
		public long FileLength
		{
			get { return GetLong(p => p.FileLength, ref _fileLength, true); }
		}

		private string _fileName;
		[Browsable(false)]
		[Category("Details")]
		[Description("A string containing just the file name portion (including any extension) of the full path to the media file in the filesystem.")]
		public string FileName
		{
			get { return GetString(p => p.FileName, ref _fileName); }
		}

		private string _fileNameWithoutExtension;
		[Browsable(false)]
		[Category("Details")]
		[Description("A string containing just the file name portion (excluding any extension) of the full path to the media file in the filesystem.")]
		public string FileNameWithoutExtension
		{
			get { return GetString(p => p.FileNameWithoutExtension, ref _fileNameWithoutExtension); }
		}

		private string _filePath;
		[Category("Details")]
		[Description("A string containing the full path to the media file or folder in the filesystem.")]
		public string FilePath
		{
			get { return GetFileOrCommonFolderPath(p => p.FilePath, ref _filePath); }
		}

		private long _fileSize = long.MaxValue;
		[Category("Details")]
		[DefaultValue(0)]
		[Description("A long integer containing the byte length of the media file in the filesystem.")]
		public long FileSize
		{
			get
			{
				return GetLong(p => p.FileSize, ref _fileSize, true);
				//return Files.Sum(f => f.FileSize);
			}
		}

		private string _firstAlbumArtist;
		[Browsable(false)]
		[Category("Personnel")]
		[Description("A string containing the first band or artist who is credited in the creation of the entire album or collection containing the media described by the selected item(s), or null if no value is present.")]
		public string FirstAlbumArtist
		{
			get { return GetString(p => p.FirstAlbumArtist, ref _firstAlbumArtist); }
		}

		private string _firstAlbumArtistSort;
		[Browsable(false)]
		[Category("Personnel")]
		[Description("A string containing the sort names for the first band or artist who is credited in the creation of the entire album or collection containing the media described by the selected item(s), or null if no value is present.")]
		public string FirstAlbumArtistSort
		{
			get { return GetString(p => p.FirstAlbumArtistSort, ref _firstAlbumArtistSort); }
		}

		private string _firstArtist;
		[Browsable(false)]
		[Category("Personnel")]
		[Description("A string containing the sort name for the first performer or artist who performed in the media described by the selected item(s), or null if no value is present. (Obsolete. For album artists, use FirstAlbumArtist. For track artists, use FirstPerformer.)")]
		public string FirstArtist
		{
			get { return GetString(p => p.FirstArtist, ref _firstArtist); }
		}

		private string _firstComposer;
		[Browsable(false)]
		[Category("Personnel")]
		[Description("A string containing the first composer of the media represented by the selected item(s), or null if no value is present.")]
		public string FirstComposer
		{
			get { return GetString(p => p.FirstComposer, ref _firstComposer); }
		}

		private string _firstComposerSort;
		[Browsable(false)]
		[Category("Personnel")]
		[Description("A string containing the sort name for first composer of the media represented by the selected item(s), or null if no value is present.")]
		public string FirstComposerSort
		{
			get { return GetString(p => p.FirstComposerSort, ref _firstComposerSort); }
		}

		private string _firstGenre;
		[Browsable(false)]
		[Category("Details")]
		[Description("A string containing the first genre of the media represented by the selected item(s), or null if no value is present.")]
		public string FirstGenre
		{
			get { return GetString(p => p.FirstGenre, ref _firstGenre); }
		}

		private string _firstPerformer;
		[Browsable(false)]
		[Category("Personnel")]
		[Description("A string containing the first performer or artist who performed in the media described by the selected item(s), or null if no value is present.")]
		public string FirstPerformer
		{
			get { return GetString(p => p.FirstPerformer, ref _firstPerformer); }
		}

		private string _firstPerformerSort;
		[Browsable(false)]
		[Category("Personnel")]
		[Description("A string containing the sort name for the first performer or artist who performed in the media described by the selected item(s), or null if no value is present.")]
		public string FirstPerformerSort
		{
			get { return GetString(p => p.FirstPerformerSort, ref _firstPerformerSort); }
		}

		private string[] _genres;
		[Category("Details")]
		[Description("A string array containing the genres of the media represented by the selected item(s), or an empty array if no value is present.")]
		public string[] Genres
		{
			get { return GetStringArray(p => p.Genres, ref _genres); }
			set
			{
				SetValue(p => p.Genres = value);
				_genres = null;
				_firstGenre = null;
				_joinedGenres = null;
				_isClassical = Logical.Unknown;
			}
		}

		private string _grouping;
		[Description("A string containing the grouping on the album which the media in the selected item(s) belongs to, or null if no value is present.")]
		public string Grouping
		{
			get { return GetString(p => p.Grouping, ref _grouping); }
			set
			{
				SetValue(p => p.Grouping = value);
				_grouping = null;
			}
		}

		private long _invariantEndPosition = long.MaxValue;
		[Category("Format")]
		[DefaultValue(0)]
		[Description("A long integer containing the position at which the invariant portion of the selected item(s) ends.")]
		public long InvariantEndPosition
		{
			get { return GetLong(p => p.InvariantEndPosition, ref _invariantEndPosition, false); }
		}

		private long _invariantStartPosition = long.MaxValue;
		[Category("Format")]
		[DefaultValue(0)]
		[Description("A long integer containing the position at which the invariant portion of the selected item(s) begins.")]
		public long InvariantStartPosition
		{
			get { return GetLong(p => p.InvariantStartPosition, ref _invariantStartPosition, false); }
		}

		private Logical _isClassical;
		[Category("Details")]
		[Description("A bool indicating whether or not the first genre of the selected item(s) is 'Classical'.")]
		public Logical IsClassical
		{
			get { return GetLogical(p => p.IsClassical, ref _isClassical); }
		}

		private Logical _isEmpty;
		[Category("Format")]
		[Description("A bool indicating whether the selected item(s) contains any tag values.")]
		public Logical IsEmpty
		{
			get { return GetLogical(p => p.IsEmpty, ref _isEmpty); }
		}

		private string _joinedAlbumArtists;
		[Category("Personnel")]
		[Description("A string containing the artist(s) credited in the creation of the entire album or collection containing the media described by the selected item(s), or null if no value is present.")]
		public string JoinedAlbumArtists
		{
			get { return GetString(p => p.JoinedAlbumArtists, ref _joinedAlbumArtists); }
		}

		private string _joinedArtists;
		[Category("Personnel")]
		[Description("A string containing the sort names for the performers or artists who performed in the media described by the selected item(s), or null if no value is present. (Obsolete. For album artists, use JoinedAlbumArtists. For track artists, use JoinedPerformers.)")]
		public string JoinedArtists
		{
			get { return GetString(p => p.JoinedArtists, ref _joinedArtists); }
		}

		private string _joinedComposers;
		[Category("Personnel")]
		[Description("A string containing the composers of the media represented by the selected item(s), or null if no value is present.")]
		public string JoinedComposers
		{
			get { return GetString(p => p.JoinedComposers, ref _joinedComposers); }
		}

		private string _joinedGenres;
		[Category("Details")]
		[Description("A string containing the genres of the media represented by the selected item(s), or null if no value is present.")]
		public string JoinedGenres
		{
			get { return GetString(p => p.JoinedGenres, ref _joinedGenres); }
		}

		private string _joinedPerformers;
		[Category("Personnel")]
		[Description("A string containing the performers or artists who performed in the media described by the selected item(s), or null if no value is present.")]
		public string JoinedPerformers
		{
			get { return GetString(p => p.JoinedPerformers, ref _joinedPerformers); }
		}

		[Browsable(false)]
		public string JoinedPerformersIndex
		{
			get { return JoinedPerformersSort.Coalesce(JoinedPerformers).GetIndex(); }
		}

		private string _joinedPerformersSort;
		[Category("Personnel")]
		[Description("A string containing the sort names for the performers or artists who performed in the media described by the selected item(s), or null if no value is present.")]
		public string JoinedPerformersSort
		{
			get { return GetString(p => p.JoinedPerformersSort, ref _joinedPerformersSort); }
		}

		private string _lyrics;
		[Description("A string containing the lyrics or script of the media represented by the selected item(s), or null if no value is present.")]
		public string Lyrics
		{
			get { return GetString(p => p.Lyrics, ref _lyrics); }
			set
			{
				SetValue(p => p.Lyrics = value);
				_lyrics = null;
			}
		}

		private string _millennium;
		[Browsable(false)]
		[Category("Details")]
		[Description("A string containing the millennium that the media represented by the selected item(s) was created, or zero if no value is present.")]
		public string Millennium
		{
			get { return GetString(p => p.Millennium, ref _millennium); }
		}

		private string _mimeType;
		[Category("Format")]
		[Description("A string containing the MimeType of the media represented by the selected item(s), or null if no value is present.")]
		public string MimeType
		{
			get { return GetString(p => p.MimeType, ref _mimeType); }
		}

		private string _musicBrainzArtistId;
		[Category("MusicBrainz")]
		[Description("A string containing the MusicBrainz Artist ID for the media described by the selected item(s), or null if no value is present.")]
		public string MusicBrainzArtistId
		{
			get { return GetString(p => p.MusicBrainzArtistId, ref _musicBrainzArtistId); }
			set
			{
				SetValue(p => p.MusicBrainzArtistId = value);
				_musicBrainzArtistId = null;
			}
		}

		private string _musicBrainzDiscId;
		[Category("MusicBrainz")]
		[Description("A string containing the MusicBrainz Disc ID for the media described by the selected item(s), or null if no value is present.")]
		public string MusicBrainzDiscId
		{
			get { return GetString(p => p.MusicBrainzDiscId, ref _musicBrainzDiscId); }
			set
			{
				SetValue(p => p.MusicBrainzDiscId = value);
				_musicBrainzDiscId = null;
			}
		}

		private string _musicBrainzReleaseArtistId;
		[Category("MusicBrainz")]
		[Description("A string containing the MusicBrainz Release Artist ID for the media described by the selected item(s), or null if no value is present.")]
		public string MusicBrainzReleaseArtistId
		{
			get { return GetString(p => p.MusicBrainzReleaseArtistId, ref _musicBrainzReleaseArtistId); }
			set
			{
				SetValue(p => p.MusicBrainzReleaseArtistId = value);
				_musicBrainzReleaseArtistId = null;
			}
		}

		private string _musicBrainzReleaseCountry;
		[Category("MusicBrainz")]
		[Description("A string containing the MusicBrainz Release Country for the media described by the selected item(s), or null if no value is present.")]
		public string MusicBrainzReleaseCountry
		{
			get { return GetString(p => p.MusicBrainzReleaseCountry, ref _musicBrainzReleaseCountry); }
			set
			{
				SetValue(p => p.MusicBrainzReleaseCountry = value);
				_musicBrainzReleaseCountry = null;
			}
		}

		private string _musicBrainzReleaseId;
		[Category("MusicBrainz")]
		[Description("A string containing the MusicBrainz Release ID for the media described by the selected item(s), or null if no value is present.")]
		public string MusicBrainzReleaseId
		{
			get { return GetString(p => p.MusicBrainzReleaseId, ref _musicBrainzReleaseId); }
			set
			{
				SetValue(p => p.MusicBrainzReleaseId = value);
				_musicBrainzReleaseId = null;
			}
		}

		private string _musicBrainzReleaseStatus;
		[Category("MusicBrainz")]
		[Description("A string containing the MusicBrainz Release Status for the media described by the selected item(s), or null if no value is present.")]
		public string MusicBrainzReleaseStatus
		{
			get { return GetString(p => p.MusicBrainzReleaseStatus, ref _musicBrainzReleaseStatus); }
			set
			{
				SetValue(p => p.MusicBrainzReleaseStatus = value);
				_musicBrainzReleaseStatus = null;
			}
		}

		private string _musicBrainzReleaseType;
		[Category("MusicBrainz")]
		[Description("A string containing the MusicBrainz Release Type for the media described by the selected item(s), or null if no value is present.")]
		public string MusicBrainzReleaseType
		{
			get { return GetString(p => p.MusicBrainzReleaseType, ref _musicBrainzReleaseType); }
			set
			{
				SetValue(p => p.MusicBrainzReleaseType = value);
				_musicBrainzReleaseType = null;
			}
		}

		private string _musicBrainzTrackId;
		[Category("MusicBrainz")]
		[Description("A string containing the MusicBrainz Track ID for the media described by the selected item(s), or null if no value is present.")]
		public string MusicBrainzTrackId
		{
			get { return GetString(p => p.MusicBrainzTrackId, ref _musicBrainzTrackId); }
			set
			{
				SetValue(p => p.MusicBrainzTrackId = value);
				_musicBrainzTrackId = null;
			}
		}

		private string _musicIpId;
		[Category("MusicBrainz")]
		[Description("A string containing the MusicIP Puid for the media described by the selected item(s), or null if no value is present.")]
		public string MusicIpId
		{
			get { return GetString(p => p.MusicIpId, ref _musicIpId); }
			set
			{
				SetValue(p => p.MusicIpId = value);
				_musicIpId = null;
			}
		}

		private string[] _performers;
		[Category("Personnel")]
		[Description("A string array containing the performers or artists who performed in the media described by the selected item(s), or an empty array if no value is present.")]
		public string[] Performers
		{
			get { return GetStringArray(p => p.Performers, ref _performers); }
			set
			{
				SetValue(p => p.Performers = value);
				_performers = null;
				_firstPerformer = null;
				_joinedPerformers = null;
			}
		}

		private string[] _performersSort;
		[Category("Personnel")]
		[Description("A string array containing the sort names for the performers or artists who performed in the media described by the selected item(s), or an empty array if no value is present.")]
		public string[] PerformersSort
		{
			get { return GetStringArray(p => p.PerformersSort, ref _performersSort); }
			set
			{
				SetValue(p => p.PerformersSort = value);
				_performersSort = null;
				_firstPerformerSort = null;
				_joinedPerformersSort = null;
			}
		}

		private int _photoHeight = int.MaxValue;
		[Category("Media")]
		[DefaultValue(0)]
		[Description("An integer value containing the height of the photo represented by the selected item(s).")]
		public int PhotoHeight
		{
			get { return GetInt(p => p.PhotoHeight, ref _photoHeight); }
		}

		private int _photoQuality = int.MaxValue;
		[Category("Media")]
		[DefaultValue(0)]
		[Description("An integer containing the (format specific) quality indicator of the photo represented by the selected item(s). A value of 0 means that there was no quality indicator for the format or the file.")]
		public int PhotoQuality
		{
			get { return GetInt(p => p.PhotoQuality, ref _photoQuality); }
		}

		private int _photoWidth = int.MaxValue;
		[Category("Media")]
		[DefaultValue(0)]
		[Description("An integer value containing the width of the photo represented by the selected item(s).")]
		public int PhotoWidth
		{
			get { return GetInt(p => p.PhotoWidth, ref _photoWidth); }
		}

		private int _pictureCount = int.MaxValue;
		[Category("Media")]
		[Description("An integer containing the number of embedded pictures in the selected item(s).")]
		public int PictureCount
		{
			get { return GetInt(p => p.PictureCount, ref _pictureCount); }
		}

		private Picture[] _pictures;
		[Category("Media")]
		[Description("A Picture array containing the embedded pictures in the selected item(s).")]
		public Picture[] Pictures
		{
			get { return GetPictures(p => p.Pictures, ref _pictures); }
		}

		private Logical _possiblyCorrupt;
		[Category("Format")]
		[Description("A bool indicating whether the selected item(s) contains any tag values.")]
		public Logical PossiblyCorrupt
		{
			get { return GetLogical(p => p.PossiblyCorrupt, ref _possiblyCorrupt); }
		}

		private TagLib.TagTypes _tagTypes = TagLib.TagTypes.AllTags;
		[Category("Format")]
		[Description("Gets the tag types contained in the selected item(s).")]
		public TagLib.TagTypes TagTypes
		{
			get { return GetTagTypes(p => p.TagTypes, ref _tagTypes); }
		}

		private TagLib.TagTypes _tagTypesOnDisk = TagLib.TagTypes.AllTags;
		[Category("Format")]
		[Description("Gets the tag types contained in the physical file represented by the selected item(s).")]
		public TagLib.TagTypes TagTypesOnDisk
		{
			get { return GetTagTypes(p => p.TagTypesOnDisk, ref _tagTypesOnDisk); }
		}

		private string _title;
		[Category("Details")]
		[Description("A string containing the title for the media described by the selected item(s), or null if no value is present.")]
		public string Title
		{
			get { return GetString(p => p.Title, ref _title); }
			set
			{
				SetValue(p => p.Title = value);
				_title = null;
			}
		}

		[Browsable(false)]
		public string TitleIndex
		{
			get { return TitleSort.Coalesce(Title).GetIndex(); }
		}

		private string _titleSort;
		[Category("Details")]
		[Description("A string containing the sort name for the Track Title in the media described by the selected item(s), or null if no value is present.")]
		public string TitleSort
		{
			get { return GetString(p => p.TitleSort, ref _titleSort); }
			set
			{
				SetValue(p => p.TitleSort = value);
				_titleSort = null;
			}
		}

		private int _trackCount = int.MaxValue;
		[Category("Media")]
		[DefaultValue(0)]
		[Description("An unsigned integer containing the number of tracks in the album containing the media represented by the selected item(s), or zero if not specified.")]
		public int TrackCount
		{
			get { return GetInt(p => p.TrackCount, ref _trackCount); }
			set
			{
				SetValue(p => p.TrackCount = value);
				_trackCount = value;
			}
		}

		private int _trackNumber = int.MaxValue;
		[Category("Media")]
		[DefaultValue(0)]
		[Description("An unsigned integer containing the position of the media represented by the selected item(s) in its containing album, or zero if not specified.")]
		public int TrackNumber
		{
			get { return GetInt(p => p.TrackNumber, ref _trackNumber); }
			set
			{
				SetValue(p => p.TrackNumber = value);
				_trackNumber = value;
			}
		}

		private string _trackOf;
		[Browsable(false)]
		[Category("Media")]
		[Description("A string containing both the number of the track, and the total number of tracks in the album, containing the media represented by the selected item(s).")]
		public string TrackOf
		{
			get { return GetString(p => p.TrackOf, ref _trackOf); }
		}

		private TrackStatus _trackStatus;
		[Category("Selection")]
		[Description("An enumeration value containing the combined TrackStatus values of all items in the selection. Possible values are:\n\n"
			+ "Unknown - the item has no recognised TrackStatus value.\n"
			+ "Current - the item's library entry exactly matches its media file.\n"
			+ "New - the item's media file does not yet have a corresponding library entry.\n"
			+ "Updated - the item's media file contains more recent edits than its library entry.\n"
			+ "Pending - the item's library entry contains more recent edits than its media file.\n"
			+ "Deleted - the item's media file no longer exists; its library entry is orphaned.\n\n"
			+ "During synchronization: any New items are added to the library; any Updated items have their library entries brought up to date; "
			+ "any Pending edits are applied to the corresponding media files; and any Deleted items are removed from the library.")]
		public TrackStatus Status
		{
			get { return GetTrackStatus(p => p.Status, ref _trackStatus); }
		}

		private int _videoHeight = int.MaxValue;
		[Category("Media")]
		[DefaultValue(0)]
		[Description("An integer containing the height of the video represented by the selected item(s).")]
		public int VideoHeight
		{
			get { return GetInt(p => p.VideoHeight, ref _videoHeight); }
		}

		private int _videoWidth = int.MaxValue;
		[Category("Media")]
		[DefaultValue(0)]
		[Description("An integer containing the width of the video represented by the selected item(s).")]
		public int VideoWidth
		{
			get { return GetInt(p => p.VideoWidth, ref _videoWidth); }
		}

		private int _year = int.MaxValue;
		[Category("Details")]
		[DefaultValue(0)]
		[Description("An unsigned intreger containing the year that the media represented by the selected item(s) was created, or zero if no value is present.")]
		public int Year
		{
			get { return GetInt(p => p.Year, ref _year); }
			set
			{
				SetValue(p => p.Year = value);
				_year = value;
				_decade = null;
				_century = null;
				_millennium = null;
				_yearAlbum = null;
			}
		}

		private string _yearAlbum;
		[Browsable(false)]
		[Category("Details")]
		public string YearAlbum
		{
			get { return GetString(p => p.YearAlbum, ref _yearAlbum); }
		}

		#endregion

		#region Getters & Setters

		private string GetFileOrCommonFolderPath(Func<ITrack, string> getString, ref string result)
		{
			if (result == null)
			{
				result = string.Empty;
				if (Tracks != null && Tracks.Any())
				{
					// The following adapted from http://rosettacode.org/wiki/Find_common_directory_path#C.23
					var path = result;
					var paths = Tracks.Select(getString).ToList();
					var segments = paths.First(s => s.Length == paths.Max(t => t.Length)).Split('\\').ToList();
					for (var index = 0; index < segments.Count; index++)
					{
						var segment = segments[index];
						if (!paths.All(s => s.StartsWith(path + segment)))
							break;
						path += segment;
						if (index < segments.Count - 1)
							path += "\\";
					}
					result = path;
				}
			}
			return result;
		}

		private int GetInt(Func<ITrack, int> getInt, ref int result)
		{
			if (result == int.MaxValue)
			{
				result = 0;
				if (Tracks != null)
				{
					var first = true;
					foreach (var value in Tracks.Select(getInt))
					{
						if (first)
						{
							result = value;
							first = false;
						}
						else if (result != value)
						{
							result = 0;
							break;
						}
					}
				}
			}
			return result;
		}

		private Logical GetLogical(Func<ITrack, Logical> getLogical, ref Logical result)
		{
			if (result == Logical.Unknown && Tracks != null)
				foreach (var value in Tracks.Select(getLogical))
				{
					result |= value;
					if (result == (Logical.Yes | Logical.No))
						break;
				}
			return result;
		}

		private long GetLong(Func<ITrack, long> getLong, ref long result, bool sum)
		{
			if (result == long.MaxValue)
			{
				result = 0;
				if (Tracks != null)
				{
					var first = true;
					foreach (var value in Tracks.Select(getLong))
						if (first)
						{
							result = value;
							first = false;
						}
						else if (sum)
							result += value;
						else if (result != value)
						{
							result = 0;
							break;
						}
				}
			}
			return result;
		}

		private Picture[] GetPictures(Func<ITrack, Picture[]> getPictures, ref Picture[] result)
		{
			if (result == null)
			{
				var pictureList = new List<Picture>();
				if (Tracks != null)
					foreach (var pictures in Tracks
						.Select(getPictures)
						.Where(p => p != null))
					{
						pictureList.AddRange(pictures);
						if (pictureList.Any())
							break;
					}
				result = pictureList.ToArray();
			}
			return result;
		}

		private string GetString(Func<ITrack, string> getString, ref string result)
		{
			if (result == null)
			{
				result = string.Empty;
				var first = true;
				foreach (var value in Tracks.Select(getString))
					if (first)
					{
						result = value;
						first = false;
					}
					else if (result != value)
					{
						result = string.Empty;
						break;
					}
			}
			return result;
		}

		private string[] GetStringArray(Func<ITrack, string[]> getStringArray, ref string[] result)
		{
			if (result == null)
			{
				var values = new List<string>();
				if (Tracks != null)
					values.AddRange(Tracks.SelectMany(getStringArray).Distinct());
				result = values.ToArray();
			}
			return result;
		}

		private TagLib.TagTypes GetTagTypes(Func<ITrack, TagLib.TagTypes> getTagTypes, ref TagLib.TagTypes result)
		{
			if (result == TagLib.TagTypes.AllTags)
			{
				result = 0;
				if (Tracks != null)
					result = Tracks
						.Select(getTagTypes)
						.Aggregate(result, (current, tagTypes) => current | tagTypes);
			}
			return result;
		}

		private TimeSpan GetTimeSpan(Func<ITrack, TimeSpan> getTimeSpan, ref TimeSpan result)
		{
			if (result == TimeSpan.MaxValue)
			{
				result = TimeSpan.Zero;
				if (Tracks != null)
					result = Tracks
						.Select(getTimeSpan)
						.Aggregate(result, (current, timeSpan) => current + timeSpan);
			}
			return result;
		}

		private TrackStatus GetTrackStatus(Func<ITrack, TrackStatus> getTrackStatus, ref TrackStatus result)
		{
			if (result == TrackStatus.Unknown && Tracks != null)
				result = Tracks
					.Select(getTrackStatus)
					.Aggregate(result, (current, trackStatus) => current | trackStatus);
			return result;
		}

		private void SetValue(Action<ITrack> setValue)
		{
			foreach (var file in Tracks)
				setValue(file);
		}

		#endregion
	}
}
