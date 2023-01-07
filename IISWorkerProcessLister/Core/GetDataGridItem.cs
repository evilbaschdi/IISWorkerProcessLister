using System.Windows.Controls;

namespace IISWorkerProcessLister.Core;

/// <summary>
/// </summary>
public class GetDataGridItem : IDataGridItem
{
    /// <summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public DataGrid ValueFor(object sender)
    {
        //Get the clicked MenuItem
        var menuItem = (MenuItem)sender;

        //Get the ContextMenu to which the menuItem belongs
        var contextMenu = (ContextMenu)menuItem.Parent;

        //Find the placementTarget
        var item = (DataGrid)contextMenu.PlacementTarget;
        return item;
    }
}