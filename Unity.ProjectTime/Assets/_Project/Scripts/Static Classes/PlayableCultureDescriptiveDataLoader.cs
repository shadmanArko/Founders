using _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Static_Classes
{
    
    public static class PlayableCultureDescriptiveDataLoader
    {
        private static PlayableCultureDescriptiveDataSo _loadedObject =
            Resources.Load<PlayableCultureDescriptiveDataSo>("ScriptableObjects/playableCultureDescriptiveDataSo");
        public static string GetPlayableCultureName(this PlayableCulture playableCulture)
        {
            
            foreach (var playableCultureDescriptiveData in _loadedObject.playableCultureDescriptiveDatas)
            {
                if (playableCultureDescriptiveData.label == playableCulture.label)
                {
                    return playableCultureDescriptiveData.name;
                }
                
            }

            return $"{playableCulture.label} not localized";
        }
    }
}