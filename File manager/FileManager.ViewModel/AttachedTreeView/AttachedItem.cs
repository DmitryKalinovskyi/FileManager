using File_manager.FileManager.ViewModel.TreeView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.ViewModel.AttachedTreeView
{
    public abstract class AttachedItem
    {
        public static AttachedItem GetInstance(string path)
        {
            try
            {
                var attributes = File.GetAttributes(path);

                if((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    return new AttachedDirectory(path);
                }
                else
                {
                    return new AttachedFile(path);
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }

            return null;
        }

        public string Path { get; protected set; }

        public abstract Bitmap IconBitmap { get;  }

        public abstract string Name { get; }
    }
}
