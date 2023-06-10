using File_manager.FileManager.Core.ViewModelBase;
using File_manager.FileManager.Services.Utilities;
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
        public ObservableCollection<FileItemTreeView> Items{ get; set; }

        public FileManagerTreeView()
        {
            Items = new();

            Initialize();
        }

        private void Initialize()
        {
            FileGroupTreeView MyPc = new FileGroupTreeView("PcIcon", "My pc");

            // load drives
            var drives = DriveInfo.GetDrives().Select(drive => new DriveItemTreeView(drive.Name));

            // add important folders

            var specialFolders = new string[]
            {
                "Desktop",
                "Downloads",
                "Documents",
                "Music",
                "Pictures",
                "Videos"
            };

            foreach(string folder in specialFolders)
            {
                var path = PathHelper.GetSpecialFolder(folder);

                MyPc.AddItem(new ShortcutItemTreeView(path, folder + "Icon", folder));
            }

            foreach(var drive in drives)
            {
                MyPc.AddItem(drive);
            }

            Items.Add(MyPc);

            // Load all drives
            //LoadDrives();

            //Initialize pc icon
        }

        //private void LoadDrives()
        //{
        //    var drives = DriveInfo.GetDrives().Select(drive => new DriveItemTreeView(drive.Name));

        //    foreach(var drive in drives)
        //    {
        //        Drives.Add(drive);
        //    }
        //}


    }
}
