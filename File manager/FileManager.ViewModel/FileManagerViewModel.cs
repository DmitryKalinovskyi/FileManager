using File_manager.FileManager.Core.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel
{

    /// <summary>
    /// 
    /// </summary>
    public class FileManagerViewModel: NotifyViewModel
    {
        public FileManagerGrid FileGrid { get; set; }

        public FileManagerViewModel()
        {
            FileGrid = new("C:\\Users\\Свєта\\Desktop\\IsolatedFolder");
        }
    }
}
