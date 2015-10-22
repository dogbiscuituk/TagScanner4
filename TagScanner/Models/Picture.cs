using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagScanner.Models
{
	[Serializable]
    public class Picture
    {
        #region Lifetime Management

		public Picture(string filePath, int index, TagLib.IPicture picture)
		{
			_description = picture.Description;
			_filePath = filePath;
			_index = index;
			_mimeType = picture.MimeType;
			_type = picture.Type;
		}

        #endregion

        #region Properties

	    private readonly string _filePath;
	    private readonly int _index;

	    private long _dataSize = long.MaxValue;
	    public long DataSize
	    {
		    get
		    {
			    if (_dataSize == long.MaxValue)
				    Init();
			    return _dataSize;
		    }
	    }

	    private readonly string _description;
		public string Description
		{
            get { return _description; }
        }

	    private readonly string _mimeType;
        public string MimeType
        {
            get { return _mimeType; }
        }

        private PixelFormat _pixelFormat = 0;
        public PixelFormat PixelFormat
        {
            get
            {
                if (_pixelFormat == 0)
                    Init();
                return _pixelFormat;
            }
        }

	    private readonly TagLib.PictureType _type;
        public TagLib.PictureType Type
        {
            get { return _type; }
        }

        private Size _size;
        public Size Size
        {
            get
            {
	            if (_size.IsEmpty)
		            Init();
                return _size;
            }
        }

        #endregion

        #region Methods

		public Image GetImage()
		{
			try
			{
				using (var file = TagLib.File.Create(_filePath))
					return GetImage(file.Tag.Pictures[_index]);
			}
			catch (FileNotFoundException)
			{
				return null;
			}
		}

	    private Image GetImage(TagLib.IPicture picture)
	    {
		    using (var stream = GetStream(picture.Data.Data))
			    try
			    {
				    return Image.FromStream(stream);
			    }
			    catch (ArgumentException)
			    {
				    return null;
			    }
	    }

	    private Stream GetStream(byte[] data)
	    {
			var stream = new MemoryStream(data);
		    _dataSize = stream.Length;
		    return stream;
	    }

	    private void Init()
	    {
		    using (var image = GetImage())
		    {
			    try
			    {
				    _pixelFormat = image.PixelFormat;
				    _size = image.Size;
			    }
			    catch (NullReferenceException)
			    {
				    _pixelFormat = PixelFormat.Undefined;
				    _size = Size.Empty;
			    }
		    }
	    }

	    public override string ToString()
        {
            return string.Format(
                "{0} {1} {2} ({3}x{4}, {5}, {6} bytes)",
                Type, MimeType, Description, Size.Width, Size.Height, PixelFormat, DataSize);
        }

        #endregion
    }
}
