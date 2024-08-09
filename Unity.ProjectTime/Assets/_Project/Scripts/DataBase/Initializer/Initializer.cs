using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.DataBase.Initializer
{
    public class Initializer
    {

        public void LoadCoreDataFile()
        {
            var sourceFolderPath = Path.Combine(Application.dataPath, "CoreData");
            var coreDataFilePath = Path.Combine(Application.persistentDataPath, "CoreData");
             
            
            if (!Directory.Exists(coreDataFilePath))
            {
                Directory.CreateDirectory(coreDataFilePath);
            }
            
            CopyFolderContents(sourceFolderPath, coreDataFilePath);
        }
        
        // public void MoveFolder()
        // {
        //     string sourceFolderPath = Path.Combine(Application.streamingAssetsPath, "Original Data (Do Not Edit)");
        //     string destinationFolderPath = Path.Combine(Application.dataPath, "CoreData");
        //     
        //     CopyFolderContents(sourceFolderPath, destinationFolderPath);
        // }
        
        
        private void CopyFolderContents(string sourceFolderPath, string destinationFolderPath)
        {
            string[] files = Directory.GetFiles(sourceFolderPath);
            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                string destinationPath = Path.Combine(destinationFolderPath, fileName);

                if (File.Exists(destinationPath))
                {
                    // Compare the content of the existing file and the new file
                    if (File.ReadAllText(filePath) != File.ReadAllText(destinationPath))
                    {
                        File.Copy(filePath, destinationPath, true);
                    }
                }
                else
                {
                    File.Copy(filePath, destinationPath, true);
                }
            } 
        
            string[] folders = Directory.GetDirectories(sourceFolderPath);
            foreach (string folderPath in folders)
            {
                string folderName = Path.GetFileName(folderPath);
                string destinationPath = Path.Combine(destinationFolderPath, folderName);
                if (!Directory.Exists(destinationPath))
                    Directory.CreateDirectory(destinationPath);
                CopyFolderContents(folderPath, destinationPath);
            }
        
        }
        
        public async Task CreateNewSaveFile(string saveFileName)
        {
            var rootSaveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
            
            if (!Directory.Exists(rootSaveDirectory))
            {
                Directory.CreateDirectory(rootSaveDirectory);
            }
            
            if (!File.Exists($"{Application.persistentDataPath}/Saves/{saveFileName}.json"))
            {
                await BootstrapSaveData(saveFileName);
            }
            else
            {
                //Todo Warning Massage to replace the same file which has same name
            }
        }

        public string[] GetDirectoryOfAllJsonFiles()
        {
            string[] jsonFiles = Directory.GetFiles(Path.Combine(Application.persistentDataPath, "Saves"), "*.json");

            return jsonFiles;
        }

        public string GetNameOfJsonFile(string fileDirectory)
        {
            string fileName = Path.GetFileNameWithoutExtension(fileDirectory);

            return fileName;
        }
        
        private async Task BootstrapSaveData(string saveFileName)
        {
            await using var writer = new StreamWriter($"{Application.persistentDataPath}/Saves/{saveFileName}.json");
            await writer.WriteAsync("");
        }
        
    }
}