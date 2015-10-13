using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Devis.Tools;
using Devis.ViewModels;

namespace Devis.Controls
{
    /// <summary>
    /// Selection context
    /// </summary>
    internal class QuoteDetailSelectionContext
    {
        private readonly DataGrid _dataGrid;
        public IEnumerable<DataGridCell> SelectedCells { get; private set; }
        public DataGridRow CurrentRow { get; private set; }
        public LineViewModel Model { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="dataGrid"></param>
        public QuoteDetailSelectionContext(IEnumerable<DataGridCell> cells, DataGrid dataGrid)
        {
            _dataGrid = dataGrid;
            SelectedCells = cells;
            if (SelectedCells != null)
            {
                var firstCell = SelectedCells.FirstOrDefault();
                if (firstCell != null)
                {
                    var rowIndex = DataGridHelper.GetRowIndex(firstCell);
                    CurrentRow = _dataGrid.GetRowAtIndex(rowIndex);
                    Model = CurrentRow.DataContext as LineViewModel;
                }
            }
        }

        /// <summary>
        /// Returns true if the line is selected
        /// </summary>
        public bool IsRowSelected
        {
            get
            {
                if (SelectedCells == null || _dataGrid == null || _dataGrid.Columns == null)
                    return false;

                return _dataGrid.Columns.Count() == SelectedCells.Count();
            }
        }

        
        

        /// <summary>
        /// Returns true if a cell only is selected
        /// </summary>
        public bool IsCellSelected
        {
            get
            {
                if (SelectedCells == null || _dataGrid == null || _dataGrid.Columns == null)
                    return false;

                return SelectedCells.Any();
            }
        }
    }
}