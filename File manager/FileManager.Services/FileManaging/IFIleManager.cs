using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.Services.FileManaging
{
    public interface IFIleManager: IFIleCreator, IFIleOpener, IFileDeleter
    {
        public void MoveTo(string sourcePath, string destinationPath);

        public void MoveToDirectory(string sourcePath, string directory);

        public void Rename(string path, string newName);
    }
}
