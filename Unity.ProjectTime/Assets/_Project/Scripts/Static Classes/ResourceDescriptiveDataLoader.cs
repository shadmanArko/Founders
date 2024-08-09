using _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Static_Classes
{
    
    public static class ResourceDescriptiveDataLoader
    {
        private static ResourcesDescriptiveDataObject loadedObject = Resources.Load<ResourcesDescriptiveDataObject>("ScriptableObjects/ResourcesDescriptiveDataScriptableObject");
        public static string GetResourceName(this NaturalResource naturalResource)
        {
            if (loadedObject == null)
            {
                loadedObject =  Resources.Load<ResourcesDescriptiveDataObject>("ScriptableObjects/ResourcesDescriptiveDataScriptableObject");
            }
            foreach (var resourceDescriptiveData in loadedObject.naturalResourceDescriptiveDatas)
            {
                if (resourceDescriptiveData.label == naturalResource.IconName)
                {
                    return resourceDescriptiveData.name;
                }
                
            }

            return $"{naturalResource.Name} not localized";
        }
    }
}
