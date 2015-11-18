using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TagScanner.Models
{
	public class SimpleCondition
	{
		#region Public Interface

		#region Lifetime Management

		public SimpleCondition(string text)
		{
			PropertyName = TakeWord(ref text);
			foreach (var @operator in AllOperators)
				if (text.StartsWith(@operator))
				{
					Operator = @operator;
					ValueString = text.Substring(@operator.Length).TrimStart();
					return;
				}
		}

		#endregion

		#region Properties

		public string Operator { get; private set; }

		public string PropertyName { get; private set; }

		public string ValueString { get; private set; }

		#endregion

		#region Methods

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

		public Expression ToExpression(ParameterExpression parameter)
		{
			Expression
				leftOperand = Expression.Convert(Expression.Property(parameter, PropertyName), PropertyType),
				rightOperand = Metadata.StringTags.Contains(ValueString)
					? Expression.Convert(Expression.Property(parameter, ValueString), PropertyType)
					: (Expression)Expression.Constant(Value);
			var binaryType = OperatorToExpressionType(Operator);
			if (PropertyTypeName == "String")
			{
				leftOperand = NullCheck(leftOperand);
				rightOperand = NullCheck(rightOperand);
				var methodName = OperatorToMethodName(Operator);
				var methodInfo = typeof(string).GetMethod(methodName, new[] { typeof(string) });
				leftOperand = Expression.Call(leftOperand, methodInfo, rightOperand);
				switch (Operator)
				{
					case Models.Operator.Containing:
					case Models.Operator.StartingWith:
					case Models.Operator.EndingWith:
						return leftOperand;
					case Models.Operator.NotContaining:
					case Models.Operator.NotStartingWith:
					case Models.Operator.NotEndingWith:
						return Expression.Not(leftOperand);
				}
				rightOperand = Expression.Constant(0);
			}
			return Expression.MakeBinary(binaryType, leftOperand, rightOperand);
		}

		#endregion

		#endregion

		#region Private Implementation

		#region Properties

		private Type PropertyType
		{
			get
			{
				return Metadata.GetPropertyInfo(PropertyName).PropertyType;
			}
		}

		private string PropertyTypeName
		{
			get
			{
				return PropertyType.Name;
			}
		}

		private object Value
		{
			get
			{
				switch (PropertyTypeName)
				{
					case "DateTime":
						return DateTime.Parse(ValueString);
					case "Int32":
						return Convert.ToInt32(ValueString);
					case "Int64":
						return Convert.ToInt64(ValueString);
					case "Logical":
						return ValueString == "true" ? Logical.Yes : Logical.No;
					case "String":
						return ValueString;
					case "TimeSpan":
						return TimeSpan.Parse(ValueString);
				}
				return null;
			}
		}

		#endregion

		#region Static Properties

		private static string[] ComparableTypes = new[]
		{
			"DateTime",
			"Int32",
			"Int64",
			"String",
			"TimeSpan"
		};

		private static string[] ComparisonOperators = new[]
		{
			Models.Operator.LessThan,
			Models.Operator.NotGreaterThan,
			Models.Operator.NotLessThan,
			Models.Operator.GreaterThan
		};

		private static string[] EqualityOperators = new[]
		{
			Models.Operator.Equal,
			Models.Operator.NotEqual
		};

		private static string[] StringTypes = new[]
		{
			"String"
		};

		private static string[] StringOperators = new[]
		{
			Models.Operator.Containing,
			Models.Operator.StartingWith,
			Models.Operator.EndingWith,
			Models.Operator.NotContaining,
			Models.Operator.NotStartingWith,
			Models.Operator.NotEndingWith
		};

		private static IEnumerable<string> AllOperators =
			StringOperators
			.Union(EqualityOperators)
			.Union(ComparisonOperators);

		#endregion

		#region Static Methods

		private static BinaryExpression NullCheck(Expression expression)
		{
			return Expression.Coalesce(expression, Expression.Constant(string.Empty));
		}

		private static ExpressionType OperatorToExpressionType(string @operator)
		{
			switch (@operator)
			{
				case Models.Operator.Equal:
					return ExpressionType.Equal;
				case Models.Operator.NotEqual:
					return ExpressionType.NotEqual;
				case Models.Operator.LessThan:
					return ExpressionType.LessThan;
				case Models.Operator.NotGreaterThan:
					return ExpressionType.LessThanOrEqual;
				case Models.Operator.NotLessThan:
					return ExpressionType.GreaterThanOrEqual;
				case Models.Operator.GreaterThan:
					return ExpressionType.GreaterThan;
			}
			return ExpressionType.Equal;
		}

		private static string OperatorToMethodName(string @operator)
		{
			switch (@operator)
			{
				case Models.Operator.Containing:
				case Models.Operator.NotContaining:
					return "Contains";
				case Models.Operator.StartingWith:
				case Models.Operator.NotStartingWith:
					return "StartsWith";
				case Models.Operator.EndingWith:
				case Models.Operator.NotEndingWith:
					return "EndsWith";
				default:
					return "CompareTo";
			}
		}

		private static string TakeWord(ref string text)
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

		#endregion

		#endregion
	}
}
