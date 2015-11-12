using AxWMPLib;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WMPLib;
using TagScanner.Views;
using TagScanner.Models;
using System.Collections.Generic;

namespace TagScanner.Controllers
{
	public class PlayerController : SdiController
	{
		public PlayerController(LibraryFormController gridFormController, ToolStripDropDownItem recentMenu)
			: base(gridFormController.Model, Properties.Settings.Default.PlayerFilter, "PlayerMRU", recentMenu)
		{
			GridFormController = gridFormController;
			Player.CurrentItemChange += Player_CurrentItemChange;
		}

		private LibraryFormController _gridFormController;
		private LibraryFormController GridFormController
		{
			get
			{
				return _gridFormController;
			}
			set
			{
				_gridFormController = value;
				View.GridPopupPlay.Click += PlaylistAddToCurrent_Click;
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

		private DataGridView PlaylistGrid
		{
			get
			{
				return View.PlaylistGrid;
			}
		}

		private List<Track> CurrentPlaylist = new List<Track>();

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
			{
				CurrentPlaylist = new List<Track>();
				Player.currentPlaylist = Player.newPlaylist(string.Empty, string.Empty);
			}
			CurrentPlaylist.AddRange(tracks);
			PlaylistGrid.DataSource = CurrentPlaylist;
            foreach (var track in tracks)
				Player.currentPlaylist.appendItem(Player.newMedia(track.FilePath));
			Player.Ctlcontrols.play();
			View.TabControl.SelectedTab = View.tabPlayer;
		}

		private void Player_CurrentItemChange(object sender, _WMPOCXEvents_CurrentItemChangeEvent e)
		{
			UpdatePlaylist(e.pdispMedia as IWMPMedia);
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

		private void UpdatePlaylist(IWMPMedia currentItem)
		{
			for (var index = 0; index < CurrentPlaylist.Count; index++)
				if (CurrentPlaylist[index].FilePath == currentItem.sourceURL)
				{
					PlaylistGrid.ClearSelection();
                    PlaylistGrid.Rows[index].Selected = true;
					break;
				}
		}
	}
}
