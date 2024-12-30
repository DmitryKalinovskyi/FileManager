using File_manager.FileManager.Services.Utilities;
using File_manager.FileManager.ViewModel.TreeView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel.AttachedTreeView
{
    internal class AttachedDirectory : AttachedItem, ISelectableTreeItem
    {
        public AttachedDirectory(string path)
        {
            Path = path;
            Initialize();
        }

        private static Bitmap? _iconBitmap;

        private void Initialize()
        {
            try
            {

                if (_iconBitmap == null)
                {
                    string? path;

                    if (CustomResources.TryGetResourcePath("FolderIcon", out path))
                    {
                        Icon icon = new Icon(path);

                        _iconBitmap = icon.ToBitmap();
                    }
                }
            }
            catch
            {
                Trace.WriteLine("DirectoryInfo initialization failed");
            }
        }

        public override Bitmap IconBitmap => _iconBitmap;

        public override string Name => System.IO.Path.GetFileName(Path);

        public void Select()
        {
            FileManagerViewModel.Instance.FileGrid.Path = Path;
        }
    }
}
