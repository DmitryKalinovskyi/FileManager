﻿using File_manager.FileManager.Services.Utilities;
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
    public class DriveInfoViewModel : FileItemViewModel
    {
        private readonly DriveInfo _driveInfo;

        public DriveInfoViewModel(DriveInfo driveInfo)
        {
            _driveInfo = driveInfo;

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

                    if (CustomResources.TryGetResourcePath("DriveIcon", out path))
                    {
                        Icon icon = new Icon(path);

                        _iconBitmap = icon.ToBitmap();
                    }
                }
            }
            catch
            {
                Trace.WriteLine("DriveInfo initialization failed");
            }

        }

        public override Bitmap? IconBitmap => _iconBitmap;

        public override string Name { get => _driveInfo.Name; set { } }
        public override string Extension { get => "Drive"; set { } }

        public override long Size => _driveInfo.TotalSize - _driveInfo.TotalFreeSpace;

        public override string CreationTime => "";

        public override string LastEditTime => "";

        public override void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //open drive
            FileManagerViewModel.Instance.FileGrid.Path = _driveInfo.Name;

        }
    }
}
