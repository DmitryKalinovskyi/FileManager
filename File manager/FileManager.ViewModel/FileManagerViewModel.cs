using File_manager.FileManager.Core.Commands;
using File_manager.FileManager.Core.ViewModelBase;
using File_manager.FileManager.Services;
using File_manager.FileManager.Services.FileManaging;
using File_manager.FileManager.ViewModel.TreeView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace File_manager.FileManager.ViewModel
{

    /// <summary>
    /// 
    /// </summary>
    public class FileManagerViewModel: NotifyViewModel
    {
        #region Commands
        private RelayCommand _fileItemsToDirectoryCommand;
        public RelayCommand FileItemsToDirectoryCommand
        {
            get
            {
                return _fileItemsToDirectoryCommand ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        ////from and to as string
                        //var args = obj as object[];
                        //string[] from = args[0] as string;
                        //string to = args[1] as string;

                        //if (from == to)
                        //    return;

                        //if(Path.)


                        //foreach(var path in from)
                        //FileManager.MoveTo(path, to);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _updateDirectories;
        public RelayCommand UpdateDirectories
        {
            get
            {
                return _updateDirectories ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        FileGrid.Update();
                        FileTree.Update();
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _createFolderCommand;
        public RelayCommand CreateFolderCommand
        {
            get
            {
                return _createFolderCommand ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        string path = FileGrid.Path;

                        if(path == "")
                        {
                            MessageBox.Show("You can't create folder here!");
                            return;
                        }

                        // get free folder name
                        string name = "New Folder";
                        if (Path.Exists(Path.Combine(path, name)))
                        {
                            int index = 1;
                            while (true)
                            {
                                if (Path.Exists(Path.Combine(path, name + $"({index})")) == false)
                                {
                                    break;
                                }
                                index++;
                            }

                            name += $"({index})";
                        }

                        string newFullPath = Path.Combine(path, name);
                        Directory.CreateDirectory(newFullPath);

                        // Update displaying
                        FileGrid.AddItem(newFullPath);

                        FileTree.Update();

                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _directoryUpCommand;
        public RelayCommand DirectoryUpCommand
        {
            get
            {
                return _directoryUpCommand ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        DirectoryInfo? directory = Directory.GetParent(FileGrid.Path);

                        if (directory != null)
                        {
                            FileGrid.Path = directory.FullName;
                        }
                        else
                        {
                            FileGrid.Path = "";
                        }
                    }
                    catch (Exception ex)
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

        #region Flags
        public bool IsHidden
        {
            get { return (FileAttributes.Hidden & AllowedAttributes) != 0; }
            set
            {
                AllowedAttributes = value ?
                    AllowedAttributes | FileAttributes.Hidden :
                    AllowedAttributes & (~FileAttributes.Hidden);

                OnPropertyChanged(nameof(IsHidden));
                UpdateDirectories.Execute(null);

            }
        }
        public bool IsReadOnly
        {
            get { return (FileAttributes.ReadOnly & AllowedAttributes) != 0; }
            set
            {
                AllowedAttributes = value ?
                    AllowedAttributes | FileAttributes.ReadOnly :
                    AllowedAttributes & (~FileAttributes.ReadOnly);

                OnPropertyChanged(nameof(IsReadOnly));
                UpdateDirectories.Execute(null);
            }
        }
        public bool IsSystem
        {
            get { return (FileAttributes.System & AllowedAttributes) != 0; }
            set
            {
                AllowedAttributes = value ?
                    AllowedAttributes | FileAttributes.System :
                    AllowedAttributes & (~FileAttributes.System);

                OnPropertyChanged(nameof(IsSystem));
                UpdateDirectories.Execute(null);

            }
        }

        private FileAttributes _allAttributes = Enum.GetValues(typeof(FileAttributes))
    .Cast<FileAttributes>()
    .Aggregate((current, next) => current | next);

        public FileAttributes AllowedAttributes;

        #endregion

        public static FileManagerViewModel Instance { get; set; }

        public FileListViewModel FileGrid { get; set; }
        public FIleTreeViewModel FileTree { get; set; }

        public IFIleManager FileManager { get; set; }

        public FileManagerViewModel()
        {
            if (Instance != null)
                return;

            Instance = this;
            AllowedAttributes = _allAttributes;
            //  FileGrid = new(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            FileGrid = new("C:\\Users\\Свєта\\Desktop\\IsolatedFolder");
            FileTree = new();

            FileManager = new Services.FileManaging.FileManager();
        }
    }
}
