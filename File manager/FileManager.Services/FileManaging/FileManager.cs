using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.Services.FileManaging
{
    public class FileManager : IFIleManager
    {
        public void Create(string path, string filename)
        {

        }

        public void DeleteCollection(string[] path)
        {
            throw new NotImplementedException();
        }

        public void Delete(string path)
        {
            throw new NotImplementedException();
        }

        public void MoveTo(string from, string to)
        {
            throw new NotImplementedException();
        }

        public void Open(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            };

            Process.Start(startInfo);
        }

        public void MoveToDirectory(string sourcePath, string directory)
        {
            throw new NotImplementedException();
        }

        public void Rename(string path, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
