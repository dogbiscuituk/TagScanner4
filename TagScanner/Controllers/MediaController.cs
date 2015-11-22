using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagScanner.Models;

namespace TagScanner.Controllers
{
	public class MediaController : MruController
	{
		public MediaController(LibraryFormController libraryFormController, ToolStripDropDownItem recentMenu)
			: base(libraryFormController.Model, "MediaMRU", recentMenu)
		{
            var filter = Properties.Settings.Default.MediaFilter;
			OpenFileDialog = new OpenFileDialog
			{
				Filter = filter,
				Multiselect = true,
				Title = "Select the media file(s) to add"
			};
			FolderBrowserDialog = new FolderBrowserDialog
			{
				Description = "Select the media folder to add"
			};
			LibraryFormController = libraryFormController;
		}

		public void AddFiles()
		{
			if (OpenFileDialog.ShowDialog(LibraryFormController.View) == DialogResult.OK)
				AddFiles(OpenFileDialog.FileNames);
		}

		public void AddFolder()
		{
			if (FolderBrowserDialog.ShowDialog(LibraryFormController.View) == DialogResult.OK)
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
			else if (MessageBox.Show(
				string.Format("Folder \"{0}\" no longer exists. Remove from menu?", folderPath),
				"Add Recent Folder",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
				RemoveItem(item);
		}

		private readonly FolderBrowserDialog FolderBrowserDialog;
		private readonly OpenFileDialog OpenFileDialog;
		private readonly LibraryFormController LibraryFormController;

		private IProgress<ProgressEventArgs> CreateNewProgress()
		{
			return LibraryFormController.StatusController.CreateNewProgress();
		}

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

		private string MakeItem(string folderPath, string filter)
		{
			return string.Concat(folderPath, '|', filter);
        }
	}
}
