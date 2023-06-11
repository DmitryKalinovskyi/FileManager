using File_manager.FileManager.Core.ViewModelBase;
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
    public abstract class TreeItemViewModel: NotifyViewModel
    {
        public ObservableCollection<TreeItemViewModel> Items { get; set; } = new();

        public abstract Bitmap? IconBitmap { get; }

        public abstract string Name { get; }

        public bool IsExpanded { get; set; }

        //public abstract void UpdateItems();

        //public abstract void LoadItems();

        //public abstract void UnloadItems();

        protected virtual void Initialize(params object[] args) { }

    }
}
