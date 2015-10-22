using System;

namespace TagScanner.Models
{
	public interface ITrack
	{
		string Album { get; set; }
		string[] AlbumArtists { get; set; }
		string[] AlbumArtistsSort { get; set; }
		string AlbumIndex { get; }
		string AlbumSort { get; set; }
		string AmazonId { get; set; }
		string[] Artists { get; set; }
		int AudioBitrate { get; }
		int AudioChannels { get; }
		int AudioSampleRate { get; }
		int BeatsPerMinute { get; set; }
		int BitsPerSample { get; }
		string Century { get; }
		string Comment { get; set; }
		string[] Composers { get; set; }
		string[] ComposersSort { get; set; }
		string Conductor { get; set; }
		string Copyright { get; set; }
		string Decade { get; }
		string Description { get; }
		int DiscCount { get; set; }
		int DiscNumber { get; set; }
		string DiscOf { get; }
		string DiscTrack { get; }
		TimeSpan Duration { get; }
		string FileExtension { get; }
		long FileLength { get; }
		string FileName { get; }
		string FileNameWithoutExtension { get; }
		string FilePath { get; }
		long FileSize { get; }
		string FirstAlbumArtist { get; }
		string FirstAlbumArtistSort { get; }
		string FirstArtist { get; }
		string FirstComposer { get; }
		string FirstComposerSort { get; }
		string FirstGenre { get; }
		string FirstPerformer { get; }
		string FirstPerformerSort { get; }
		string[] Genres { get; set; }
		string Grouping { get; set; }
		long InvariantEndPosition { get; }
		long InvariantStartPosition { get; }
		Logical IsClassical { get; }
		Logical IsEmpty { get; }
		string JoinedAlbumArtists { get; }
		string JoinedArtists { get; }
		string JoinedComposers { get; }
		string JoinedGenres { get; }
		string JoinedPerformers { get; }
		string JoinedPerformersIndex { get; }
		string JoinedPerformersSort { get; }
		string Lyrics { get; set; }
		string Millennium { get; }
		string MimeType { get; }
		string MusicBrainzArtistId { get; set; }
		string MusicBrainzDiscId { get; set; }
		string MusicBrainzReleaseArtistId { get; set; }
		string MusicBrainzReleaseCountry { get; set; }
		string MusicBrainzReleaseId { get; set; }
		string MusicBrainzReleaseStatus { get; set; }
		string MusicBrainzReleaseType { get; set; }
		string MusicBrainzTrackId { get; set; }
		string MusicIpId { get; set; }
		string[] Performers { get; set; }
		string[] PerformersSort { get; set; }
		int PhotoHeight { get; }
		int PhotoQuality { get; }
		int PhotoWidth { get; }
		int PictureCount { get; }
		Picture[] Pictures { get; }
		Logical PossiblyCorrupt { get; }
        TagLib.TagTypes TagTypes { get; }
		TagLib.TagTypes TagTypesOnDisk { get; }
		string Title { get; set; }
		string TitleIndex { get; }
		string TitleSort { get; set; }
		int TrackCount { get; set; }
		int TrackNumber { get; set; }
		string TrackOf { get; }
		TrackStatus Status { get; }
		int VideoHeight { get; }
		int VideoWidth { get; }
		int Year { get; set; }
		string YearAlbum { get; }
	}
}
