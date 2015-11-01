using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagScanner.Models;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class ReplaceDialogController
	{
		public ReplaceDialogController()
		{
			View = new ReplaceDialog();
			View.cbSourceTag.Items.Add("(any)");
            View.cbSourceTag.Items.AddRange(Metadata.WritableTextTags);
			View.cbSourceTag.SelectedIndex = 0;
			View.cbDestinationTag.Items.Add("(same as source)");
			View.cbDestinationTag.Items.AddRange(Metadata.WritableTextTags);
			View.cbDestinationTag.SelectedIndex = 0;
			View.btnExpressionBuilderFind.Click += BtnExpressionBuilderFind_Click;
			CaptureClicks(View.popupFindMenu, ExpressionBuilderFindItem_Click);
			View.btnExpressionBuilderReplace.Click += BtnExpressionBuilderReplace_Click;
			CaptureClicks(View.popupReplaceMenu, ExpressionBuilderReplaceItem_Click);
			View.cbLookIn.SelectedIndex = 0;
			View.cbUseRegex.CheckedChanged += CbUseRegex_CheckedChanged;
		}

		private void CbUseRegex_CheckedChanged(object sender, EventArgs e)
		{
			UpdateControls();
        }

		public void ShowDialog(IWin32Window owner)
		{
			UpdateControls();
			if (View.ShowDialog(owner) == DialogResult.OK)
			{
			}
		}

		private ReplaceDialog View { get; set; }

		private void BtnExpressionBuilderFind_Click(object sender, EventArgs e)
		{
			ExpressionBuilderPopup(View.popupFindMenu, View.btnExpressionBuilderFind);
		}

		private void BtnExpressionBuilderReplace_Click(object sender, EventArgs e)
		{
			ExpressionBuilderPopup(View.popupReplaceMenu, View.btnExpressionBuilderReplace);
		}

		private void CaptureClicks(ContextMenuStrip popupMenu, EventHandler itemClick)
		{
			foreach (var item in popupMenu.Items.OfType<ToolStripMenuItem>())
				item.Click +=
					item == View.popupFindRegularExpressionHelp ||
					item == View.popupReplaceRegularExpressionHelp
						? PopupRegularExpressionHelp_Click
						: itemClick;
		}

		private void ExpressionBuilderFindItem_Click(object sender, EventArgs e)
		{
			InjectPattern(sender, View.cbFindWhat);
		}

		private void ExpressionBuilderPopup(ToolStripDropDown popupMenu, Control button)
		{
			popupMenu.Show(button.PointToScreen(new Point(button.Width, 0)));
		}

		private void ExpressionBuilderReplaceItem_Click(object sender, EventArgs e)
		{
			InjectPattern(sender, View.cbReplaceWith);
		}

		private void InjectPattern(object sender, Control control)
		{
			control.Text += ((ToolStripMenuItem)sender).ShortcutKeyDisplayString.AmpersandUnescape();
		}

		private void PopupRegularExpressionHelp_Click(object sender, EventArgs e)
		{
			Process.Start("https://msdn.microsoft.com/en-us/library/az24scfc(v=vs.110).aspx");
		}

		private void UpdateControls()
		{
			View.btnExpressionBuilderFind.Enabled = View.btnExpressionBuilderReplace.Enabled = View.cbUseRegex.Checked;
		}
	}
}
