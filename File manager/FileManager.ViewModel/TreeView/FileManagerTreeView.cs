using File_manager.FileManager.Core.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel.TreeView
{
    public class FileManagerTreeView: NotifyViewModel
    {
        public ObservableCollection<FileItemTreeView> Drives { get; set; }

        public FileManagerTreeView()
        {
            Drives = new();

            Initialize();
        }

        private void Initialize()
        {
            // Load all drives
            LoadDrives();

            //Initialize pc icon
        }

        private void LoadDrives()
        {
            var drives = DriveInfo.GetDrives().Select(drive => new DriveItemTreeView(drive.Name));

            foreach(var drive in drives)
            {
                Drives.Add(drive);
            }
        }


    }
}
