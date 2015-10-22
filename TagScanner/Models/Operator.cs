using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagScanner.Models
{
	public class Operator
	{
		public const string Containing = "contains";
		public const string NotContaining = "does not contain";
		public const string NotEndingWith = "does not end with";
		public const string NotStartingWith = "does not start with";
		public const string EndingWith = "ends with";
		public const string Equal = "=";
		public const string GreaterThan = ">";
		public const string NotLessThan = "≥";
		public const string LessThan = "<";
		public const string NotGreaterThan = "≤";
		public const string NotEqual = "≠";
		public const string StartingWith = "starts with";
	}
}
