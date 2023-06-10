using File_manager.FileManager.Core.Commands;
using File_manager.FileManager.Core.ViewModelBase;
using File_manager.FileManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel
{
    public class FileManagerGrid: NotifyViewModel
    {
        #region Commands

        private RelayCommand _directoryUpCommand;
        public RelayCommand DirectoryUpCommand
        {
            get
            {
                return _directoryUpCommand ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        DirectoryInfo? directory = Directory.GetParent(Path);

                        if(directory != null)
                        {
                            Path = directory.FullName;
                        }
                        else
                        {
                            Path = "";
                        }
                    }
                    catch(Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _openDirectoryCommand;
        public RelayCommand OpenDirectoryCommand
        {
            get
            {
                return _openDirectoryCommand ?? new RelayCommand((obj) =>
                {
                    try
                    {


                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        #endregion

        //add directories
        public ObservableCollection<FileItemViewModel> Items { get; set; }

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

                UpdateItems();
            }
        }
        


        public FileManagerGrid()
        {
            _path = "C:";
            Items = new();
            UpdateItems();
        }

        public FileManagerGrid(string path)
        {
            _path = path;
            Items = new();
            UpdateItems();
        }

        public void UpdateItems()
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

            var directories = dir.GetDirectories().Select(x => new DirectoryInfoViewModel(x));

            foreach (var d in directories)
            {
                Items.Add(d);
            }

            var files = dir.GetFiles().Select(x => new FileInfoViewModel(x));

            foreach (var file in files)
            {
                Items.Add(file);
            }

           
        }
    }
}
