using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Devis.Tools
{
    public static class DataGridHelper
    {

        public static DataGridRow GetRowAtIndex(this DataGrid grid, int index)
        {
            var itemsSource = grid.ItemsSource.OfType<object>().ToArray();
            if (itemsSource.Length == 0  || index > (itemsSource.Length - 1) || index < 0) 
                return null;

            var item = itemsSource[index];
            var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
            return row;
        }

        public static DataGridCell GetCell(DataGridCellInfo dataGridCellInfo)
        {
            if (!dataGridCellInfo.IsValid)
            {
                return null;
            }

            FrameworkElement cellContent = dataGridCellInfo.Column.GetCellContent(dataGridCellInfo.Item);
            if (cellContent != null)
            {
                return (DataGridCell) cellContent.Parent;
            }
            return null;
        }

        public static int GetRowIndex(DataGridCell dataGridCell)
        {
            // Use reflection to get DataGridCell.RowDataItem property value.
            PropertyInfo rowDataItemProperty = dataGridCell.GetType()
                .GetProperty("RowDataItem", BindingFlags.Instance | BindingFlags.NonPublic);

            DataGrid dataGrid = GetDataGridFromChild(dataGridCell);

            return dataGrid.Items.IndexOf(rowDataItemProperty.GetValue(dataGridCell, null));
        }

        public static DataGrid GetDataGridFromChild(DependencyObject dataGridPart)
        {
            if (VisualTreeHelper.GetParent(dataGridPart) == null)
            {
                throw new NullReferenceException("Control is null.");
            }
            if (VisualTreeHelper.GetParent(dataGridPart) is DataGrid)
            {
                return (DataGrid) VisualTreeHelper.GetParent(dataGridPart);
            }
            return GetDataGridFromChild(VisualTreeHelper.GetParent(dataGridPart));
        }
    }
}