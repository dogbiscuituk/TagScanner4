using System;
using System.Windows.Forms;
using TagScanner.Models;

namespace TagScanner.Controllers
{
	public class StatusController
	{
		public StatusController(Model model, StatusStrip statusStrip)
		{
			Model = model;
			StatusBar = statusStrip.Items;
		}

		private readonly Model Model;
		private readonly ToolStripItemCollection StatusBar;

		public IProgress<ProgressEventArgs> CreateNewProgress()
		{
			var progressBar = new ToolStripProgressBar { Style = ProgressBarStyle.Continuous };
			var cancelButton = new ToolStripSplitButton { DropDownButtonWidth = 0, Text = "Cancel" };
			cancelButton.ButtonClick += CancelButton_ButtonClick;
			StatusBar.AddRange(new ToolStripItem[] { progressBar, cancelButton });
			var progress = new Progress<ProgressEventArgs>((e) =>
			{
				if (!e.Continue)
					return;
				e.Continue = e.Index < e.Count && cancelButton.Enabled;
				if (e.Continue)
				{
					progressBar.Maximum = e.Count;
					progressBar.Value = e.Index;
					if (e.Track != null)
					{
						Model.Modified = true;
						e.Track.PropertyChanged += Model.Track_PropertyChanged;
					}
				}
				else
				{
					cancelButton.ButtonClick -= CancelButton_ButtonClick;
					StatusBar.Remove(cancelButton);
					StatusBar.Remove(progressBar);
				}
			});
			return progress;
		}

		private void CancelButton_ButtonClick(object sender, EventArgs e)
		{
			((ToolStripItem)sender).Enabled = false;
		}
	}
}
