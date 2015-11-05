using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TagScanner.Models;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class ReplaceDialogController
	{
		#region Constructors

		public ReplaceDialogController(GridController gridController)
		{
			GridController = gridController;
            View = new ReplaceDialog();
			InitTags(SourceTagBox, "(any)", Metadata.TextTags);
			SourceTagBox.SelectedIndexChanged += SourceTagBox_SelectedIndexChanged;
			InitTags(TargetTagBox, "(same as source)", Metadata.WritableTextTags);
			TargetTagBox.SelectedIndexChanged += Control_Changed;

			View.btnExpressionBuilderFind.Click += BtnExpressionBuilderFind_Click;
			CaptureClicks(View.popupFindMenu, ExpressionBuilderFindItem_Click);
			View.btnExpressionBuilderReplace.Click += BtnExpressionBuilderReplace_Click;
			CaptureClicks(View.popupReplaceMenu, ExpressionBuilderReplaceItem_Click);
			View.cbUseRegex.CheckedChanged += Control_Changed;

			View.rbAllTracks.CheckedChanged += Control_Changed;
			View.rbCurrentSelection.CheckedChanged += Control_Changed;
		}

		#endregion

		#region Public Methods

		public void ShowDialog(IWin32Window owner)
		{
			UpdateControls();
			if (View.ShowDialog(owner) == DialogResult.OK)
			{
				var result = PerformReplace();
				MessageBox.Show(owner, string.Format("{0} replacements made.", result), "Replace");
			}
		}

		#endregion

		#region Event Handlers

		private void BtnExpressionBuilderFind_Click(object sender, EventArgs e)
		{
			ExpressionBuilderPopup(View.popupFindMenu, View.btnExpressionBuilderFind);
		}

		private void BtnExpressionBuilderReplace_Click(object sender, EventArgs e)
		{
			ExpressionBuilderPopup(View.popupReplaceMenu, View.btnExpressionBuilderReplace);
		}

		private void Control_Changed(object sender, EventArgs e)
		{
			UpdateControls();
		}

		private void ExpressionBuilderFindItem_Click(object sender, EventArgs e)
		{
			InjectPattern(sender, View.cbFindWhat);
		}

		private void ExpressionBuilderReplaceItem_Click(object sender, EventArgs e)
		{
			InjectPattern(sender, View.cbReplaceWith);
		}

		private void PopupRegularExpressionHelp_Click(object sender, EventArgs e)
		{
			Process.Start("https://msdn.microsoft.com/en-us/library/az24scfc(v=vs.110).aspx");
		}

		private void SourceTagBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (FindInAnyTag)
				TargetTagBox.SelectedIndex = 0;
			UpdateControls();
		}

		#endregion

		#region Private Properties

		private IEnumerable<Track> AllTracks { get { return GridController.Model.Tracks; } }
		private int ErrorCount { get; set; }
		private ErrorProvider ErrorProvider {  get { return View.ErrorProvider; } }
		private bool FindInAnyTag { get { return SourceTagBox.SelectedIndex == 0; } }
		private GridController GridController { get; }
		private bool ReplaceInSameTag { get { return TargetTagBox.SelectedIndex == 0; } }
		private IEnumerable<Track> Scope { get { return ScopeAll ? AllTracks : SelectedTracks; } }
		private bool ScopeAll { get { return View.rbAllTracks.Checked; } }
		private IEnumerable<Track> SelectedTracks { get { return GridController.Selection.Tracks; } }
		private string SourcePattern { get { return View.cbFindWhat.Text; } }
		private string SourceTag { get { return SourceTagBox.Text; } }
		private ComboBox SourceTagBox { get { return View.cbSourceTag; } }
		private string TargetPattern { get { return View.cbReplaceWith.Text; } }
		private string TargetTag { get { return TargetTagBox.Text; } }
		private ComboBox TargetTagBox { get { return View.cbTargetTag; } }
		private ReplaceDialog View { get; set; }

		private FindOptions Options
		{
			get
			{
				var options = FindOptions.None;
				if (View.cbMatchCase.Checked)
					options |= FindOptions.MatchCase;
				if (View.cbMatchWholeWord.Checked)
					options |= FindOptions.WholeWord;
				if (View.cbUseRegex.Checked)
					options |= FindOptions.UseRegex;
				return options;
			}
		}

		private RegexOptions RegexOptions
		{
			get
			{
				var result = RegexOptions.Multiline;
				if ((Options & FindOptions.MatchCase) == 0)
					result |= RegexOptions.IgnoreCase;
				return result;
			}
		}

		#endregion

		#region Private Methods

		private void CaptureClicks(ContextMenuStrip popupMenu, EventHandler itemClick)
		{
			foreach (var item in popupMenu.Items.OfType<ToolStripMenuItem>())
				item.Click +=
					item == View.popupFindRegularExpressionHelp ||
					item == View.popupReplaceRegularExpressionHelp
						? PopupRegularExpressionHelp_Click
						: itemClick;
		}

		private void ExpressionBuilderPopup(ToolStripDropDown popupMenu, Control button)
		{
			popupMenu.Show(button.PointToScreen(new Point(button.Width, 0)));
		}

		private void InitTags(ComboBox comboBox, string firstTag, string[] moreTags)
		{
			var tags = comboBox.Items;
			tags.Add(firstTag);
			tags.AddRange(moreTags);
			comboBox.SelectedIndex = 0;
		}

		private void InjectPattern(object sender, Control control)
		{
			control.Text += ((ToolStripMenuItem)sender).ShortcutKeyDisplayString.AmpersandUnescape();
		}

		private int PerformReplace()
		{
			var result = 0;
			foreach (var track in Scope)
				result += PerformReplace(track);
			return result;
		}

		private int PerformReplace(Track track)
		{
			var result = 0;
			if (FindInAnyTag)
				foreach (var sourceTag in ReplaceInSameTag ? Metadata.WritableTextTags : Metadata.StringTags)
					result += PerformReplace(track, sourceTag, ReplaceInSameTag ? sourceTag : TargetTag);
			else
				result += PerformReplace(track, SourceTag, TargetTag);
			return result;
		}

		private int PerformReplace(Track track, string sourceTag, string targetTag)
		{
			var source = track.GetPropertyValue(sourceTag);
			var target = targetTag == sourceTag ? source : track.GetPropertyValue(targetTag);
			var sources = source is string ? new[] { (string)source } : source as string[];
			var targets = new string[sources.Length];
			for (var index = 0; index < sources.Length; index++)
				targets[index] = Regex.Replace(sources[index], SourcePattern, TargetPattern, RegexOptions);
			object targetValue;
			if (target is string)
				targetValue = targets.Aggregate((s, t) => s + "; " + t);
			else
				targetValue = targets;
			return track.SetPropertyValue(targetTag, targetValue) ? 1 : 0;
		}

		private void SetError(Control control, string message)
		{
			ErrorProvider.SetIconPadding(control, 4);
			ErrorProvider.SetError(control, message);
			ErrorCount++;
		}

		private void UpdateControls()
		{
            View.btnExpressionBuilderFind.Enabled =
				View.btnExpressionBuilderReplace.Enabled =
				View.cbUseRegex.Checked;
			View.ErrorProvider.Clear();
			ErrorCount = 0;
			if (!(ScopeAll || SelectedTracks.Any()))
				SetError(View.rbCurrentSelection, "Current selection is empty.");
			if (ScopeAll && !AllTracks.Any())
				SetError(View.rbAllTracks, "There are no tracks in this library.");
			if (!FindInAnyTag && ReplaceInSameTag && !Metadata.WritableStringTags.Contains(SourceTag))
				SetError(TargetTagBox, string.Format("Source Tag '{0}' is not writable.", SourceTag));
			View.btnReplaceAll.Enabled = ErrorCount == 0;
        }

		#endregion

		[Flags]
		public enum FindOptions
		{
			None = 0x00,
			MatchCase = 0x01,
			WholeWord = 0x02,
			UseRegex = 0x04
		}
	}
}
