using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;
using TagScanner.Models;

namespace TagScanner.Controllers
{
	public class PersistenceController : SdiController, IObserveTracks
	{
		public PersistenceController(Model model, Control view, ToolStripDropDownItem recentMenu)
			: base(model, Properties.Settings.Default.LibraryFilter, "LibraryMRU", recentMenu)
		{
			View = view;
		}

		private readonly Control View;

		public string WindowCaption
		{
			get
			{
				var text = Path.GetFileNameWithoutExtension(FilePath);
				if (string.IsNullOrWhiteSpace(text))
					text = "(untitled)";
				var modified = Model.Modified;
				if (modified)
					text = string.Concat("* ", text);
				text = string.Concat(text, " - ", Application.ProductName);
				return text;
			}
		}

		public void TrackPropertyChanged(Track sender, string propertyName)
		{
			Model.Modified = true;
		}

		protected override void ClearDocument()
		{
			Model.Tracks = new List<Track>();
		}

		protected override bool LoadFromStream(Stream stream, string format)
		{
			var result =
				IsXml(format)
					? UseStream(() => Model.Tracks = (List<Track>)GetXmlSerializer().Deserialize(stream))
					: UseStream(() => Model.Tracks = (List<Track>)GetBinaryFormatter().Deserialize(stream));
			foreach (var track in Model.Tracks)
				track.Observers.Add(this);
			return result;
		}

		protected override bool SaveToStream(Stream stream, string format)
		{
			if (IsXml(format))
				return UseStream(() => GetXmlSerializer().Serialize(stream, Model.Tracks));
			return UseStream(() => GetBinaryFormatter().Serialize(stream, Model.Tracks));
		}

		private static BinaryFormatter GetBinaryFormatter()
		{
			return new BinaryFormatter();
        }

		private static XmlSerializer GetXmlSerializer()
		{
			return new XmlSerializer(typeof(List<Track>), new[] { typeof(TimeSpan) });
		}

		private static bool IsXml(string format)
		{
			return format.EndsWith("x");
        }
	}
}
