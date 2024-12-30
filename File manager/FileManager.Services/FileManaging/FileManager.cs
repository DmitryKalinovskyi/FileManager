using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            try
            {
                FileOperationAPIWrapper.Send(path, 
                    FileOperationAPIWrapper.FileOperationFlags.FOF_WANTNUKEWARNING |
                    FileOperationAPIWrapper.FileOperationFlags.FOF_ALLOWUNDO);
            }
            catch (IOException e)
            {
                Trace.WriteLine($"An error occurred while deleting the file: {e.Message}");
            }
            catch (UnauthorizedAccessException e)
            {
                Trace.WriteLine($"Access to the file is denied: {e.Message}");
            }
            catch (Exception e)
            {
                Trace.WriteLine($"An error occurred: {e.Message}");
            }
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
            string destination = Path.Combine(directory, Path.GetFileName(sourcePath));

            if (Path.Exists(destination))
            {
                throw new Exception("Such path exist");
            }

            Directory.Move(sourcePath, destination);
        }

        public void Rename(string path, string newName)
        {
            string folder = Path.GetDirectoryName(path);
            string destinationPath = Path.Combine(folder, newName);

            Trace.WriteLine($"Moving from {path} to {destinationPath}");
            Directory.Move(path, destinationPath);
        }
    }
}
