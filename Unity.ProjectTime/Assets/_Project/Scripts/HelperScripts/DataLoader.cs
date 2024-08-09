using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.HelperScripts
{
    public static class DataLoader
    {
        public static async Task GetDataFromJson<T>(T item, string dir)
        {
            if (!File.Exists(dir))
            {
                return;
            }
            
            using var dataFileReader = new StreamReader(dir);
            var dataJson = await dataFileReader.ReadToEndAsync();
            
            if (string.IsNullOrEmpty(dataJson)) 
            {
                Debug.LogError("Failed to read JSON file at " + dir);
                return;
            }
            
            JsonUtility.FromJsonOverwrite(dataJson, item);

        }
    }
}