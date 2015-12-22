﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TagScanner.Models;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class FindReplaceDialogController
	{
		#region Constructors

		public FindReplaceDialogController(LibraryGridController libraryGridController)
		{
			View = new FindReplaceDialog();
			LibraryGridController = libraryGridController;
			InitTags(SourceTagBox, "(any)", Metadata.TextTags);
			SourceTagBox.SelectedIndexChanged += SourceTagBox_SelectedIndexChanged;
			InitTags(TargetTagBox, "(same as source)", Metadata.WritableTextTags);
			TargetTagBox.SelectedIndexChanged += Control_Changed;
			SourcePatternBox.TextChanged += Control_Changed;
			SourceRegexButton.Click += SourceRegexButton_Click;
			CaptureClicks(PopupFindMenu, ExpressionBuilderFindItem_Click);
			TargetRegexButton.Click += TargetRegexButton_Click;
			CaptureClicks(PopupReplaceMenu, ExpressionBuilderReplaceItem_Click);
			UseRegexCheckbox.CheckedChanged += Control_Changed;
			ScopeAllRadioButton.CheckedChanged += Control_Changed;
			ScopeSelectionRadioButton.CheckedChanged += Control_Changed;
		}

		#endregion

		#region Public Methods

		public void ShowDialog(IWin32Window owner, bool replaceMode)
		{
			ReplaceMode = replaceMode;
			UpdateControls();
		retry:
			if (View.ShowDialog(owner) != DialogResult.OK)
				return;
			PerformFind();
			if (PreviewResults)
				switch (new ReplacePreviewController(owner).Execute(Results))
				{
					case DialogResult.Cancel:
						return;
					case DialogResult.Retry:
						goto retry;
				}
			MessageBox.Show(
				owner,
				string.Format("{0} replacement(s) made.", PerformReplace()),
				"Replace");
		}

		#endregion

		#region Event Handlers

		private void Control_Changed(object sender, EventArgs e)
		{
			UpdateControls();
		}

		private void ExpressionBuilderFindItem_Click(object sender, EventArgs e)
		{
			InjectPattern(sender, SourcePatternBox);
		}

		private void ExpressionBuilderReplaceItem_Click(object sender, EventArgs e)
		{
			InjectPattern(sender, TargetPatternBox);
		}

		private void PopupRegularExpressionHelp_Click(object sender, EventArgs e)
		{
			Process.Start("https://msdn.microsoft.com/en-us/library/az24scfc(v=vs.110).aspx");
		}

		private void SourceRegexButton_Click(object sender, EventArgs e)
		{
			ExpressionBuilderPopup(PopupFindMenu, SourceRegexButton);
		}

		private void SourceTagBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (FindInAnyTag)
				TargetTagBox.SelectedIndex = 0;
			UpdateControls();
		}

		private void TargetRegexButton_Click(object sender, EventArgs e)
		{
			ExpressionBuilderPopup(PopupReplaceMenu, TargetRegexButton);
		}

		#endregion

		#region Private Properties

		private IEnumerable<Track> AllTracks { get { return LibraryGridController.Model.Tracks; } }
		private int ErrorCount { get; set; }
		private ErrorProvider ErrorProvider { get { return View.ErrorProvider; } }
		private bool FindInAnyTag { get { return SourceTagBox.SelectedIndex == 0; } }
		private LibraryGridController LibraryGridController { get; }
		private bool MatchCase { get { return MatchCaseCheckbox.Checked; } }
		private CheckBox MatchCaseCheckbox { get { return View.cbMatchCase; } }
		private ContextMenuStrip PopupFindMenu { get { return View.popupFindMenu; } }
		private ToolStripMenuItem PopupFindRegularExpressionHelp { get { return View.popupFindRegularExpressionHelp; } }
		private ContextMenuStrip PopupReplaceMenu { get { return View.popupReplaceMenu; } }
		private ToolStripMenuItem PopupReplaceRegularExpressionHelp { get { return View.popupReplaceRegularExpressionHelp; } }
		private bool PreviewResults { get { return View.cbPreview.Checked; } }
		private Regex Regex { get; set; }
		private Button ReplaceAllButton { get { return View.btnReplaceAll; } }
		private bool ReplaceInSameTag { get { return TargetTagBox.SelectedIndex == 0; } }
		private bool ReplaceMode { get; set; }
		private IEnumerable<Track> Scope { get { return ScopeAll ? AllTracks : SelectedTracks; } }
		private bool ScopeAll { get { return ScopeAllRadioButton.Checked; } }
		private RadioButton ScopeAllRadioButton { get { return View.rbAllTracks; } }
		private RadioButton ScopeSelectionRadioButton { get { return View.rbCurrentSelection; } }
		private IEnumerable<Track> SelectedTracks { get { return LibraryGridController.Selection.Tracks; } }
		private string SourcePattern { get { return SourcePatternBox.Text; } }
		private ComboBox SourcePatternBox { get { return View.cbSourcePattern; } }
		private Button SourceRegexButton { get { return View.btnSourceRegex; } }
		private string SourceTag { get { return SourceTagBox.Text; } }
		private ComboBox SourceTagBox { get { return View.cbSourceTag; } }
		private string TargetPattern { get { return TargetPatternBox.Text; } }
		private ComboBox TargetPatternBox { get { return View.cbTargetPattern; } }
		private Button TargetRegexButton { get { return View.btnTargetRegex; } }
		private string TargetTag { get { return TargetTagBox.Text; } }
		private ComboBox TargetTagBox { get { return View.cbTargetTag; } }
		private bool UseRegex { get { return UseRegexCheckbox.Checked; } }
		private CheckBox UseRegexCheckbox { get { return View.cbUseRegex; } }
		private FindReplaceDialog View { get; set; }

		#endregion

		#region Private Fields

		private List<FindReplaceResult> Results = new List<FindReplaceResult>();

		#endregion

		#region Private Methods

		private void CaptureClicks(ContextMenuStrip popupMenu, EventHandler itemClick)
		{
			foreach (var item in popupMenu.Items.OfType<ToolStripMenuItem>())
				item.Click +=
					item == PopupFindRegularExpressionHelp ||
					item == PopupReplaceRegularExpressionHelp
						? PopupRegularExpressionHelp_Click
						: itemClick;
		}

		private void ExpressionBuilderPopup(ToolStripDropDown popupMenu, Control button)
		{
			popupMenu.Show(button.PointToScreen(new Point(button.Width, 0)));
		}

		private void InitRegex(bool compiled)
		{
			var options = RegexOptions.Multiline;
			if (MatchCase)
				options |= RegexOptions.IgnoreCase;
			if (compiled)
				options |= RegexOptions.Compiled;
			Regex = new Regex(SourcePattern, options);
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

		private int PerformFind()
		{
			Results.Clear();
			if (UseRegex)
				InitRegex(true);
			var result = 0;
			foreach (var track in Scope)
				result += PerformFind(track);
			return result;
		}

		private int PerformFind(Track track)
		{
			var result = 0;
			if (FindInAnyTag)
				foreach (var sourceTag in ReplaceInSameTag ? Metadata.WritableTextTags : Metadata.StringTags)
					result += PerformFind(track, sourceTag);
			else
				result += PerformFind(track, SourceTag);
			return result;
		}

		private int PerformFind(Track track, string sourceTag)
		{
			var targetTag = ReplaceInSameTag ? sourceTag : TargetTag;
			var source = track.GetPropertyValue(sourceTag) ?? string.Empty;
			var target = targetTag == sourceTag ? source : track.GetPropertyValue(targetTag) ?? string.Empty;
			var sources = source is string ? new[] { (string)source } : source as string[];
			var targets = new string[sources.Length];
			var changed = false;
			for (var index = 0; index < sources.Length; index++)
			{
				targets[index] = Replace(sources[index]);
				if (targets[index] != sources[index])
					changed = true;
			}
			if (changed)
			{
				target =
					target is string
					? (object)targets.Aggregate((s, t) => s + "; " + t)
					: targets;
				Results.Add(new FindReplaceResult(track, targetTag, source, target));
			}
			return changed ? 1 : 0;
		}

		private int PerformReplace()
		{
			var replacements = Results.Where(r => r.Replace);
            foreach (var replacement in replacements)
				replacement.Track.SetPropertyValue(replacement.Tag, replacement.NewValue);
			return replacements.Count();
		}

		private string Replace(string source)
		{
			return
				UseRegex
					? Regex.Replace(source, TargetPattern)
					: MatchCase
						? source.Replace(SourcePattern, TargetPattern)
						: ReplaceCaseInsensitive(source);
		}

		public string ReplaceCaseInsensitive(string value)
		{
			if (value == null)
				return null;
			if (string.IsNullOrEmpty(SourcePattern))
				return value;
			var result = new StringBuilder();
			var p = 0;
			while (true)
			{
				int q = value.IndexOf(SourcePattern, p, StringComparison.InvariantCultureIgnoreCase);
				if (q < 0)
					break;
				result.Append(value, p, q - p).Append(TargetPattern);
				p = q + SourcePattern.Length;
			}
			return result.Append(value, p, value.Length - p).ToString();
		}

		private void SetError(Control control, string message)
		{
			ErrorProvider.SetError(control, message);
			ErrorCount++;
		}

		private void UpdateControls()
		{
			View.Text = ReplaceMode ? "Replace" : "Find";
			SourceRegexButton.Enabled = TargetRegexButton.Enabled = UseRegex;
			ErrorProvider.Clear();
			ErrorCount = 0;
			if (!(ScopeAll || SelectedTracks.Any()))
				SetError(ScopeSelectionRadioButton, "The current selection is empty.");
			if (ScopeAll && !AllTracks.Any())
				SetError(ScopeAllRadioButton, "There are no tracks in this library.");
			if (ReplaceMode && !FindInAnyTag && ReplaceInSameTag && !Metadata.WritableStringTags.Contains(SourceTag))
				SetError(TargetTagBox, string.Format("Source tag '{0}' is not writable.", SourceTag));
			if (UseRegex)
				try { InitRegex(false); }
				catch (ArgumentException ex) { SetError(SourceRegexButton, ex.Message); }
			ReplaceAllButton.Enabled = ErrorCount == 0;
		}
	}

	#endregion
}
