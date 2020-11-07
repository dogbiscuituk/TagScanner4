namespace TagScanner.Controllers
{
	using System;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Windows.Forms;
	using TagScanner.Models;
	using TagScanner.Views;

	public class FilterDialogController : SdiController
	{
		#region Lifetime Management

		public FilterDialogController(LibraryGridController libraryGridController, ToolStripDropDownItem recentMenu)
			: base(libraryGridController.Model, Properties.Settings.Default.SearchFilter, "FilterMRU", recentMenu)
		{
			_libraryGridController = libraryGridController;
			_simpleFilterEditController = new SimpleFilterController(View);
			_simpleFilterEditController.ValueChanged += FilterController_ValueChanged;
			_compoundFilterEditController = new CompoundFilterController(View);
			_compoundFilterEditController.ValueChanged += FilterController_ValueChanged;
			UpdateControls();
		}

		private void FilterController_ValueChanged(object sender, EventArgs e)
		{
			SelectedNodeText = ((FilterController)sender).Text;
			InvalidateFilter();
		}

		private readonly LibraryGridController _libraryGridController;
		private readonly SimpleFilterController _simpleFilterEditController;
		private readonly CompoundFilterController _compoundFilterEditController;

		#endregion

		public DialogResult ShowDialog(IWin32Window owner)
		{
			var result = View.ShowDialog(owner);
			if (result == DialogResult.OK)
				Apply();
			return result;
		}

		private void Apply()
		{
			_libraryGridController.Filter = Predicate;
		}

		#region View

		private FilterDialog _view;
		private FilterDialog View
		{
			get
			{
				if (_view == null)
					CreateFilterDialog();
				return _view;
			}
		}

		private void CreateFilterDialog()
		{
			_view = new FilterDialog();
			View.btnAddRootFilter.Click += BtnAddRootFilter_Click;
			View.btnAddRootGroup.Click += BtnAddRootGroup_Click;
			View.btnClearAll.Click += BtnClearAll_Click;
			View.btnDelete.Click += BtnDelete_Click;
			View.btnMoveUp.Click += BtnMoveUp_Click;
			View.btnMoveDown.Click += BtnMoveDown_Click;
			View.btnOpen.Click += BtnOpen_Click;
			View.btnSave.Click += BtnSave_Click;
			View.btnSaveAs.Click += BtnSaveAs_Click;
			View.popupAddChildFilter.Click += PopupAddChildFilter_Click;
			View.popupAddChildGroup.Click += PopupAddChildGroup_Click;
			View.popupDelete.Click += BtnDelete_Click;
			View.popupInsertSiblingFilter.Click += PopupInsertSiblingFilter_Click;
			View.popupInsertSiblingGroup.Click += PopupInsertSiblingGroup_Click;
			TreeView.AfterSelect += TreeView_AfterSelect;
			TreeView.DragDrop += TreeView_DragDrop;
			TreeView.DragOver += TreeView_DragOver;
			TreeView.ItemDrag += TreeView_ItemDrag;
			TreeView.MouseDown += TreeView_MouseDown;
		}

		private TreeView TreeView => View.TreeView;

		private TreeNodeCollection TreeViewNodes => TreeView.Nodes;

		private TreeNode SelectedNode
		{
			get => TreeView.SelectedNode;
			set => TreeView.SelectedNode = value;
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
				var selectedNode = SelectedNode;
				if (selectedNode != null)
					selectedNode.Text = value;
			}
		}

		private readonly string
			_defaultFilterText = string.Concat(Metadata.SortableTags[0], " contains");

		private readonly string
			_defaultGroupText = Metadata.QuantifierStrings[0];

		#endregion

		#region Commands

		private void BtnAddRootFilter_Click(object sender, EventArgs e)
		{
			AddFilter(TreeViewNodes);
		}

		private void BtnAddRootGroup_Click(object sender, EventArgs e)
		{
			AddGroup(TreeViewNodes);
		}

		private void BtnClearAll_Click(object sender, EventArgs e)
		{
			Clear();
		}

		private void BtnDelete_Click(object sender, EventArgs e)
		{
			TreeViewNodes.Remove(SelectedNode);
			UpdateControls();
		}

		private void BtnMoveDown_Click(object sender, EventArgs e)
		{
		}

		private void BtnMoveUp_Click(object sender, EventArgs e)
		{
		}

		private void BtnOpen_Click(object sender, EventArgs e)
		{
			Open();
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void BtnSaveAs_Click(object sender, EventArgs e)
		{
			SaveAs();
		}

		private void PopupAddChildFilter_Click(object sender, EventArgs e)
		{
			AddFilter(SelectedNode.Nodes);
		}

		private void PopupAddChildGroup_Click(object sender, EventArgs e)
		{
			AddGroup(SelectedNode.Nodes);
		}

		private void PopupInsertSiblingFilter_Click(object sender, EventArgs e)
		{
			InsertFilter(SelectedNode);
		}

		private void PopupInsertSiblingGroup_Click(object sender, EventArgs e)
		{
			InsertGroup(SelectedNode);
		}
		#endregion

		#region Control Events

		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			UpdateControls();
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

		private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			TreeView.DoDragDrop(SelectedNode, DragDropEffects.Move);
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
			return IsQuantifier(SelectedNodeText);
		}

		private static bool IsQuantifier(string text)
		{
			return Metadata.QuantifierStrings.Contains(text);
        }

		private void UpdateControls()
		{
			UpdateEditControllers();
			InvalidateFilter();
			var canAddChild = IsGroupSelected();
			var canDelete = SelectedNode != null;
			var canInsertSibling = canDelete;
			View.btnClearAll.Enabled = TreeViewNodes.Count > 0;
			View.btnDelete.Enabled = View.popupDelete.Visible = canDelete;
			View.popupAddChildFilter.Visible = View.popupAddChildGroup.Visible = canAddChild;
			View.popupInsertSiblingFilter.Visible = View.popupInsertSiblingGroup.Visible = canInsertSibling;
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

		public Predicate<object> Predicate => Test;

		private bool Test(object track)
		{
			return Function((ITrack)track);
		}

		private Func<ITrack, bool> _function;
		private Func<ITrack, bool> Function => _function ?? (_function = GetFunction());

		private Func<ITrack, bool> GetFunction()
		{
			var expression = GetCompoundExpression(Metadata.QuantifierStrings[0], TreeViewNodes);
			var lambda = Expression.Lambda<Func<ITrack, bool>>(expression, _parameter);
			var function = lambda.Compile();
			return function;
		}

		private Expression GetCompoundExpression(string quantifierString, TreeNodeCollection nodes)
		{
			if (nodes == null || nodes.Count < 1)
				return Expression.Constant(true);
			var quantifier = Metadata.GetQuantifier(quantifierString);
			var binaryType =
				(quantifier & Metadata.Quantifiers.And) != 0
				? ExpressionType.AndAlso
				: ExpressionType.OrElse;
            Expression expression = null;
			var first = true;
			foreach (var subCondition in nodes.Cast<TreeNode>().Select(GetExpression))
			{
				expression = first ? subCondition : Expression.MakeBinary(binaryType, expression, subCondition);
				first = false;
			}
			if ((quantifier & Metadata.Quantifiers.Not) != 0)
				expression = Expression.IsFalse(expression);
			return expression;
		}

		private Expression GetExpression(TreeNode node)
		{
			var text = node.Text;
			return IsQuantifier(text)
				? GetCompoundExpression(text, node.Nodes)
				: new SimpleCondition(text).ToExpression(_parameter);
		}

		private static readonly ParameterExpression _parameter = Expression.Parameter(typeof(ITrack), "track");

		private void InvalidateFilter()
		{
			_function = null;
		}

		protected override bool LoadFromStream(Stream stream, string format)
		{
			return true;
		}

		protected override bool SaveToStream(Stream stream, string format)
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
