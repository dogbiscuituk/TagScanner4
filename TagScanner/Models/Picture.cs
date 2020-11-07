namespace TagScanner.Models
{
	using System;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;

	[Serializable]
    public class Picture
    {
		#region Constructors

		public Picture() { }

		public Picture(string filePath, int index, TagLib.IPicture picture)
		{
			Description = picture.Description;
			FilePath = filePath;
			Index = index;
			MimeType = picture.MimeType;
			Type = picture.Type.ToString();
		}

		#endregion

		#region Properties

		public string Description { get; set; }
		public string FilePath { get; set; }
		public int Index { get; set; }
		public string MimeType { get; set; }
		public string Type { get; set; }

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

		private PixelFormat _pixelFormat = PixelFormat.Undefined;
        public PixelFormat PixelFormat
        {
            get
            {
                if (_pixelFormat == PixelFormat.Undefined)
                    Init();
                return _pixelFormat;
            }
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
				using (var file = TagLib.File.Create(FilePath))
					return GetImage(file.Tag.Pictures[Index]);
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
            return $"{Type} {MimeType} {Description} ({Size.Width}x{Size.Height}, {PixelFormat}, {DataSize} bytes)";
        }

        #endregion
    }
}
