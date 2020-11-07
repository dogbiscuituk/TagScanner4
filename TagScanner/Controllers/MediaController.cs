namespace TagScanner.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using TagScanner.Models;
    using TagScanner.Properties;

    public class MediaController : MruController
    {
        #region Public Interface

        public MediaController(LibraryFormController libraryFormController, ToolStripDropDownItem recentMenu)
            : base(libraryFormController.Model, "MediaMRU", recentMenu) => _libraryFormController = libraryFormController;

        public void AddFiles()
        {
            if (OpenFileDialog.ShowDialog(_libraryFormController.View) == DialogResult.OK)
                AddFiles(OpenFileDialog.FileNames);
        }

        public void AddFolder()
        {
            if (FolderBrowserDialog.ShowDialog(_libraryFormController.View) == DialogResult.OK)
            {
                var folderPath = FolderBrowserDialog.SelectedPath;
                var filters = OpenFileDialog.Filter.Split('|');
                var filterIndex = OpenFileDialog.FilterIndex;
                var filter = filters[2 * filterIndex - 1];
                AddItem(MakeItem(folderPath, filter));
                AddFolder(folderPath, filter);
            }
        }

        public void Rescan()
        {
            foreach (var folder in Model.Folders)
            {
                var folderParts = folder.Split('|');
                AddFolder(folderParts[0], folderParts[1]);
            }
        }

        protected override void Reopen(ToolStripItem menuItem)
        {
            var item = menuItem.Tag.ToString();
            var itemParts = item.Split('|');
            var folderPath = itemParts[0];
            var filter = itemParts[1];
            if (Directory.Exists(folderPath))
                AddFolder(folderPath, filter);
            else if (
                MessageBox.Show(
                    string.Format(Resources.S_FolderNoLongerExists, folderPath),
                    Resources.S_AddRecentFolder,
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                RemoveItem(item);
        }

        #endregion

        #region Private Properties

        private readonly LibraryFormController _libraryFormController;
        private FolderBrowserDialog _folderBrowserDialog;
        private OpenFileDialog _openFileDialog;

        private FolderBrowserDialog FolderBrowserDialog =>
            _folderBrowserDialog ?? (_folderBrowserDialog = new FolderBrowserDialog
            {
                Description = Resources.S_SelectTheMediaFolderToAdd
            });

        private OpenFileDialog OpenFileDialog =>
            _openFileDialog ?? (_openFileDialog = new OpenFileDialog
            {
                Filter = Settings.Default.MediaFilter,
                Multiselect = true,
                Title = Resources.S_SelectTheMediaFilesToAdd
            });

        #endregion

        #region Private Methods

        private IProgress<ProgressEventArgs> CreateNewProgress() => _libraryFormController.StatusController.CreateNewProgress();

        private void AddFiles(string[] filePaths)
        {
            var progress = CreateNewProgress();
            Task.Run(() => Model.AddFiles(filePaths, progress));
        }

        private void AddFolder(string folderPath, string filter)
        {
            var progress = CreateNewProgress();
            Task.Run(() => Model.AddFolder(folderPath, filter, progress));
        }

        private static string MakeItem(string folderPath, string filter) => string.Concat(folderPath, '|', filter);

        #endregion
    }
}
