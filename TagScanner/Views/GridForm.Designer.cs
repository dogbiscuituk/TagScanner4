namespace TagScanner.Views
{
	partial class GridForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.GridElementHost = new System.Windows.Forms.Integration.ElementHost();
			this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.PopupColumns = new System.Windows.Forms.ToolStripMenuItem();
			this.PopupFilters = new System.Windows.Forms.ToolStripMenuItem();
			this.PopupGroups = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.PopupPlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.PopupPlaylistCreateNew = new System.Windows.Forms.ToolStripMenuItem();
			this.PopupPlaylistAddToCurrent = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
			this.PopupPlaylistOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.PopupPlaylistReopen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
			this.PopupPlaylistSave = new System.Windows.Forms.ToolStripMenuItem();
			this.PopupPlaylistSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.PictureBox = new System.Windows.Forms.PictureBox();
			this.TabControl = new System.Windows.Forms.TabControl();
			this.tabProperties = new System.Windows.Forms.TabPage();
			this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.tabPlayer = new System.Windows.Forms.TabPage();
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
			this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewByArtist = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewByGenre = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewByYear = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.ViewByAlbumTitle = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewBySongTitle = new System.Windows.Forms.ToolStripMenuItem();
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
			this.PopupMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
			this.TabControl.SuspendLayout();
			this.tabProperties.SuspendLayout();
			this.tabPlayer.SuspendLayout();
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
			this.GridElementHost.ContextMenuStrip = this.PopupMenu;
			this.GridElementHost.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GridElementHost.Location = new System.Drawing.Point(0, 0);
			this.GridElementHost.Margin = new System.Windows.Forms.Padding(0);
			this.GridElementHost.Name = "GridElementHost";
			this.GridElementHost.Size = new System.Drawing.Size(547, 516);
			this.GridElementHost.TabIndex = 2;
			this.GridElementHost.Text = "GridContainerHost";
			this.GridElementHost.Child = null;
			// 
			// PopupMenu
			// 
			this.PopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PopupColumns,
            this.PopupFilters,
            this.PopupGroups,
            this.toolStripMenuItem6,
            this.PopupPlaylist});
			this.PopupMenu.Name = "PopupMenu";
			this.PopupMenu.Size = new System.Drawing.Size(132, 98);
			// 
			// PopupColumns
			// 
			this.PopupColumns.Name = "PopupColumns";
			this.PopupColumns.Size = new System.Drawing.Size(131, 22);
			this.PopupColumns.Text = "&Columns...";
			// 
			// PopupFilters
			// 
			this.PopupFilters.Name = "PopupFilters";
			this.PopupFilters.Size = new System.Drawing.Size(131, 22);
			this.PopupFilters.Text = "&Filters...";
			// 
			// PopupGroups
			// 
			this.PopupGroups.Name = "PopupGroups";
			this.PopupGroups.Size = new System.Drawing.Size(131, 22);
			this.PopupGroups.Text = "&Groups...";
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(128, 6);
			// 
			// PopupPlaylist
			// 
			this.PopupPlaylist.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PopupPlaylistCreateNew,
            this.PopupPlaylistAddToCurrent,
            this.toolStripMenuItem8,
            this.PopupPlaylistOpen,
            this.PopupPlaylistReopen,
            this.toolStripMenuItem9,
            this.PopupPlaylistSave,
            this.PopupPlaylistSaveAs});
			this.PopupPlaylist.Name = "PopupPlaylist";
			this.PopupPlaylist.Size = new System.Drawing.Size(131, 22);
			this.PopupPlaylist.Text = "&Playlist";
			// 
			// PopupPlaylistCreateNew
			// 
			this.PopupPlaylistCreateNew.Name = "PopupPlaylistCreateNew";
			this.PopupPlaylistCreateNew.Size = new System.Drawing.Size(153, 22);
			this.PopupPlaylistCreateNew.Text = "Create &New";
			// 
			// PopupPlaylistAddToCurrent
			// 
			this.PopupPlaylistAddToCurrent.Name = "PopupPlaylistAddToCurrent";
			this.PopupPlaylistAddToCurrent.Size = new System.Drawing.Size(153, 22);
			this.PopupPlaylistAddToCurrent.Text = "Add to &Current";
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(150, 6);
			// 
			// PopupPlaylistOpen
			// 
			this.PopupPlaylistOpen.Name = "PopupPlaylistOpen";
			this.PopupPlaylistOpen.Size = new System.Drawing.Size(153, 22);
			this.PopupPlaylistOpen.Text = "&Open...";
			// 
			// PopupPlaylistReopen
			// 
			this.PopupPlaylistReopen.Name = "PopupPlaylistReopen";
			this.PopupPlaylistReopen.Size = new System.Drawing.Size(153, 22);
			this.PopupPlaylistReopen.Text = "&Reopen";
			// 
			// toolStripMenuItem9
			// 
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new System.Drawing.Size(150, 6);
			// 
			// PopupPlaylistSave
			// 
			this.PopupPlaylistSave.Name = "PopupPlaylistSave";
			this.PopupPlaylistSave.Size = new System.Drawing.Size(153, 22);
			this.PopupPlaylistSave.Text = "&Save";
			// 
			// PopupPlaylistSaveAs
			// 
			this.PopupPlaylistSaveAs.Name = "PopupPlaylistSaveAs";
			this.PopupPlaylistSaveAs.Size = new System.Drawing.Size(153, 22);
			this.PopupPlaylistSaveAs.Text = "Save &As...";
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
			this.TabControl.Controls.Add(this.tabProperties);
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
			// tabProperties
			// 
			this.tabProperties.BackColor = System.Drawing.SystemColors.Control;
			this.tabProperties.Controls.Add(this.PropertyGrid);
			this.tabProperties.Location = new System.Drawing.Point(4, 22);
			this.tabProperties.Margin = new System.Windows.Forms.Padding(0);
			this.tabProperties.Name = "tabProperties";
			this.tabProperties.Size = new System.Drawing.Size(225, 352);
			this.tabProperties.TabIndex = 0;
			this.tabProperties.Text = "Properties";
			// 
			// PropertyGrid
			// 
			this.PropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.PropertyGrid.Margin = new System.Windows.Forms.Padding(0);
			this.PropertyGrid.Name = "PropertyGrid";
			this.PropertyGrid.Size = new System.Drawing.Size(225, 352);
			this.PropertyGrid.TabIndex = 0;
			// 
			// tabPlayer
			// 
			this.tabPlayer.Controls.Add(this.MediaPlayer);
			this.tabPlayer.Location = new System.Drawing.Point(4, 22);
			this.tabPlayer.Name = "tabPlayer";
			this.tabPlayer.Padding = new System.Windows.Forms.Padding(3);
			this.tabPlayer.Size = new System.Drawing.Size(225, 333);
			this.tabPlayer.TabIndex = 4;
			this.tabPlayer.Text = "Player";
			this.tabPlayer.UseVisualStyleBackColor = true;
			// 
			// MediaPlayer
			// 
			this.MediaPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MediaPlayer.Enabled = true;
			this.MediaPlayer.Location = new System.Drawing.Point(3, 3);
			this.MediaPlayer.Name = "MediaPlayer";
			this.MediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer.OcxState")));
			this.MediaPlayer.Size = new System.Drawing.Size(219, 327);
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
            this.EditInvertSelection});
			this.EditMenu.Name = "EditMenu";
			this.EditMenu.Size = new System.Drawing.Size(39, 20);
			this.EditMenu.Text = "&Edit";
			// 
			// EditSelectAll
			// 
			this.EditSelectAll.Name = "EditSelectAll";
			this.EditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.EditSelectAll.Size = new System.Drawing.Size(164, 22);
			this.EditSelectAll.Text = "Select &All";
			// 
			// EditInvertSelection
			// 
			this.EditInvertSelection.Name = "EditInvertSelection";
			this.EditInvertSelection.Size = new System.Drawing.Size(164, 22);
			this.EditInvertSelection.Text = "&Invert Selection";
			// 
			// ViewMenu
			// 
			this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewByArtist,
            this.ViewByGenre,
            this.ViewByYear,
            this.toolStripMenuItem4,
            this.ViewByAlbumTitle,
            this.ViewBySongTitle});
			this.ViewMenu.Name = "ViewMenu";
			this.ViewMenu.Size = new System.Drawing.Size(44, 20);
			this.ViewMenu.Text = "&View";
			// 
			// ViewByArtist
			// 
			this.ViewByArtist.Name = "ViewByArtist";
			this.ViewByArtist.Size = new System.Drawing.Size(152, 22);
			this.ViewByArtist.Text = "by &Artist";
			// 
			// ViewByGenre
			// 
			this.ViewByGenre.Name = "ViewByGenre";
			this.ViewByGenre.Size = new System.Drawing.Size(152, 22);
			this.ViewByGenre.Text = "by &Genre";
			// 
			// ViewByYear
			// 
			this.ViewByYear.Name = "ViewByYear";
			this.ViewByYear.Size = new System.Drawing.Size(152, 22);
			this.ViewByYear.Text = "by &Year";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(149, 6);
			// 
			// ViewByAlbumTitle
			// 
			this.ViewByAlbumTitle.Name = "ViewByAlbumTitle";
			this.ViewByAlbumTitle.Size = new System.Drawing.Size(152, 22);
			this.ViewByAlbumTitle.Text = "by Al&bum Title";
			// 
			// ViewBySongTitle
			// 
			this.ViewBySongTitle.Name = "ViewBySongTitle";
			this.ViewBySongTitle.Size = new System.Drawing.Size(152, 22);
			this.ViewBySongTitle.Text = "by &Song Title";
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
			// GridForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.StatusBar);
			this.Controls.Add(this.MainMenu);
			this.Name = "GridForm";
			this.Text = "ID3 Tag Explorer";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.PopupMenu.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
			this.TabControl.ResumeLayout(false);
			this.tabProperties.ResumeLayout(false);
			this.tabPlayer.ResumeLayout(false);
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
		public System.Windows.Forms.ContextMenuStrip PopupMenu;
		public System.Windows.Forms.ToolStripMenuItem PopupColumns;
		public System.Windows.Forms.ToolStripMenuItem PopupGroups;
		public System.Windows.Forms.ToolStripMenuItem PopupFilters;
		public System.Windows.Forms.TabControl TabControl;
		public System.Windows.Forms.TabPage tabProperties;
		public System.Windows.Forms.PropertyGrid PropertyGrid;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		public System.Windows.Forms.ToolStripMenuItem PopupPlaylist;
		public AxWMPLib.AxWindowsMediaPlayer MediaPlayer;
		public System.Windows.Forms.ToolStripMenuItem PopupPlaylistCreateNew;
		public System.Windows.Forms.ToolStripMenuItem PopupPlaylistAddToCurrent;
		public System.Windows.Forms.TabPage tabPlayer;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
		public System.Windows.Forms.ToolStripMenuItem PopupPlaylistOpen;
		public System.Windows.Forms.ToolStripMenuItem PopupPlaylistReopen;
		public System.Windows.Forms.ToolStripMenuItem PopupPlaylistSave;
		public System.Windows.Forms.ToolStripMenuItem PopupPlaylistSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
		public System.Windows.Forms.ToolStripMenuItem ViewByArtist;
		public System.Windows.Forms.ToolStripMenuItem ViewByGenre;
		public System.Windows.Forms.ToolStripMenuItem ViewByYear;
		public System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		public System.Windows.Forms.ToolStripMenuItem ViewByAlbumTitle;
		public System.Windows.Forms.ToolStripMenuItem ViewBySongTitle;
	}
}

