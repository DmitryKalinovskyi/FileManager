using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_manager.FileManager.Services
{
    public interface IDataProvider
    {
        public void Save<T>(string path, T data);

        public T Load<T>(string path);
    }

    public class DataLoadException : Exception
    {
        public DataLoadException(string message) : base(message)
        {
        }

        public DataLoadException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class DataSaveException : Exception
    {
        public DataSaveException(string message) : base(message)
        {
        }

        public DataSaveException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
