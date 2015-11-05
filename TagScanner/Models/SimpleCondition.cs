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

		public string Operation { get; private set; }

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
			var op = OperatorToExpressionType(Operation);
			if (PropertyTypeName == "String")
			{
				leftOperand = NullCheck(leftOperand);
				rightOperand = NullCheck(rightOperand);
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

		#region Static Methods

		private static BinaryExpression NullCheck(Expression expression)
		{
			return Expression.Coalesce(expression, Expression.Constant(string.Empty));
		}

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
