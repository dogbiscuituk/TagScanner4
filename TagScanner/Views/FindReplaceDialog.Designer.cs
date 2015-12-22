namespace TagScanner.Views
{
	partial class FindReplaceDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.label4 = new System.Windows.Forms.Label();
			this.popupFindMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tMatchAnyCharacterOneTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tMatchAnyCharacterZeroOrMoreTimesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tMatchAnyCharacterOneOrMoreTimesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchAnySingleCharacterInTheSetabcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchAnySingleCharacterNTInTheSetabcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchAnyCharacterInTheRangeAToFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchAnyWordCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchAnyWhitespaceCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchAnyDecimalDigitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.matchAnyCharacterZeroOrOneTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchAnyCharacterZeroOrMoreTimesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchAnyCharacterOneOrMoreTimesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchThreeConsecutiveDecimalDigitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.matchAtBeginningOrEndOfWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchAtBeginningOfLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchALineBreakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.matchAtEndOfLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.captureAndImplicitlyNumberTheSubexpressiondogcatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backreferenceTheFirstCapturedSubexpressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.captureSubexpressiondogcatAndNameItpetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backreferenceTheCapturedSubexpressionNamedpetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.popupFindRegularExpressionHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.popupReplaceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.substituteTheSubstringMatchedByCapturedGroupNumber1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.substituteTheSubstringMatchedByTheNamedGrouppetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.substituteALiteralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.substituteACopyOfTheWholeMatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.substituteTheLastGroupThatWasCapturedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.popupReplaceRegularExpressionHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.cbMatchCase = new System.Windows.Forms.CheckBox();
			this.cbUseRegex = new System.Windows.Forms.CheckBox();
			this.btnReplaceAll = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.rbCurrentSelection = new System.Windows.Forms.RadioButton();
			this.rbAllTracks = new System.Windows.Forms.RadioButton();
			this.label6 = new System.Windows.Forms.Label();
			this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.cbPreview = new System.Windows.Forms.CheckBox();
			this.FindPanel = new System.Windows.Forms.Panel();
			this.ReplacePanel = new System.Windows.Forms.Panel();
			this.cbTargetTag = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnTargetRegex = new System.Windows.Forms.Button();
			this.cbTargetPattern = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSourceRegex = new System.Windows.Forms.Button();
			this.cbSourcePattern = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbSourceTag = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.popupFindMenu.SuspendLayout();
			this.popupReplaceMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
			this.FindPanel.SuspendLayout();
			this.ReplacePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(216, 129);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "&Look in:";
			// 
			// popupFindMenu
			// 
			this.popupFindMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tMatchAnyCharacterOneTimeToolStripMenuItem,
            this.tMatchAnyCharacterZeroOrMoreTimesToolStripMenuItem,
            this.tMatchAnyCharacterOneOrMoreTimesToolStripMenuItem,
            this.matchAnySingleCharacterInTheSetabcToolStripMenuItem,
            this.matchAnySingleCharacterNTInTheSetabcToolStripMenuItem,
            this.matchAnyCharacterInTheRangeAToFToolStripMenuItem,
            this.matchAnyWordCharacterToolStripMenuItem,
            this.matchAnyWhitespaceCharacterToolStripMenuItem,
            this.matchAnyDecimalDigitToolStripMenuItem,
            this.toolStripMenuItem1,
            this.matchAnyCharacterZeroOrOneTimeToolStripMenuItem,
            this.matchAnyCharacterZeroOrMoreTimesToolStripMenuItem,
            this.matchAnyCharacterOneOrMoreTimesToolStripMenuItem,
            this.matchThreeConsecutiveDecimalDigitsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.matchAtBeginningOrEndOfWordToolStripMenuItem,
            this.matchAtBeginningOfLineToolStripMenuItem,
            this.matchALineBreakToolStripMenuItem,
            this.matchAtEndOfLineToolStripMenuItem,
            this.toolStripMenuItem3,
            this.captureAndImplicitlyNumberTheSubexpressiondogcatToolStripMenuItem,
            this.backreferenceTheFirstCapturedSubexpressionToolStripMenuItem,
            this.captureSubexpressiondogcatAndNameItpetToolStripMenuItem,
            this.backreferenceTheCapturedSubexpressionNamedpetToolStripMenuItem,
            this.toolStripMenuItem4,
            this.popupFindRegularExpressionHelp});
			this.popupFindMenu.Name = "popupFindMenu";
			this.popupFindMenu.Size = new System.Drawing.Size(438, 512);
			// 
			// tMatchAnyCharacterOneTimeToolStripMenuItem
			// 
			this.tMatchAnyCharacterOneTimeToolStripMenuItem.Name = "tMatchAnyCharacterOneTimeToolStripMenuItem";
			this.tMatchAnyCharacterOneTimeToolStripMenuItem.ShortcutKeyDisplayString = ".";
			this.tMatchAnyCharacterOneTimeToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.tMatchAnyCharacterOneTimeToolStripMenuItem.Text = "Match any character one time";
			// 
			// tMatchAnyCharacterZeroOrMoreTimesToolStripMenuItem
			// 
			this.tMatchAnyCharacterZeroOrMoreTimesToolStripMenuItem.Name = "tMatchAnyCharacterZeroOrMoreTimesToolStripMenuItem";
			this.tMatchAnyCharacterZeroOrMoreTimesToolStripMenuItem.ShortcutKeyDisplayString = ".*";
			this.tMatchAnyCharacterZeroOrMoreTimesToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.tMatchAnyCharacterZeroOrMoreTimesToolStripMenuItem.Text = "Match any character zero or more times";
			// 
			// tMatchAnyCharacterOneOrMoreTimesToolStripMenuItem
			// 
			this.tMatchAnyCharacterOneOrMoreTimesToolStripMenuItem.Name = "tMatchAnyCharacterOneOrMoreTimesToolStripMenuItem";
			this.tMatchAnyCharacterOneOrMoreTimesToolStripMenuItem.ShortcutKeyDisplayString = ".+";
			this.tMatchAnyCharacterOneOrMoreTimesToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.tMatchAnyCharacterOneOrMoreTimesToolStripMenuItem.Text = "Match any character one or more times";
			// 
			// matchAnySingleCharacterInTheSetabcToolStripMenuItem
			// 
			this.matchAnySingleCharacterInTheSetabcToolStripMenuItem.Name = "matchAnySingleCharacterInTheSetabcToolStripMenuItem";
			this.matchAnySingleCharacterInTheSetabcToolStripMenuItem.ShortcutKeyDisplayString = "[abc]";
			this.matchAnySingleCharacterInTheSetabcToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAnySingleCharacterInTheSetabcToolStripMenuItem.Text = "Match any single character in the set \'abc\'";
			// 
			// matchAnySingleCharacterNTInTheSetabcToolStripMenuItem
			// 
			this.matchAnySingleCharacterNTInTheSetabcToolStripMenuItem.Name = "matchAnySingleCharacterNTInTheSetabcToolStripMenuItem";
			this.matchAnySingleCharacterNTInTheSetabcToolStripMenuItem.ShortcutKeyDisplayString = "[^abc]";
			this.matchAnySingleCharacterNTInTheSetabcToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAnySingleCharacterNTInTheSetabcToolStripMenuItem.Text = "Match any single character NOT in the set \'abc\'";
			// 
			// matchAnyCharacterInTheRangeAToFToolStripMenuItem
			// 
			this.matchAnyCharacterInTheRangeAToFToolStripMenuItem.Name = "matchAnyCharacterInTheRangeAToFToolStripMenuItem";
			this.matchAnyCharacterInTheRangeAToFToolStripMenuItem.ShortcutKeyDisplayString = "[a-f]";
			this.matchAnyCharacterInTheRangeAToFToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAnyCharacterInTheRangeAToFToolStripMenuItem.Text = "Match any character in the range a to f";
			// 
			// matchAnyWordCharacterToolStripMenuItem
			// 
			this.matchAnyWordCharacterToolStripMenuItem.Name = "matchAnyWordCharacterToolStripMenuItem";
			this.matchAnyWordCharacterToolStripMenuItem.ShortcutKeyDisplayString = "\\w";
			this.matchAnyWordCharacterToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAnyWordCharacterToolStripMenuItem.Text = "Match any word character";
			// 
			// matchAnyWhitespaceCharacterToolStripMenuItem
			// 
			this.matchAnyWhitespaceCharacterToolStripMenuItem.Name = "matchAnyWhitespaceCharacterToolStripMenuItem";
			this.matchAnyWhitespaceCharacterToolStripMenuItem.ShortcutKeyDisplayString = "[^\\S\\r\\n]";
			this.matchAnyWhitespaceCharacterToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAnyWhitespaceCharacterToolStripMenuItem.Text = "Match any whitespace character";
			// 
			// matchAnyDecimalDigitToolStripMenuItem
			// 
			this.matchAnyDecimalDigitToolStripMenuItem.Name = "matchAnyDecimalDigitToolStripMenuItem";
			this.matchAnyDecimalDigitToolStripMenuItem.ShortcutKeyDisplayString = "\\d";
			this.matchAnyDecimalDigitToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAnyDecimalDigitToolStripMenuItem.Text = "Match any decimal digit";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(434, 6);
			// 
			// matchAnyCharacterZeroOrOneTimeToolStripMenuItem
			// 
			this.matchAnyCharacterZeroOrOneTimeToolStripMenuItem.Name = "matchAnyCharacterZeroOrOneTimeToolStripMenuItem";
			this.matchAnyCharacterZeroOrOneTimeToolStripMenuItem.ShortcutKeyDisplayString = "e?";
			this.matchAnyCharacterZeroOrOneTimeToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAnyCharacterZeroOrOneTimeToolStripMenuItem.Text = "Match \'e\' zero or one time";
			// 
			// matchAnyCharacterZeroOrMoreTimesToolStripMenuItem
			// 
			this.matchAnyCharacterZeroOrMoreTimesToolStripMenuItem.Name = "matchAnyCharacterZeroOrMoreTimesToolStripMenuItem";
			this.matchAnyCharacterZeroOrMoreTimesToolStripMenuItem.ShortcutKeyDisplayString = "e*";
			this.matchAnyCharacterZeroOrMoreTimesToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAnyCharacterZeroOrMoreTimesToolStripMenuItem.Text = "Match \'e\' zero or more times";
			// 
			// matchAnyCharacterOneOrMoreTimesToolStripMenuItem
			// 
			this.matchAnyCharacterOneOrMoreTimesToolStripMenuItem.Name = "matchAnyCharacterOneOrMoreTimesToolStripMenuItem";
			this.matchAnyCharacterOneOrMoreTimesToolStripMenuItem.ShortcutKeyDisplayString = "e+";
			this.matchAnyCharacterOneOrMoreTimesToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAnyCharacterOneOrMoreTimesToolStripMenuItem.Text = "Match \'e\' one or more times";
			// 
			// matchThreeConsecutiveDecimalDigitsToolStripMenuItem
			// 
			this.matchThreeConsecutiveDecimalDigitsToolStripMenuItem.Name = "matchThreeConsecutiveDecimalDigitsToolStripMenuItem";
			this.matchThreeConsecutiveDecimalDigitsToolStripMenuItem.ShortcutKeyDisplayString = "\\d{2,4}";
			this.matchThreeConsecutiveDecimalDigitsToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchThreeConsecutiveDecimalDigitsToolStripMenuItem.Text = "Match two to four consecutive decimal digits";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(434, 6);
			// 
			// matchAtBeginningOrEndOfWordToolStripMenuItem
			// 
			this.matchAtBeginningOrEndOfWordToolStripMenuItem.Name = "matchAtBeginningOrEndOfWordToolStripMenuItem";
			this.matchAtBeginningOrEndOfWordToolStripMenuItem.ShortcutKeyDisplayString = "\\b";
			this.matchAtBeginningOrEndOfWordToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAtBeginningOrEndOfWordToolStripMenuItem.Text = "Match at beginning or end of word";
			// 
			// matchAtBeginningOfLineToolStripMenuItem
			// 
			this.matchAtBeginningOfLineToolStripMenuItem.Name = "matchAtBeginningOfLineToolStripMenuItem";
			this.matchAtBeginningOfLineToolStripMenuItem.ShortcutKeyDisplayString = "^";
			this.matchAtBeginningOfLineToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAtBeginningOfLineToolStripMenuItem.Text = "Match at beginning of line";
			// 
			// matchALineBreakToolStripMenuItem
			// 
			this.matchALineBreakToolStripMenuItem.Name = "matchALineBreakToolStripMenuItem";
			this.matchALineBreakToolStripMenuItem.ShortcutKeyDisplayString = "\\r?\\n";
			this.matchALineBreakToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchALineBreakToolStripMenuItem.Text = "Match a line break";
			// 
			// matchAtEndOfLineToolStripMenuItem
			// 
			this.matchAtEndOfLineToolStripMenuItem.Name = "matchAtEndOfLineToolStripMenuItem";
			this.matchAtEndOfLineToolStripMenuItem.ShortcutKeyDisplayString = "(?=\\r?$)";
			this.matchAtEndOfLineToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.matchAtEndOfLineToolStripMenuItem.Text = "Match at end of line";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(434, 6);
			// 
			// captureAndImplicitlyNumberTheSubexpressiondogcatToolStripMenuItem
			// 
			this.captureAndImplicitlyNumberTheSubexpressiondogcatToolStripMenuItem.Name = "captureAndImplicitlyNumberTheSubexpressiondogcatToolStripMenuItem";
			this.captureAndImplicitlyNumberTheSubexpressiondogcatToolStripMenuItem.ShortcutKeyDisplayString = "(dog|cat)";
			this.captureAndImplicitlyNumberTheSubexpressiondogcatToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.captureAndImplicitlyNumberTheSubexpressiondogcatToolStripMenuItem.Text = "Capture and implicitly number the subexpression \'dog|cat\'";
			// 
			// backreferenceTheFirstCapturedSubexpressionToolStripMenuItem
			// 
			this.backreferenceTheFirstCapturedSubexpressionToolStripMenuItem.Name = "backreferenceTheFirstCapturedSubexpressionToolStripMenuItem";
			this.backreferenceTheFirstCapturedSubexpressionToolStripMenuItem.ShortcutKeyDisplayString = "\\1";
			this.backreferenceTheFirstCapturedSubexpressionToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.backreferenceTheFirstCapturedSubexpressionToolStripMenuItem.Text = "Backreference the first captured subexpression";
			// 
			// captureSubexpressiondogcatAndNameItpetToolStripMenuItem
			// 
			this.captureSubexpressiondogcatAndNameItpetToolStripMenuItem.Name = "captureSubexpressiondogcatAndNameItpetToolStripMenuItem";
			this.captureSubexpressiondogcatAndNameItpetToolStripMenuItem.ShortcutKeyDisplayString = "(?<pet>dog|cat)";
			this.captureSubexpressiondogcatAndNameItpetToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.captureSubexpressiondogcatAndNameItpetToolStripMenuItem.Text = "Capture subexpression \'dog|cat\' and name it \'pet\'";
			// 
			// backreferenceTheCapturedSubexpressionNamedpetToolStripMenuItem
			// 
			this.backreferenceTheCapturedSubexpressionNamedpetToolStripMenuItem.Name = "backreferenceTheCapturedSubexpressionNamedpetToolStripMenuItem";
			this.backreferenceTheCapturedSubexpressionNamedpetToolStripMenuItem.ShortcutKeyDisplayString = "\\k<pet>";
			this.backreferenceTheCapturedSubexpressionNamedpetToolStripMenuItem.Size = new System.Drawing.Size(437, 22);
			this.backreferenceTheCapturedSubexpressionNamedpetToolStripMenuItem.Text = "Backreference the captured subexpression named \'pet\'";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(434, 6);
			// 
			// popupFindRegularExpressionHelp
			// 
			this.popupFindRegularExpressionHelp.Name = "popupFindRegularExpressionHelp";
			this.popupFindRegularExpressionHelp.Size = new System.Drawing.Size(437, 22);
			this.popupFindRegularExpressionHelp.Text = "Regular Expression Help";
			// 
			// popupReplaceMenu
			// 
			this.popupReplaceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.substituteTheSubstringMatchedByCapturedGroupNumber1ToolStripMenuItem,
            this.substituteTheSubstringMatchedByTheNamedGrouppetToolStripMenuItem,
            this.substituteALiteralToolStripMenuItem,
            this.substituteACopyOfTheWholeMatchToolStripMenuItem,
            this.substituteTheLastGroupThatWasCapturedToolStripMenuItem,
            this.toolStripMenuItem5,
            this.popupReplaceRegularExpressionHelp});
			this.popupReplaceMenu.Name = "popupReplaceMenu";
			this.popupReplaceMenu.Size = new System.Drawing.Size(425, 142);
			// 
			// substituteTheSubstringMatchedByCapturedGroupNumber1ToolStripMenuItem
			// 
			this.substituteTheSubstringMatchedByCapturedGroupNumber1ToolStripMenuItem.Name = "substituteTheSubstringMatchedByCapturedGroupNumber1ToolStripMenuItem";
			this.substituteTheSubstringMatchedByCapturedGroupNumber1ToolStripMenuItem.ShortcutKeyDisplayString = "$1";
			this.substituteTheSubstringMatchedByCapturedGroupNumber1ToolStripMenuItem.Size = new System.Drawing.Size(424, 22);
			this.substituteTheSubstringMatchedByCapturedGroupNumber1ToolStripMenuItem.Text = "Substitute the substring matched by captured group number 1";
			// 
			// substituteTheSubstringMatchedByTheNamedGrouppetToolStripMenuItem
			// 
			this.substituteTheSubstringMatchedByTheNamedGrouppetToolStripMenuItem.Name = "substituteTheSubstringMatchedByTheNamedGrouppetToolStripMenuItem";
			this.substituteTheSubstringMatchedByTheNamedGrouppetToolStripMenuItem.ShortcutKeyDisplayString = "${pet}";
			this.substituteTheSubstringMatchedByTheNamedGrouppetToolStripMenuItem.Size = new System.Drawing.Size(424, 22);
			this.substituteTheSubstringMatchedByTheNamedGrouppetToolStripMenuItem.Text = "Substitute the substring matched by the named group \'pet\'";
			// 
			// substituteALiteralToolStripMenuItem
			// 
			this.substituteALiteralToolStripMenuItem.Name = "substituteALiteralToolStripMenuItem";
			this.substituteALiteralToolStripMenuItem.ShortcutKeyDisplayString = "$$";
			this.substituteALiteralToolStripMenuItem.Size = new System.Drawing.Size(424, 22);
			this.substituteALiteralToolStripMenuItem.Text = "Substitute a literal \'$\'";
			// 
			// substituteACopyOfTheWholeMatchToolStripMenuItem
			// 
			this.substituteACopyOfTheWholeMatchToolStripMenuItem.Name = "substituteACopyOfTheWholeMatchToolStripMenuItem";
			this.substituteACopyOfTheWholeMatchToolStripMenuItem.ShortcutKeyDisplayString = "$&&";
			this.substituteACopyOfTheWholeMatchToolStripMenuItem.Size = new System.Drawing.Size(424, 22);
			this.substituteACopyOfTheWholeMatchToolStripMenuItem.Text = "Substitute a copy of the whole match";
			// 
			// substituteTheLastGroupThatWasCapturedToolStripMenuItem
			// 
			this.substituteTheLastGroupThatWasCapturedToolStripMenuItem.Name = "substituteTheLastGroupThatWasCapturedToolStripMenuItem";
			this.substituteTheLastGroupThatWasCapturedToolStripMenuItem.ShortcutKeyDisplayString = "$+";
			this.substituteTheLastGroupThatWasCapturedToolStripMenuItem.Size = new System.Drawing.Size(424, 22);
			this.substituteTheLastGroupThatWasCapturedToolStripMenuItem.Text = "Substitute the last group that was captured";
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(421, 6);
			// 
			// popupReplaceRegularExpressionHelp
			// 
			this.popupReplaceRegularExpressionHelp.Name = "popupReplaceRegularExpressionHelp";
			this.popupReplaceRegularExpressionHelp.Size = new System.Drawing.Size(424, 22);
			this.popupReplaceRegularExpressionHelp.Text = "Regular Expression Help";
			// 
			// cbMatchCase
			// 
			this.cbMatchCase.AutoSize = true;
			this.cbMatchCase.Location = new System.Drawing.Point(14, 150);
			this.cbMatchCase.Name = "cbMatchCase";
			this.cbMatchCase.Size = new System.Drawing.Size(82, 17);
			this.cbMatchCase.TabIndex = 11;
			this.cbMatchCase.Text = "Match &case";
			this.cbMatchCase.UseVisualStyleBackColor = true;
			// 
			// cbUseRegex
			// 
			this.cbUseRegex.AutoSize = true;
			this.cbUseRegex.Location = new System.Drawing.Point(14, 173);
			this.cbUseRegex.Name = "cbUseRegex";
			this.cbUseRegex.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cbUseRegex.Size = new System.Drawing.Size(138, 17);
			this.cbUseRegex.TabIndex = 12;
			this.cbUseRegex.Text = "Use regular e&xpressions";
			this.cbUseRegex.UseVisualStyleBackColor = true;
			// 
			// btnReplaceAll
			// 
			this.btnReplaceAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReplaceAll.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnReplaceAll.Location = new System.Drawing.Point(296, 237);
			this.btnReplaceAll.Name = "btnReplaceAll";
			this.btnReplaceAll.Size = new System.Drawing.Size(75, 23);
			this.btnReplaceAll.TabIndex = 17;
			this.btnReplaceAll.Text = "Replace All";
			this.btnReplaceAll.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(377, 237);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 18;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// rbCurrentSelection
			// 
			this.rbCurrentSelection.AutoSize = true;
			this.rbCurrentSelection.Location = new System.Drawing.Point(216, 149);
			this.rbCurrentSelection.Name = "rbCurrentSelection";
			this.rbCurrentSelection.Size = new System.Drawing.Size(104, 17);
			this.rbCurrentSelection.TabIndex = 15;
			this.rbCurrentSelection.Text = "C&urrent selection";
			this.rbCurrentSelection.UseVisualStyleBackColor = true;
			// 
			// rbAllTracks
			// 
			this.rbAllTracks.AutoSize = true;
			this.rbAllTracks.Checked = true;
			this.rbAllTracks.Location = new System.Drawing.Point(216, 172);
			this.rbAllTracks.Name = "rbAllTracks";
			this.rbAllTracks.Size = new System.Drawing.Size(68, 17);
			this.rbAllTracks.TabIndex = 16;
			this.rbAllTracks.TabStop = true;
			this.rbAllTracks.Text = "&All tracks";
			this.rbAllTracks.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(11, 130);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(43, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "&Options";
			// 
			// ErrorProvider
			// 
			this.ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.ErrorProvider.ContainerControl = this;
			// 
			// cbPreview
			// 
			this.cbPreview.AutoSize = true;
			this.cbPreview.Checked = true;
			this.cbPreview.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbPreview.Location = new System.Drawing.Point(14, 196);
			this.cbPreview.Name = "cbPreview";
			this.cbPreview.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cbPreview.Size = new System.Drawing.Size(97, 17);
			this.cbPreview.TabIndex = 13;
			this.cbPreview.Text = "Preview results";
			this.cbPreview.UseVisualStyleBackColor = true;
			// 
			// FindPanel
			// 
			this.FindPanel.Controls.Add(this.btnSourceRegex);
			this.FindPanel.Controls.Add(this.cbSourcePattern);
			this.FindPanel.Controls.Add(this.label2);
			this.FindPanel.Controls.Add(this.cbSourceTag);
			this.FindPanel.Controls.Add(this.label1);
			this.FindPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.FindPanel.Location = new System.Drawing.Point(0, 0);
			this.FindPanel.Name = "FindPanel";
			this.FindPanel.Size = new System.Drawing.Size(464, 59);
			this.FindPanel.TabIndex = 19;
			// 
			// ReplacePanel
			// 
			this.ReplacePanel.Controls.Add(this.cbTargetTag);
			this.ReplacePanel.Controls.Add(this.label5);
			this.ReplacePanel.Controls.Add(this.btnTargetRegex);
			this.ReplacePanel.Controls.Add(this.cbTargetPattern);
			this.ReplacePanel.Controls.Add(this.label3);
			this.ReplacePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.ReplacePanel.Location = new System.Drawing.Point(0, 59);
			this.ReplacePanel.Name = "ReplacePanel";
			this.ReplacePanel.Size = new System.Drawing.Size(464, 59);
			this.ReplacePanel.TabIndex = 20;
			// 
			// cbTargetTag
			// 
			this.cbTargetTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTargetTag.FormattingEnabled = true;
			this.cbTargetTag.Location = new System.Drawing.Point(252, 26);
			this.cbTargetTag.Name = "cbTargetTag";
			this.cbTargetTag.Size = new System.Drawing.Size(199, 21);
			this.cbTargetTag.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(249, 10);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(81, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "&Destination tag:";
			// 
			// btnTargetRegex
			// 
			this.btnTargetRegex.Location = new System.Drawing.Point(216, 25);
			this.btnTargetRegex.Margin = new System.Windows.Forms.Padding(0);
			this.btnTargetRegex.Name = "btnTargetRegex";
			this.btnTargetRegex.Size = new System.Drawing.Size(33, 23);
			this.btnTargetRegex.TabIndex = 12;
			this.btnTargetRegex.Text = "(a)+";
			this.ToolTip.SetToolTip(this.btnTargetRegex, "Regular Expression Builder");
			this.btnTargetRegex.UseVisualStyleBackColor = true;
			// 
			// cbTargetPattern
			// 
			this.cbTargetPattern.FormattingEnabled = true;
			this.cbTargetPattern.Location = new System.Drawing.Point(14, 26);
			this.cbTargetPattern.Name = "cbTargetPattern";
			this.cbTargetPattern.Size = new System.Drawing.Size(199, 21);
			this.cbTargetPattern.TabIndex = 11;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "&Replace with:";
			// 
			// btnSourceRegex
			// 
			this.btnSourceRegex.Location = new System.Drawing.Point(216, 25);
			this.btnSourceRegex.Margin = new System.Windows.Forms.Padding(0);
			this.btnSourceRegex.Name = "btnSourceRegex";
			this.btnSourceRegex.Size = new System.Drawing.Size(33, 23);
			this.btnSourceRegex.TabIndex = 7;
			this.btnSourceRegex.Text = "(a)+";
			this.ToolTip.SetToolTip(this.btnSourceRegex, "Regular Expression Builder");
			this.btnSourceRegex.UseVisualStyleBackColor = true;
			// 
			// cbSourcePattern
			// 
			this.cbSourcePattern.FormattingEnabled = true;
			this.cbSourcePattern.Location = new System.Drawing.Point(14, 26);
			this.cbSourcePattern.Name = "cbSourcePattern";
			this.cbSourcePattern.Size = new System.Drawing.Size(199, 21);
			this.cbSourcePattern.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "&Find what:";
			// 
			// cbSourceTag
			// 
			this.cbSourceTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSourceTag.FormattingEnabled = true;
			this.cbSourceTag.Location = new System.Drawing.Point(252, 26);
			this.cbSourceTag.Name = "cbSourceTag";
			this.cbSourceTag.Size = new System.Drawing.Size(199, 21);
			this.cbSourceTag.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(249, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "&Source tag:";
			// 
			// FindReplaceDialog
			// 
			this.AcceptButton = this.btnReplaceAll;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(464, 272);
			this.Controls.Add(this.ReplacePanel);
			this.Controls.Add(this.cbPreview);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.rbAllTracks);
			this.Controls.Add(this.rbCurrentSelection);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnReplaceAll);
			this.Controls.Add(this.cbUseRegex);
			this.Controls.Add(this.cbMatchCase);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.FindPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FindReplaceDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.popupFindMenu.ResumeLayout(false);
			this.popupReplaceMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
			this.FindPanel.ResumeLayout(false);
			this.FindPanel.PerformLayout();
			this.ReplacePanel.ResumeLayout(false);
			this.ReplacePanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ToolStripMenuItem tMatchAnyCharacterOneTimeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tMatchAnyCharacterZeroOrMoreTimesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tMatchAnyCharacterOneOrMoreTimesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchAnySingleCharacterInTheSetabcToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchAnySingleCharacterNTInTheSetabcToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchAnyCharacterInTheRangeAToFToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchAnyWordCharacterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchAnyDecimalDigitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchAnyWhitespaceCharacterToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem matchAnyCharacterZeroOrOneTimeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchAnyCharacterZeroOrMoreTimesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchAnyCharacterOneOrMoreTimesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchThreeConsecutiveDecimalDigitsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem matchAtBeginningOrEndOfWordToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchAtBeginningOfLineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchALineBreakToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem matchAtEndOfLineToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem captureAndImplicitlyNumberTheSubexpressiondogcatToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem backreferenceTheFirstCapturedSubexpressionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem captureSubexpressiondogcatAndNameItpetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem backreferenceTheCapturedSubexpressionNamedpetToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem substituteTheSubstringMatchedByCapturedGroupNumber1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem substituteTheSubstringMatchedByTheNamedGrouppetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem substituteALiteralToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem substituteACopyOfTheWholeMatchToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem substituteTheLastGroupThatWasCapturedToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolTip ToolTip;
		public System.Windows.Forms.ContextMenuStrip popupFindMenu;
		public System.Windows.Forms.ContextMenuStrip popupReplaceMenu;
		public System.Windows.Forms.ToolStripMenuItem popupFindRegularExpressionHelp;
		public System.Windows.Forms.ToolStripMenuItem popupReplaceRegularExpressionHelp;
		public System.Windows.Forms.CheckBox cbMatchCase;
		public System.Windows.Forms.CheckBox cbUseRegex;
		public System.Windows.Forms.RadioButton rbCurrentSelection;
		public System.Windows.Forms.RadioButton rbAllTracks;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.ErrorProvider ErrorProvider;
		public System.Windows.Forms.Button btnReplaceAll;
		public System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.CheckBox cbPreview;
		private System.Windows.Forms.Panel ReplacePanel;
		public System.Windows.Forms.ComboBox cbTargetTag;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.Button btnTargetRegex;
		public System.Windows.Forms.ComboBox cbTargetPattern;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel FindPanel;
		public System.Windows.Forms.Button btnSourceRegex;
		public System.Windows.Forms.ComboBox cbSourcePattern;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.ComboBox cbSourceTag;
		private System.Windows.Forms.Label label1;
	}
}