namespace TagScanner.Views
{
	partial class LibraryForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LibraryForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.GridElementHost = new System.Windows.Forms.Integration.ElementHost();
			this.GridPopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.GridPopupTags = new System.Windows.Forms.ToolStripMenuItem();
			this.GridPopupFilters = new System.Windows.Forms.ToolStripMenuItem();
			this.GridPopupGroups = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.GridPopupPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.PictureBox = new System.Windows.Forms.PictureBox();
			this.TabControl = new System.Windows.Forms.TabControl();
			this.tabTags = new System.Windows.Forms.TabPage();
			this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.PropertyGridPopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.PropertyGridPopupTagVisibility = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPlayer = new System.Windows.Forms.TabPage();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.PlaylistGrid = new System.Windows.Forms.DataGridView();
			this.MediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.FileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.FileReopen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.FileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.FileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.FileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.EditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.EditInvertSelection = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.EditFind = new System.Windows.Forms.ToolStripMenuItem();
			this.EditReplace = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewArtists = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewAlbums = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewTracks = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewGenres = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewYears = new System.Windows.Forms.ToolStripMenuItem();
			this.AddMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.AddMedia = new System.Windows.Forms.ToolStripMenuItem();
			this.AddFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.AddRecentFolders = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.AddFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.AddFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.StatusBar = new System.Windows.Forms.StatusStrip();
			this.ModifiedLabel = new System.Windows.Forms.ToolStripStatusLabel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.GridPopupMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
			this.TabControl.SuspendLayout();
			this.tabTags.SuspendLayout();
			this.PropertyGridPopupMenu.SuspendLayout();
			this.tabPlayer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PlaylistGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).BeginInit();
			this.MainMenu.SuspendLayout();
			this.StatusBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.GridElementHost);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(784, 516);
			this.splitContainer1.SplitterDistance = 547;
			this.splitContainer1.TabIndex = 7;
			// 
			// GridElementHost
			// 
			this.GridElementHost.ContextMenuStrip = this.GridPopupMenu;
			this.GridElementHost.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GridElementHost.Location = new System.Drawing.Point(0, 0);
			this.GridElementHost.Margin = new System.Windows.Forms.Padding(0);
			this.GridElementHost.Name = "GridElementHost";
			this.GridElementHost.Size = new System.Drawing.Size(547, 516);
			this.GridElementHost.TabIndex = 2;
			this.GridElementHost.Text = "GridContainerHost";
			this.GridElementHost.Child = null;
			// 
			// GridPopupMenu
			// 
			this.GridPopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GridPopupTags,
            this.GridPopupFilters,
            this.GridPopupGroups,
            this.toolStripMenuItem6,
            this.GridPopupPlay});
			this.GridPopupMenu.Name = "PopupMenu";
			this.GridPopupMenu.Size = new System.Drawing.Size(122, 98);
			// 
			// GridPopupTags
			// 
			this.GridPopupTags.Name = "GridPopupTags";
			this.GridPopupTags.Size = new System.Drawing.Size(121, 22);
			this.GridPopupTags.Text = "&Tags...";
			// 
			// GridPopupFilters
			// 
			this.GridPopupFilters.Name = "GridPopupFilters";
			this.GridPopupFilters.Size = new System.Drawing.Size(121, 22);
			this.GridPopupFilters.Text = "&Filters...";
			// 
			// GridPopupGroups
			// 
			this.GridPopupGroups.Name = "GridPopupGroups";
			this.GridPopupGroups.Size = new System.Drawing.Size(121, 22);
			this.GridPopupGroups.Text = "&Groups...";
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(118, 6);
			// 
			// GridPopupPlay
			// 
			this.GridPopupPlay.Name = "GridPopupPlay";
			this.GridPopupPlay.Size = new System.Drawing.Size(121, 22);
			this.GridPopupPlay.Text = "&Play";
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.PictureBox);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.TabControl);
			this.splitContainer2.Size = new System.Drawing.Size(233, 516);
			this.splitContainer2.SplitterDistance = 134;
			this.splitContainer2.TabIndex = 1;
			// 
			// PictureBox
			// 
			this.PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PictureBox.Location = new System.Drawing.Point(0, 0);
			this.PictureBox.Name = "PictureBox";
			this.PictureBox.Size = new System.Drawing.Size(233, 134);
			this.PictureBox.TabIndex = 0;
			this.PictureBox.TabStop = false;
			// 
			// TabControl
			// 
			this.TabControl.Controls.Add(this.tabTags);
			this.TabControl.Controls.Add(this.tabPlayer);
			this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabControl.Location = new System.Drawing.Point(0, 0);
			this.TabControl.Margin = new System.Windows.Forms.Padding(0);
			this.TabControl.Multiline = true;
			this.TabControl.Name = "TabControl";
			this.TabControl.Padding = new System.Drawing.Point(0, 0);
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(233, 378);
			this.TabControl.TabIndex = 1;
			// 
			// tabTags
			// 
			this.tabTags.BackColor = System.Drawing.SystemColors.Control;
			this.tabTags.Controls.Add(this.PropertyGrid);
			this.tabTags.Location = new System.Drawing.Point(4, 22);
			this.tabTags.Margin = new System.Windows.Forms.Padding(0);
			this.tabTags.Name = "tabTags";
			this.tabTags.Size = new System.Drawing.Size(225, 352);
			this.tabTags.TabIndex = 0;
			this.tabTags.Text = "Tags";
			// 
			// PropertyGrid
			// 
			this.PropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.PropertyGrid.ContextMenuStrip = this.PropertyGridPopupMenu;
			this.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.PropertyGrid.Margin = new System.Windows.Forms.Padding(0);
			this.PropertyGrid.Name = "PropertyGrid";
			this.PropertyGrid.Size = new System.Drawing.Size(225, 352);
			this.PropertyGrid.TabIndex = 0;
			// 
			// PropertyGridPopupMenu
			// 
			this.PropertyGridPopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PropertyGridPopupTagVisibility});
			this.PropertyGridPopupMenu.Name = "PropertyGridPopupMenu";
			this.PropertyGridPopupMenu.Size = new System.Drawing.Size(150, 26);
			// 
			// PropertyGridPopupTagVisibility
			// 
			this.PropertyGridPopupTagVisibility.Name = "PropertyGridPopupTagVisibility";
			this.PropertyGridPopupTagVisibility.Size = new System.Drawing.Size(149, 22);
			this.PropertyGridPopupTagVisibility.Text = "&Tag Visibility...";
			// 
			// tabPlayer
			// 
			this.tabPlayer.Controls.Add(this.splitContainer3);
			this.tabPlayer.Location = new System.Drawing.Point(4, 22);
			this.tabPlayer.Name = "tabPlayer";
			this.tabPlayer.Padding = new System.Windows.Forms.Padding(3);
			this.tabPlayer.Size = new System.Drawing.Size(225, 352);
			this.tabPlayer.TabIndex = 4;
			this.tabPlayer.Text = "Player";
			this.tabPlayer.UseVisualStyleBackColor = true;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(3, 3);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.PlaylistGrid);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.MediaPlayer);
			this.splitContainer3.Size = new System.Drawing.Size(219, 346);
			this.splitContainer3.SplitterDistance = 96;
			this.splitContainer3.TabIndex = 1;
			// 
			// PlaylistGrid
			// 
			this.PlaylistGrid.AllowUserToAddRows = false;
			this.PlaylistGrid.AllowUserToDeleteRows = false;
			this.PlaylistGrid.AllowUserToOrderColumns = true;
			this.PlaylistGrid.AllowUserToResizeRows = false;
			this.PlaylistGrid.BackgroundColor = System.Drawing.SystemColors.Window;
			this.PlaylistGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.PlaylistGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
			this.PlaylistGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.PlaylistGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PlaylistGrid.GridColor = System.Drawing.SystemColors.Window;
			this.PlaylistGrid.Location = new System.Drawing.Point(0, 0);
			this.PlaylistGrid.MultiSelect = false;
			this.PlaylistGrid.Name = "PlaylistGrid";
			this.PlaylistGrid.ReadOnly = true;
			this.PlaylistGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.PlaylistGrid.Size = new System.Drawing.Size(219, 96);
			this.PlaylistGrid.TabIndex = 0;
			// 
			// MediaPlayer
			// 
			this.MediaPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MediaPlayer.Enabled = true;
			this.MediaPlayer.Location = new System.Drawing.Point(0, 0);
			this.MediaPlayer.Name = "MediaPlayer";
			this.MediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer.OcxState")));
			this.MediaPlayer.Size = new System.Drawing.Size(219, 246);
			this.MediaPlayer.TabIndex = 0;
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.EditMenu,
            this.ViewMenu,
            this.AddMenu,
            this.HelpMenu});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(784, 24);
			this.MainMenu.TabIndex = 8;
			this.MainMenu.Text = "menuStrip1";
			// 
			// FileMenu
			// 
			this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNew,
            this.FileOpen,
            this.FileReopen,
            this.toolStripMenuItem1,
            this.FileSave,
            this.FileSaveAs,
            this.toolStripMenuItem5,
            this.FileExit});
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(37, 20);
			this.FileMenu.Text = "&File";
			// 
			// FileNew
			// 
			this.FileNew.Name = "FileNew";
			this.FileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.FileNew.Size = new System.Drawing.Size(155, 22);
			this.FileNew.Text = "&New";
			// 
			// FileOpen
			// 
			this.FileOpen.Name = "FileOpen";
			this.FileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.FileOpen.Size = new System.Drawing.Size(155, 22);
			this.FileOpen.Text = "&Open...";
			// 
			// FileReopen
			// 
			this.FileReopen.Name = "FileReopen";
			this.FileReopen.Size = new System.Drawing.Size(155, 22);
			this.FileReopen.Text = "&Reopen";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 6);
			// 
			// FileSave
			// 
			this.FileSave.Name = "FileSave";
			this.FileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.FileSave.Size = new System.Drawing.Size(155, 22);
			this.FileSave.Text = "&Save";
			// 
			// FileSaveAs
			// 
			this.FileSaveAs.Name = "FileSaveAs";
			this.FileSaveAs.Size = new System.Drawing.Size(155, 22);
			this.FileSaveAs.Text = "Save &As...";
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(152, 6);
			// 
			// FileExit
			// 
			this.FileExit.Name = "FileExit";
			this.FileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.FileExit.Size = new System.Drawing.Size(155, 22);
			this.FileExit.Text = "E&xit";
			// 
			// EditMenu
			// 
			this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditSelectAll,
            this.EditInvertSelection,
            this.toolStripMenuItem2,
            this.EditFind,
            this.EditReplace});
			this.EditMenu.Name = "EditMenu";
			this.EditMenu.Size = new System.Drawing.Size(39, 20);
			this.EditMenu.Text = "&Edit";
			// 
			// EditSelectAll
			// 
			this.EditSelectAll.Name = "EditSelectAll";
			this.EditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.EditSelectAll.Size = new System.Drawing.Size(167, 22);
			this.EditSelectAll.Text = "Select &All";
			// 
			// EditInvertSelection
			// 
			this.EditInvertSelection.Name = "EditInvertSelection";
			this.EditInvertSelection.Size = new System.Drawing.Size(167, 22);
			this.EditInvertSelection.Text = "&Invert Selection";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(164, 6);
			// 
			// EditFind
			// 
			this.EditFind.Name = "EditFind";
			this.EditFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.EditFind.Size = new System.Drawing.Size(167, 22);
			this.EditFind.Text = "&Find...";
			// 
			// EditReplace
			// 
			this.EditReplace.Name = "EditReplace";
			this.EditReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
			this.EditReplace.Size = new System.Drawing.Size(167, 22);
			this.EditReplace.Text = "&Replace...";
			// 
			// ViewMenu
			// 
			this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewArtists,
            this.ViewAlbums,
            this.ViewTracks,
            this.ViewGenres,
            this.ViewYears});
			this.ViewMenu.Name = "ViewMenu";
			this.ViewMenu.Size = new System.Drawing.Size(44, 20);
			this.ViewMenu.Text = "&View";
			// 
			// ViewArtists
			// 
			this.ViewArtists.Name = "ViewArtists";
			this.ViewArtists.Size = new System.Drawing.Size(115, 22);
			this.ViewArtists.Text = "&Artists";
			// 
			// ViewAlbums
			// 
			this.ViewAlbums.Name = "ViewAlbums";
			this.ViewAlbums.Size = new System.Drawing.Size(115, 22);
			this.ViewAlbums.Text = "Albu&ms";
			// 
			// ViewTracks
			// 
			this.ViewTracks.Name = "ViewTracks";
			this.ViewTracks.Size = new System.Drawing.Size(115, 22);
			this.ViewTracks.Text = "&Tracks";
			// 
			// ViewGenres
			// 
			this.ViewGenres.Name = "ViewGenres";
			this.ViewGenres.Size = new System.Drawing.Size(115, 22);
			this.ViewGenres.Text = "&Genres";
			// 
			// ViewYears
			// 
			this.ViewYears.Name = "ViewYears";
			this.ViewYears.Size = new System.Drawing.Size(115, 22);
			this.ViewYears.Text = "&Years";
			// 
			// AddMenu
			// 
			this.AddMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddMedia,
            this.AddFolder,
            this.toolStripMenuItem3,
            this.AddRecentFolders});
			this.AddMenu.Name = "AddMenu";
			this.AddMenu.Size = new System.Drawing.Size(41, 20);
			this.AddMenu.Text = "&Add";
			// 
			// AddMedia
			// 
			this.AddMedia.Name = "AddMedia";
			this.AddMedia.Size = new System.Drawing.Size(151, 22);
			this.AddMedia.Text = "&Media...";
			// 
			// AddFolder
			// 
			this.AddFolder.Name = "AddFolder";
			this.AddFolder.Size = new System.Drawing.Size(151, 22);
			this.AddFolder.Text = "&Folder...";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(148, 6);
			// 
			// AddRecentFolders
			// 
			this.AddRecentFolders.Name = "AddRecentFolders";
			this.AddRecentFolders.Size = new System.Drawing.Size(151, 22);
			this.AddRecentFolders.Text = "&Recent Folders";
			// 
			// HelpMenu
			// 
			this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpAbout});
			this.HelpMenu.Name = "HelpMenu";
			this.HelpMenu.Size = new System.Drawing.Size(44, 20);
			this.HelpMenu.Text = "&Help";
			// 
			// HelpAbout
			// 
			this.HelpAbout.Name = "HelpAbout";
			this.HelpAbout.Size = new System.Drawing.Size(107, 22);
			this.HelpAbout.Text = "&About";
			// 
			// AddFolderDialog
			// 
			this.AddFolderDialog.Description = "Select the folder to add";
			// 
			// AddFileDialog
			// 
			this.AddFileDialog.Multiselect = true;
			this.AddFileDialog.Title = "Select the media file(s) to add";
			// 
			// StatusBar
			// 
			this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifiedLabel});
			this.StatusBar.Location = new System.Drawing.Point(0, 540);
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Size = new System.Drawing.Size(784, 22);
			this.StatusBar.TabIndex = 9;
			this.StatusBar.Text = "Status";
			// 
			// ModifiedLabel
			// 
			this.ModifiedLabel.Name = "ModifiedLabel";
			this.ModifiedLabel.Size = new System.Drawing.Size(55, 17);
			this.ModifiedLabel.Text = "Modified";
			this.ModifiedLabel.Visible = false;
			// 
			// LibraryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.StatusBar);
			this.Controls.Add(this.MainMenu);
			this.Name = "LibraryForm";
			this.Text = "ID3 Tag Explorer";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.GridPopupMenu.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
			this.TabControl.ResumeLayout(false);
			this.tabTags.ResumeLayout(false);
			this.PropertyGridPopupMenu.ResumeLayout(false);
			this.tabPlayer.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PlaylistGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).EndInit();
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.StatusBar.ResumeLayout(false);
			this.StatusBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.SplitContainer splitContainer1;
		public System.Windows.Forms.SplitContainer splitContainer2;
		public System.Windows.Forms.PictureBox PictureBox;
		public System.Windows.Forms.MenuStrip MainMenu;
		public System.Windows.Forms.ToolStripMenuItem FileMenu;
		public System.Windows.Forms.ToolStripMenuItem FileExit;
		public System.Windows.Forms.ToolStripMenuItem EditMenu;
		public System.Windows.Forms.ToolStripMenuItem EditSelectAll;
		public System.Windows.Forms.ToolStripMenuItem EditInvertSelection;
		public System.Windows.Forms.ToolStripMenuItem ViewMenu;
		public System.Windows.Forms.ToolStripMenuItem HelpMenu;
		public System.Windows.Forms.ToolStripMenuItem HelpAbout;
		public System.Windows.Forms.FolderBrowserDialog AddFolderDialog;
		public System.Windows.Forms.OpenFileDialog AddFileDialog;
		public System.Windows.Forms.StatusStrip StatusBar;
		public System.Windows.Forms.ToolStripMenuItem FileNew;
		public System.Windows.Forms.ToolStripMenuItem FileOpen;
		public System.Windows.Forms.ToolStripMenuItem FileSave;
		public System.Windows.Forms.ToolStripMenuItem FileSaveAs;
		public System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		public System.Windows.Forms.ToolStripMenuItem AddMenu;
		public System.Windows.Forms.ToolStripMenuItem AddMedia;
		public System.Windows.Forms.ToolStripMenuItem AddFolder;
		public System.Windows.Forms.ToolStripMenuItem AddRecentFolders;
		public System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		public System.Windows.Forms.ToolStripMenuItem FileReopen;
		public System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		public System.Windows.Forms.ToolStripStatusLabel ModifiedLabel;
		public System.Windows.Forms.Integration.ElementHost GridElementHost;
		public System.Windows.Forms.ContextMenuStrip GridPopupMenu;
		public System.Windows.Forms.ToolStripMenuItem GridPopupTags;
		public System.Windows.Forms.ToolStripMenuItem GridPopupGroups;
		public System.Windows.Forms.ToolStripMenuItem GridPopupFilters;
		public System.Windows.Forms.TabControl TabControl;
		public System.Windows.Forms.TabPage tabTags;
		public System.Windows.Forms.PropertyGrid PropertyGrid;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		public System.Windows.Forms.ToolStripMenuItem GridPopupPlay;
		public AxWMPLib.AxWindowsMediaPlayer MediaPlayer;
		public System.Windows.Forms.TabPage tabPlayer;
		public System.Windows.Forms.ToolStripMenuItem ViewArtists;
		public System.Windows.Forms.ToolStripMenuItem ViewGenres;
		public System.Windows.Forms.ToolStripMenuItem ViewYears;
		public System.Windows.Forms.ToolStripMenuItem ViewAlbums;
		public System.Windows.Forms.ToolStripMenuItem ViewTracks;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		public System.Windows.Forms.ToolStripMenuItem EditFind;
		public System.Windows.Forms.ToolStripMenuItem EditReplace;
		private System.Windows.Forms.ContextMenuStrip PropertyGridPopupMenu;
		public System.Windows.Forms.ToolStripMenuItem PropertyGridPopupTagVisibility;
		private System.Windows.Forms.SplitContainer splitContainer3;
		public System.Windows.Forms.DataGridView PlaylistGrid;
	}
}

