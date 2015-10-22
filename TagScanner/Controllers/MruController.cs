using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TagScanner.Models;
using Win32 = Microsoft.Win32;

namespace TagScanner.Controllers
{
	public class MruController
	{
		protected MruController(Model model, string subKeyName, ToolStripDropDownItem recentMenu)
		{
			Model = model;
			if (string.IsNullOrWhiteSpace(subKeyName))
				throw new ArgumentNullException("subKeyName");
			SubKeyName = string.Concat(@"Software\", Application.ProductName, @"\", subKeyName);
			RecentMenu = recentMenu;
			RefreshRecentMenu();
		}

		protected readonly Model Model;
		protected OpenFileDialog OpenFileDialog;

		protected void AddItem(string item)
		{
			try
			{
				var key = CreateSubKey();
				if (key == null)
					return;
				try
				{
					for (int index = 0; ; index++)
					{
						var name = index.ToString();
						var value = key.GetValue(name, null) as string;
						if (value == null)
						{
							key.SetValue(name, item);
							break;
						}
						if (value == item)
							break;
					}
				}
				finally
				{
					key.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			RefreshRecentMenu();
		}

		protected void RemoveItem(string item)
		{
			try
			{
				var key = OpenSubKey(true);
				if (key == null)
					return;
				try
				{
					foreach (var name in key.GetValueNames())
						if ((key.GetValue(name, null) as string) == item)
						{
							key.DeleteValue(name, true);
							break;
						}
				}
				finally
				{
					key.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			RefreshRecentMenu();
		}

		protected virtual void Reopen(ToolStripItem menuItem)
		{
		}

		private string SubKeyName;
		private ToolStripDropDownItem RecentMenu;

		private void OnItemClick(object sender, EventArgs e)
		{
			Reopen((ToolStripItem)sender);
		}

		private void OnRecentClear_Click(object sender, EventArgs e)
		{
			try
			{
				Win32.RegistryKey key = OpenSubKey(true);
				if (key == null)
					return;
				foreach (string name in key.GetValueNames())
					key.DeleteValue(name, true);
				key.Close();
				if (RecentMenu != null)
				{
					RecentMenu.DropDownItems.Clear();
					RecentMenu.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		private void RefreshRecentMenu()
		{
			if (RecentMenu == null)
				return;
			var items = RecentMenu.DropDownItems;
			items.Clear();
			Win32.RegistryKey key = null;
			try
			{
				key = OpenSubKey(false);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			bool ok = key != null;
			if (ok)
			{
				foreach (var name in key.GetValueNames())
				{
					var value = key.GetValue(name, null) as string;
					if (value == null)
						continue;
					try
					{
						var text = CompactMenuText(value.Split('|')[0]);
						var item = items.Add(text, null, OnItemClick);
						item.Tag = value;
						item.ToolTipText = value.Replace('|', '\n');
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
					}
				}
				ok = items.Count > 0;
				if (ok)
				{
					items.Add("-");
					items.Add("Clear this list").Click += OnRecentClear_Click;
				}
			}
			RecentMenu.Enabled = ok;
		}

		private static string CompactMenuText(string text)
		{
			var result = Path.ChangeExtension(text, string.Empty).TrimEnd('.');
			TextRenderer.MeasureText(
				result,
				SystemFonts.MenuFont,
				new Size(320, 0),
				TextFormatFlags.PathEllipsis | TextFormatFlags.ModifyString);
			var length = result.IndexOf((char)0);
			if (length >= 0)
				result = result.Substring(0, length);
			return result.Replace("&", "&&");
		}

		private Win32.RegistryKey CreateSubKey()
		{
			return Win32.Registry.CurrentUser.CreateSubKey(SubKeyName, Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
		}

		private Win32.RegistryKey OpenSubKey(bool writable)
		{
			return Win32.Registry.CurrentUser.OpenSubKey(SubKeyName, writable);
		}
	}
}
