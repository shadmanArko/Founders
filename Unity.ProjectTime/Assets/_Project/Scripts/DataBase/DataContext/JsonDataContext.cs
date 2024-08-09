using System.IO;
using System.Threading.Tasks;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.DataContext;
using UnityEngine;
using Zenject;

namespace SandBox.Arko.Scripts
{
    public class JsonDataContext : DataContext
    {
        private string SaveDataFilePath => $"{Application.persistentDataPath}/Saves/gg.json";

       
        
        [Inject]
        private SaveDataScriptableObject _saveDataScriptableObject;

        public JsonDataContext( SaveDataScriptableObject saveDataScriptableObject)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
        }

       
        public override async Task Load()
        {
            if (!File.Exists(SaveDataFilePath))
            {
                return;
            }

            using var saveDataFileReader = new StreamReader(SaveDataFilePath);
            var saveDataJson = await saveDataFileReader.ReadToEndAsync();

            if (string.IsNullOrEmpty(saveDataJson)) 
            {
                Debug.LogError("Failed to read JSON file at " + SaveDataFilePath);
                return;
            }

            SaveData jsonSaveData = JsonUtility.FromJson<SaveData>(saveDataJson);

            if (jsonSaveData == null) 
            {
                Debug.LogError("Failed to parse JSON data into PlayerData object");
                return;
            }
            

            // TODO Edit here

            _saveDataScriptableObject.Save = jsonSaveData.Save;
        }

        public override async Task Save()
        {
            SaveData saveData = new SaveData(_saveDataScriptableObject.Save);
            var json = JsonUtility.ToJson(saveData);
            await using var writer = new StreamWriter(SaveDataFilePath);
            await writer.WriteAsync(json);
        }
    }
}