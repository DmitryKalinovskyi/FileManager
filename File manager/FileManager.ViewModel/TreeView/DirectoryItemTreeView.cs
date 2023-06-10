using File_manager.FileManager.Services.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel.TreeView
{
    public class DirectoryItemTreeView : FileItemTreeView
    {
        public DirectoryItemTreeView(string path) : base(path)
        {
            Items = new() { new EmptyItemTreeView() };

            Initialize();
        }

        private static Bitmap? _iconBitmap;

        private void Initialize()
        {
            if (_iconBitmap == null)
            {
                string path;

                if (CustomResources.TryGetResourcePath("FolderIcon", out path))
                {
                    _iconBitmap = new Icon(path).ToBitmap();
                }
            }
        }

        public override string Name { get => System.IO.Path.GetFileName(Path); }

        public override Bitmap? IconBitmap => _iconBitmap;

        public override void UpdateItems()
        {
            Items.Clear();

            var items = Directory.GetDirectories(Path).Select(directory => new DirectoryItemTreeView(directory));

            foreach (var item in items)
            {
                Trace.WriteLine(item.Path);

                Items.Add(item);
            }
        }

        public override void SelectItem()
        {
            FileManagerViewModel.Instance.FileGrid.Path = Path;
        }
    }
}
