using System.ComponentModel;
using EvilBaschdi.Core;

namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    /// </summary>
    public interface IItemsSource : IValue<BindingList<IWorkerProcessItem>>
    {
    }
}