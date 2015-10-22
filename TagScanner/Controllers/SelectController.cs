using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagScanner.Models;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class SelectController
	{
		#region Public Interface

		#region Lifetime Management

		public SelectController(Control host, OptionsDialogController.Options options = 0)
		{
			var showCheckBoxes = (options & OptionsDialogController.Options.ShowCheckBoxes) != 0;
			View = new ColumnChooser
			{
				listView2 = { CheckBoxes = showCheckBoxes },
				lblCheckBoxes = { Visible = showCheckBoxes }
			};
			CanAddAll = (options & OptionsDialogController.Options.EnableAddAll) != 0;
			host.Controls.Add(View);
		}

		#endregion

		#region Properties

		private bool _canAddAll = true;
		public bool CanAddAll
		{
			get
			{
				return _canAddAll;
			}
			set
			{
				if (CanAddAll == value)
					return;
				_canAddAll = value;
				UpdateControls();
			}
		}

		public Order[] ChosenOrders
		{
			get { return ListViewItems2.Select(p => new Order(p.Text, p.Checked)).ToArray(); }
		}

		public string[] ChosenColumnNames
		{
			get
			{
				return ListViewItems2.Select(p => p.Text).ToArray();
            }
		}

		#endregion

		#region Methods

		public void Init(IEnumerable<string> propertyNames, IEnumerable<Order> selectedOrders)
		{
			ListView1.Items.Clear();
			foreach (var propertyName in propertyNames.Where(p => !selectedOrders.Any(q => q.PropertyName == p)))
				ListView1.Items.Add(propertyName);
			ListView2.Items.Clear();
			foreach (var order in selectedOrders)
				ListView2.Items.Add(order.PropertyName).Checked = order.Descending;
			UpdateControls();
		}

		#endregion

		#endregion

		#region Private Implementation

		#region Properties

		private ListView ListView1
		{
			get { return View.listView1; }
		}

		private ListView ListView2
		{
			get { return View.listView2; }
		}

		private ListView.ListViewItemCollection Items1
		{
			get { return ListView1.Items; }
		}

		private ListView.ListViewItemCollection Items2
		{
			get { return ListView2.Items; }
		}

		private int ItemCount1
		{
			get { return Items1.Count; }
		}

		private int ItemCount2
		{
			get { return Items2.Count; }
		}

		private IEnumerable<ListViewItem> ListViewItems2
		{
			get { return Items2.OfType<ListViewItem>(); }
		}

		private ListView.SelectedIndexCollection Selection1
		{
			get { return ListView1.SelectedIndices; }
		}

		private ListView.SelectedIndexCollection Selection2
		{
			get { return ListView2.SelectedIndices; }
		}

		private int SelectionCount1
		{
			get { return Selection1.Count; }
		}

		private int SelectionCount2
		{
			get { return Selection2.Count; }
		}

		private ColumnChooser _view;
		private ColumnChooser View
		{
			get
			{
				return _view;
			}
			set
			{
				if (View != null)
				{
					View.btnAdd.Click -= btnAdd_Click;
					View.btnAddAll.Click -= btnAddAll_Click;
					View.btnRemove.Click -= btnRemove_Click;
					View.btnRemoveAll.Click -= btnRemoveAll_Click;
					View.btnMoveUp.Click -= btnMoveUp_Click;
					View.btnMoveDown.Click -= btnMoveDown_Click;
					ListView1.DoubleClick -= btnAdd_Click;
					ListView1.SelectedIndexChanged -= ListView_SelectedIndexChanged;
					ListView2.DoubleClick -= btnRemove_Click;
					ListView2.SelectedIndexChanged -= ListView_SelectedIndexChanged;
				}
				_view = value;
				if (View != null)
				{
					View.btnAdd.Click += btnAdd_Click;
					View.btnAddAll.Click += btnAddAll_Click;
					View.btnRemove.Click += btnRemove_Click;
					View.btnRemoveAll.Click += btnRemoveAll_Click;
					View.btnMoveUp.Click += btnMoveUp_Click;
					View.btnMoveDown.Click += btnMoveDown_Click;
					ListView1.DoubleClick += btnAdd_Click;
					ListView1.DragDrop += ListView_DragDrop;
					ListView1.DragEnter += ListView_DragEnter;
					ListView1.ItemDrag += ListView_ItemDrag;
					ListView1.SelectedIndexChanged += ListView_SelectedIndexChanged;
					ListView2.DoubleClick += btnRemove_Click;
					ListView2.DragDrop += ListView_DragDrop;
					ListView2.DragEnter += ListView_DragEnter;
					ListView2.ItemDrag += ListView_ItemDrag;
					ListView2.SelectedIndexChanged += ListView_SelectedIndexChanged;
					UpdateControls();
				}
			}
		}

		#endregion

		#region Drag & Drop

		private ListView _dragSource, _dragTarget;

		private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			_dragSource = sender as ListView;
			if (_dragSource != null)
				_dragSource.DoDragDrop(_dragSource.SelectedItems, DragDropEffects.Move);
		}

		private void ListView_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect =
				sender != _dragSource || sender == ListView2
					? DragDropEffects.Move
					: DragDropEffects.None;
		}

		private void ListView_DragDrop(object sender, DragEventArgs e)
		{
			_dragTarget = sender as ListView;
			if (_dragTarget == null)
				return;
			if (_dragTarget == ListView2)
			{
				var p = _dragTarget.PointToClient(new Point(e.X, e.Y));
				var targetItem = _dragTarget.GetItemAt(p.X, p.Y);
				var count = ItemCount2;
				var delta = targetItem != null ? count - targetItem.Index : 0;
				if (_dragSource == ListView1)
				{
					MoveSelected(_dragSource, _dragTarget);
					if (delta > 0)
						for (var index = count; index < ItemCount2; index++)
							MoveItem(index, index - delta);
				}
				else if (_dragSource == ListView2)
				{
					var items = ListView2.SelectedItems.OfType<ListViewItem>().ToList();
					if (targetItem == null || !items.Contains(targetItem))
					{
						foreach (var item in items)
							Items2.Remove(item);
						if (targetItem == null)
							foreach (var item in items)
								Items2.Add(item);
						else
						{
							var index = Items2.IndexOf(targetItem);
							foreach (var item in items)
								Items2.Insert(index++, item);
						}
					}
				}
			}
			else if (_dragSource == ListView2 && _dragTarget == ListView1)
				MoveSelected(_dragSource, _dragTarget);
		}

		#endregion

		#region Methods

		private void MoveAll(ListView source, ListView target)
		{
			MoveItems(source, target, source.Items);
		}

		private void MoveDown()
		{
			for (var index = SelectionCount2; index > 0;)
				MoveItemBy(--index, +1);
			UpdateControls();
		}

		private void MoveItem(int oldIndex, int newIndex)
		{
			var item = Items2[oldIndex];
			Items2.RemoveAt(oldIndex);
			Items2.Insert(newIndex, item);
		}

		private void MoveItemBy(int index, int delta)
		{
			var oldIndex = Selection2[index];
			var newIndex = oldIndex + delta;
			MoveItem(oldIndex, newIndex);
		}

		private void MoveItems(ListView source, ListView target, IEnumerable items)
		{
			foreach (var item in items.OfType<ListViewItem>().ToList())
			{
				source.Items.Remove(item);
				target.Items.Add(item);
			}
			UpdateControls();
		}

		private void MoveSelected(ListView source, ListView target)
		{
			MoveItems(source, target, source.SelectedItems);
		}

		private void MoveUp()
		{
			for (var index = 0; index < SelectionCount2;)
				MoveItemBy(index++, -1);
			UpdateControls();
		}

		private void UpdateControls()
		{
			var canAdd = SelectionCount1 > 0;
			var canRemove = SelectionCount2 > 0;
			var canMoveUp = canRemove && Selection2[0] > 0;
			var canMoveDown = canRemove && Selection2[SelectionCount2 - 1] < ItemCount2 - 1;
			View.btnAdd.Enabled = canAdd;
			View.btnAddAll.Enabled = ItemCount1 > 0 && CanAddAll;
			View.btnRemove.Enabled = canRemove;
			View.btnRemoveAll.Enabled = ItemCount2 > 0;
			View.btnMoveUp.Enabled = canMoveUp;
			View.btnMoveDown.Enabled = canMoveDown;
		}

		#endregion

		#region Events

		private void btnAdd_Click(object sender, EventArgs e)
		{
			MoveSelected(ListView1, ListView2);
		}

		private void btnAddAll_Click(object sender, EventArgs e)
		{
			MoveAll(ListView1, ListView2);
		}

		private void btnMoveDown_Click(object sender, EventArgs e)
		{
			MoveDown();
		}

		private void btnMoveUp_Click(object sender, EventArgs e)
		{
			MoveUp();
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			MoveSelected(ListView2, ListView1);
		}

		private void btnRemoveAll_Click(object sender, EventArgs e)
		{
			MoveAll(ListView2, ListView1);
		}

		private void ListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateControls();
		}

		#endregion

		#endregion
	}
}
