using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using TagScanner.Models;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class FilterController : SdiController
	{
		#region Lifetime Management

		public FilterController(Model model, Control host, ToolStripDropDownItem recentMenu)
			: base(model, Properties.Settings.Default.SearchFilter, "FilterMRU", recentMenu)
		{
			View = new FilterEditor();
			host.Controls.Add(View);
			_simpleFilterEditController = new SimpleFilterEditController(View);
			_simpleFilterEditController.ValueChanged += FilterController_ValueChanged;
			_compoundFilterEditController = new CompoundFilterEditController(View);
			_compoundFilterEditController.ValueChanged += FilterController_ValueChanged;
			UpdateControls();
		}

		private void FilterController_ValueChanged(object sender, EventArgs e)
		{
			SelectedNodeText = ((FilterEditController)sender).Text;
			InvalidateFilter();
		}

		private SimpleFilterEditController _simpleFilterEditController;
		private CompoundFilterEditController _compoundFilterEditController;

		#endregion

		#region View

		private FilterEditor _view;
		private FilterEditor View
		{
			get
			{
				return _view;
			}
			set
			{
				if (View != null)
				{
					View.btnAddRootFilter.Click -= BtnAddRootFilter_Click;
					View.btnAddRootGroup.Click -= BtnAddRootGroup_Click;
					View.btnDelete.Click -= BtnDelete_Click;
					View.btnClearAll.Click -= BtnClear_Click;
					View.btnMoveUp.Click -= BtnMoveUp_Click;
					View.btnMoveDown.Click -= BtnMoveDown_Click;
					View.btnOpen.Click -= BtnOpen_Click;
					View.btnSave.Click -= BtnSave_Click;
					View.btnSaveAs.Click -= BtnSaveAs_Click;
					View.popupAddChildFilter.Click -= PopupAddFilter_Click;
					View.popupAddChildGroup.Click -= PopupAddGroup_Click;
					View.popupDelete.Click -= BtnDelete_Click;
					TreeView.AfterSelect -= TreeView_AfterSelect;
					TreeView.DragDrop -= TreeView_DragDrop;
					TreeView.DragOver -= TreeView_DragOver;
					TreeView.ItemDrag -= TreeView_ItemDrag;
					TreeView.MouseDown -= TreeView_MouseDown;
				}
				_view = value;
				if (View != null)
				{
					View.btnAddRootFilter.Click += BtnAddRootFilter_Click;
					View.btnAddRootGroup.Click += BtnAddRootGroup_Click;
					View.btnDelete.Click += BtnDelete_Click;
					View.btnClearAll.Click += BtnClear_Click;
					View.btnMoveUp.Click += BtnMoveUp_Click;
					View.btnMoveDown.Click += BtnMoveDown_Click;
					View.btnOpen.Click += BtnOpen_Click;
					View.btnSave.Click += BtnSave_Click;
					View.btnSaveAs.Click += BtnSaveAs_Click;
					View.popupAddChildFilter.Click += PopupAddFilter_Click;
					View.popupAddChildGroup.Click += PopupAddGroup_Click;
					View.popupInsertSiblingFilter.Click += PopupInsertFilter_Click;
					View.popupInsertSiblingGroup.Click += PopupInsertGroup_Click;
					View.popupDelete.Click += BtnDelete_Click;
					TreeView.AfterSelect += TreeView_AfterSelect;
					TreeView.DragDrop += TreeView_DragDrop;
					TreeView.DragOver += TreeView_DragOver;
					TreeView.ItemDrag += TreeView_ItemDrag;
					TreeView.MouseDown += TreeView_MouseDown;
				}
			}
		}

		private TreeView TreeView { get { return View.TreeView; } }

		private TreeNodeCollection TreeViewNodes { get { return TreeView.Nodes; } }

		private TreeNode SelectedNode
		{
			get
			{
				return TreeView.SelectedNode;
			}
			set
			{
				TreeView.SelectedNode = value;
			}
		}

		private string SelectedNodeText
		{
			get
			{
				var selectedNode = SelectedNode;
				return selectedNode != null ? selectedNode.Text : string.Empty;
			}
			set
			{
				SelectedNode.Text = value;
			}
		}

		private string
			_defaultFilterText = string.Concat(SimpleCondition.SortableColumnNames[0], " contains"),
			_defaultGroupText = CompoundCondition.Quantifiers[0];

		#endregion

		#region Commands

		private void BtnAddRootFilter_Click(object sender, EventArgs e)
		{
			AddFilter(TreeViewNodes);
		}

		private void PopupAddFilter_Click(object sender, EventArgs e)
		{
			AddFilter(SelectedNode.Nodes);
		}

		private void PopupInsertFilter_Click(object sender, EventArgs e)
		{
			InsertFilter(SelectedNode);
		}

		private void BtnAddRootGroup_Click(object sender, EventArgs e)
		{
			AddGroup(TreeViewNodes);
		}

		private void PopupAddGroup_Click(object sender, EventArgs e)
		{
			AddGroup(SelectedNode.Nodes);
		}

		private void PopupInsertGroup_Click(object sender, EventArgs e)
		{
			InsertGroup(SelectedNode);
		}

		private void BtnSaveAs_Click(object sender, EventArgs e)
		{
			SaveAs();
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void BtnOpen_Click(object sender, EventArgs e)
		{
			Open();
		}

		private void BtnMoveDown_Click(object sender, EventArgs e)
		{
		}

		private void BtnMoveUp_Click(object sender, EventArgs e)
		{
		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			Clear();
		}

		private void BtnDelete_Click(object sender, EventArgs e)
		{
			TreeViewNodes.Remove(SelectedNode);
			UpdateControls();
		}

		#endregion

		#region Control Events

		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			UpdateControls();
		}

		private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			TreeView.DoDragDrop(SelectedNode, DragDropEffects.Move);
		}

		private void TreeView_DragOver(object sender, DragEventArgs e)
		{
			var source = SelectedNode;
			var target = GetTarget(e);
			e.Effect = source != null && target != null && !IsAncestorOf(source, target)
				? DragDropEffects.Move
				: DragDropEffects.None;
		}

		private TreeNode GetTarget(DragEventArgs e)
		{
			return TreeView.GetNodeAt(TreeView.PointToClient(new Point(e.X, e.Y)));
		}

		private void TreeView_DragDrop(object sender, DragEventArgs e)
		{
			var source = SelectedNode;
			var target = GetTarget(e);
			var targetIndex = target.Index;
			if (source.Parent == target.Parent && source.Index < target.Index)
				targetIndex--;
			GetSiblings(source).Remove(source);
			GetSiblings(target).Insert(targetIndex, source);
			UpdateControls();
		}

		private void TreeView_MouseDown(object sender, MouseEventArgs e)
		{
			SelectedNode = TreeView.GetNodeAt(e.Location);
		}

		#endregion

		#region Methods

		private void AddFilter(TreeNodeCollection nodes)
		{
			AddNode(nodes, _defaultFilterText);
		}

		private void AddGroup(TreeNodeCollection nodes)
		{
			AddNode(nodes, _defaultGroupText);
		}

		private void AddNode(TreeNodeCollection nodes, string text)
		{
			SelectedNode = nodes.Add(text);
		}

		private TreeNodeCollection GetSiblings(TreeNode node)
		{
			var parent = node.Parent;
			return parent != null ? parent.Nodes : TreeViewNodes;
		}

		private void InsertFilter(TreeNode sibling)
		{
			InsertNode(sibling, _defaultFilterText);
		}

		private void InsertGroup(TreeNode sibling)
		{
			InsertNode(sibling, _defaultGroupText);
		}

		private void InsertNode(TreeNode node, string text)
		{
			SelectedNode = GetSiblings(node).Insert(node.Index, text);
		}

		private bool IsAncestorOf(TreeNode ancestor, TreeNode descendant)
		{
			for (var node = descendant; node != null; node = node.Parent)
				if (ancestor == node)
					return true;
			return false;
		}

		private bool IsGroupSelected()
		{
			return CompoundCondition.Quantifiers.Contains(SelectedNodeText);
		}

		private void UpdateControls()
		{
			UpdateEditControllers();
			InvalidateFilter();
			var canDelete = SelectedNode != null;
			var canAddChild = IsGroupSelected();
			var canInsertSibling = canDelete;
			View.popupAddChildFilter.Visible = View.popupAddChildGroup.Visible = canAddChild;
			View.popupInsertSiblingFilter.Visible = View.popupInsertSiblingGroup.Visible = canInsertSibling;
			View.btnDelete.Enabled = View.popupDelete.Visible = canDelete;
			View.btnClearAll.Enabled = TreeViewNodes.Count > 0;
		}

		private void UpdateEditControllers()
		{
			var text = SelectedNodeText;
			var compound = IsGroupSelected();
			var simple = !string.IsNullOrWhiteSpace(text) && !compound;
			_simpleFilterEditController.Visible = simple;
			_compoundFilterEditController.Visible = compound;
			if (simple)
				_simpleFilterEditController.Text = text;
			else if (compound)
				_compoundFilterEditController.Text = text;
		}

		#endregion

		#region Predicate

		public Predicate<object> Predicate
		{
			get
			{
				return Test;
			}
		}

		private bool Test(object track)
		{
			return Function((ITrack)track);
		}

		private Func<ITrack, bool> _function;
		private Func<ITrack, bool> Function
		{
			get
			{
				return _function ?? (_function = GetFunction());
			}
		}

		private Func<ITrack, bool> GetFunction()
		{
			var expression = GetCompoundExpression("All of these are true:", TreeViewNodes);
			var lambda = Expression.Lambda<Func<ITrack, bool>>(expression, _parameter);
			var function = lambda.Compile();
			return function;
		}

		private Expression GetCompoundExpression(string quantifier, TreeNodeCollection nodes)
		{
			if (nodes == null || nodes.Count < 1)
				return Expression.Constant(true);
			Expression result = null;
			var first = true;
			foreach (var subCondition in nodes.Cast<TreeNode>().Select(GetExpression))
			{
				result = first ? subCondition : Expression.MakeBinary(ExpressionType.AndAlso, result, subCondition);
				first = false;
			}
			return result;
		}

		private Expression GetExpression(TreeNode node)
		{
			var text = node.Text;
			return CompoundCondition.Quantifiers.Contains(text)
				? GetCompoundExpression(text, node.Nodes)
				: new SimpleCondition(text).ToExpression(_parameter);
		}

		private static ParameterExpression _parameter = Expression.Parameter(typeof(ITrack), "track");

		private void InvalidateFilter()
		{
			_function = null;
		}

		protected override bool LoadFromStream(Stream stream)
		{
			return true;
		}

		protected override bool SaveToStream(Stream stream)
		{
			return true;
		}

		protected override void ClearDocument()
		{
			View.TreeView.Nodes.Clear();
        }

		#endregion
	}
}
