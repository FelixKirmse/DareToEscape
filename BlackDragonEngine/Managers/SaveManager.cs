using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;
using BlackDragonEngine.Helpers;
using System.Runtime.Serialization.Formatters.Binary;
using BlackDragonEngine.Providers;
using System.Security.Cryptography;

namespace BlackDragonEngine.Managers
{
    public static class SaveManager<T>
    {      
        public static T CurrentSaveState;
        public static readonly string SaveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\DareToEscape\saves\";
        public static EventHelper SaveHelper = new EventHelper();

        public static string CurrentSaveFile
        {
            get 
            {
                return SaveFilePath + GetMD5Hash(VariableProvider.SaveSlot) + ".svf";
            }
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
            FileStream fs = new FileStream(SaveFilePath + GetMD5Hash(saveSlot) + ".svf", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, CurrentSaveState);
            fs.Close(); 
        }

        public static void Load(string saveSlot)
        {            
            FileStream fs = new FileStream(SaveFilePath + GetMD5Hash(saveSlot) + ".svf", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            CurrentSaveState = (T)formatter.Deserialize(fs);
            fs.Close();
            SaveHelper.LoadHelp();
        }        

        public static string GetMD5Hash(string TextToHash)
        {
            //Prüfen ob Daten übergeben wurden.
            if ((TextToHash == null) || (TextToHash.Length == 0))
            {
                return string.Empty;
            }

            //MD5 Hash aus dem String berechnen. Dazu muss der string in ein Byte[]
            //zerlegt werden. Danach muss das Resultat wieder zurück in ein string.
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);

            return System.BitConverter.ToString(result);
        } 
    }
}
