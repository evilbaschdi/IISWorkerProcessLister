using IISWorkerProcessLister.Core;

namespace IISWorkerProcessLister.Internal;

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
        _dataGridItem = dataGridItem ?? throw new ArgumentNullException(nameof(dataGridItem));
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    /// <summary>
    /// </summary>
    //Get the underlying item, that you cast to your object that is bound
    //to the DataGrid (and has subject and state as property)
    public IWorkerProcessItem Value => (IWorkerProcessItem)_dataGridItem.ValueFor(_sender).SelectedCells[0].Item;
}