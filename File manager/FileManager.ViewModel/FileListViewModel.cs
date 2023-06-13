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
using System.Security.AccessControl;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace File_manager.FileManager.ViewModel
{
    public class FileListViewModel: NotifyViewModel
    {
        public ObservableCollection<ListItemViewModel> Items { get; set; }

        private FileSystemWatcher _systemWatcher;

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

                OpenPath();
            }
        }
        
        public FileListViewModel()
        {
            _path = "";
            Items = new();
            OpenPath();
        }

        public FileListViewModel(string path)
        {
            _path = path;
            Items = new();
            OpenPath();
        }

        public void DirectoryUpdated(object sender, FileSystemEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                UpdateItems();
                Trace.WriteLine("System item deleted or created!!");
            }
            );
        }

        private void fileSystemItemRenamed(object sender, RenamedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                UpdateItems();
                Trace.WriteLine("Sysetm item renamed!!");
            }
            );
        }

        public void OpenPath()
        {
            if (_systemWatcher != null)
            {
                _systemWatcher.Dispose();
            }
            if (_path != null && string.IsNullOrWhiteSpace(_path) == false)
            {
                _systemWatcher = new FileSystemWatcher(_path);

                _systemWatcher.IncludeSubdirectories = false;
                _systemWatcher.Renamed += fileSystemItemRenamed;
                _systemWatcher.Created += DirectoryUpdated;
                _systemWatcher.Deleted += DirectoryUpdated;

                _systemWatcher.EnableRaisingEvents = true;
            }


            UpdateItems();
        }

        public void UpdateItems()
        {
            Items.Clear();

            if (string.IsNullOrWhiteSpace(_path) || _path == null)
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

        //public void AddItem(string path)
        //{
        //    if(File.Exists(path))
        //    {
        //        Items.Add(new FileInfoViewModel(new FileInfo(path)));
        //    }
        //    else if (Directory.Exists(path))
        //    {
        //        Items.Add(new DirectoryInfoViewModel(new DirectoryInfo(path)));
        //    }
        //}

        //public void RemoveItem(string path)
        //{
        //    var items = Items.Where(item => item.FullName == path);

        //    foreach (var item in items)
        //        Items.Remove(item);
        //}
    }
}
