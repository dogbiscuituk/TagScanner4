﻿namespace TagScanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

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

        public string Operator { get; }

        public string PropertyName { get; }

        public string ValueString { get; }

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
                    case Operators.Containing:
                    case Operators.StartingWith:
                    case Operators.EndingWith:
                        return leftOperand;
                    case Operators.NotContaining:
                    case Operators.NotStartingWith:
                    case Operators.NotEndingWith:
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

        private Type PropertyType => Metadata.GetPropertyInfo(PropertyName).PropertyType;

        private string PropertyTypeName => PropertyType.Name;

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

        private static readonly string[] ComparableTypes = new[]
        {
            "DateTime",
            "Int32",
            "Int64",
            "String",
            "TimeSpan"
        };

        private static readonly string[] ComparisonOperators = new[]
        {
            Operators.LessThan,
            Operators.NotGreaterThan,
            Operators.NotLessThan,
            Operators.GreaterThan
        };

        private static readonly string[] EqualityOperators = new[]
        {
            Operators.Equal,
            Operators.NotEqual
        };

        private static readonly string[] StringTypes = new[]
        {
            "String"
        };

        private static readonly string[] StringOperators = new[]
        {
            Operators.Containing,
            Operators.StartingWith,
            Operators.EndingWith,
            Operators.NotContaining,
            Operators.NotStartingWith,
            Operators.NotEndingWith
        };

        private static readonly IEnumerable<string> AllOperators =
            StringOperators
            .Union(EqualityOperators)
            .Union(ComparisonOperators);

        #endregion

        #region Static Methods

        private static BinaryExpression NullCheck(Expression expression) => Expression.Coalesce(expression, Expression.Constant(string.Empty));

        private static ExpressionType OperatorToExpressionType(string @operator)
        {
            switch (@operator)
            {
                case Operators.Equal:
                    return ExpressionType.Equal;
                case Operators.NotEqual:
                    return ExpressionType.NotEqual;
                case Operators.LessThan:
                    return ExpressionType.LessThan;
                case Operators.NotGreaterThan:
                    return ExpressionType.LessThanOrEqual;
                case Operators.NotLessThan:
                    return ExpressionType.GreaterThanOrEqual;
                case Operators.GreaterThan:
                    return ExpressionType.GreaterThan;
            }
            return ExpressionType.Equal;
        }

        private static string OperatorToMethodName(string @operator)
        {
            switch (@operator)
            {
                case Operators.Containing:
                case Operators.NotContaining:
                    return "Contains";
                case Operators.StartingWith:
                case Operators.NotStartingWith:
                    return "StartsWith";
                case Operators.EndingWith:
                case Operators.NotEndingWith:
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
