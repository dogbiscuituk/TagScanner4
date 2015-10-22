using System;
using System.Windows.Forms;
using TagScanner.Controllers;

namespace TagScanner
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new GridFormController().View);
		}
	}

	/*
	Time taken to scan folder C:\Music
	(16,602 files, 2,167 folders, 135 GB)
		1. Single threaded
			1.1. After PC restart: 4 min 18 sec
			1.2. Immediate repeat: 0 min 29 sec
		2. Multi threaded
			2.1. After PC restart: 4 min 45 sec
			2.2. Immediate repeat: 0 min 20 sec
	*/
}
