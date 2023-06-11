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

                        // Update FileGrid
                        FileGrid.AddItem(newFullPath);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

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
            //  FileGrid = new(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            FileGrid = new("C:\\Users\\Свєта\\Desktop\\IsolatedFolder");
            FileTree = new();

            FileManager = new Services.FileManaging.FileManager();
        }
    }
}
