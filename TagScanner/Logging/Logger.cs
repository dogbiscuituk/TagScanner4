using System;
using System.Diagnostics;

namespace TagScanner.Logging
{
	public static class Logger
	{
		public static void LogException(Exception ex, string filePath)
		{
			Debug.WriteLine("{0} - {1} - {2}", ex.GetType(), ex.Message, filePath);
		}
	}
}
