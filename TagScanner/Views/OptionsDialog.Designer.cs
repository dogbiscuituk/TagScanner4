namespace TagScanner.Views
{
	partial class OptionsDialog
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
			this.TabControl = new System.Windows.Forms.TabControl();
			this.ColumnsPage = new System.Windows.Forms.TabPage();
			this.FiltersPage = new System.Windows.Forms.TabPage();
			this.GroupsPage = new System.Windows.Forms.TabPage();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.TabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// TabControl
			// 
			this.TabControl.Controls.Add(this.ColumnsPage);
			this.TabControl.Controls.Add(this.FiltersPage);
			this.TabControl.Controls.Add(this.GroupsPage);
			this.TabControl.Location = new System.Drawing.Point(12, 12);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(528, 389);
			this.TabControl.TabIndex = 13;
			// 
			// ColumnsPage
			// 
			this.ColumnsPage.Location = new System.Drawing.Point(4, 22);
			this.ColumnsPage.Name = "ColumnsPage";
			this.ColumnsPage.Padding = new System.Windows.Forms.Padding(3);
			this.ColumnsPage.Size = new System.Drawing.Size(520, 363);
			this.ColumnsPage.TabIndex = 0;
			this.ColumnsPage.Text = "Columns";
			this.ColumnsPage.UseVisualStyleBackColor = true;
			// 
			// FiltersPage
			// 
			this.FiltersPage.Location = new System.Drawing.Point(4, 22);
			this.FiltersPage.Name = "FiltersPage";
			this.FiltersPage.Size = new System.Drawing.Size(520, 363);
			this.FiltersPage.TabIndex = 3;
			this.FiltersPage.Text = "Filters";
			this.FiltersPage.UseVisualStyleBackColor = true;
			// 
			// GroupsPage
			// 
			this.GroupsPage.Location = new System.Drawing.Point(4, 22);
			this.GroupsPage.Name = "GroupsPage";
			this.GroupsPage.Padding = new System.Windows.Forms.Padding(3);
			this.GroupsPage.Size = new System.Drawing.Size(520, 363);
			this.GroupsPage.TabIndex = 1;
			this.GroupsPage.Text = "Groups";
			this.GroupsPage.UseVisualStyleBackColor = true;
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(462, 407);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 19;
			this.btnApply.Text = "&Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(381, 407);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 18;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(300, 407);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 17;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// OptionsDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(552, 442);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.TabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.TabControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.TabControl TabControl;
		public System.Windows.Forms.TabPage ColumnsPage;
		public System.Windows.Forms.TabPage FiltersPage;
		public System.Windows.Forms.TabPage GroupsPage;
		public System.Windows.Forms.Button btnApply;
		public System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.Button btnOK;
	}
}