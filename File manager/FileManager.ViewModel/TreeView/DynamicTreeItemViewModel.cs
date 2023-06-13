using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel.TreeView
{
    public abstract class DynamicTreeItemViewModel: TreeItemViewModel, IDynamicTreeViewItem
    {
        public virtual void Update()
        {
            foreach (var item in Items)
            {
                if (item is IDynamicTreeViewItem dynamicItem)
                    dynamicItem.Update();
            }
        }
    }
}
