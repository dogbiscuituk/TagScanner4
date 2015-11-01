﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TagScanner.Models
{
	public static class Metadata
	{
		public static readonly PropertyInfo[] PropertyInfos = typeof(ITrack).GetProperties();

		private static readonly IEnumerable<PropertyInfo> SortablePropertyInfos =
			from prop in PropertyInfos
			let type = prop.PropertyType
			let name = type.Name
			where !type.IsArray && name != "TagTypes" && name != "TrackStatus"
			select prop;

		public static readonly string[] SortableTags = SortablePropertyInfos.Select(p => p.Name).ToArray();

		private static readonly IEnumerable<PropertyInfo> StringPropertyInfos =
			from prop in PropertyInfos
			where prop.PropertyType.Name == "String"
			select prop;

		public static readonly string[] StringTags = StringPropertyInfos.Select(p => p.Name).ToArray();

		private static readonly IEnumerable<PropertyInfo> WritableTextPropertyInfos =
			from prop in PropertyInfos
			let type = prop.PropertyType.Name
			where prop.CanWrite && type.StartsWith("String")
			select prop;

		public static readonly string[] WritableTextTags = WritableTextPropertyInfos.Select(p => p.Name).ToArray();

		public static string AmpersandEscape(this string s)
		{
			return s.Replace("&", "&&");
		}

		public static string AmpersandUnescape(this string s)
		{
			return s.Replace("&&", "&");
		}

		public static PropertyInfo GetPropertyInfo(string propertyName)
		{
			return PropertyInfos.FirstOrDefault(p => p.Name == propertyName);
		}

		public static Type GetPropertyType(string propertyName)
		{
			return GetPropertyInfo(propertyName).PropertyType;
		}

		public static string GetPropertyTypeName(string propertyName)
		{
			return GetPropertyType(propertyName).Name;
		}

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
	}
}
