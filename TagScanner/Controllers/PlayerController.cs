namespace TagScanner.Controllers
{
    using AxWMPLib;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Data;
    using System.Windows.Forms;
    using TagScanner.Models;
    using TagScanner.Views;
    using WMPLib;

    public class PlayerController : GridController
    {
        public PlayerController(LibraryFormController gridFormController, ToolStripDropDownItem recentMenu)
        {
            GridFormController = gridFormController;
            View.PlaylistElementHost.Child = new GridElement();
            DataGrid.AutoGenerateColumns = false;
            InitColumns();
            DataGrid.ItemsSource = new ListCollectionView(CurrentPlaylist);
            Player.CurrentItemChange += Player_CurrentItemChange;
        }

        public System.Windows.Controls.DataGrid PlaylistGrid => DataGrid;

        private LibraryFormController _gridFormController;
        private LibraryFormController GridFormController
        {
            get => _gridFormController;
            set
            {
                _gridFormController = value;
                View.GridPopupPlayAddToQueue.Click += PlaylistAddToQueue_Click;
                View.GridPopupPlayNewPlaylist.Click += PlaylistCreateNew_Click;
            }
        }

        private LibraryForm View => GridFormController.View;

        private AxWindowsMediaPlayer Player => View.MediaPlayer;

        protected override System.Windows.Controls.DataGrid DataGrid => ((GridElement)View.PlaylistElementHost.Child).DataGrid;

        private readonly ObservableCollection<Track> CurrentPlaylist = new ObservableCollection<Track>();

        private void PlaylistAddToQueue_Click(object sender, EventArgs e) => PlaySelection(newPlaylist: false);

        private void PlaylistCreateNew_Click(object sender, EventArgs e) => PlaySelection(newPlaylist: true);

        private void PlaySelection(bool newPlaylist)
        {
            var tracks = GridFormController.LibraryGridController.Selection.Tracks;
            if (!tracks.Any())
                return;
            if (newPlaylist)
            {
                CurrentPlaylist.Clear();
                Player.currentPlaylist = Player.newPlaylist(string.Empty, string.Empty);
            }
            foreach (var track in tracks)
            {
                CurrentPlaylist.Add(track);
                Player.currentPlaylist.appendItem(Player.newMedia(track.FilePath));
            }
            Player.Ctlcontrols.play();
            View.TabControl.SelectedTab = View.tabPlayer;
        }

        private void Player_CurrentItemChange(object sender, _WMPOCXEvents_CurrentItemChangeEvent e) => UpdatePlaylist(e.pdispMedia as IWMPMedia);

        private void UpdatePlaylist(IWMPMedia currentItem)
        {
            foreach (var t in CurrentPlaylist)
                if (t.FilePath == currentItem.sourceURL)
                {
                    DataGrid.SelectedItems.Clear();
                    DataGrid.SelectedItems.Add(t);
                    break;
                }
        }

        protected override PropertyInfo[] GetPropertyInfos() => new[]
                {
                    "Title",
                    "JoinedPerformers",
                    "Album"
                }
                .Select(name => Metadata.TrackPropertyInfos.First(p => p.Name == name))
                .ToArray();
    }
}
