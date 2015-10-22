using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagScanner.Models
{
	public class CompoundCondition
	{
		#region Public Interface

		#region Lifetime Management

		public CompoundCondition(string text)
		{

		}

		#endregion

		public string Quantifier { get; private set; }

		public static readonly string[] Quantifiers = new[]
		{
			"All of these are true:",
			"At least one of these is true:",
			"At least one of these is false:",
			"All of these are false:"
		};

		#endregion
	}
}