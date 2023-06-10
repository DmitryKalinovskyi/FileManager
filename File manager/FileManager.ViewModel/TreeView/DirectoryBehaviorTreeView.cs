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
    public abstract class DirectoryBehaviorTreeView: FileItemTreeView
    {
        public string Path { get; set; }

        public DirectoryBehaviorTreeView(string path)
        {
            Path = path;
            Items = new() { new EmptyItemTreeView() };
        }

        public override void UpdateItems()
        {
            Items.Clear();

            var items = Directory.GetDirectories(Path).Select(directory => new DirectoryItemTreeView(directory));

            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public override void SelectItem()
        {
            FileManagerViewModel.Instance.FileGrid.Path = Path;
        }
    }
}
