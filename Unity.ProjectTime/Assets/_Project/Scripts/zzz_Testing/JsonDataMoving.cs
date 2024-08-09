using System.IO;
using UnityEngine;

namespace _Project.Scripts.zzz_Testing
{
    public class JsonDataMoving : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
            MoveFolder();
        }

        public void SaveTextToFile(string fileName, string content)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "../../"+fileName);
            File.WriteAllText(filePath, content);
        }
        public void MoveFolder()
        {
            string sourceFolderPath = Path.Combine(Application.streamingAssetsPath, "Original Data (Do Not Edit)");
            string destinationFolderPath = Path.Combine(Application.dataPath, "CoreData");

            if (!Directory.Exists(destinationFolderPath))
                Directory.CreateDirectory(destinationFolderPath);

            CopyFolderContents(sourceFolderPath, destinationFolderPath);
        }


        private void CopyFolderContents(string sourceFolderPath, string destinationFolderPath)
        {
            string[] files = Directory.GetFiles(sourceFolderPath);
            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                string destinationPath = Path.Combine(destinationFolderPath, fileName);
                File.Copy(filePath, destinationPath, true);
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
    
    }
}
