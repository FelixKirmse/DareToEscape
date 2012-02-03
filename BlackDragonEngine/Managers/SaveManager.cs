using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;

namespace BlackDragonEngine.Managers
{
    public class SaveManager<T>
    {
        public readonly string SaveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) +
                                              @"\DareToEscape\saves\";

        public readonly EventHelper SaveHelper = new EventHelper();
        public T CurrentSaveState;

        public string CurrentSaveFile
        {
            get { return SaveFilePath + GetMD5Hash(VariableProvider.SaveSlot) + ".svf"; }
        }

        public void Save()
        {
            SaveHelper.SaveHelp();
            Save(VariableProvider.SaveSlot);
        }

        public void Save(string saveSlot)
        {
            if (!Directory.Exists(SaveFilePath))
                Directory.CreateDirectory(SaveFilePath);
            using (var fs = new FileStream(SaveFilePath + GetMD5Hash(saveSlot) + ".svf", FileMode.Create))
            using (var gzs = new GZipStream(fs, CompressionMode.Compress))
            {
                var xmls = new XmlSerializer(CurrentSaveState.GetType());
                xmls.Serialize(gzs, CurrentSaveState);
            }
        }

        public void Load(string saveSlot)
        {
            using (var fs = new FileStream(SaveFilePath + GetMD5Hash(saveSlot) + ".svf", FileMode.Open))
            using (var gzs = new GZipStream(fs, CompressionMode.Decompress))
            {
                var xmls = new XmlSerializer(CurrentSaveState.GetType());
                CurrentSaveState = (T) xmls.Deserialize(gzs);
            }
            SaveHelper.LoadHelp();
        }

        private string GetMD5Hash(string textToHash)
        {
            if (string.IsNullOrEmpty(textToHash))
            {
                return string.Empty;
            }
            var md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(textToHash);
            byte[] result = md5.ComputeHash(bytes);

            return BitConverter.ToString(result);
        }
    }
}