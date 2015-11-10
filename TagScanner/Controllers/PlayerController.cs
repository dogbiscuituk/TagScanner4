using AxWMPLib;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class PlayerController : SdiController
	{
		public PlayerController(LibraryController gridFormController, ToolStripDropDownItem recentMenu)
			: base(gridFormController.Model, Properties.Settings.Default.PlayerFilter, "PlayerMRU", recentMenu)
		{
			GridFormController = gridFormController;
		}

		private LibraryController _gridFormController;
		private LibraryController GridFormController
		{
			get
			{
				return _gridFormController;
			}
			set
			{
				_gridFormController = value;
				View.PopupPlaylistCreateNew.Click += PlaylistCreateNew_Click;
				View.PopupPlaylistAddToCurrent.Click += PlaylistAddToCurrent_Click;
			}
		}

		private LibraryForm View
		{
			get
			{
				return GridFormController.View;
			}
		}

		private AxWindowsMediaPlayer Player
		{
			get
			{
				return View.MediaPlayer;
			}
		}

		private void PlaylistCreateNew_Click(object sender, EventArgs e)
		{
			PlaySelection(newPlaylist: true);
		}

		private void PlaylistAddToCurrent_Click(object sender, EventArgs e)
		{
			PlaySelection(newPlaylist: false);
		}

		private void PlaySelection(bool newPlaylist)
		{
			var tracks = GridFormController.LibraryGridController.Selection.Tracks;
			if (!tracks.Any())
				return;
			if (newPlaylist)
				Player.currentPlaylist = Player.newPlaylist(string.Empty, string.Empty);
			foreach (var track in tracks)
				Player.currentPlaylist.appendItem(Player.newMedia(track.FilePath));
			Player.Ctlcontrols.play();
			View.TabControl.SelectedTab = View.tabPlayer;
		}

		protected override bool LoadFromStream(Stream stream, string format)
		{
			return true;
		}

		protected override bool SaveToStream(Stream stream, string format)
		{
			return true;
		}

		protected override void ClearDocument()
		{
		}
	}
}
