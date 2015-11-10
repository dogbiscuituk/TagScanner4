﻿using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using TagScanner.Models;
using TagScanner.ValueConverters;
using TagScanner.Views;

namespace TagScanner.Controllers
{
	public class ReplacePreviewController : GridController
	{
		public ReplacePreviewController(IWin32Window owner)
		{
			Owner = owner;
			View = new ReplacePreview();
			DataGrid.GridLinesVisibility = DataGridGridLinesVisibility.Vertical;
			DataGrid.SelectionMode = DataGridSelectionMode.Single;
			DataGrid.SelectionUnit = DataGridSelectionUnit.Cell;
			InitColumns();
		}

		public DialogResult Execute(List<FindReplaceResult> results)
		{
			View.Text = string.Format("Replace preview - {0} matches found", results.Count);
			DataGrid.ItemsSource = new ListCollectionView(results);
			return View.ShowDialog(Owner);
		}

		private IWin32Window Owner;
		private ReplacePreview View;

		protected override System.Windows.Controls.DataGrid DataGrid { get { return View.GridElement.DataGrid; } }

		private Brush GetBrush(string propertyName)
		{
			switch (propertyName)
			{
				case "OldValue":
					return Brushes.Red;
				case "NewValue":
					return Brushes.Green;
			}
			return Brushes.Black;
		}

		protected override DataGridBoundColumn GetColumn(PropertyInfo propertyInfo)
		{
			var propertyName = propertyInfo.Name;
            switch (propertyName)
			{
				case "OldValueSort":
				case "NewValueSort":
					return null;
			}
			var column = base.GetColumn(propertyInfo);
			column.SortMemberPath = GetSortMemberPath(propertyName);
			if (column is DataGridTextColumn)
				((DataGridTextColumn)column).Foreground = GetBrush(propertyInfo.Name);
            return column;
		}

		protected override IValueConverter GetConverter(PropertyInfo propertyInfo)
		{
			var result = base.GetConverter(propertyInfo);
			if (result == null)
				switch (propertyInfo.PropertyType.Name)
				{
					case "Object":
						return new StringsConverter();
				}
			return result;
		}

		protected override PropertyInfo[] GetPropertyInfos()
		{
			return typeof(FindReplaceResult).GetProperties();
		}

		private string GetSortMemberPath(string propertyName)
		{
			switch (propertyName)
			{
				case "OldValue":
				case "NewValue":
					return propertyName + "Path";
			}
			return propertyName;
		}
	}
}
