namespace TagScanner.Views
{
	partial class FilterDialog
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
			this.btnReopen = new System.Windows.Forms.Button();
			this.btnAddRootGroup = new System.Windows.Forms.Button();
			this.btnSaveAs = new System.Windows.Forms.Button();
			this.btnAddRootFilter = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnOpen = new System.Windows.Forms.Button();
			this.btnClearAll = new System.Windows.Forms.Button();
			this.ValueBox = new System.Windows.Forms.Panel();
			this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.popupDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.popupInsertSiblingFilter = new System.Windows.Forms.ToolStripMenuItem();
			this.popupInsertSiblingGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.popupAddChildFilter = new System.Windows.Forms.ToolStripMenuItem();
			this.popupAddChildGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.OperatorBox = new System.Windows.Forms.ComboBox();
			this.PropertyBox = new System.Windows.Forms.ComboBox();
			this.TreeView = new System.Windows.Forms.TreeView();
			this.QuantifierBox = new System.Windows.Forms.ComboBox();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.PopupMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnReopen
			// 
			this.btnReopen.Location = new System.Drawing.Point(460, 253);
			this.btnReopen.Name = "btnReopen";
			this.btnReopen.Size = new System.Drawing.Size(30, 23);
			this.btnReopen.TabIndex = 50;
			this.ToolTip.SetToolTip(this.btnReopen, "Reopen");
			this.btnReopen.UseVisualStyleBackColor = true;
			// 
			// btnAddRootGroup
			// 
			this.btnAddRootGroup.Location = new System.Drawing.Point(460, 79);
			this.btnAddRootGroup.Name = "btnAddRootGroup";
			this.btnAddRootGroup.Size = new System.Drawing.Size(30, 23);
			this.btnAddRootGroup.TabIndex = 45;
			this.ToolTip.SetToolTip(this.btnAddRootGroup, "Add Root Group");
			this.btnAddRootGroup.UseVisualStyleBackColor = true;
			// 
			// btnSaveAs
			// 
			this.btnSaveAs.Location = new System.Drawing.Point(460, 311);
			this.btnSaveAs.Name = "btnSaveAs";
			this.btnSaveAs.Size = new System.Drawing.Size(30, 23);
			this.btnSaveAs.TabIndex = 49;
			this.ToolTip.SetToolTip(this.btnSaveAs, "Save As...");
			this.btnSaveAs.UseVisualStyleBackColor = true;
			// 
			// btnAddRootFilter
			// 
			this.btnAddRootFilter.Location = new System.Drawing.Point(460, 50);
			this.btnAddRootFilter.Name = "btnAddRootFilter";
			this.btnAddRootFilter.Size = new System.Drawing.Size(30, 23);
			this.btnAddRootFilter.TabIndex = 44;
			this.ToolTip.SetToolTip(this.btnAddRootFilter, "Add Root Filter");
			this.btnAddRootFilter.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(460, 282);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(30, 23);
			this.btnSave.TabIndex = 48;
			this.ToolTip.SetToolTip(this.btnSave, "Save...");
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(460, 108);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(30, 23);
			this.btnDelete.TabIndex = 43;
			this.ToolTip.SetToolTip(this.btnDelete, "Delete");
			this.btnDelete.UseVisualStyleBackColor = true;
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(460, 224);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(30, 23);
			this.btnOpen.TabIndex = 47;
			this.ToolTip.SetToolTip(this.btnOpen, "Open");
			this.btnOpen.UseVisualStyleBackColor = true;
			// 
			// btnClearAll
			// 
			this.btnClearAll.Location = new System.Drawing.Point(460, 137);
			this.btnClearAll.Name = "btnClearAll";
			this.btnClearAll.Size = new System.Drawing.Size(30, 23);
			this.btnClearAll.TabIndex = 46;
			this.ToolTip.SetToolTip(this.btnClearAll, "Clear All");
			this.btnClearAll.UseVisualStyleBackColor = true;
			// 
			// ValueBox
			// 
			this.ValueBox.BackColor = System.Drawing.SystemColors.Window;
			this.ValueBox.Location = new System.Drawing.Point(300, 13);
			this.ValueBox.Name = "ValueBox";
			this.ValueBox.Size = new System.Drawing.Size(154, 21);
			this.ValueBox.TabIndex = 40;
			// 
			// popupDelete
			// 
			this.popupDelete.Name = "popupDelete";
			this.popupDelete.Size = new System.Drawing.Size(178, 22);
			this.popupDelete.Text = "Delete";
			// 
			// popupInsertSiblingFilter
			// 
			this.popupInsertSiblingFilter.Name = "popupInsertSiblingFilter";
			this.popupInsertSiblingFilter.Size = new System.Drawing.Size(178, 22);
			this.popupInsertSiblingFilter.Text = "Insert Sibling Filter";
			// 
			// popupInsertSiblingGroup
			// 
			this.popupInsertSiblingGroup.Name = "popupInsertSiblingGroup";
			this.popupInsertSiblingGroup.Size = new System.Drawing.Size(178, 22);
			this.popupInsertSiblingGroup.Text = "Insert Sibling Group";
			// 
			// popupAddChildFilter
			// 
			this.popupAddChildFilter.Name = "popupAddChildFilter";
			this.popupAddChildFilter.Size = new System.Drawing.Size(178, 22);
			this.popupAddChildFilter.Text = "Add Child Filter";
			// 
			// popupAddChildGroup
			// 
			this.popupAddChildGroup.Name = "popupAddChildGroup";
			this.popupAddChildGroup.Size = new System.Drawing.Size(178, 22);
			this.popupAddChildGroup.Text = "Add Child Group";
			// 
			// PopupMenu
			// 
			this.PopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.popupAddChildGroup,
            this.popupAddChildFilter,
            this.popupInsertSiblingGroup,
            this.popupInsertSiblingFilter,
            this.popupDelete});
			this.PopupMenu.Name = "PopupMenu";
			this.PopupMenu.Size = new System.Drawing.Size(179, 114);
			// 
			// OperatorBox
			// 
			this.OperatorBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.OperatorBox.FormattingEnabled = true;
			this.OperatorBox.Items.AddRange(new object[] {
            "containing",
            "starting with",
            "ending with",
            "not containing",
            "not starting with",
            "not ending with",
            "=",
            "<>",
            "<",
            ">",
            "<=",
            ">="});
			this.OperatorBox.Location = new System.Drawing.Point(178, 13);
			this.OperatorBox.Name = "OperatorBox";
			this.OperatorBox.Size = new System.Drawing.Size(116, 21);
			this.OperatorBox.TabIndex = 38;
			// 
			// PropertyBox
			// 
			this.PropertyBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PropertyBox.FormattingEnabled = true;
			this.PropertyBox.Location = new System.Drawing.Point(12, 13);
			this.PropertyBox.Name = "PropertyBox";
			this.PropertyBox.Size = new System.Drawing.Size(160, 21);
			this.PropertyBox.TabIndex = 37;
			// 
			// TreeView
			// 
			this.TreeView.AllowDrop = true;
			this.TreeView.ContextMenuStrip = this.PopupMenu;
			this.TreeView.HideSelection = false;
			this.TreeView.Location = new System.Drawing.Point(12, 40);
			this.TreeView.Name = "TreeView";
			this.TreeView.Size = new System.Drawing.Size(442, 310);
			this.TreeView.TabIndex = 36;
			// 
			// QuantifierBox
			// 
			this.QuantifierBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.QuantifierBox.FormattingEnabled = true;
			this.QuantifierBox.Location = new System.Drawing.Point(12, 13);
			this.QuantifierBox.Name = "QuantifierBox";
			this.QuantifierBox.Size = new System.Drawing.Size(442, 21);
			this.QuantifierBox.TabIndex = 39;
			// 
			// btnMoveDown
			// 
			this.btnMoveDown.Image = global::TagScanner.Properties.Resources.arrow_Down_16xLG;
			this.btnMoveDown.Location = new System.Drawing.Point(460, 195);
			this.btnMoveDown.Name = "btnMoveDown";
			this.btnMoveDown.Size = new System.Drawing.Size(30, 23);
			this.btnMoveDown.TabIndex = 42;
			this.ToolTip.SetToolTip(this.btnMoveDown, "Move Down");
			this.btnMoveDown.UseVisualStyleBackColor = true;
			// 
			// btnMoveUp
			// 
			this.btnMoveUp.Image = global::TagScanner.Properties.Resources.arrow_Up_16xLG;
			this.btnMoveUp.Location = new System.Drawing.Point(460, 166);
			this.btnMoveUp.Name = "btnMoveUp";
			this.btnMoveUp.Size = new System.Drawing.Size(30, 23);
			this.btnMoveUp.TabIndex = 41;
			this.ToolTip.SetToolTip(this.btnMoveUp, "Move Up");
			this.btnMoveUp.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(415, 360);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 52;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(334, 360);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 51;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// FilterDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(501, 395);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnReopen);
			this.Controls.Add(this.btnAddRootGroup);
			this.Controls.Add(this.btnSaveAs);
			this.Controls.Add(this.btnAddRootFilter);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.btnClearAll);
			this.Controls.Add(this.btnMoveDown);
			this.Controls.Add(this.btnMoveUp);
			this.Controls.Add(this.ValueBox);
			this.Controls.Add(this.OperatorBox);
			this.Controls.Add(this.PropertyBox);
			this.Controls.Add(this.TreeView);
			this.Controls.Add(this.QuantifierBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FilterDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Filters";
			this.PopupMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.Button btnReopen;
		private System.Windows.Forms.ToolTip ToolTip;
		public System.Windows.Forms.Button btnAddRootGroup;
		public System.Windows.Forms.Button btnSaveAs;
		public System.Windows.Forms.Button btnAddRootFilter;
		public System.Windows.Forms.Button btnSave;
		public System.Windows.Forms.Button btnDelete;
		public System.Windows.Forms.Button btnOpen;
		public System.Windows.Forms.Button btnClearAll;
		public System.Windows.Forms.Button btnMoveDown;
		public System.Windows.Forms.Button btnMoveUp;
		public System.Windows.Forms.Panel ValueBox;
		public System.Windows.Forms.ToolStripMenuItem popupDelete;
		public System.Windows.Forms.ToolStripMenuItem popupInsertSiblingFilter;
		public System.Windows.Forms.ToolStripMenuItem popupInsertSiblingGroup;
		public System.Windows.Forms.ToolStripMenuItem popupAddChildFilter;
		public System.Windows.Forms.ToolStripMenuItem popupAddChildGroup;
		private System.Windows.Forms.ContextMenuStrip PopupMenu;
		public System.Windows.Forms.ComboBox OperatorBox;
		public System.Windows.Forms.ComboBox PropertyBox;
		public System.Windows.Forms.TreeView TreeView;
		public System.Windows.Forms.ComboBox QuantifierBox;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
	}
}