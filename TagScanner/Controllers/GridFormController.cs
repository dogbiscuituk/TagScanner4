using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TagScanner.Models;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class GridFormController
	{
		#region Lifetime Management

		public GridFormController()
		{
			View = new GridForm();
			Model = new Model();
			Model.ModifiedChanged += Model_ModifiedChanged;
			GridController = new GridController(Model, View.GridElementHost);
			GridController.SelectionChanged += GridViewController_SelectionChanged;
			new PictureController(View.PictureBox, View.PropertyGrid);
			var statusController = new StatusController(Model, View.StatusBar);
			PersistenceController = new PersistenceController(Model, View, View.FileReopen);
			PersistenceController.FileLoading += PersistenceController_FileLoading;
            PersistenceController.FilePathChanged += PersistenceController_FilePathChanged;
			PersistenceController.FileSaving += PersistenceController_FileSaving;
			MediaController = new MediaController(Model, statusController, View.AddRecentFolders);
			PlayerController = new PlayerController(this, View.PopupPlaylistReopen);
			Model_ModifiedChanged(Model, EventArgs.Empty);
			GridController.ViewByArtist();
			GridViewController_SelectionChanged(this, EventArgs.Empty);
        }

		#endregion

		#region View

		private GridForm _view;
		public GridForm View
		{
			get
			{
				return _view;
			}
			set
			{
				_view = value;
				View.FileMenu.DropDownOpening += FileMenu_DropDownOpening;
				View.FileNew.Click += FileNew_Click;
				View.FileOpen.Click += FileOpen_Click;
				View.FileSave.Click += FileSave_Click;
				View.FileSaveAs.Click += FileSaveAs_Click;
				View.FileExit.Click += FileExit_Click;
				View.EditSelectAll.Click += EditSelectAll_Click;
				View.EditInvertSelection.Click += EditInvertSelection_Click;
				View.EditFind.Click += EditFind_Click;
				View.EditReplace.Click += EditReplace_Click;
				View.ViewByArtist.Click += ViewByArtist_Click;
				View.ViewByGenre.Click += ViewByGenre_Click;
				View.ViewByYear.Click += ViewByYear_Click;
				View.ViewByAlbumTitle.Click += ViewByAlbumTitle_Click;
				View.ViewBySongTitle.Click += ViewBySongTitle_Click;
				View.AddMedia.Click += AddMedia_Click;
				View.AddFolder.Click += AddFolder_Click;
				View.HelpAbout.Click += HelpAbout_Click;
				View.GridPopupTags.Click += PopupTags_Click;
				View.GridPopupFilters.Click += PopupFilters_Click;
				View.PropertyGridPopupTagVisibility.Click += PropertyGridPopupTagVisibility_Click;
				View.FormClosing += FormClosing;
			}
		}

		#endregion

		#region Fields

		public readonly Model Model;
		public readonly GridController GridController;
		public readonly MediaController MediaController;
		public readonly PersistenceController PersistenceController;
		public readonly PlayerController PlayerController;

		#endregion

		#region Main Menu

		private void FileMenu_DropDownOpening(object sender, EventArgs e)
		{
			View.FileSave.Enabled = Model.Modified;
		}

		private void FileNew_Click(object sender, EventArgs e)
		{
			PersistenceController.Clear();
		}

		private void FileOpen_Click(object sender, EventArgs e)
		{
			PersistenceController.Open();
		}

		private void FileSave_Click(object sender, EventArgs e)
		{
			PersistenceController.Save();
		}

		private void FileSaveAs_Click(object sender, EventArgs e)
		{
			PersistenceController.SaveAs();
		}

		private void FileExit_Click(object sender, EventArgs e)
		{
			View.Close();
		}

		private void EditSelectAll_Click(object sender, EventArgs e)
		{
			GridController.SelectAll();
		}

		private void EditInvertSelection_Click(object sender, EventArgs e)
		{
			GridController.InvertSelection();
		}

		private void EditFind_Click(object sender, EventArgs e)
		{
			EditQuery();
		}

		private void EditReplace_Click(object sender, EventArgs e)
		{
			ReplaceDialogController.ShowDialog(View);
		}

		private void ViewByArtist_Click(object sender, EventArgs e)
		{
			GridController.ViewByArtist();
		}

		private void ViewByGenre_Click(object sender, EventArgs e)
		{
			GridController.ViewByGenre();
		}

		private void ViewByYear_Click(object sender, EventArgs e)
		{
			GridController.ViewByYear();
		}

		private void ViewByAlbumTitle_Click(object sender, EventArgs e)
		{
			GridController.ViewByAlbumTitle();
		}

		private void ViewBySongTitle_Click(object sender, EventArgs e)
		{
			GridController.ViewBySongTitle();
		}

		private void AddMedia_Click(object sender, EventArgs e)
		{
			MediaController.AddFiles();
		}

		private void AddFolder_Click(object sender, EventArgs e)
		{
			MediaController.AddFolder();
		}

		private void HelpAbout_Click(object sender, EventArgs e)
		{
			MessageBox.Show(
				string.Concat("Version ", Application.ProductVersion),
				string.Concat("About ", Application.ProductName));
		}

		#endregion

		#region Popup Menus

		private void PopupTags_Click(object sender, EventArgs e)
		{
			//EditQuery(0);
			GridController.EditTagVisibility();
		}

		private void PopupFilters_Click(object sender, EventArgs e)
		{
			EditQuery();
		}

		private void PropertyGridPopupTagVisibility_Click(object sender, EventArgs e)
		{
			var trackVisibleTags = Selection.GetTrackVisibleTags();
			var ok = new TagSelectorController(Selection.TrackPropertyInfos).Execute(trackVisibleTags);
			if (ok)
			{
				Selection.SetTrackVisibleTags(trackVisibleTags);
				UpdatePropertyGrid();
			}
		}

		#endregion

		#region Event Handlers

		private void FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !PersistenceController.SaveIfModified();
		}

		private void Model_ModifiedChanged(object sender, EventArgs e)
		{
			View.Text = PersistenceController.WindowCaption;
			View.ModifiedLabel.Visible = Model.Modified;
		}

		private void GridViewController_SelectionChanged(object sender, EventArgs e)
		{
			UpdatePropertyGrid();
		}

		private void PersistenceController_FileSaving(object sender, CancelEventArgs e)
		{
			var pendingTracks = Model.Tracks.Where(track => track.Status == TrackStatus.Pending).ToList();
			var deletedTracks = Model.Tracks.Where(track => track.Status == TrackStatus.Deleted).ToList();
			if (!pendingTracks.Any() && !deletedTracks.Any())
				return;
			var message = new StringBuilder(
				"You may synchronise the library file with the ID3 track data in your media prior to saving. "
				+ "If you choose to do this, the following operations will be performed:\n\n");
			if (pendingTracks.Any())
				message.AppendFormat(
					"    * {0} track(s) will have embedded ID3 tag data updated to match the content of the library file.\n",
					pendingTracks.Count());
			if (deletedTracks.Any())
				message.AppendFormat(
					"    * {0} track(s), which cannot be found, will have ID3 tag data removed from the library file.\n",
					deletedTracks.Count());
			message.Append("\nDo you want to perform this synchronisation prior to saving?");
			var decision = MessageBox.Show(
				message.ToString(),
				"Synchronise library file (optional)",
				MessageBoxButtons.YesNoCancel,
				MessageBoxIcon.Question);
			switch (decision)
			{
				case DialogResult.Yes:
					foreach (var track in deletedTracks)
						Model.Tracks.Remove(track);
					foreach (var track in pendingTracks)
						track.Save();
					break;
				case DialogResult.Cancel:
					e.Cancel = true;
					break;
			}
		}

		private void PersistenceController_FilePathChanged(object sender, EventArgs e)
		{
			View.Text = PersistenceController.WindowCaption;
		}

		private void PersistenceController_FileLoading(object sender, CancelEventArgs e)
		{
		}

		#endregion

		private void EditQuery()
		{
			FilterDialogController.ShowDialog(View);
		}

		private void UpdatePropertyGrid()
		{
			View.PropertyGrid.SelectedObject = GridController.Selection;
		}

		private FilterDialogController _filterDialogController;
		private FilterDialogController FilterDialogController
		{
			get
			{
				return
					_filterDialogController
					?? (_filterDialogController = new FilterDialogController(GridController, null));
			}
		}

		private ReplaceDialogController _replaceDialogController;
		private ReplaceDialogController ReplaceDialogController
		{
			get
			{
				return
					_replaceDialogController
					?? (_replaceDialogController = new ReplaceDialogController());
			}
		}
	}
}
