using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TagScanner.Models;
using TagScanner.Properties;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class LibraryFormController
	{
		#region Lifetime Management

		public LibraryFormController()
		{
			View = new LibraryForm();
			Model = new Model();
			Model.ModifiedChanged += Model_ModifiedChanged;
			LibraryGridController = new LibraryGridController(Model, View.GridElementHost);
			LibraryGridController.SelectionChanged += LibraryGridController_SelectionChanged;
			StatusController = new StatusController(Model, View.StatusBar);
			PersistenceController = new PersistenceController(Model, View.FileReopen);
            PersistenceController.FilePathChanged += PersistenceController_FilePathChanged;
			PersistenceController.FileSaving += PersistenceController_FileSaving;
			MediaController = new MediaController(this, View.AddRecentFolders);
			PlayerController = new PlayerController(this, null);
			new PictureController(View.PictureBox, View.PropertyGrid, PlayerController.PlaylistGrid);
			ModifiedChanged();
			LibraryGridController.ViewByArtist();
			UpdatePropertyGrid();
		}

		#endregion

		#region View

		private LibraryForm _view;
		public LibraryForm View
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
				View.ViewByArtistAlbum.Click += ViewByArtistAlbum_Click;
				View.ViewByArtist.Click += ViewByArtist_Click;
				View.ViewByGenre.Click += ViewByGenre_Click;
				View.ViewByYear.Click += ViewByYear_Click;
				View.ViewByAlbum.Click += ViewByAlbum_Click;
				View.ViewByNone.Click += ViewByNone_Click;
				View.ViewRefresh.Click += ViewRefresh_Click;
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
		public readonly LibraryGridController LibraryGridController;
		public readonly MediaController MediaController;
		public readonly PersistenceController PersistenceController;
		public readonly PlayerController PlayerController;
		public readonly StatusController StatusController;

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
			LibraryGridController.SelectAll();
		}

		private void EditInvertSelection_Click(object sender, EventArgs e)
		{
			LibraryGridController.InvertSelection();
		}

		private void EditFind_Click(object sender, EventArgs e)
		{
			FindReplaceDialogController.ShowDialog(View, false);
		}

		private void EditReplace_Click(object sender, EventArgs e)
		{
			FindReplaceDialogController.ShowDialog(View, true);
		}

		private void ViewByArtistAlbum_Click(object sender, EventArgs e)
		{
			LibraryGridController.ViewByArtistAlbum();
		}

		private void ViewByArtist_Click(object sender, EventArgs e)
		{
			LibraryGridController.ViewByArtist();
		}

		private void ViewByAlbum_Click(object sender, EventArgs e)
		{
			LibraryGridController.ViewByAlbum();
		}

		private void ViewByNone_Click(object sender, EventArgs e)
		{
			LibraryGridController.ViewByNone();
		}

		private void ViewByGenre_Click(object sender, EventArgs e)
		{
			LibraryGridController.ViewByGenre();
		}

		private void ViewByYear_Click(object sender, EventArgs e)
		{
			LibraryGridController.ViewByYear();
		}

		private void ViewRefresh_Click(object sender, EventArgs e)
		{
			MediaController.Rescan();
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
				string.Format("{0}\n{1}\nVersion {2}", 
					Application.CompanyName,
					Application.ProductName,
					Application.ProductVersion),
				string.Concat("About ", Application.ProductName));
		}

		#endregion

		#region Popup Menus

		private void PopupTags_Click(object sender, EventArgs e)
		{
			LibraryGridController.EditTagVisibility();
		}

		private void PopupFilters_Click(object sender, EventArgs e)
		{
			EditQuery();
		}

		private void PropertyGridPopupTagVisibility_Click(object sender, EventArgs e)
		{
			var visibleTags = Metadata.GetVisibleTags();
			var ok = new TagSelectorController(Metadata.SelectionPropertyInfos).Execute(visibleTags);
			if (ok)
			{
				Metadata.SetVisibleTags(visibleTags);
				UpdatePropertyGrid();
			}
		}

		#endregion

		#region Event Handlers

		private void FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !PersistenceController.SaveIfModified();
		}

		private void LibraryGridController_SelectionChanged(object sender, EventArgs e)
		{
			UpdatePropertyGrid();
		}

		private void Model_ModifiedChanged(object sender, EventArgs e)
		{
			ModifiedChanged();
		}

		private void ModifiedChanged()
		{
			View.Text = PersistenceController.WindowCaption;
			View.ModifiedLabel.Visible = Model.Modified;
		}

		private void PersistenceController_FileSaving(object sender, CancelEventArgs e)
		{
			e.Cancel = !ContinueSaving();
		}

		private bool ContinueSaving()
		{
            var tracks = Model.Tracks.Where(t => (t.FileStatus & FileStatus.Changed) != 0).ToList();
			if (!tracks.Any())
				return true;
			var message = new StringBuilder();
			Say(message, tracks, FileStatus.Changed, Resources.TracksChanged);
			Say(message, tracks, FileStatus.New, Resources.TracksAdded);
			Say(message, tracks, FileStatus.Updated, Resources.TracksUpdated);
			Say(message, tracks, FileStatus.Pending, Resources.TracksPending);
			Say(message, tracks, FileStatus.Deleted, Resources.TracksDeleted);
			message.Append(Resources.ConfirmSync);
			var decision = MessageBox.Show(
				message.ToString(),
				Resources.ConfirmSyncCaption,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question) == DialogResult.Yes;
			if (decision)
				foreach (var track in tracks)
					ProcessTrack(track);
			return decision;
		}

		private bool ProcessTrack(Track track)
		{
			var result = false;
			try
			{
				result = Model.ProcessTrack(track);
			}
			catch (IOException ex)
			{
				MessageBox.Show(View, ex.Message, "Error streaming track", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return result;
		}

		private List<Track> GetTracks(FileStatus status)
		{
			return Model.Tracks.Where(track => track.FileStatus == status).ToList();
		}

		private void Say(StringBuilder message, List<Track> tracks, FileStatus status, string format)
		{
			var count = tracks.Count(t => (t.FileStatus & status) != 0);
			if (count > 0)
				message.AppendFormat(format, count);
		}

		private void PersistenceController_FilePathChanged(object sender, EventArgs e)
		{
			View.Text = PersistenceController.WindowCaption;
		}

		#endregion

		private void EditQuery()
		{
			FilterDialogController.ShowDialog(View);
		}

		private void UpdatePropertyGrid()
		{
			View.PropertyGrid.SelectedObject = LibraryGridController.Selection;
		}

		private FilterDialogController _filterDialogController;
		private FilterDialogController FilterDialogController
		{
			get
			{
				return
					_filterDialogController
					?? (_filterDialogController = new FilterDialogController(LibraryGridController, null));
			}
		}

		private FindReplaceDialogController _findReplaceDialogController;
		private FindReplaceDialogController FindReplaceDialogController
		{
			get
			{
				return
					_findReplaceDialogController
					?? (_findReplaceDialogController = new FindReplaceDialogController(LibraryGridController));
			}
		}
	}
}
