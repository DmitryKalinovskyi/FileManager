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
    public class AttachedFile: AttachedItem, ISelectableTreeItem
    {
        public AttachedFile(string path)
        {
            Path = path;
            Initialize();
        }

        private Bitmap? _iconBitmap;

        private void Initialize()
        {
            try
            {

                _iconBitmap = Icon.ExtractAssociatedIcon(Path)?.ToBitmap();
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
            FileManagerViewModel.Instance.FileGrid.Path = System.IO.Path.GetDirectoryName(Path);
        }
    }
}
