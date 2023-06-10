using File_manager.FileManager.Core.ViewModelBase;
using File_manager.FileManager.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace File_manager.FileManager.ViewModel
{
    /// <summary>
    /// Manages FileInfo in directories and his properties
    /// </summary>
    public class FileInfoViewModel: FileItemViewModel
    {
        private readonly FileInfo _fileInfo;

        public FileInfoViewModel(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;

            Initialize();
        }

        private Bitmap? _iconBitmap;

        private void Initialize()
        {
            try
            {

                //Load icon

                var path = _fileInfo.FullName;

                if (path != null)
                {
                    _iconBitmap = Icon.ExtractAssociatedIcon(path)?.ToBitmap();
                }
            }
            catch
            {
                Trace.WriteLine("FileInfoWrapper initialization failed...");
            }
        }


        public override Bitmap? IconBitmap => _iconBitmap;

        public override string Name
        {
            get { return _fileInfo.Name; }
            set
            {
                // Rename
                Rename(value);

                OnPropertyChanged(nameof(Name));
            }
        }


            
        public override void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileManagerViewModel.Instance.FileOpener.Open(_fileInfo.FullName);
        }


        //public override string? => _fileInfo.DirectoryName;

        public override string Extension
        {
            get { return _fileInfo.Extension; }
            set
            {
                //change extension

                OnPropertyChanged(nameof(Extension));
            }
        }

        public override string CreationTime => _fileInfo.CreationTime.ToString("g");
         
        public override string LastEditTime => _fileInfo.LastWriteTime.ToString("g");

        public override long Size => _fileInfo.Length;

        private void Rename(string newName)
        {
            string currentDirectory = _fileInfo.Directory.FullName;
            string newFilePath = Path.Combine(currentDirectory, newName);
            _fileInfo.MoveTo(newFilePath);
        }
    }
}
