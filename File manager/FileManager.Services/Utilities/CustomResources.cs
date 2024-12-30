using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.Services.Utilities
{
    public static class CustomResources
    {
        // All project custom resources
        private static Dictionary<string, string> _relativeResources = new()
        {
            {"FolderIcon", "ArtWork\\folder-icon-1024x1024.ico"},
            {"DriveIcon", "ArtWork\\Drive.ico"},
            {"PcIcon", "ArtWork\\PcIcon.ico"},

            {"DesktopIcon", "ArtWork\\desktop-64.ico"},
            {"DocumentsIcon", "ArtWork\\docs-96.ico"},
            {"DownloadsIcon", "ArtWork\\download-96.ico"},
            {"PicturesIcon", "ArtWork\\image-96.ico"},
            {"MusicIcon", "ArtWork\\music-library-96.ico"},
            {"VideosIcon", "ArtWork\\video-96.ico"},
        };

        public static string GetResourcePath(string resourceName) => _relativeResources[resourceName];

        public static bool TryGetResourcePath(string resourceName, out string? resourcePath)
        {
            if(_relativeResources.ContainsKey(resourceName))
            {
                resourcePath = GetResourcePath(resourceName);
                if (File.Exists(resourcePath) == false)
                    return false;

                return true;
            }
            else
            {
                resourcePath = null;
                return false;
            }
        }
    }
}
