using File_manager.FileManager.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel
{
    /// <summary>
    /// Manages FileInfo in directories and his properties
    /// </summary>
    public class FileInfoViewModel
    {
        private FileInfo _fileInfo;


        public FileInfoViewModel(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;

            Initialize();
        }

        private Icon? _icon;

        private void Initialize()
        {
            try
            {

                //Load icon

                var path = _fileInfo.FullName;

                if (path != null)
                {
                    _icon = Icon.ExtractAssociatedIcon(path);
                }
            }
            catch
            {
                Trace.WriteLine("FileInfoWrapper initialization failed...");
            }
        }


        // Custom definitions
        public Icon? Icon => _icon;

        public string Name => _fileInfo.Name;

        // Definitions for DataGrid
        public string Attribute => _fileInfo.Attributes.ToString();

        public string? Path => _fileInfo.DirectoryName;

        public string Type => _fileInfo.Extension;

        public string CreationTime => _fileInfo.CreationTime.ToString("g");

        public string LastEditTime => _fileInfo.LastWriteTime.ToString("g");

        //public string? Path 
        //{
        //    get { return _fileInfo.DirectoryName; }
        //    set 
        //    {
        //        if(value == null && Path
        //            .)

        //        _fileInfo.MoveTo(value);
        //    }
        //}

    }
}
