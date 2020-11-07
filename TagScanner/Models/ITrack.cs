using System;

namespace TagScanner.Models
{
	public interface ITrack
	{
		string Album { get; set; }
		string[] AlbumArtists { get; set; }
		int AlbumArtistsCount { get; }
		string[] AlbumArtistsSort { get; set; }
		int AlbumArtistsSortCount { get; }
		string AlbumSort { get; set; }
		string AmazonId { get; set; }
		string[] Artists { get; set; }
		int ArtistsCount { get; }
		int AudioBitrate { get; }
		int AudioChannels { get; }
		int AudioSampleRate { get; }
		int BeatsPerMinute { get; set; }
		int BitsPerSample { get; }
		string Century { get; }
		string Codecs { get; }
		string Comment { get; set; }
		string[] Composers { get; set; }
		int ComposersCount { get; }
		string[] ComposersSort { get; set; }
		int ComposersSortCount { get; }
		string Conductor { get; set; }
		string Copyright { get; set; }
		string Decade { get; }
		string Description { get; }
		int DiscCount { get; set; }
		int DiscNumber { get; set; }
		string DiscOf { get; }
		string DiscTrack { get; }
		TimeSpan Duration { get; }
		string FileAttributes { get; }
		DateTime FileCreationTime { get; }
		DateTime FileCreationTimeUtc { get; }
		string FileExtension { get; }
		DateTime FileLastAccessTime { get; }
		DateTime FileLastAccessTimeUtc { get; }
		DateTime FileLastWriteTime { get; }
		DateTime FileLastWriteTimeUtc { get; }
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
		int GenresCount { get; }
		string Grouping { get; set; }
		double ImageAltitude { get; set; }
		string ImageCreator { get; set; }
		DateTime ImageDateTime { get; set; }
		double ImageExposureTime { get; set; }
		double ImageFNumber { get; set; }
		double ImageFocalLength { get; set; }
		int ImageFocalLengthIn35mmFilm { get; set; }
		int ImageISOSpeedRatings { get; set; }
		string[] ImageKeywords { get; set; }
		double ImageLatitude { get; set; }
		double ImageLongitude { get; set; }
		string ImageMake { get; set; }
		string ImageModel { get; set; }
		TagLib.Image.ImageOrientation ImageOrientation { get; set; }
		int ImageRating { get; set; }
		string ImageSoftware { get; set; }
		long InvariantEndPosition { get; }
		long InvariantStartPosition { get; }
		Logical IsClassical { get; }
		Logical IsEmpty { get; }
		string JoinedAlbumArtists { get; }
		string JoinedArtists { get; }
		string JoinedComposers { get; }
		string JoinedGenres { get; }
		string JoinedPerformers { get; }
		string JoinedPerformersSort { get; }
		string Lyrics { get; set; }
		TagLib.MediaTypes MediaTypes { get; }
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
		int PerformersCount { get; }
		string[] PerformersSort { get; set; }
		int PerformersSortCount { get; }
		int PhotoHeight { get; }
		int PhotoQuality { get; }
		int PhotoWidth { get; }
		Picture[] Pictures { get; }
		int PicturesCount { get; }
		Logical PossiblyCorrupt { get; }
        TagLib.TagTypes TagTypes { get; }
		TagLib.TagTypes TagTypesOnDisk { get; }
		string Title { get; set; }
		string TitleSort { get; set; }
		int TrackCount { get; set; }
		int TrackNumber { get; set; }
		string TrackOf { get; }
		FileStatus FileStatus { get; }
		int VideoHeight { get; }
		int VideoWidth { get; }
		int Year { get; set; }
		string YearAlbum { get; }
	}
}
