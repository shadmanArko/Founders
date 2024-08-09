using System;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.zzz_Testing.Mod_Testing
{
    public class GetMassage : MonoBehaviour
    {
    
        string jsonFilePath = @"C:\Users\User\Documents\_Work_Dont Touch\_Unity Projects\ProjectTime\Mods\01\Assembly.Json";

        private Assembly assembly;
        public string dllPath;

        public TMP_Text massage;

        public TMP_Text dllFilePath;
        // Start is called before the first frame update
        void Start()
        {
            jsonFilePath = @"C:\Users\User\Documents\_Work_Dont Touch\_Unity Projects\ProjectTime\Mods\01\Assembly.Json";
       
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        [ContextMenu("Init")]
        public void Init()
        {
            LoadDllsFromJson(jsonFilePath);
            var type = assembly.GetType("Test02.Class1");
            var obj = Activator.CreateInstance(type);
            var method = type.GetMethod("SendTheMassage");
            var msg = method.Invoke(obj, new object[]{});
            massage.text = msg.ToString();
        }

        [ContextMenu("Fire")]
        public void Fire()
        {
       
        }
    
        void LoadDllsFromJson(string jsonFilePath)
        {
            // var dataFileReader = new StreamReader(jsonFilePath);
            // var dataJson = await  dataFileReader.ReadToEndAsync();
            // Read the JSON file
            //string jsonContent = File.ReadAllText(jsonFilePath);
            //JsonUtility.FromJsonOverwrite(dataJson, dllPath);
            // dllPath = JsonUtility.FromJson<string>(dataJson);
            dllPath = File.ReadAllText(jsonFilePath);

            dllFilePath.text = dllPath;
            // string[] dllPaths = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(jsonContent);
            assembly = Assembly.LoadFrom(dllPath);

            // Load the DLLs
            // foreach (string dllPath in dllPaths)
            // {
            //     
            //     assembly = Assembly.LoadFrom(dllPath);
            //     
            //     
            //     // try
            //     // {
            //     //     Assembly assembly = Assembly.LoadFrom(dllPath);
            //     //
            //     //     // Do something with the loaded assembly
            //     //     // For example, you can access types or call methods from the assembly
            //     //
            //     //     //Console.WriteLine("Loaded DLL: " + dllPath);
            //     // }
            //     // catch (Exception ex)
            //     // {
            //     //     Console.WriteLine("Failed to load DLL: " + dllPath);
            //     //     Console.WriteLine(ex.Message);
            //     // }
            // }
        }
    }
}
