using File_manager.FileManager.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel.TreeView
{
    public class FileGroupTreeView : FileItemTreeView
    {
       

        public FileGroupTreeView(string iconResourceName, string shortcutName)
        {
            _shortcutName = shortcutName;
            Initialize(iconResourceName);
        }

        public FileGroupTreeView(string shortcutName)
        {
            _shortcutName = shortcutName;
            
            Initialize("FolderIcon");
        }

        private Bitmap _iconBitmap;
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

        public override void SelectItem()
        {
            // reference to FileGrid are not defined
        }

        public override void UpdateItems()
        {
            // ?
        }

        public void AddItem(FileItemTreeView item)
        {
            Items.Add(item);
        }

        public void RemoveItem(FileItemTreeView item)
        {
            Items.Remove(item);
        }
    }
}
