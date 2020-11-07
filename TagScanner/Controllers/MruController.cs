namespace TagScanner.Controllers
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using TagScanner.Models;
    using Win32 = Microsoft.Win32;

    public class MruController
    {
        protected MruController(Model model, string subKeyName, ToolStripDropDownItem recentMenu)
        {
            if (string.IsNullOrWhiteSpace(subKeyName))
                throw new ArgumentNullException(nameof(subKeyName));
            Model = model;
            SubKeyName = $@"Software\{Application.CompanyName}\{Application.ProductName}\{subKeyName}";
            RecentMenu = recentMenu;
            RefreshRecentMenu();
        }

        protected readonly Model Model;

        protected void AddItem(string item)
        {
            try
            {
                var key = CreateSubKey();
                if (key == null)
                    return;
                try
                {
                    DeleteItem(key, item);
                    key.SetValue($"{DateTime.Now:yyyyMMddHHmmssFF}", item);
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
                    DeleteItem(key, item);
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

        private void DeleteItem(Win32.RegistryKey key, string item)
        {
            var name = key
                .GetValueNames()
                .FirstOrDefault(n => key.GetValue(n, null) as string == item);
            if (name != null)
                key.DeleteValue(name);
        }

        protected virtual void Reopen(ToolStripItem menuItem)
        {
        }

        private readonly string SubKeyName;
        private readonly ToolStripDropDownItem RecentMenu;

        private void OnItemClick(object sender, EventArgs e) => Reopen((ToolStripItem)sender);

        private void OnRecentClear_Click(object sender, EventArgs e)
        {
            try
            {
                var key = OpenSubKey(true);
                if (key == null)
                    return;
                foreach (var name in key.GetValueNames())
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
            var ok = key != null;
            if (ok)
            {
                foreach (var name in key.GetValueNames().OrderByDescending(n => n))
                {
                    if (!(key.GetValue(name, null) is string value))
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

        private Win32.RegistryKey CreateSubKey() => Win32.Registry.CurrentUser.CreateSubKey(SubKeyName, Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);

        private Win32.RegistryKey OpenSubKey(bool writable) => Win32.Registry.CurrentUser.OpenSubKey(SubKeyName, writable);

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
            return result.AmpersandEscape();
        }
    }
}
