using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TagScanner.ValueConverters;

namespace TagScanner.Models
{
	public class SimpleCondition
	{
		#region Public Interface

		#region Lifetime Management

		public SimpleCondition(string text)
		{
			PropertyName = TakeWord(ref text);
			foreach (var op in AllOperators)
				if (text.StartsWith(op))
				{
					Operation = op;
					ValueString = text.Substring(op.Length).TrimStart();
					return;
				}
		}

		#endregion

		#region Properties

		public static IEnumerable<string> GetOperatorsForType(string propertyTypeName)
		{
			var result = new List<string>();
			if (StringTypes.Contains(propertyTypeName))
				result.AddRange(StringOperators);
			result.AddRange(EqualityOperators);
			if (ComparableTypes.Contains(propertyTypeName))
				result.AddRange(ComparisonOperators);
			return result;
		}

		public string Operation { get; private set; }

		public string PropertyName { get; private set; }

		public Type PropertyType
		{
			get
			{
				return GetPropertyInfo(PropertyName).PropertyType;
            }
		}

		public string PropertyTypeName
		{
			get
			{
				return PropertyType.Name;
			}
		}

		public string ValueString { get; private set; }

		public object Value
		{
			get
			{
				switch (PropertyTypeName)
				{
					case "String":
						return ValueString;
					case "Int32":
						return Convert.ToInt32(ValueString);
					case "Int64":
						return Convert.ToInt64(ValueString);
					case "TimeSpan":
						return TimeSpan.Parse(ValueString);
					case "Logical":
						return ValueString == "true" ? Logical.Yes : Logical.No;
				}
				return null;
			}
		}

		#endregion

		#region Methods

		private static ExpressionType OperatorToExpressionType(string op)
		{
			switch (op)
			{
				case Operator.Equal:
					return ExpressionType.Equal;
				case Operator.NotEqual:
					return ExpressionType.NotEqual;
				case Operator.LessThan:
					return ExpressionType.LessThan;
				case Operator.NotGreaterThan:
					return ExpressionType.LessThanOrEqual;
				case Operator.NotLessThan:
					return ExpressionType.GreaterThanOrEqual;
				case Operator.GreaterThan:
					return ExpressionType.GreaterThan;
			}
			return ExpressionType.Equal;
		}

		private static string OperatorToMethodName(string op)
		{
			switch (op)
			{
				case Operator.Containing:
				case Operator.NotContaining:
					return "Contains";
				case Operator.StartingWith:
				case Operator.NotStartingWith:
					return "StartsWith";
				case Operator.EndingWith:
				case Operator.NotEndingWith:
					return "EndsWith";
				default:
					return "CompareTo";
			}
		}

		public static string TakeWord(ref string text)
		{
			if (string.IsNullOrWhiteSpace(text))
				return string.Empty;
			var p = text.IndexOf(' ');
			if (p < 0)
				p = text.Length;
			var word = text.Substring(0, p);
			text = text.Substring(p).TrimStart();
			return word;
		}

		public Expression ToExpression(ParameterExpression parameter)
		{
			Expression
				property = Expression.Property(parameter, PropertyName),
				leftOperand = Expression.Convert(property, PropertyType),
				rightOperand = Expression.Constant(Value);
			var op = OperatorToExpressionType(Operation);
			if (PropertyTypeName == "String")
			{
				leftOperand = Expression.Coalesce(leftOperand, Expression.Constant(string.Empty));
				var methodName = OperatorToMethodName(Operation);
				var methodInfo = typeof(string).GetMethod(methodName, new[] { typeof(string) });
				leftOperand = Expression.Call(leftOperand, methodInfo, rightOperand);
				switch (Operation)
				{
					case Operator.Containing:
					case Operator.StartingWith:
					case Operator.EndingWith:
						return leftOperand;
					case Operator.NotContaining:
					case Operator.NotStartingWith:
					case Operator.NotEndingWith:
						return Expression.Not(leftOperand);
				}
				rightOperand = Expression.Constant(0);
			}
			return Expression.MakeBinary(op, leftOperand, rightOperand);
		}

		public override string ToString()
		{
			return string.Format("{0} {1} {2}", PropertyName, Operation, ValueString);
		}

		#endregion

		private static readonly PropertyInfo[] PropertyInfos = typeof(ITrack).GetProperties();

		public static readonly IEnumerable<PropertyInfo> SortablePropertyInfos =
			PropertyInfos.Where(
				p => !p.PropertyType.IsArray
				&& p.PropertyType.Name != "TagTypes"
				&& p.PropertyType.Name != "TrackStatus");

		public static readonly string[] SortableColumnNames = SortablePropertyInfos.Select(p => p.Name).ToArray();

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

		#endregion

		#region Private Implementation

		private static string[] ComparableTypes = new[]
		{
			"String",
			"Int32",
			"Int64",
			"TimeSpan"
		};

		private static string[] ComparisonOperators = new[]
		{
			Operator.LessThan,
			Operator.NotGreaterThan,
			Operator.NotLessThan,
			Operator.GreaterThan
		};

		private static string[] EqualityOperators = new[]
		{
			Operator.Equal,
			Operator.NotEqual
		};

		private static string[] StringTypes = new[]
		{
			"String"
		};

		private static string[] StringOperators = new[]
		{
			Operator.Containing,
			Operator.StartingWith,
			Operator.EndingWith,
			Operator.NotContaining,
			Operator.NotStartingWith,
			Operator.NotEndingWith
		};

		private static IEnumerable<string> AllOperators =
			StringOperators
			.Union(EqualityOperators)
			.Union(ComparisonOperators);

		#endregion
	}
}
