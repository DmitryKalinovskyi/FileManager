using File_manager.FileManager.Core.Commands;
using File_manager.FileManager.Core.ViewModelBase;
using File_manager.FileManager.Services;
using File_manager.FileManager.Services.FileManaging;
using File_manager.FileManager.View;
using File_manager.FileManager.ViewModel.ListView;
using File_manager.FileManager.ViewModel.TreeView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace File_manager.FileManager.ViewModel
{

    public class FileManagerViewModel: NotifyViewModel
    {
        #region Commands
        private RelayCommand _updateDirectories;
        public RelayCommand UpdateDirectories
        {
            get
            {
                return _updateDirectories ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        FileTree.Update();
                        FileGrid.UpdateItems();
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

                        FileTree.Update();

                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _createFileCommand;
        public RelayCommand CreateFileCommand
        {
            get
            {
                return _createFileCommand ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        string path = FileGrid.Path;

                        if (path == "")
                        {
                            MessageBox.Show("You can't create file here!");
                            return;
                        }

                        // get free file name

                        string extension = (string)obj;
                        string name = "New file";

                        if (Path.Exists(Path.Combine(path, name + extension)))
                        {
                            int index = 1;
                            while (true)
                            {
                                if (Path.Exists(Path.Combine(path, name + $"({index})" + extension)) == false)
                                {
                                    break;
                                }
                                index++;
                            }

                            name += $"({index})";
                        }

                        string newFullPath = Path.Combine(path, name + extension);
                        File.Create(newFullPath);

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

        private RelayCommand _dropFilesCommand;
        public RelayCommand DropFilesCommand
        {
            get
            {
                return _dropFilesCommand ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        string[] paths = (string[])obj;


                        // try to move all inside current path
                        foreach (string path in paths)
                        {
                            if(Path.GetDirectoryName(path) == FileGrid.Path)
                            {
                                MessageBox.Show("Такий файл уже існує в папці!");
                            }
                            else
                            {
                                // move
                                FileManager.MoveToDirectory(path, FileGrid.Path);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _dropFilesinDirectoryCommand;
        public RelayCommand DropFilesinDirectoryCommand
        {
            get
            {
                return _dropFilesinDirectoryCommand ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        object[] args = (object[])obj;

                        string[] paths = (string[])args[0];
                        string targetPath = (string)args[1];

                        // try to move all inside [targetPath]
                        foreach (string path in paths)
                        {
                            try
                            {
                                // move
                                FileManager.MoveToDirectory(path, targetPath);
                            }
                            catch
                            {
                                MessageBox.Show($"Failed to move file {Path.GetFileName(path)} into {targetPath}");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _deleteItems;
        public RelayCommand DeleteItems
        {
            get
            {
                return _deleteItems ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        string[] paths = (string[])obj;
                        Trace.WriteLine("Path to delete:");

                        foreach (string path in paths)
                        {
                            Trace.WriteLine(path);
                        }


                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _deleteItemCommand;
        public RelayCommand DeleteItemCommand
        {
            get
            {
                return _deleteItemCommand ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        string path = (string)obj;
                        Trace.WriteLine("Path to delete: " + path);

                        FileManager.Delete(path);

                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _openInNewWindow;
        public RelayCommand OpenInNewWindow
        {
            get
            {
                return _openInNewWindow ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        string path = (string)obj;

                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
                     //   startInfo.Arguments = path;

                        Process.Start(startInfo);

                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _openItem;
        public RelayCommand OpenItem
        {
            get
            {
                return _openItem ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        ((ListItemViewModel)obj).Open();

                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _openProperties;
        public RelayCommand OpenProperties
        {
            get
            {
                return _openProperties ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        SystemItemPropertiesWindow propertiesWindow = new SystemItemPropertiesWindow();
                        propertiesWindow.Owner = App.Current.MainWindow;
                        propertiesWindow.DataContext = obj;

                        propertiesWindow.Show();
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                });
            }
        }

        private RelayCommand _renameItem;
        public RelayCommand RenameItem
        {
            get
            {
                return _renameItem ?? new RelayCommand((obj) =>
                {
                    try
                    {
                        object[] args = (object[])obj;
                        ListItemViewModel item = (ListItemViewModel)args[0];

                        string newName = (string)args[1];

                        FileManager.Rename(item.FullName, newName);
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

        public bool ShowExtension
        {
            get { return (FileDisplayProperties.NameWithExtension & DisplayProperties) != 0; }
            set
            {
                DisplayProperties = value ?
                    DisplayProperties | FileDisplayProperties.NameWithExtension :
                    DisplayProperties & (~FileDisplayProperties.NameWithExtension);

                OnPropertyChanged(nameof(ShowExtension));
                UpdateDirectories.Execute(null);

            }
        }

        private FileAttributes _allAttributes = Enum.GetValues(typeof(FileAttributes))
    .Cast<FileAttributes>()
    .Aggregate((current, next) => current | next);

        public FileAttributes AllowedAttributes;

        public FileDisplayProperties DisplayProperties;

        #endregion

        public static FileManagerViewModel Instance { get; set; }

        public FileListViewModel FileGrid { get; set; }
        public FileTreeViewModel FileTree { get; set; }

        public IFIleManager FileManager { get; set; }

        private string DefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public FileManagerViewModel()
        {
            if (Instance != null)
                return;

            Instance = this;
            AllowedAttributes = _allAttributes;
            //  FileGrid = new();
            FileGrid = new(DefaultPath);
            FileTree = new();

            FileManager = new Services.FileManaging.FileManager();
        }

        public FileManagerViewModel(string[] args)
        {
            if (Instance != null)
                return;

            Instance = this;
            AllowedAttributes = _allAttributes;

            string path = args.Length > 0? args[0] : DefaultPath;

            FileGrid = new(path);
            FileTree = new();

            FileManager = new Services.FileManaging.FileManager();
        }
    }

    [Flags]
    public enum FileDisplayProperties
    {
        NameWithExtension = 1
    }

}
