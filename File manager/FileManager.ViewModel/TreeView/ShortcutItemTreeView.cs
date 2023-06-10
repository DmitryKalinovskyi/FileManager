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
    public class ShortcutItemTreeView : DirectoryBehaviorTreeView
    {
        public ShortcutItemTreeView(string path, string iconResourceName, string shortcutName) : base(path)
        {
            _shortcutName = shortcutName;
            Items = new() { new EmptyItemTreeView() };

            Initialize(iconResourceName);
        }

        public ShortcutItemTreeView(string path, string shortcutName) : this(path, "FolderIcon", shortcutName)
        {
        }

        private Bitmap? _iconBitmap;

        private string _shortcutName;

        protected override void Initialize(params object[] args)
        {
            if (_iconBitmap == null)
            {
                string path;

                if (CustomResources.TryGetResourcePath(args[0].ToString(), out path))
                {
                    _iconBitmap = new Icon(path).ToBitmap();
                }
            }
        }

        public override Bitmap? IconBitmap => _iconBitmap;

        public override string Name => _shortcutName;

    }
}
