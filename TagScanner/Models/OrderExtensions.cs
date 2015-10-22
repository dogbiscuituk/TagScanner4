using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TagScanner.Models
{
	public static class OrderExtensions
	{
		#region Public Interface

		public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, string propertyName)
		{
			return source.OrderBy(GetFunc<T>(propertyName));
		}

		public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
		{
			return source.OrderBy(GetExpression<T>(propertyName));
		}

		public static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> source, string propertyName)
		{
			return source.OrderByDescending(GetFunc<T>(propertyName));
		}

		public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
		{
			return source.OrderByDescending(GetExpression<T>(propertyName));
		}

		public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> source, string propertyName)
		{
			return source.ThenBy(GetFunc<T>(propertyName));
		}

		public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
		{
			return source.ThenBy(GetExpression<T>(propertyName));
		}

		public static IOrderedEnumerable<T> ThenByDescending<T>(this IOrderedEnumerable<T> source, string propertyName)
		{
			return source.ThenByDescending(GetFunc<T>(propertyName));
		}

		public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
		{
			return source.ThenByDescending(GetExpression<T>(propertyName));
		}

		#endregion

		#region Private Implementation

		private static Expression<Func<T, object>> GetExpression<T>(string propertyName)
		{
			var expression = Expression.Parameter(typeof(T), "propertyName");
			var conversion = Expression.Convert(Expression.Property(expression, propertyName), typeof(object));
			return Expression.Lambda<Func<T, object>>(conversion, expression);
		}

		private static Func<T, object> GetFunc<T>(string propertyName)
		{
			return GetExpression<T>(propertyName).Compile();
		}

		#endregion
	}
}
