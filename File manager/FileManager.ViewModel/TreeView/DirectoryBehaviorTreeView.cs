using File_manager.FileManager.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace File_manager.FileManager.ViewModel.TreeView
{
    public abstract class DirectoryBehaviorTreeView: DynamicTreeItemViewModel, ILazyLoader, ISelectableTreeItem
    {
        public string Path { get; set; }

        public DirectoryBehaviorTreeView(string path)
        {
            Path = path;
            Items = new() { new EmptyItemTreeView() };
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

        public override void Update()
        {
            if (IsExpanded == false)
                return;

            // Get all folder name inside [Path]
            HashSet<string> newItemsNames = new();
            var directories = Directory.GetDirectories(Path);
            foreach (var d in directories)
            {
                string name = System.IO.Path.GetFileName(d);

                newItemsNames.Add(name);
            }

            // Find all items that disappeared
            List<TreeItemViewModel> oldItems = new();

            foreach (var item in Items)
            {
                if (newItemsNames.Contains(item.Name))
                {
                    newItemsNames.Remove(item.Name);
                }
                else
                {
                    oldItems.Add(item);
                }
            }

            // Remove old 
            foreach (var item in oldItems)
                Items.Remove(item);

            // Update all items inside
            base.Update();

            // Add new items
            foreach (var name in newItemsNames)
            {
                Items.Add(new DirectoryItemTreeView(System.IO.Path.Combine(Path, name)));
            }
        }
    }
}
