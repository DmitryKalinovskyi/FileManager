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
        public ObservableCollection<FileInfoViewModel> Items { get; set; }

        private string _path = "C:\\Users";

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
            Items = new();
            UpdateItems();
        }

        public void UpdateItems()
        {
            var dir = new DirectoryInfo(_path);
            var files = dir.GetFiles().Select(x => new FileInfoViewModel(x));

            Items.Clear();

            foreach (var file in files)
            {
                Items.Add(file);
            }
        }
    }
}
