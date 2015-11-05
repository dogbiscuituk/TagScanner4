using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagScanner.Models;

namespace TagScanner.Controllers
{
	public class MediaController : MruController
	{
		public MediaController(Model model, StatusController statusController, ToolStripDropDownItem recentMenu)
			: base(model, "MediaMRU", recentMenu)
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
			StatusController = statusController;
		}

		public void AddFiles()
		{
			if (OpenFileDialog.ShowDialog() == DialogResult.OK)
				AddFiles(OpenFileDialog.FileNames);
		}

		public void AddFolder()
		{
			if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				var folderPath = FolderBrowserDialog.SelectedPath;
				var filters = OpenFileDialog.Filter.Split('|');
				var filterIndex = OpenFileDialog.FilterIndex;
				var filter = filters[2 * filterIndex - 1];
                AddItem(MakeItem(folderPath, filter));
				AddFolder(folderPath, filter);
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
		private readonly StatusController StatusController;

		private void AddFiles(string[] filePaths)
		{
			var progress = StatusController.CreateNewProgress();
			Task.Run(() => Model.AddFiles(filePaths, progress));
		}

		private void AddFolder(string folderPath, string filter)
		{
			var progress = StatusController.CreateNewProgress();
			Task.Run(() => Model.AddFolder(folderPath, filter, progress));
		}

		private string MakeItem(string folderPath, string filter)
		{
			return string.Concat(folderPath, '|', filter);
        }
	}
}
