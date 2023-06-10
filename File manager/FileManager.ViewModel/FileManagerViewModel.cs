using File_manager.FileManager.Core.ViewModelBase;
using File_manager.FileManager.Services;
using File_manager.FileManager.ViewModel.TreeView;
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
        public static FileManagerViewModel Instance { get; set; }

        public FileManagerGrid FileGrid { get; set; }
        public FileManagerTreeView FileTree { get; set; }

        public IFIleOpener FileOpener { get; set; }

        public FileManagerViewModel()
        {
            if (Instance != null)
                return;

            Instance = this;
            FileGrid = new("C:\\Users\\Свєта\\Desktop\\IsolatedFolder");
            FileTree = new();

            FileOpener = new DefaultFileOpener();
        }
    }
}
