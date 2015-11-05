using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;
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
			/*
				The asymmetrical use of XmlTextReader below is necessitated by a bug in .NET's XML Serialization routines.
				These can serialize an object to XML, which will subsequently throw an exception when trying to deserialize.
				For example, when a string contains an unprintable character like char(1), this will get serialized to &#x1;
				but fail on subsequent attempted deserialization. Note that it is actually the serialization step which is at
				fault, because the definition of an XML character (see "http://www.w3.org/TR/2000/REC-xml-20001006#charsets")
				specifically excludes all such control characters except tab, line feed, and carriage return:

					Char ::= #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]

				The use of XmlTextReader gets round this problem by defaulting its Normalization property to false, hence
				disabling character range checking for numeric entities. As a result, character entities such as &#1; are
				allowed during deserialization too. The default TextReader variant on the other hand creates an XmlTextReader
				with its Normalization property set to true, which was causing the observed failure at deserialization time.
			*/
			var result =
				IsXml(format)
					? UseStream(() => Model.Tracks = (List<Track>)GetXmlSerializer().Deserialize(new XmlTextReader(stream)))
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
			return new XmlSerializer(typeof(List<Track>));
		}

		private static bool IsXml(string format)
		{
			return format.EndsWith("x");
        }
	}
}
