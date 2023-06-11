using File_manager.FileManager.Core.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace File_manager.FileManager.ViewModel.ListView
{
    public abstract class ListItemViewModel : NotifyViewModel
    {
        public abstract Bitmap? IconBitmap { get; }

        public abstract string Name { get; set; }

        public abstract string Extension { get; set; }

        public abstract long Size { get; }

        public abstract string FullName { get; }

        public abstract string CreationTime { get; }

        public abstract string LastEditTime { get; }

        public abstract float Opacity { get; }

        public abstract void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e);
    }
}
