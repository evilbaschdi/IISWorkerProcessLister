using System.Windows.Controls;

namespace IISWorkerProcessLister.Core
{
    public class GetDataGridItem : IDataGridItem
    {
        public DataGrid Item(object sender)
        {
            //Get the clicked MenuItem
            var menuItem = (MenuItem) sender;

            //Get the ContextMenu to which the menuItem belongs
            var contextMenu = (ContextMenu) menuItem.Parent;

            //Find the placementTarget
            var item = (DataGrid) contextMenu.PlacementTarget;
            return item;
        }
    }
}