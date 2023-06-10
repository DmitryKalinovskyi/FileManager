﻿using File_manager.FileManager.Core.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace File_manager.FileManager.ViewModel.TreeView
{
    public abstract class FileItemTreeView: NotifyViewModel
    {
        public ObservableCollection<FileItemTreeView> Items { get; set; } = new();

        public abstract Bitmap? IconBitmap { get; }

        public abstract string Name { get; }

        public abstract void UpdateItems();

        public abstract void SelectItem();

        protected virtual void Initialize(params object[] args) { }

    }
}