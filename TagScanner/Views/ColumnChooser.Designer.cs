namespace TagScanner.Views
{
	partial class ColumnChooser
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lblAvailableColumns = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lblSelectedColumns = new System.Windows.Forms.Label();
			this.listView2 = new System.Windows.Forms.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lblCheckBoxes = new System.Windows.Forms.Label();
			this.btnRemoveAll = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnAddAll = new System.Windows.Forms.Button();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// lblAvailableColumns
			// 
			this.lblAvailableColumns.AutoSize = true;
			this.lblAvailableColumns.Location = new System.Drawing.Point(20, 20);
			this.lblAvailableColumns.Name = "lblAvailableColumns";
			this.lblAvailableColumns.Size = new System.Drawing.Size(93, 13);
			this.lblAvailableColumns.TabIndex = 0;
			this.lblAvailableColumns.Text = "&Available Columns";
			// 
			// listView1
			// 
			this.listView1.AllowDrop = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listView1.FullRowSelect = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(23, 36);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(200, 310);
			this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listView1.TabIndex = 1;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 161;
			// 
			// lblSelectedColumns
			// 
			this.lblSelectedColumns.AutoSize = true;
			this.lblSelectedColumns.Location = new System.Drawing.Point(262, 20);
			this.lblSelectedColumns.Name = "lblSelectedColumns";
			this.lblSelectedColumns.Size = new System.Drawing.Size(92, 13);
			this.lblSelectedColumns.TabIndex = 2;
			this.lblSelectedColumns.Text = "&Selected Columns";
			// 
			// listView2
			// 
			this.listView2.AllowDrop = true;
			this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
			this.listView2.FullRowSelect = true;
			this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView2.HideSelection = false;
			this.listView2.Location = new System.Drawing.Point(265, 36);
			this.listView2.Name = "listView2";
			this.listView2.Size = new System.Drawing.Size(200, 310);
			this.listView2.TabIndex = 4;
			this.listView2.UseCompatibleStateImageBehavior = false;
			this.listView2.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Name";
			this.columnHeader2.Width = 161;
			// 
			// lblCheckBoxes
			// 
			this.lblCheckBoxes.AutoSize = true;
			this.lblCheckBoxes.Location = new System.Drawing.Point(360, 20);
			this.lblCheckBoxes.Name = "lblCheckBoxes";
			this.lblCheckBoxes.Size = new System.Drawing.Size(116, 13);
			this.lblCheckBoxes.TabIndex = 3;
			this.lblCheckBoxes.Text = "(check => descending)";
			// 
			// btnRemoveAll
			// 
			this.btnRemoveAll.Location = new System.Drawing.Point(229, 133);
			this.btnRemoveAll.Name = "btnRemoveAll";
			this.btnRemoveAll.Size = new System.Drawing.Size(30, 23);
			this.btnRemoveAll.TabIndex = 5;
			this.btnRemoveAll.Text = "<<";
			this.ToolTip.SetToolTip(this.btnRemoveAll, "Remove All");
			this.btnRemoveAll.UseVisualStyleBackColor = true;
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(229, 162);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(30, 23);
			this.btnRemove.TabIndex = 6;
			this.btnRemove.Text = "<";
			this.ToolTip.SetToolTip(this.btnRemove, "Remove Selected");
			this.btnRemove.UseVisualStyleBackColor = true;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(229, 191);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(30, 23);
			this.btnAdd.TabIndex = 7;
			this.btnAdd.Text = ">";
			this.ToolTip.SetToolTip(this.btnAdd, "Add Selected");
			this.btnAdd.UseVisualStyleBackColor = true;
			// 
			// btnAddAll
			// 
			this.btnAddAll.Location = new System.Drawing.Point(229, 220);
			this.btnAddAll.Name = "btnAddAll";
			this.btnAddAll.Size = new System.Drawing.Size(30, 23);
			this.btnAddAll.TabIndex = 8;
			this.btnAddAll.Text = ">>";
			this.ToolTip.SetToolTip(this.btnAddAll, "Add All");
			this.btnAddAll.UseVisualStyleBackColor = true;
			// 
			// btnMoveUp
			// 
			this.btnMoveUp.Image = global::TagScanner.Properties.Resources.arrow_Up_16xLG;
			this.btnMoveUp.Location = new System.Drawing.Point(471, 162);
			this.btnMoveUp.Name = "btnMoveUp";
			this.btnMoveUp.Size = new System.Drawing.Size(30, 23);
			this.btnMoveUp.TabIndex = 9;
			this.ToolTip.SetToolTip(this.btnMoveUp, "Move Up");
			this.btnMoveUp.UseVisualStyleBackColor = true;
			// 
			// btnMoveDown
			// 
			this.btnMoveDown.Image = global::TagScanner.Properties.Resources.arrow_Down_16xLG;
			this.btnMoveDown.Location = new System.Drawing.Point(471, 191);
			this.btnMoveDown.Name = "btnMoveDown";
			this.btnMoveDown.Size = new System.Drawing.Size(30, 23);
			this.btnMoveDown.TabIndex = 10;
			this.ToolTip.SetToolTip(this.btnMoveDown, "Move Down");
			this.btnMoveDown.UseVisualStyleBackColor = true;
			// 
			// ColumnChooser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnMoveDown);
			this.Controls.Add(this.btnMoveUp);
			this.Controls.Add(this.btnAddAll);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnRemoveAll);
			this.Controls.Add(this.lblCheckBoxes);
			this.Controls.Add(this.listView2);
			this.Controls.Add(this.lblSelectedColumns);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.lblAvailableColumns);
			this.Name = "ColumnChooser";
			this.Size = new System.Drawing.Size(520, 363);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblAvailableColumns;
		public System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Label lblSelectedColumns;
		public System.Windows.Forms.ListView listView2;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		public System.Windows.Forms.Label lblCheckBoxes;
		public System.Windows.Forms.Button btnRemoveAll;
		public System.Windows.Forms.Button btnRemove;
		public System.Windows.Forms.Button btnAdd;
		public System.Windows.Forms.Button btnAddAll;
		public System.Windows.Forms.Button btnMoveUp;
		public System.Windows.Forms.Button btnMoveDown;
		private System.Windows.Forms.ToolTip ToolTip;
	}
}
