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
			PersistenceController = new PersistenceController(Model, View, View.FileReopen);
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
			if (PersistenceController.Clear())
				MediaController.AddFolder();
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
			EditQuery();
		}

		private void EditReplace_Click(object sender, EventArgs e)
		{
			ReplaceDialogController.ShowDialog(View);
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
            var tracks = Model.Tracks.Where(t => (t.Status & TrackStatus.Changed) != 0).ToList();
			if (!tracks.Any())
				return true;
			var message = new StringBuilder();
			Say(message, tracks, TrackStatus.Changed, Resources.TracksChanged);
			Say(message, tracks, TrackStatus.Deleted, Resources.TracksDeleted);
			Say(message, tracks, TrackStatus.Updated, Resources.TracksUpdated);
			Say(message, tracks, TrackStatus.Pending, Resources.TracksPending);
			message.Append(Resources.ConfirmSync);
			var decision = MessageBox.Show(
				message.ToString(),
				Resources.ConfirmSyncCaption,
				MessageBoxButtons.YesNoCancel,
				MessageBoxIcon.Question);
			switch (decision)
			{
				case DialogResult.Yes:
					foreach (var track in tracks)
						ProcessTrack(track);
					break;
				case DialogResult.Cancel:
					return false;
			}
			return true;
		}

		private bool ProcessTrack(Track track)
		{
			switch (track.Status)
			{
				case TrackStatus.Deleted:
					return DropTrack(track);
				case TrackStatus.Updated:
					return LoadTrack(track);
				case TrackStatus.Pending:
					return SaveTrack(track);
			}
			return false;
		}

		private List<Track> GetTracks(TrackStatus status)
		{
			return Model.Tracks.Where(track => track.Status == status).ToList();
		}

		private void Say(StringBuilder message, List<Track> tracks, TrackStatus status, string format)
		{
			var count = tracks.Count(t => t.Status == status);
			if (count > 0)
				message.AppendFormat(format, count);
		}

		private bool DropTrack(Track track)
		{
			return Model.Tracks.Remove(track);
		}

		private bool LoadTrack(Track track)
		{
			var result = true;
			try
			{
				track.Load();
			}
			catch (IOException ex)
			{
				MessageBox.Show(View, ex.Message, "Error reading track", MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = false;
			}
			return result;
		}

		private bool SaveTrack(Track track)
		{
			var result = true;
			try
			{
				track.Save();
			}
			catch (IOException ex)
			{
				MessageBox.Show(View, ex.Message, "Error writing track", MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = false;
			}
			return result;
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

		private ReplaceDialogController _replaceDialogController;
		private ReplaceDialogController ReplaceDialogController
		{
			get
			{
				return
					_replaceDialogController
					?? (_replaceDialogController = new ReplaceDialogController(LibraryGridController));
			}
		}
	}
}
