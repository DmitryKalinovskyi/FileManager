using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace File_manager.FileManager.ViewModel
{
    public class DirectoryInfoViewModel : FileItemViewModel
    {
        private DirectoryInfo _directoryInfo;

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
                    ////Check wheter resource finded 
                    //var resourceKey = "FolderIcon";
                    //var resource = App.Current.TryFindResource(resourceKey);

                    //Trace.WriteLine(resource != null ? "Resource founded!" : "Resource NOT founded");

                    //if(resource is System.Windows.Controls.Image image)
                    //{
                    //    Trace.WriteLine("Image gainded");
                    //    _iconBitmap = image.Source.;
                    //}
                    string path = "C:\\Users\\Свєта\\source\\repos\\File manager\\File manager\\FileManager.Resources\\ArtWork\\folder-icon-1024x1024.ico";
                   // string path2 = "..\\FileManager.Resources\\ArtWork\\folder-icon-1024x1024.ico";



                    Icon icon = new Icon(path);

                    if(icon != null ) 
                    {
                        Trace.WriteLine("Icon founded!");
                        _iconBitmap = icon.ToBitmap();
                    }

                }
            }
            catch
            {
                Trace.WriteLine("FileInfoWrapper initialization failed...");
            }
        }

        public override void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileManagerViewModel.Instance.FileGrid.Path = _directoryInfo.FullName;
        }

        public override Bitmap? IconBitmap => _iconBitmap;

        public override string Size => "";

        public override string CreationTime => _directoryInfo.CreationTime.ToShortDateString();

        public override string LastEditTime => _directoryInfo.LastWriteTime.ToShortDateString();

        public override string Name 
        {
            get { return _directoryInfo.Name; }
            set {
                //change folder name

                Rename(value);

                OnPropertyChanged(nameof(Name));
            }
        }
        public override string Extension 
        {
            get { return "Folder"; }
            set {
                //nothing todo
            }
        }

        private void Rename(string newName)
        {
            string currentDirectory = _directoryInfo.Root.FullName;
            string newFilePath = Path.Combine(currentDirectory, newName);
            _directoryInfo.MoveTo(newFilePath);
        }

        // Undefined
    }
}
