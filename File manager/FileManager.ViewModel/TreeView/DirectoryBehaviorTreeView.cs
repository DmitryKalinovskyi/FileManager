using File_manager.FileManager.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel.TreeView
{
    public abstract class DirectoryBehaviorTreeView: TreeItemViewModel, ILazyLoader, IDynamicTreeViewItem, ISelectableTreeItem
    {
        public string Path { get; set; }

        public DirectoryBehaviorTreeView(string path)
        {
            Path = path;
            Items = new() { new EmptyItemTreeView() };
        }

        public void UpdateItems()
        {
            Items.Clear();

            var items = Directory.GetDirectories(Path).Select(directory => new DirectoryItemTreeView(directory));

            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public void Select()
        {
            FileManagerViewModel.Instance.FileGrid.Path = Path;
        }

        public void Load()
        {
            Items.Clear();
            var items = Directory.GetDirectories(Path).Select(directory => new DirectoryItemTreeView(directory));

            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public void Unload()
        {
            Items.Clear();
            Items.Add(new EmptyItemTreeView());
        }

        public void Update()
        {
            if (IsExpanded == false)
                return;


            foreach(var item in Items)
            {
                if (item is DirectoryBehaviorTreeView treeViewItem)
                    treeViewItem.Update();
            }
        }
    }
}
