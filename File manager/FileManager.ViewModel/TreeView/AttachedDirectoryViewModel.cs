using File_manager.FileManager.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel.TreeView
{
    public class AttachedDirectoryViewModel : TreeItemViewModel
    {
        public string Path { get; set; }

        public AttachedDirectoryViewModel(string path)
        {
            Path = path;
            Initialize();
        }

        private static Bitmap? _iconBitmap;

        protected override void Initialize(params object[] args)
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

        public override Bitmap? IconBitmap => _iconBitmap;

        public override string Name => System.IO.Path.GetFileName(Path);
    }
}
