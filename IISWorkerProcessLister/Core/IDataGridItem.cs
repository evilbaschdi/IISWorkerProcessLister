using System.Windows.Controls;

namespace IISWorkerProcessLister.Core
{
    /// <summary>
    /// </summary>
    public interface IDataGridItem
    {
        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        DataGrid Item(object sender);
    }
}