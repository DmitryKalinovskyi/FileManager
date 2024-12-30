using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.Model
{
    [Serializable]
    public class FileManagerModel
    {
        public FileAttributes AllowedAttributes { get; set; }

        public FileDisplayProperties DisplayProperties { get; set; }

        public List<string> Favorites { get; set; }

        public FileManagerModel()
        {
            Favorites = new();

            FileAttributes _allAttributes = Enum.GetValues(typeof(FileAttributes))
    .Cast<FileAttributes>()
    .Aggregate((current, next) => current | next);
            AllowedAttributes = _allAttributes;
        }
    }

    [Flags]
    public enum FileDisplayProperties
    {
        NameWithExtension = 1
    }
}
