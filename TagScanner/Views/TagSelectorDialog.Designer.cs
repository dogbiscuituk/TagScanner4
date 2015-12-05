namespace TagScanner.Views
{
	partial class TagSelectorDialog
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
			this.ListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.PopupCheck = new System.Windows.Forms.ToolStripMenuItem();
			this.PopupUncheck = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.PopupView = new System.Windows.Forms.ToolStripMenuItem();
			this.PopupViewList = new System.Windows.Forms.ToolStripMenuItem();
			this.PopupViewGrouped = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.PopupViewAlphabetical = new System.Windows.Forms.ToolStripMenuItem();
			this.PopupMenu.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ListView
			// 
			this.ListView.AllowDrop = true;
			this.ListView.CheckBoxes = true;
			this.ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.ListView.ContextMenuStrip = this.PopupMenu;
			this.ListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListView.FullRowSelect = true;
			this.ListView.HideSelection = false;
			this.ListView.Location = new System.Drawing.Point(0, 0);
			this.ListView.Name = "ListView";
			this.ListView.Size = new System.Drawing.Size(624, 402);
			this.ListView.TabIndex = 2;
			this.ListView.UseCompatibleStateImageBehavior = false;
			this.ListView.View = System.Windows.Forms.View.List;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 200;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Type";
			this.columnHeader2.Width = 100;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Writable";
			this.columnHeader3.Width = 80;
			// 
			// PopupMenu
			// 
			this.PopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PopupCheck,
            this.PopupUncheck,
            this.toolStripMenuItem1,
            this.PopupView});
			this.PopupMenu.Name = "PopupMenu";
			this.PopupMenu.Size = new System.Drawing.Size(153, 98);
			// 
			// PopupCheck
			// 
			this.PopupCheck.Name = "PopupCheck";
			this.PopupCheck.Size = new System.Drawing.Size(152, 22);
			this.PopupCheck.Text = "&Check";
			// 
			// PopupUncheck
			// 
			this.PopupUncheck.Name = "PopupUncheck";
			this.PopupUncheck.Size = new System.Drawing.Size(152, 22);
			this.PopupUncheck.Text = "&Uncheck";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
			// 
			// PopupView
			// 
			this.PopupView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PopupViewList,
            this.PopupViewGrouped,
            this.PopupViewAlphabetical});
			this.PopupView.Name = "PopupView";
			this.PopupView.Size = new System.Drawing.Size(152, 22);
			this.PopupView.Text = "&View";
			// 
			// PopupViewList
			// 
			this.PopupViewList.Name = "PopupViewList";
			this.PopupViewList.Size = new System.Drawing.Size(152, 22);
			this.PopupViewList.Text = "&List";
			// 
			// PopupViewGrouped
			// 
			this.PopupViewGrouped.Name = "PopupViewGrouped";
			this.PopupViewGrouped.Size = new System.Drawing.Size(152, 22);
			this.PopupViewGrouped.Text = "&Grouped";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.btnOK);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 402);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(624, 39);
			this.panel1.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(537, 7);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 20;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(456, 7);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 19;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// PopupViewAlphabetical
			// 
			this.PopupViewAlphabetical.Name = "PopupViewAlphabetical";
			this.PopupViewAlphabetical.Size = new System.Drawing.Size(152, 22);
			this.PopupViewAlphabetical.Text = "&Alphabetical";
			// 
			// TagSelectorDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(624, 441);
			this.Controls.Add(this.ListView);
			this.Controls.Add(this.panel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TagSelectorDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Visible Tags";
			this.PopupMenu.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.ListView ListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		public System.Windows.Forms.ToolStripMenuItem PopupCheck;
		public System.Windows.Forms.ToolStripMenuItem PopupUncheck;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		public System.Windows.Forms.ToolStripMenuItem PopupViewList;
		public System.Windows.Forms.ToolStripMenuItem PopupViewGrouped;
		public System.Windows.Forms.ToolStripMenuItem PopupView;
		public System.Windows.Forms.ContextMenuStrip PopupMenu;
		public System.Windows.Forms.ToolStripMenuItem PopupViewAlphabetical;
	}
}