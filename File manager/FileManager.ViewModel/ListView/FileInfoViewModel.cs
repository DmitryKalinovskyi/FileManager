using File_manager.FileManager.Core.ViewModelBase;
using File_manager.FileManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace File_manager.FileManager.ViewModel.ListView
{
    /// <summary>
    /// Manages FileInfo in directories and his properties
    /// </summary>
    public class FileInfoViewModel: ListItemViewModel, IAttachable
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

        public override string DisplayedName
        {
            get {
                string name = _fileInfo.Name;

                if (FileManagerViewModel.Instance.ShowExtension)
                {
                    return name;
                }
                else
                {
                    return Path.GetFileNameWithoutExtension(name);
                }
            }
        }

        public override string Name
        {
            get
            {
                return _fileInfo.Name;
            }
        }

        public override void Open()
        {
            // Open file
            FileManagerViewModel.Instance.FileManager.Open(_fileInfo.FullName);
        }

        public override string Extension
        {
            get { return _fileInfo.Extension; }
        }

        public override string CreationTime => _fileInfo.CreationTime.ToString("g");
         
        public override string LastEditTime => _fileInfo.LastWriteTime.ToString("g");

        public override long Size => _fileInfo.Length;

        public override string FullName => _fileInfo.FullName;

        public override float Opacity => ((_fileInfo.Attributes & FileAttributes.System) != 0) || ((_fileInfo.Attributes & FileAttributes.Hidden) != 0)?0.5f:1f;

        public override bool IsDirectory => false;

        public void Attach()
        {
            FileManagerViewModel.Instance.FileAttached.Attach(FullName);
        }
    }
}
