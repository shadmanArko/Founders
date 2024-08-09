using _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Static_Classes
{
    
    public static class FeatureDescriptiveDataLoader
    {
        private static FeatureDescriptiveDataSo _loadedObject =
            Resources.Load<FeatureDescriptiveDataSo>("ScriptableObjects/FeatureDescriptiveDataSo");
        public static string GetFeatureName(this string terrainLabel)
        {
            
            foreach (var featureDescriptiveData in _loadedObject.featureDescriptiveDatas)
            {
                if (featureDescriptiveData.label == terrainLabel)
                {
                    return featureDescriptiveData.name;
                }
                
            }

            return "";
        }
    }
}
