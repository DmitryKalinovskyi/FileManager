﻿using File_manager.FileManager.Services.Utilities;
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
    public class DirectoryItemTreeView : DirectoryBehaviorTreeView
    {
        public DirectoryItemTreeView(string path) : base(path)
        {
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

        public override string Name { get => System.IO.Path.GetFileName(Path); }

        public override Bitmap? IconBitmap => _iconBitmap;
    }
}
