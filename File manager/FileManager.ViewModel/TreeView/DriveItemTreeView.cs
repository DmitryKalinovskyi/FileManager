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
    public class DriveItemTreeView : DirectoryBehaviorTreeView
    {
        public override string Name { get { return Path; }  }

        public override Bitmap? IconBitmap => _iconBitmap;

        public DriveItemTreeView(string path): base(path)
        {
            Initialize();
        }

        private static Bitmap? _iconBitmap;

        protected override void Initialize(params object[] args)
        {
            if (_iconBitmap == null)
            {
                string path;

                if (CustomResources.TryGetResourcePath("DriveIcon", out path))
                {
                    _iconBitmap = new Icon(path).ToBitmap();
                }
            }
        }
    }
}
