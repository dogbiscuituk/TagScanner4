﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NReco.VideoConverter;
using TagScanner.Logging;
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

		private PictureBox _pictureBox;
		private PictureBox PictureBox
		{
			get => _pictureBox;
			set
			{
				_pictureBox = value;
				PictureBox.Resize += PictureBox_Resize;
			}
		}

		private PropertyGrid _propertyGrid;
		private PropertyGrid PropertyGrid
		{
			get => _propertyGrid;
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
			get => _playlistGrid;
			set
			{
				_playlistGrid = value;
				PlaylistGrid.SelectionChanged += PlaylistGrid_SelectionChanged;
			}
		}

		private static readonly RotateFlipType[] RotateFlipTypes =
		{
			RotateFlipType.RotateNoneFlipNone,
			RotateFlipType.RotateNoneFlipNone,
			RotateFlipType.RotateNoneFlipY,
			RotateFlipType.Rotate180FlipNone,
			RotateFlipType.RotateNoneFlipX,
			RotateFlipType.Rotate90FlipY,
			RotateFlipType.Rotate90FlipNone,
			RotateFlipType.Rotate90FlipX,
			RotateFlipType.Rotate270FlipNone
		};

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

		private Image GetImageFromFile(string filePath, TagLib.Image.ImageOrientation orientation)
		{
			Image image;
			try
			{
				image = Image.FromFile(filePath);
			}
			catch (OutOfMemoryException ex)
			{
				Logger.LogException(ex, filePath);
				return null;
			}
			var rotateFlipType = GetRotateFlipType(orientation);
			if (rotateFlipType != RotateFlipType.RotateNoneFlipNone)
				image.RotateFlip(rotateFlipType);
			return image;
		}

		private Image GetImageFromTrack(ITrack track)
		{
			if (track == null)
				return null;
			var pictures = track.Pictures;
			var picture = pictures?.FirstOrDefault(p => p != null);
			if (picture != null)
				return picture.GetImage();
			var filePath = track.FilePath;
			if (string.IsNullOrWhiteSpace(filePath) || filePath.EndsWith(@"\"))
				return null;
			if ((track.MediaTypes & TagLib.MediaTypes.Photo) != 0)
				return GetImageFromFile(filePath, track.ImageOrientation);
			if ((track.MediaTypes & TagLib.MediaTypes.Video) != 0)
				return GetVideoThumbnail(filePath, track.Duration.TotalSeconds / 10);
			return null;
        }

		/// <summary>
		/// Given the EXIF orientation of an image, compute the rotation and/or reflection (flip)
		/// required to transform the image to standard "top left" orientation.
		/// </summary>
		/// <param name="orientation">The EXIF orientation of the image.</param>
		/// <returns>The System.Drawing.RotateFlipType value needed to "correct" the image.</returns>
		private static RotateFlipType GetRotateFlipType(TagLib.Image.ImageOrientation orientation)
		{
			return RotateFlipTypes[(int)orientation];
		}

		private Image GetVideoThumbnail(string filePath, double frameTimeSeconds)
		{
			var videoConverter = new FFMpegConverter();
            using (var stream = new MemoryStream())
			{
				videoConverter.GetVideoThumbnail(filePath, stream, (float)frameTimeSeconds);
				return Image.FromStream(stream);
			}
		}

		private void InitPicture()
		{
			// If a Picture is selected in the PropertyGrid,
			// then use that particular Picture.
			var gridItem = PropertyGrid.SelectedGridItem;
			if (gridItem?.Value is Picture picture)
			{
				SetPicture(picture);
				return;
			}
			// If no Picture is selected in the PropertyGrid,
			// then use the first Picture in the selection, if any.
			InitPictureFromTrack(PropertyGrid.SelectedObject as Selection);
		}

		private void InitPictureFromTrack(ITrack track)
		{
			SetImage(GetImageFromTrack(track));
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

		private void SetImage(Image image)
		{
			PictureBox.Image = image;
			InitSizeMode();
		}

		private void SetPicture(Picture picture)
		{
			SetImage(picture?.GetImage());
		}

		#endregion
	}
}
