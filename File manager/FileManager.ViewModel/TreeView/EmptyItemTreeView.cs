﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel.TreeView
{
    public class EmptyItemTreeView : TreeItemViewModel
    {
        public override Bitmap? IconBitmap => null;

        public override string Name => "Loading...";
    }
}
