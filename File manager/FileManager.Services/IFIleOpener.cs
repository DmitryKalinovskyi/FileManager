using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.Services
{
    public interface IFIleOpener
    {
        public void Open(string path);
    }
}
