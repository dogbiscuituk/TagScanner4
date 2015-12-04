using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class TagSelectorController : IComparer
	{
		public TagSelectorController(IEnumerable<PropertyInfo> propertyInfos)
		{
			_propertyInfos = propertyInfos;
		}

		public bool Execute(List<string> visibleTagNames)
		{
			using (_dialog = new TagSelectorDialog())
			{
				_listView = _dialog.ListView;
				var items = _listView.Items;
				foreach (var propertyInfo in _propertyInfos)
				{
					var name = propertyInfo.Name;
                    var item = items.Add(name);
					var subItems = item.SubItems;
					item.Checked = visibleTagNames.Contains(name);
					subItems.Add(propertyInfo.PropertyType.Name);
					subItems.Add(propertyInfo.CanWrite ? "Yes" : "No");
				}
				_listView.ColumnClick += ListView_ColumnClick;
				_listView.ListViewItemSorter = this;
				_dialog.PopupMenu.Opening += PopupMenu_Opening;
				_dialog.PopupCheck.Click += PopupCheck_Click;
				_dialog.PopupUncheck.Click += PopupUncheck_Click;
				_dialog.PopupView.DropDownOpening += PopupView_DropDownOpening;
				_dialog.PopupViewDetails.Click += PopupViewDetails_Click;
				_dialog.PopupViewList.Click += PopupViewList_Click;
				var ok = _dialog.ShowDialog() == DialogResult.OK;
				if (ok)
				{
					visibleTagNames.Clear();
					visibleTagNames.AddRange(
						_dialog.ListView.Items
							.Cast<ListViewItem>()
							.Where(t => t.Checked)
							.Select(t => t.Text));
                }
				return ok;
			}
		}

		private TagSelectorDialog _dialog;
		private ListView _listView;
		private IEnumerable<PropertyInfo> _propertyInfos;
		private int _sortColumn;
		private bool _sortDescending;

		private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (_sortColumn != e.Column)
			{
				_sortColumn = e.Column;
				_sortDescending = false;
			}
			else
				_sortDescending = !_sortDescending;
			_listView.Sort();
		}

		private void PopupMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			var selectionExists = _listView.SelectedItems.Count > 0;
			_dialog.PopupCheck.Enabled = selectionExists;
			_dialog.PopupUncheck.Enabled = selectionExists;
		}

		private void PopupCheck_Click(object sender, EventArgs e)
		{
			SetChecked(true);
		}

		private void PopupUncheck_Click(object sender, EventArgs e)
		{
			SetChecked(false);
		}

		private void PopupView_DropDownOpening(object sender, EventArgs e)
		{
			var view = _listView.View;
			_dialog.PopupViewList.Checked = view == View.List;
			_dialog.PopupViewDetails.Checked = view == View.Details;
		}

		private void PopupViewList_Click(object sender, EventArgs e)
		{
			SetView(View.List);
		}

		private void PopupViewDetails_Click(object sender, EventArgs e)
		{
			SetView(View.Details);
		}

		public int Compare(object x, object y)
		{
			var result = string.Compare(GetValue(x), GetValue(y));
			return _sortDescending ? -result : +result;
		}

		private string GetValue(object o)
		{
			var item = (ListViewItem)o;
            return _sortColumn == 0 ? item.Text : item.SubItems[_sortColumn].Text;
        }

		private void SetChecked(bool isChecked)
		{
			foreach (ListViewItem item in _listView.SelectedItems)
				item.Checked = isChecked;
		}

		private void SetView(View view)
		{
			_listView.View = view;
		}
	}
}
