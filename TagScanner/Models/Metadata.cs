namespace TagScanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public static class Metadata
    {
        #region Fields

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

        #region Tag Attributes

        public static string GetTagCategory(string propertyName) => (string)UseTagAttribute(propertyName, typeof(CategoryAttribute), "categoryValue");

        public static string GetTagDescription(string propertyName) => (string)UseTagAttribute(propertyName, typeof(DescriptionAttribute), "description");

        private static bool GetTagVisible(string propertyName) => UseTagVisible(propertyName);

        public static List<string> GetVisibleTags() => SelectionTags.Where(t => GetTagVisible(t)).ToList();

        private static void SetTagVisible(string propertyName, bool isBrowsable) => UseTagVisible(propertyName, isBrowsable);

        public static void SetVisibleTags(IEnumerable<string> visibleTags)
        {
            foreach (var t in SelectionTags)
                SetTagVisible(t, visibleTags.Contains(t));
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
                case QuantifierStringAnd: return Quantifiers.And;
                case QuantifierStringOr: return Quantifiers.Or;
                case QuantifierStringNand: return Quantifiers.Nand;
                case QuantifierStringNor: return Quantifiers.Nor;
                default: return Quantifiers.None;
            };
        }

        #endregion

        #region Public Utility Methods

        public static string AmpersandEscape(this string s) => s.Replace("&", "&&");

        public static string AmpersandUnescape(this string s) => s.Replace("&&", "&");

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

        public static PropertyInfo GetPropertyInfo(string propertyName) => TrackPropertyInfos.FirstOrDefault(p => p.Name == propertyName);

        public static string GetPropertyTypeName(string propertyName) => GetPropertyType(propertyName).Name;

        #endregion

        #region Private Helper Methods

        private static PropertyDescriptor GetPropertyDescriptor(string propertyName) => TypeDescriptor.GetProperties(typeof(Selection))[propertyName];

        private static Type GetPropertyType(string propertyName) => GetPropertyInfo(propertyName).PropertyType;

        private static object UseTagAttribute(string propertyName, Type attributeType, string fieldName, object value = null) => UseTagAttribute(GetPropertyDescriptor(propertyName), attributeType, fieldName, value);

        private static object UseTagAttribute(PropertyDescriptor descriptor, Type attributeType, string fieldName, object value = null)
        {
            var fieldInfo = attributeType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var attribute = descriptor.Attributes[attributeType];
            if (value == null)
                return fieldInfo.GetValue(attribute);
            fieldInfo.SetValue(attribute, value);
            return value;
        }

        private static bool UseTagVisible(string propertyName, bool? isBrowsable = null) => (bool)UseTagAttribute(propertyName, typeof(BrowsableAttribute), "browsable", isBrowsable);

        #endregion
    }
}
