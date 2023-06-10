using System;
using System.Collections.Generic;
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

        private void Initialize()
        {
            //load drive icon
        }

        public override Bitmap? IconBitmap => null;

        public override string Name { get => _driveInfo.Name; set { } }
        public override string Extension { get => "Drive"; set { } }

        public override string Size => "?";

        public override string CreationTime => "";

        public override string LastEditTime => "";

        public override void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //open drive
            FileManagerViewModel.Instance.FileGrid.Path = _driveInfo.Name;

        }
    }
}
