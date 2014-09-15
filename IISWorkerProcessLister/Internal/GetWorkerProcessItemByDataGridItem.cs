using IISWorkerProcessLister.Core;
using System;

namespace IISWorkerProcessLister.Internal
{
    public class GetWorkerProcessItemByDataGridItem : IWorkerProcessDataGridItem
    {
        private readonly IDataGridItem _dataGridItem;
        private readonly object _sender;

        public GetWorkerProcessItemByDataGridItem(IDataGridItem dataGridItem, object sender)
        {
            if (dataGridItem == null)
            {
                throw new ArgumentNullException("dataGridItem");
            }
            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }
            _dataGridItem = dataGridItem;
            _sender = sender;
        }

        public IWorkerProcessItem Item
        {
            get
            {
                //Get the underlying item, that you cast to your object that is bound
                //to the DataGrid (and has subject and state as property)
                return (IWorkerProcessItem)_dataGridItem.Item(_sender).SelectedCells[0].Item;
            }
        }
    }
}