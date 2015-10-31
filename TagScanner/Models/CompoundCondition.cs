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

		#endregion
	}
}