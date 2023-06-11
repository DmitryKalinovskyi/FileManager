using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.Services.FileManaging
{
    public interface IFIleCreator
    {
        public void Create(string path, string filename);
    }
}
