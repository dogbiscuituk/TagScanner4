using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms.Integration;
using TagScanner.Models;
using TagScanner.ValueConverters;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class LibraryGridController : GridController
	{
		#region Constructor

		public LibraryGridController(Model model, ElementHost view)
		{
			Model = model;
			View = view;
		}

		#endregion

		#region Model

		private Model _model;
		public Model Model
		{
			get
			{
				return _model;
			}
			set
			{
				_model = value;
				Model.TracksChanged += Model_TracksChanged;
			}
		}

		private void Model_TracksChanged(object sender, EventArgs e)
		{
			RefreshDataSource();
		}

		#endregion

		#region View

		private ElementHost _view;
		private ElementHost View
		{
			get
			{
				return _view;
			}
			set
			{
				_view = value;
				View.Child = new GridElement();
				InitColumns();
				DataGrid.SelectionChanged += Grid_SelectionChanged;
				RefreshDataSource();
			}
		}

		protected override DataGrid DataGrid { get { return ((GridElement)View.Child).DataGrid; } }

		private ListCollectionView ListCollectionView
		{
			get { return (ListCollectionView)DataGrid.ItemsSource; }
			set { DataGrid.ItemsSource = value; }
		}

		private void RefreshDataSource()
		{
			if (View.InvokeRequired)
				View.Invoke(new Action(RefreshDataSource));
			else
			{
				ListCollectionView = new ListCollectionView(Model.Tracks);
				InitGroups();
			}
		}

		#endregion

		#region Columns

		public void EditTagVisibility()
		{
			var trackVisibleTags = VisibleTags.ToList();
			var ok = new TagSelectorController(Metadata.TrackPropertyInfos).Execute(trackVisibleTags);
			if (ok)
				VisibleTags = VisibleTags.Intersect(trackVisibleTags).Union(trackVisibleTags);
		}

		protected override IValueConverter GetConverter(PropertyInfo propertyInfo)
		{
			var result = base.GetConverter(propertyInfo);
			if (result == null)
				switch (propertyInfo.Name)
				{
					case "FileSize":
						return new FileSizeConverter();
				}
			return result;
		}

		protected override PropertyInfo[] GetPropertyInfos()
		{
			return Metadata.TrackPropertyInfos;
		}

		private IEnumerable<string> _visibleTags = new[] { "FilePath" };
		public IEnumerable<string> VisibleTags
		{
			get
			{
				return _visibleTags;
			}
			set
			{
				if (VisibleTags.SequenceEqual(value))
					return;
				_visibleTags = value;
				InitVisibleTags();
			}
		}

		private void InitVisibleTags()
		{
			foreach (var column in DataGrid.Columns)
				column.Visibility = Visibility.Collapsed;
			var displayIndex = 0;
			foreach (var tag in VisibleTags)
			{
				var column = DataGrid.Columns.Single(c => (string)c.Header == tag);
				column.DisplayIndex = displayIndex++;
				column.Visibility = Visibility.Visible;
			}
		}

		#endregion

		#region Filter

		private Predicate<object> _filter = p => true;
		public Predicate<object> Filter
		{
			get
			{
				return _filter;
			}
			set
			{
				_filter = value;
				InitFilter();
			}
		}

		private void InitFilter()
		{
			ListCollectionView.Filter = Filter;
		}

		#endregion

		#region Sorting and Grouping

		private IEnumerable<SortDescription> _sortDescriptions = new SortDescription[0];
		public IEnumerable<SortDescription> SortDescriptions
		{
			get
			{
				return _sortDescriptions;
			}
			set
			{
				if (SortDescriptions.SequenceEqual(value))
					return;
				_sortDescriptions = value;
				InitGroups();
			}
		}

		private IEnumerable<string> _groupDescriptions = new string[0];
		public IEnumerable<string> GroupDescriptions
		{
			get
			{
				return _groupDescriptions;
			}
			set
			{
				if (GroupDescriptions.SequenceEqual(value))
					return;
				_groupDescriptions = value;
				InitGroups();
			}
		}

		private void InitGroups()
		{
			//var sortDescriptions = ListCollectionView.SortDescriptions;
			//sortDescriptions.Clear();
			//foreach (var sortDescription in SortDescriptions)
			//	sortDescriptions.Add(sortDescription);
			var groupDescriptions = ListCollectionView.GroupDescriptions;
			groupDescriptions.Clear();
			foreach (var groupDescription in GroupDescriptions)
				groupDescriptions.Add(new PropertyGroupDescription(groupDescription));
		}

		#endregion

		#region Selection

		private Selection _selection;
		public Selection Selection
		{
			get { return _selection ?? (_selection = GetSelection()); }
		}

		public event EventHandler SelectionChanged;

		private int UpdatingSelectionCount { get; set; }

		private void InvalidateSelection()
		{
			_selection = null;
		}

		private void BeginUpdateSelection()
		{
			UpdatingSelectionCount++;
		}

		private void EndUpdateSelection()
		{
			UpdatingSelectionCount--;
			OnSelectionChanged();
		}

		protected virtual void OnSelectionChanged()
		{
			if (UpdatingSelectionCount == 0)
			{
				InvalidateSelection();
				var selectionChanged = SelectionChanged;
				if (selectionChanged != null)
					selectionChanged(this, EventArgs.Empty);
			}
		}

		public void SelectAll()
		{
			BeginUpdateSelection();
			DataGrid.SelectAll();
			EndUpdateSelection();
		}

		public void InvertSelection()
		{
			var allItems = DataGrid.Items;
			var selectedItems = DataGrid.SelectedItems;
			var total = allItems.Count;
			var selection = selectedItems.Cast<object>().ToList();
			var oldCount = selection.Count;
			var newCount = total - oldCount;
			BeginUpdateSelection();
			if (newCount < oldCount)
			{
				selection = allItems.Cast<object>().Except(selection).ToList();
				selectedItems.Clear();
				foreach (var item in selection)
					selectedItems.Add(item);
			}
			else
			{
				DataGrid.SelectAll();
				foreach (var item in selection)
					selectedItems.Remove(item);
			}
			EndUpdateSelection();
		}

		private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			OnSelectionChanged();
		}

		private Selection GetSelection()
		{
			return new Selection(DataGrid.SelectedItems.Cast<Track>());
		}

		#endregion

		#region Presets

		private static IEnumerable<string>
			VisibleTagsDefault = new[] { "DiscTrack", "Title", "Duration", "FileSize" },
			VisibleTagsExtended = VisibleTagsDefault.Union(new[] { "JoinedPerformers", "Album" });

		public void ViewByAlbum()
		{
			SetQuery(VisibleTagsDefault, new[] { "Album" }, new[] { "Disc", "Track" });
		}

		public void ViewByArtist()
		{
			SetQuery(new[] { "YearAlbum", "DiscTrack", "Title", "Duration", "FileSize" }, new[] { "JoinedPerformers" }, new[] { "Disc", "Track" });
		}

		public void ViewByArtistAlbum()
		{
			SetQuery(VisibleTagsDefault, new[] { "JoinedPerformers", "YearAlbum" }, new[] { "Disc", "Track" });
		}

		public void ViewByGenre()
		{
			SetQuery(VisibleTagsDefault, new[] { "JoinedGenres", "JoinedPerformers", "YearAlbum" }, new[] { "Disc", "Track" });
		}

		public void ViewByNone()
		{
			SetQuery(VisibleTagsExtended, new string[0], new[] { "Disc", "Track" });
		}

		public void ViewByYear()
		{
			SetQuery(VisibleTagsExtended, new[] { "Decade", "Year" }, new[] { "Disc", "Track" });
		}

		private void SetQuery(
			IEnumerable<string> visibleTags,
			IEnumerable<string> groupDescriptions,
			IEnumerable<string> sortDescriptions)
		{
			VisibleTags = visibleTags.Union(VisibleTags);
			_sortDescriptions = sortDescriptions.Select(s => new SortDescription(s, ListSortDirection.Ascending));
			_groupDescriptions = groupDescriptions;
			InitGroups();
        }

		#endregion
	}
}
