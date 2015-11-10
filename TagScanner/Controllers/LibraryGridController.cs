using System;
using System.Collections.Generic;
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
		#region Lifetime Management

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
					InitColumns();
				}
				DataGrid.SelectionChanged += Grid_SelectionChanged;
				RefreshDataSource();
			}
		}

		protected override DataGrid DataGrid { get { return ((GridElement)View.Child).DataGrid; } }

		private void RefreshDataSource()
		{
			if (View.InvokeRequired)
				View.Invoke(new Action(RefreshDataSource));
			else
			{
				DataGrid.ItemsSource = new ListCollectionView(Model.Tracks);
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
			return Metadata.PropertyInfos;
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
			var listCollectionView = (ListCollectionView)DataGrid.ItemsSource;
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
			var listCollectionView = (ListCollectionView)DataGrid.ItemsSource;
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
			DataGrid.SelectAll();
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
            return new Selection(DataGrid.SelectedItems.Cast<Track>());
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
