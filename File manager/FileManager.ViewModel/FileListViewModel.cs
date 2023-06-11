using File_manager.FileManager.Core.Commands;
using File_manager.FileManager.Core.ViewModelBase;
using File_manager.FileManager.Model;
using File_manager.FileManager.ViewModel.ListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel
{
    public class FileListViewModel: NotifyViewModel
    {
        public ObservableCollection<ListItemViewModel> Items { get; set; }

        private string _path;

        public string Path 
        {
            get
            {
                return _path;
            }
            set
            {
                //is valid?

                _path = value;
                OnPropertyChanged(nameof(Path));

                LoadItems();
            }
        }
        


        public FileListViewModel()
        {
            _path = "C:";
            Items = new();
            LoadItems();
        }

        public FileListViewModel(string path)
        {
            _path = path;
            Items = new();
            LoadItems();
        }

        public void Update()
        {
            LoadItems();
        }

        public void LoadItems()
        {
            Items.Clear();

            if (_path == "" || _path == null)
            {
                //open drive location
                var drives = DriveInfo.GetDrives().Select(x => new DriveInfoViewModel(x));

                foreach (var drive in drives)
                {
                    Items.Add(drive);
                }

                return;
            }

            var dir = new DirectoryInfo(_path);

            var directories = dir.GetDirectories()
                .Where(dir =>
                (dir.Attributes & FileManagerViewModel.Instance.AllowedAttributes) == dir.Attributes)
                .Select(x => new DirectoryInfoViewModel(x));

            foreach (var d in directories)
            {
                Items.Add(d);
            }

            var files = dir.GetFiles()
                .Where(file =>
                (file.Attributes & FileManagerViewModel.Instance.AllowedAttributes) == file.Attributes)
                .Select(x => new FileInfoViewModel(x));

            foreach (var file in files)
            {
                Items.Add(file);
            }
        }

        public void AddItem(string path)
        {
            if(File.Exists(path))
            {
                Items.Add(new FileInfoViewModel(new FileInfo(path)));
            }
            else if (Directory.Exists(path))
            {
                Items.Add(new DirectoryInfoViewModel(new DirectoryInfo(path)));
            }
        }

        public void RemoveItem(string path)
        {
            var items = Items.Where(item => item.FullName == path);

            foreach (var item in items)
                Items.Remove(item);
        }
    }
}
