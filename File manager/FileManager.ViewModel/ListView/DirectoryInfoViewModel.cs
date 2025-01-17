﻿using File_manager.FileManager.Services.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace File_manager.FileManager.ViewModel.ListView
{
    public class DirectoryInfoViewModel : ListItemViewModel, IAttachable
    {
        private readonly DirectoryInfo _directoryInfo;

        public DirectoryInfoViewModel(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;

            Initialize();
        }

        private static Bitmap? _iconBitmap;

        private void Initialize()
        {
            try
            {

                if (_iconBitmap == null)
                {
                    string? path;

                    if(CustomResources.TryGetResourcePath("FolderIcon", out path))
                    {
                        Icon icon = new Icon(path);

                        _iconBitmap = icon.ToBitmap();
                    }
                }
            }
            catch
            {
                Trace.WriteLine("DirectoryInfo initialization failed");
            }
        }

        public override void Open()
        {
            // Open directory
            FileManagerViewModel.Instance.FileGrid.Path = _directoryInfo.FullName;
        }

        public override Bitmap? IconBitmap => _iconBitmap;

        public override long Size => -1;

        public override string CreationTime => _directoryInfo.CreationTime.ToShortDateString();

        public override string LastEditTime => _directoryInfo.LastWriteTime.ToShortDateString();

        public override string Name 
        {
            get { return _directoryInfo.Name; }
        }
        public override string Extension 
        {
            get { return "Folder"; }
        }

        public override string FullName => _directoryInfo.FullName;

        public override float Opacity => ((_directoryInfo.Attributes & FileAttributes.System) != 0) || ((_directoryInfo.Attributes & FileAttributes.Hidden) != 0) ? 0.5f : 1f;

        public override bool IsDirectory => true;

        public override string DisplayedName => _directoryInfo.Name;

        public void Attach()
        {
            FileManagerViewModel.Instance.FileAttached.Attach(FullName);
        }

    }
}
