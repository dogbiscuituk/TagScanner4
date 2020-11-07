namespace TagScanner
{
    using System;
    using System.Windows.Forms;
    using TagScanner.Controllers;

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LibraryFormController().View);
        }
    }
}
