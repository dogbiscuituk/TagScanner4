using System;
using System.Linq;
using System.Windows.Forms;
using TagScanner.Models;

namespace TagScanner.Controllers
{
    public class PictureController
    {
        #region Lifetime Management

        public PictureController(PictureBox pictureBox, PropertyGrid propertyGrid)
        {
            PictureBox = pictureBox;
            PropertyGrid = propertyGrid;
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
                if (PictureBox == value)
                    return;
                if (PictureBox != null)
                    PictureBox.Resize -= PictureBox_Resize;
                _pictureBox = value;
                if (PictureBox != null)
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

        #endregion

        #region Events

        private void PictureBox_Resize(object sender, EventArgs e)
        {
            InitSizeMode();
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
            // then display that particular Picture.
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
			// then display the first Picture in the selection, if any.
            var tagFile = PropertyGrid.SelectedObject as Selection;
            var result = tagFile != null && tagFile.Pictures.Any();
            Picture = result ? tagFile.Pictures[0] : null;
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
