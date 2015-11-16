using System;
using System.Linq;
using System.Windows.Forms;
using TagScanner.Models;

namespace TagScanner.Controllers
{
	public class PictureController
	{
		#region Lifetime Management

		public PictureController(PictureBox pictureBox, PropertyGrid propertyGrid, System.Windows.Controls.DataGrid playlistGrid)
		{
			PictureBox = pictureBox;
			PropertyGrid = propertyGrid;
			PlaylistGrid = playlistGrid;
		}

		#endregion

		#region Properties

		private Picture _picture;
		private Picture Picture
		{
			get { return _picture; }
			set
			{
				if (_picture == value)
					return;
				_picture = value;
				if (Picture != null)
				{
					PictureBox.Image = Picture.GetImage();
					InitSizeMode();
				}
				else
					PictureBox.Image = null;
			}
		}

		private PictureBox _pictureBox;
		private PictureBox PictureBox
		{
			get { return _pictureBox; }
			set
			{
				_pictureBox = value;
				PictureBox.Resize += PictureBox_Resize;
			}
		}

		private PropertyGrid _propertyGrid;
		private PropertyGrid PropertyGrid
		{
			get
			{
				return _propertyGrid;
			}
			set
			{
				_propertyGrid = value;
				PropertyGrid.SelectedGridItemChanged += PropertyGrid_SelectedGridItemChanged;
				PropertyGrid.SelectedObjectsChanged += PropertyGrid_SelectedObjectsChanged;
			}
		}

		private System.Windows.Controls.DataGrid _playlistGrid;
		private System.Windows.Controls.DataGrid PlaylistGrid
		{
			get
			{
				return _playlistGrid;
			}
			set
			{
				_playlistGrid = value;
				PlaylistGrid.SelectionChanged += PlaylistGrid_SelectionChanged;
			}
		}

		#endregion

		#region Events

		private void PictureBox_Resize(object sender, EventArgs e)
		{
			InitSizeMode();
		}

		private void PlaylistGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
            InitPictureFromTrack(PlaylistGrid.SelectedItem as ITrack);
		}

		private void PropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
		{
			InitPicture();
		}

		private void PropertyGrid_SelectedObjectsChanged(object sender, EventArgs e)
		{
			InitPicture();
		}

		#endregion

		#region Methods

		private void InitPicture()
		{
			// If a Picture is selected in the PropertyGrid,
			// then use that particular Picture.
			var gridItem = PropertyGrid.SelectedGridItem;
			if (gridItem != null)
			{
				var picture = gridItem.Value as Picture;
				if (picture != null)
				{
					Picture = picture;
					return;
				}
			}
			// If no Picture is selected in the PropertyGrid,
			// then use the first Picture in the selection, if any.
			InitPictureFromTrack(PropertyGrid.SelectedObject as Selection);
		}

		private void InitPictureFromTrack(ITrack track)
		{
			Picture = track != null && track.Pictures.Any() ? track.Pictures[0] : null;
		}

		private void InitSizeMode()
		{
			var image = PictureBox.Image;
			if (image == null)
				return;
			PictureBox.SizeMode =
				image.Width > PictureBox.Width || image.Height > PictureBox.Height
					? PictureBoxSizeMode.Zoom
					: PictureBoxSizeMode.CenterImage;
		}

		#endregion
	}
}
