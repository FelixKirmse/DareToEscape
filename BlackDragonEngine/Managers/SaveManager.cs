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
    public static class SaveManager<T>
    {
        public static T CurrentSaveState;

        public static readonly string SaveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) +
                                                     @"\DareToEscape\saves\";

        public static EventHelper SaveHelper = new EventHelper();

        public static string CurrentSaveFile
        {
            get { return SaveFilePath + GetMD5Hash(VariableProvider.SaveSlot) + ".svf"; }
        }

        public static void Save()
        {
            SaveHelper.SaveHelp();
            Save(VariableProvider.SaveSlot);
        }

        public static void Save(string saveSlot)
        {
            if (!Directory.Exists(SaveFilePath))
                Directory.CreateDirectory(SaveFilePath);
            var fs = new FileStream(SaveFilePath + GetMD5Hash(saveSlot) + ".svf", FileMode.Create);
            var gzs = new GZipStream(fs, CompressionMode.Compress);
            var xmls = new XmlSerializer(CurrentSaveState.GetType());
            xmls.Serialize(gzs, CurrentSaveState);
            gzs.Close();
            fs.Close();
        }

        public static void Load(string saveSlot)
        {
            var fs = new FileStream(SaveFilePath + GetMD5Hash(saveSlot) + ".svf", FileMode.Open);
            var gzs = new GZipStream(fs, CompressionMode.Decompress);
            var xmls = new XmlSerializer(CurrentSaveState.GetType());
            CurrentSaveState = (T) xmls.Deserialize(gzs);
            gzs.Close();
            fs.Close();
            SaveHelper.LoadHelp();
        }

        private static string GetMD5Hash(string TextToHash)
        {
            if (string.IsNullOrEmpty(TextToHash))
            {
                return string.Empty;
            }
            var md5 = new MD5CryptoServiceProvider();
            var textToHash = Encoding.Default.GetBytes(TextToHash);
            var result = md5.ComputeHash(textToHash);

            return BitConverter.ToString(result);
        }
    }
}