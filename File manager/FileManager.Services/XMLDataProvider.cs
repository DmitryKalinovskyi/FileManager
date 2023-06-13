using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace File_manager.FileManager.Services
{
    internal class XMLDataProvider<T> : IDataProvider<T>
    {
        private string filePath;

        public XMLDataProvider(string filePath)
        {
            this.filePath = filePath;
        }

        public T Load()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new DataLoadException("Error loading data.", ex);
            }
        }

        public void Save(T data)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                throw new DataSaveException("Error saving data.", ex);
            }
        }
    }

    
}
