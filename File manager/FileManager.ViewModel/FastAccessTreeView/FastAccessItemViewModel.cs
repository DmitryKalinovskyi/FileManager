using File_manager.FileManager.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel.FastAccessTreeView
{
    public abstract class FastAccessItemViewModel
    {
        public string Path{ get; }

        public FastAccessItemViewModel(string path)
        {
            Path = path;
            Initialize("FolderIcon");
        }

        public FastAccessItemViewModel(string path, string iconResourceKey)
        {
            Path = path;
            Initialize(iconResourceKey);
        }

        private Bitmap? _iconBitmap;

        protected virtual void Initialize(params object[] args)
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

        public virtual string Name { get => System.IO.Path.GetFileName(Path); }

        public virtual Bitmap? IconBitmap => _iconBitmap;
    }
}
