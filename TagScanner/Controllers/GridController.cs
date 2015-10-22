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
				if (View != null)
					DisconnectView();
				_view = value;
				if (View != null)
				{
					ReconnectView();
					RefreshDataSource();
				}
			}
		}

		private DataGrid Grid { get { return ((GridElement)View.Child).DataGrid; } }

		private void DisconnectView()
		{
			Grid.SelectionChanged -= Grid_SelectionChanged;
		}

		private void ReconnectView()
		{
			if (View.Child == null)
			{
				View.Child = new GridElement();
				Grid.Columns.Clear();
				foreach (var column in GetColumns())
					Grid.Columns.Add(column);
				Grid.GridLinesVisibility = DataGridGridLinesVisibility.Vertical;
            }
			Grid.SelectionChanged += Grid_SelectionChanged;
		}

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

		private IEnumerable<string> _visibleColumnNames = new[] { "FilePath" };
		public IEnumerable<string> VisibleColumnNames
		{
			get
			{
				return _visibleColumnNames;
			}
			set
			{
				if (VisibleColumnNames.SequenceEqual(value))
					return;
				_visibleColumnNames = value;
				InitVisibleColumns();
			}
		}

		private void InitVisibleColumns()
		{
			foreach (var column in Grid.Columns)
				column.Visibility = Visibility.Collapsed;
			var displayIndex = 0;
			foreach (var columnName in VisibleColumnNames)
			{
				var column = Grid.Columns.Single(c => (string)c.Header == columnName);
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

		private static DataGridBoundColumn GetColumn(string propertyTypeName)
		{
			switch (propertyTypeName)
			{
				case "String":
					return GetTextBoxColumn(StringAlignment.Near);
				case "Int32":
				case "Int64":
				case "TimeSpan":
					return GetTextBoxColumn(StringAlignment.Far);
				case "Logical":
					return GetCheckBoxColumn();
			}
			return null;
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

		private static IEnumerable<DataGridBoundColumn> GetColumns()
		{
			return SimpleCondition.SortablePropertyInfos.Select(GetColumn);
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
				case "Logical":
					return new LogicalConverter();
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

		private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			OnSelectionChanged();
		}

		private Selection GetSelection()
		{
            return new Selection(Grid.SelectedItems.Cast<ITrack>());
		}

		#endregion

		#region Presets

		public void ViewByAlbumTitle()
		{
			VisibleColumnNames = new[]
			{
				"DiscTrack",
				"Title",
				"Duration",
				"FileSize"
			};
			Groups = new[]
			{
				"Album",
				"JoinedPerformers"
			};
		}

		public void ViewByArtist()
		{
			VisibleColumnNames = new[]
			{
				"DiscTrack",
				"Title",
				"Duration",
				"FileSize"
			};
			Groups = new[]
			{
				"JoinedPerformers",
				"YearAlbum"
			};
		}

		public void ViewByGenre()
		{
			VisibleColumnNames = new[]
			{
				"DiscTrack",
				"Title",
				"Duration",
				"FileSize"
			};
			Groups = new[]
			{
				"JoinedGenres",
				"JoinedPerformers",
				"YearAlbum"
			};
		}

		public void ViewBySongTitle()
		{
			VisibleColumnNames = new[]
			{
				"DiscTrack",
				"Title",
				"Duration",
				"FileSize",
				"JoinedPerformers",
				"Album"
			};
			Groups = new string[0];
		}

		public void ViewByYear()
		{
			VisibleColumnNames = new[]
			{
				"DiscTrack",
				"Title",
				"Duration",
				"FileSize",
				"JoinedPerformers",
				"Album"
			};
			Groups = new[]
			{
				"Decade",
				"Year"
			};
		}

		#endregion
	}
}
