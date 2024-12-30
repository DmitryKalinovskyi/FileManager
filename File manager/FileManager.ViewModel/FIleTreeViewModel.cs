using File_manager.FileManager.Core.ViewModelBase;
using File_manager.FileManager.Services.Utilities;
using File_manager.FileManager.ViewModel.TreeView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel
{
    public class FileTreeViewModel: NotifyViewModel
    {
        public ObservableCollection<TreeItemViewModel> Items{ get; set; }

        public FileTreeViewModel()
        {
            Items = new();

            Initialize();
        }

        private void Initialize()
        {
            CreatePCTreeView();
        }

        private void CreatePCTreeView()
        {
            FileGroupTreeView MyPc = new FileGroupTreeView("PcIcon", "My pc");
            MyPc.IsExpanded = true;


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

            foreach (string folder in specialFolders)
            {
                var path = PathHelper.GetSpecialFolder(folder);

                MyPc.Items.Add(new ShortcutItemTreeView(path, folder + "Icon", folder));
            }

            foreach (var drive in drives)
            {
                MyPc.Items.Add(drive);
            }

            Items.Add(MyPc);
        }

      

        public void Update()
        {
            foreach (var item in Items)
            {
                if (item is IDynamicTreeViewItem dynamicItem)
                    dynamicItem.Update();
            }
        }
    }
}
