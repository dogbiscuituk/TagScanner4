using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
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

		protected override void ClearDocument()
		{
			Model.Tracks = new List<Track>();
		}

		protected override bool LoadFromStream(Stream stream)
		{
			var result = UseStream(() => Model.Tracks = (List<Track>)new BinaryFormatter().Deserialize(stream));
			foreach (var track in Model.Tracks)
				track.Observers.Add(this);
			return result;
		}

		protected override bool SaveToStream(Stream stream)
		{
			return UseStream(() => new BinaryFormatter().Serialize(stream, Model.Tracks));
		}

		public void TrackPropertyChanged(Track sender, string propertyName)
		{
			Model.Modified = true;
		}
	}
}
