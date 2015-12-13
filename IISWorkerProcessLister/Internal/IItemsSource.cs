using System.ComponentModel;

namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    /// </summary>
    public interface IItemsSource
    {
        /// <summary>
        /// </summary>
        BindingList<IWorkerProcessItem> Value { get; }
    }
}