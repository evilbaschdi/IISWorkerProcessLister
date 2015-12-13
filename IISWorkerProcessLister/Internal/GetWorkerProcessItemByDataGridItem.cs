using System;
using IISWorkerProcessLister.Core;

namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    /// </summary>
    public class GetWorkerProcessItemByDataGridItem : IWorkerProcessDataGridItem
    {
        private readonly IDataGridItem _dataGridItem;
        private readonly object _sender;

        /// <summary>
        /// </summary>
        /// <param name="dataGridItem"></param>
        /// <param name="sender"></param>
        public GetWorkerProcessItemByDataGridItem(IDataGridItem dataGridItem, object sender)
        {
            if(dataGridItem == null)
            {
                throw new ArgumentNullException(nameof(dataGridItem));
            }
            if(sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }
            _dataGridItem = dataGridItem;
            _sender = sender;
        }

        /// <summary>
        /// </summary>
        //Get the underlying item, that you cast to your object that is bound
        //to the DataGrid (and has subject and state as property)
        public IWorkerProcessItem Item => (IWorkerProcessItem) _dataGridItem.Item(_sender).SelectedCells[0].Item;
    }
}