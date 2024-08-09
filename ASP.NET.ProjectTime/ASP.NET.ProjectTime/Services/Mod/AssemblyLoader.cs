using System;
using System.IO;
using System.Reflection;

namespace ASP.NET.ProjectTime.Services.Mod
{
    public static class AssemblyLoader
    {
        
        
        public static string LoadDllsFromJson()
        {
            var dllPath = File.ReadAllText(@"C:\Users\User\Documents\_Work_Dont Touch\_Unity Projects\ProjectTime\Mods\01\Assembly.Json");

            var assembly = Assembly.LoadFrom(dllPath);
            
            var type = assembly.GetType("Test02.Class1");
            var obj = Activator.CreateInstance(type);
            var method = type.GetMethod("SendTheMassage");
            var msg = method.Invoke(obj, new object[]{});

            return msg.ToString();
        }
    }
}