using System.ComponentModel;

namespace IISWorkerProcessLister.Internal
{
    public interface IItemsSource
    {
        BindingList<IWorkerProcessItem> Value { get; }
    }
}