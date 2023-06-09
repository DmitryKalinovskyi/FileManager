﻿using System;
using System.Collections.Generic;
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
            {"FolderIcon", "FileManager.Resources\\ArtWork\\folder-icon-1024x1024.ico"},
            {"DriveIcon", "FileManager.Resources\\ArtWork\\Drive.ico"},
            {"PcIcon", "FileManager.Resources\\ArtWork\\PcIcon.ico"},

            {"DesktopIcon", "FileManager.Resources\\ArtWork\\desktop-64.ico"},
            {"DocumentsIcon", "FileManager.Resources\\ArtWork\\docs-96.ico"},
            {"DownloadsIcon", "FileManager.Resources\\ArtWork\\download-96.ico"},
            {"PicturesIcon", "FileManager.Resources\\ArtWork\\image-96.ico"},
            {"MusicIcon", "FileManager.Resources\\ArtWork\\music-library-96.ico"},
            {"VideosIcon", "FileManager.Resources\\ArtWork\\video-96.ico"},
        };

        public static string GetResourcePath(string resourceName) => PathHelper.RelativePath(_relativeResources[resourceName]);

        public static bool TryGetResourcePath(string resourceName, out string? resourcePath)
        {
            if(_relativeResources.ContainsKey(resourceName))
            {
                resourcePath = GetResourcePath(resourceName);
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
