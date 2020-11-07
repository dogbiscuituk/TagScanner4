using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using TagScanner.Models;
using TagScanner.Properties;

namespace TagScanner.Controllers
{
	public abstract class SdiController : MruController
	{
		protected SdiController(Model model, string filter, string subKeyName, ToolStripDropDownItem recentMenu)
			: base(model, subKeyName, recentMenu)
		{
			OpenFileDialog = new OpenFileDialog
			{
				Filter = filter,
				Title = Resources.S_SelectFileToOpen
			};
			SaveFileDialog = new SaveFileDialog
			{
				Filter = filter,
				Title = Resources.S_SaveFile
			};
		}

		public bool Clear()
		{
			var result = SaveIfModified();
			if (result)
			{
				ClearDocument();
				Model.Modified = false;
				FilePath = string.Empty;
			}
			return result;
		}

		public bool Open()
		{
			return SaveIfModified() && OpenFileDialog.ShowDialog() == DialogResult.OK && LoadFromFile(OpenFileDialog.FileName);
		}

		public bool Save()
		{
			return string.IsNullOrEmpty(FilePath) ? SaveAs() : SaveToFile(FilePath);
		}

		public bool SaveAs()
		{
			return SaveFileDialog.ShowDialog() == DialogResult.OK && SaveToFile(SaveFileDialog.FileName);
		}

		public bool SaveIfModified()
		{
			if (Model.Modified)
				switch (MessageBox.Show(
					Resources.S_FileContentsHaveChanged,
					Resources.S_FileModified,
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Warning))
				{
					case DialogResult.Yes:
						return Save();
					case DialogResult.No:
						return true;
					case DialogResult.Cancel:
						return false;
				}
			return true;
		}

		public event EventHandler FilePathChanged;

		public event EventHandler<CancelEventArgs> FileLoading;
		public event EventHandler<CancelEventArgs> FileSaving;

		private string _filePath = string.Empty;
		protected string FilePath
		{
			get => _filePath;
			set
			{
				if (FilePath != value)
				{
					_filePath = value;
					OnFilePathChanged();
				}
			}
		}

		protected abstract void ClearDocument();

		protected abstract bool LoadFromStream(Stream stream, string format);

		protected virtual void OnFilePathChanged()
		{
			FilePathChanged?.Invoke(this, EventArgs.Empty);
		}

		protected virtual bool OnFileLoading()
		{
			var result = true;
			var fileLoading = FileLoading;
			if (fileLoading != null)
			{
				var e = new CancelEventArgs();
				fileLoading(this, e);
				result = !e.Cancel;
			}
			return result;
		}

		protected virtual bool OnFileSaving()
		{
			var result = true;
			var fileSaving = FileSaving;
			if (fileSaving != null)
			{
				var e = new CancelEventArgs();
				fileSaving(this, e);
				result = !e.Cancel;
			}
			return result;
		}

		protected override void Reopen(ToolStripItem menuItem)
		{
			var filePath = menuItem.ToolTipText;
			if (File.Exists(filePath))
			{
				if (SaveIfModified())
					LoadFromFile(filePath);
			}
			else if (
				MessageBox.Show(
					string.Format(Resources.S_FileNoLongerExists, filePath),
					Resources.S_RepoenFile,
					MessageBoxButtons.YesNo) == DialogResult.Yes)
				RemoveItem(filePath);
		}

		protected abstract bool SaveToStream(Stream stream, string format);

		protected bool UseStream(Action action)
		{
			var result = true;
			try
			{
				action();
				Model.Modified = false;
			}
			catch (Exception x)
			{
				MessageBox.Show(
					x.Message,
					x.GetType().Name,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				result = false;
			}
			return result;
		}

		private readonly OpenFileDialog OpenFileDialog;
		private readonly SaveFileDialog SaveFileDialog;

		private bool LoadFromFile(string filePath)
		{
			var result = false;
			if (OnFileLoading())
			{
				using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
					result = LoadFromStream(stream, Path.GetExtension(filePath));
				if (result)
				{
					FilePath = filePath;
					AddItem(filePath);
				}
			}
			return result;
		}

		private bool SaveToFile(string filePath)
		{
			var result = false;
			if (OnFileSaving())
				using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
				{
					result = SaveToStream(stream, Path.GetExtension(filePath));
					if (result)
					{
						stream.Flush();
						FilePath = filePath;
						AddItem(filePath);
					}
				}
			return result;
		}
	}
}
