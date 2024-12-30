using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.Services
{
    public class AppDataManager
    {
        private IDataProvider _dataProvider;
        private string _localDirectory;

        public AppDataManager(string directory)
        {
            _dataProvider = new XMLDataProvider();
            _localDirectory = directory;

            if(Directory.Exists(directory) == false)
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void Save<T>(string fileName, T data)
        {
            _dataProvider.Save(Path.Combine(_localDirectory, fileName), data);
        }

        public T Load<T>(string fileName)
        {
            return _dataProvider.Load<T>(Path.Combine(_localDirectory, fileName));
        }
    }
}
