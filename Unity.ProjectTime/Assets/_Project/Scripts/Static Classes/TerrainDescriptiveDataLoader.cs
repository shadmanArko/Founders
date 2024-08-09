using _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Static_Classes
{
    
    public static class TerrainDescriptiveDataLoader
    {
        private static TerrainDescriptiveDataSo _loadedObject =
            Resources.Load<TerrainDescriptiveDataSo>("ScriptableObjects/TerrainDescriptiveDataSo");
        public static string GetTerrainName(this string terrainLabel)
        {
            
            foreach (var terrainDescriptiveData in _loadedObject.terrainDescriptiveDatas)
            {
                if (terrainDescriptiveData.label == terrainLabel)
                {
                    return terrainDescriptiveData.name;
                }
                
            }

            return "Terrain Data Not Found!";
        }
    }
}
