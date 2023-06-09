using File_manager.FileManager.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace File_manager.FileManager.Model
{
    public class FileInfoModel
    {
        private FileInfo _fileInfo;


        public FileInfoModel(FileInfo fileInfo)
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

            if(path != null)
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


        // Definitions for DataGrid
        public string Name => _fileInfo.Name;

        public string Attribute => _fileInfo.Attributes.ToString();

        public string? Path => _fileInfo.DirectoryName;

        public string Type => _fileInfo.Extension;

        public DateTime CreationTime => _fileInfo.CreationTime;

        public DateTime LastEditTime => _fileInfo.LastWriteTime;

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
