using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace TagScanner.Models
{
	public static class Metadata
	{
		#region Properties

		public static readonly PropertyInfo[] SelectionPropertyInfos = typeof(Selection).GetProperties();
		public static readonly PropertyInfo[] TrackPropertyInfos = typeof(ITrack).GetProperties();

		private static readonly IEnumerable<PropertyInfo> SortablePropertyInfos =
			from prop in TrackPropertyInfos
			let type = prop.PropertyType
			let typeName = type.Name
			where !type.IsArray && typeName != "TagTypes" && typeName != "FileStatus"
			select prop;

		private static readonly IEnumerable<PropertyInfo> StringPropertyInfos =
			TrackPropertyInfos.Where(p => p.PropertyType.Name == "String");

		private static readonly IEnumerable<PropertyInfo> TextPropertyInfos =
			TrackPropertyInfos.Where(p => p.PropertyType.Name.StartsWith("String"));

		private static readonly IEnumerable<PropertyInfo> WritableStringPropertyInfos =
			StringPropertyInfos.Where(p => p.CanWrite);

		private static readonly IEnumerable<PropertyInfo> WritableTextPropertyInfos =
			TextPropertyInfos.Where(p => p.CanWrite);

		public static readonly IEnumerable<string> SelectionTags = SelectionPropertyInfos.Select(p => p.Name);

		public static readonly string[] SortableTags = SortablePropertyInfos.Select(p => p.Name).ToArray();
		public static readonly string[] StringTags = StringPropertyInfos.Select(p => p.Name).ToArray();
		public static readonly string[] TextTags = TextPropertyInfos.Select(p => p.Name).ToArray();
		public static readonly string[] WritableStringTags = WritableStringPropertyInfos.Select(p => p.Name).ToArray();
		public static readonly string[] WritableTextTags = WritableTextPropertyInfos.Select(p => p.Name).ToArray();

		#endregion

		#region Categories

		public static string GetTagCategory(string propertyName)
		{
			return (string)UseTagAttribute(propertyName, typeof(CategoryAttribute), "categoryValue");
		}

		#endregion

		#region Quantifiers

		[Flags]
		public enum Quantifiers
		{
			None = 0,
			And = 1,
			Or = 2,
			Not = 4,
			Nand = Not | And,
			Nor = Not | Or
		}

		private const string
			QuantifierStringAnd = "All of these are true:",
			QuantifierStringOr = "At least one of these is true:",
			QuantifierStringNand = "At least one of these is false:",
			QuantifierStringNor = "All of these are false:";

		public static readonly string[] QuantifierStrings = new[]
		{
			QuantifierStringAnd,
			QuantifierStringOr,
			QuantifierStringNand,
			QuantifierStringNor
		};

		public static Quantifiers GetQuantifier(string quantifierString)
		{
			switch (quantifierString)
			{
				case QuantifierStringAnd:
					return Quantifiers.And;
				case QuantifierStringOr:
					return Quantifiers.Or;
				case QuantifierStringNand:
					return Quantifiers.Nand;
				case QuantifierStringNor:
					return Quantifiers.Nor;
			}
			return Quantifiers.None;
		}

		#endregion

		#region Visibilities

		public static List<string> GetTrackVisibleTags()
		{
			return SelectionTags.Where(t => GetTagVisibility(t)).ToList();
		}

		public static void SetTrackVisibleTags(IEnumerable<string> trackVisibleTags)
		{
			foreach (var t in SelectionTags)
				SetTagVisibility(t, trackVisibleTags.Contains(t));
		}

		#endregion

		#region Public Utility Methods

		public static string AmpersandEscape(this string s)
		{
			return s.Replace("&", "&&");
		}

		public static string AmpersandUnescape(this string s)
		{
			return s.Replace("&&", "&");
		}

		public static IEnumerable<string> GetDependentPropertyNames(string propertyName)
		{
			switch (propertyName)
			{
				case "Album":
					return new[] { "YearAlbum" };
				case "AlbumArtists":
					return new[] { "AlbumArtistsCount", "FirstAlbumArtist", "JoinedAlbumArtists" };
				case "AlbumArtistsSort":
					return new[] { "AlbumArtistsSortCount", "FirstAlbumArtistSort" };
				case "Artists":
					return new[] { "ArtistsCount", "FirstArtist", "JoinedArtists" };
				case "Composers":
					return new[] { "ComposersCount, FirstComposer", "JoinedComposers" };
				case "ComposersSort":
					return new[] { "ComposersSortCount, FirstComposerSort" };
				case "DiscCount":
				case "DiscNumber":
					return new[] { "DiscOf", "DiscTrack" };
				case "Genres":
					return new[] { "FirstGenre", "GenresCount", "IsClassical", "JoinedGenres" };
				case "Performers":
					return new[] { "PerformersCount", "FirstPerformer", "JoinedPerformers" };
				case "PerformersSort":
					return new[] { "PerformersSortCount", "FirstPerformerSort", "JoinedPerformersSort" };
				case "TrackCount":
				case "TrackNumber":
					return new[] { "TrackOf", "DiscTrack" };
				case "Year":
					return new[] { "Decade", "Century", "Millennium", "YearAlbum" };
			}
			return Enumerable.Empty<string>();
		}

		public static PropertyInfo GetPropertyInfo(string propertyName)
		{
			return TrackPropertyInfos.FirstOrDefault(p => p.Name == propertyName);
		}

		public static Type GetPropertyType(string propertyName)
		{
			return GetPropertyInfo(propertyName).PropertyType;
		}

		public static string GetPropertyTypeName(string propertyName)
		{
			return GetPropertyType(propertyName).Name;
		}

		#endregion

		#region Private Helper Methods

		private static PropertyDescriptor GetPropertyDescriptor(string propertyName)
		{
			return TypeDescriptor.GetProperties(typeof(Selection))[propertyName];
		}

		private static bool GetTagVisibility(string propertyName)
		{
			return UseTagVisibility(propertyName);
		}

		private static void SetTagVisibility(string propertyName, bool isBrowsable)
		{
			UseTagVisibility(propertyName, isBrowsable);
		}

		private static object UseTagAttribute(string propertyName, Type attributeType, string attributeName, object value = null)
		{
			return UseTagAttribute(GetPropertyDescriptor(propertyName), attributeType, attributeName, value);
		}

		private static object UseTagAttribute(PropertyDescriptor descriptor, Type attributeType, string attributeName, object value = null)
		{
			var fieldInfo = attributeType.GetField(attributeName, BindingFlags.NonPublic | BindingFlags.Instance);
			var attribute = descriptor.Attributes[attributeType];
			if (value == null)
				return fieldInfo.GetValue(attribute);
			fieldInfo.SetValue(attribute, value);
			return value;
		}

		private static bool UseTagVisibility(string propertyName, bool? isBrowsable = null)
		{
			return (bool)UseTagAttribute(propertyName, typeof(BrowsableAttribute), "browsable", isBrowsable);
		}

		#endregion
	}
}
