﻿using AxWMPLib;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class PlayerController : SdiController
	{
		public PlayerController(GridFormController gridFormController, ToolStripDropDownItem recentMenu)
			: base(gridFormController.Model, Properties.Settings.Default.PlayerFilter, "PlayerMRU", recentMenu)
		{
			GridFormController = gridFormController;
		}

		private GridFormController _gridFormController;
		private GridFormController GridFormController
		{
			get
			{
				return _gridFormController;
            }
			set
			{
				if (GridFormController != null)
				{
					View.PopupPlaylistCreateNew.Click -= PlaylistCreateNew_Click;
					View.PopupPlaylistAddToCurrent.Click -= PlaylistAddToCurrent_Click;
				}
				_gridFormController = value;
				if (GridFormController != null)
				{
					View.PopupPlaylistCreateNew.Click += PlaylistCreateNew_Click;
					View.PopupPlaylistAddToCurrent.Click += PlaylistAddToCurrent_Click;
				}
			}
		}

		private GridForm View
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
			var tracks = GridFormController.GridController.Selection.Tracks;
			if (!tracks.Any())
				return;
			if (newPlaylist)
				Player.currentPlaylist = Player.newPlaylist(string.Empty, string.Empty);
			foreach (var track in tracks)
				Player.currentPlaylist.appendItem(Player.newMedia(track.FilePath));
			Player.Ctlcontrols.play();
			View.TabControl.SelectedTab = View.tabPlayer;
		}

		protected override bool LoadFromStream(Stream stream)
		{
			return true;
		}

		protected override bool SaveToStream(Stream stream)
		{
			return true;
		}

		protected override void ClearDocument()
		{
		}
	}
}
