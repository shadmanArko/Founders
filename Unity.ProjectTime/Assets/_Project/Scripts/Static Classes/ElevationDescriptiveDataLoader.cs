using _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Static_Classes
{
    
    public static class ElevationDescriptiveDataLoader
    {
        private static ElevationDescriptiveDataSo _loadedObject =
            Resources.Load<ElevationDescriptiveDataSo>("ScriptableObjects/ElevationDescriptiveDataSo");
        public static string GetElevationName(this string elevationLabel)
        {
            
            foreach (var elevationDescriptiveData in _loadedObject.elevationDescriptiveDatas)
            {
                if (elevationDescriptiveData.label == elevationLabel)
                {
                    return elevationDescriptiveData.name;
                }
                
            }

            return "Elevation Data Not Found!";
        }
    }
}
