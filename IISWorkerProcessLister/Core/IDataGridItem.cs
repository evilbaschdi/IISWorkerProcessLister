using System.Windows.Controls;

namespace IISWorkerProcessLister.Core
{
    public interface IDataGridItem
    {
        DataGrid Item(object sender);
    }
}