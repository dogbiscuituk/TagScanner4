using System;
using System.Collections.Generic;
using System.Drawing;
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
	public class GridController
	{
		#region Lifetime Management

		public GridController(Model model, ElementHost view)
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
				if (Model != null)
				{
					Model.TracksChanged -= Model_TracksChanged;
				}
				_model = value;
				if (Model != null)
				{
					Model.TracksChanged += Model_TracksChanged;
				}
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
				if (View.Child == null)
				{
					View.Child = new GridElement();
					Grid.Columns.Clear();
					foreach (var column in GetColumns())
						Grid.Columns.Add(column);
					Grid.GridLinesVisibility = DataGridGridLinesVisibility.Vertical;
				}
				Grid.SelectionChanged += Grid_SelectionChanged;
				RefreshDataSource();
			}
		}

		private DataGrid Grid { get { return ((GridElement)View.Child).DataGrid; } }

		private void RefreshDataSource()
		{
			if (View.InvokeRequired)
				View.Invoke(new Action(RefreshDataSource));
			else
			{
				Grid.ItemsSource = new ListCollectionView(Model.Tracks);
				InitGroups();
			}
		}

		#endregion

		#region Columns

		public void EditTagVisibility()
		{
			var trackVisibleTags = VisibleTags.ToList();
			var ok = new TagSelectorController(Metadata.PropertyInfos).Execute(trackVisibleTags);
			if (ok)
				VisibleTags = VisibleTags.Intersect(trackVisibleTags).Union(trackVisibleTags);
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
			foreach (var column in Grid.Columns)
				column.Visibility = Visibility.Collapsed;
			var displayIndex = 0;
			foreach (var tag in VisibleTags)
			{
				var column = Grid.Columns.Single(c => (string)c.Header == tag);
				column.DisplayIndex = displayIndex++;
				column.Visibility = Visibility.Visible;
			}
		}

		private static DataGridBoundColumn GetCheckBoxColumn()
		{
			var column = new DataGridCheckBoxColumn();
			column.Width = 80;
			return column;
		}

		private static DataGridBoundColumn GetColumn(PropertyInfo propertyInfo)
		{
			var propertyTypeName = propertyInfo.PropertyType.Name;
			var column = GetColumn(propertyTypeName);
			if (column != null)
			{
				var propertyName = propertyInfo.Name;
				var binding = new Binding(propertyName)
				{
					Mode = propertyInfo.CanWrite ? BindingMode.TwoWay : BindingMode.OneWay,
					Converter = GetConverter(propertyInfo)
				};
				column.Binding = binding;
				column.CellStyle = GetColumnStyle(propertyInfo);
				column.Header = propertyName;
			}
			return column;
		}

		private static DataGridBoundColumn GetColumn(string propertyTypeName)
		{
			switch (propertyTypeName)
			{
				case "Int32":
				case "Int64":
				case "TimeSpan":
					return GetTextBoxColumn(StringAlignment.Far);
				case "Logical":
					return GetCheckBoxColumn();
			}
			return GetTextBoxColumn(StringAlignment.Near);
		}

		private static IEnumerable<DataGridBoundColumn> GetColumns()
		{
			return Metadata.PropertyInfos.Select(GetColumn);
		}

		private static Style GetColumnStyle(PropertyInfo propertyInfo)
		{
			switch (propertyInfo.PropertyType.Name)
			{
				case "Int32":
				case "Int64":
				case "TimeSpan":
					return RightAlignStyle;
			}
			switch (propertyInfo.Name)
			{
				case "DiscOf":
				case "DiscTrack":
				case "TrackOf":
					return RightAlignStyle;
			}
			return null;
		}

		private static IValueConverter GetConverter(PropertyInfo propertyInfo)
		{
			switch (propertyInfo.PropertyType.Name)
			{
				case "DateTime":
					return new DateTimeConverter();
				case "Logical":
					return new LogicalConverter();
				case "String[]":
					return new StringsConverter();
				case "TimeSpan":
					return new TimeSpanConverter();
			}
			switch (propertyInfo.Name)
			{
				case "FileSize":
					return new FileSizeConverter();
			}
			return null;
		}

		private static DataGridBoundColumn GetTextBoxColumn(StringAlignment alignment)
		{
			var column = new DataGridTextColumn();
			column.Width = alignment == StringAlignment.Near ? 160 : 80;
			return column;
		}

		private static Style _rightAlignStyle;
		private static Style RightAlignStyle
		{
			get
			{
				if (_rightAlignStyle == null)
				{
					_rightAlignStyle = new Style(typeof(DataGridCell));
					_rightAlignStyle.Setters.Add(new Setter
					{
						Property = FrameworkElement.HorizontalAlignmentProperty,
						Value = HorizontalAlignment.Right
					});
				}
				return _rightAlignStyle;
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
			var listCollectionView = (ListCollectionView)Grid.ItemsSource;
			listCollectionView.Filter = Filter;
        }

		#endregion

		#region Groups

		private IEnumerable<string> _groups = new string[0];
		public IEnumerable<string> Groups
		{
			get
			{
				return _groups;
			}
			set
			{
				if (Groups.SequenceEqual(value))
					return;
				_groups = value;
				InitGroups();
			}
		}

		private void InitGroups()
		{
			var listCollectionView = (ListCollectionView)Grid.ItemsSource;
            var groupDescriptions = listCollectionView.GroupDescriptions;
			groupDescriptions.Clear();
			foreach (var group in Groups)
                groupDescriptions.Add(new PropertyGroupDescription(group));
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
			Grid.SelectAll();
		}

		public void InvertSelection()
		{
        }

		public void Find()
		{

		}

		public void Replace()
		{

		}

		private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			OnSelectionChanged();
		}

		private Selection GetSelection()
		{
            return new Selection(Grid.SelectedItems.Cast<Track>());
		}

		#endregion

		#region Presets

		private static string[] VisibleTagsDefault = new[] { "DiscTrack", "Title", "Duration", "FileSize"};
		private static IEnumerable<string> VisibleTagsExtended = VisibleTagsDefault.Union(new[] { "JoinedPerformers", "Album" });

		private void ViewBy(IEnumerable<string> visibleTags, IEnumerable<string> groups)
		{
			VisibleTags = visibleTags.Union(VisibleTags);
			Groups = groups;
		}

		public void ViewByAlbumTitle()
		{
			ViewBy(VisibleTagsDefault, new[] { "Album", "JoinedPerformers" });
		}

		public void ViewByArtist()
		{
			ViewBy(VisibleTagsDefault, new[] { "JoinedPerformers", "YearAlbum" });
		}

		public void ViewByGenre()
		{
			ViewBy(VisibleTagsDefault, new[] { "JoinedGenres", "JoinedPerformers", "YearAlbum" });
		}

		public void ViewBySongTitle()
		{
			ViewBy(VisibleTagsExtended, new string[0]);
		}

		public void ViewByYear()
		{
			ViewBy(VisibleTagsExtended, new[] { "Decade", "Year" });
		}

		#endregion
	}
}
