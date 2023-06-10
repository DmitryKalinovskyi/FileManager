using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace File_manager.FileManager.Services.Utilities
{
    public static class PathHelper
    {
        /// <summary>
        ///  Example
        ///  С:\\Users\\You\\Folder\\relativePath
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="Folder"></param>
        /// <returns></returns>
        public static string RelativePath(string relativePath)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(basePath[..(basePath.LastIndexOf("File manager", StringComparison.OrdinalIgnoreCase) + "File manager".Length)], relativePath);
        }

        public static string GetSpecialFolder(string folder)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), folder);
        }
    }
}
