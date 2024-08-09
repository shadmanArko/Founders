using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "NewGameSettingsData", menuName = "ScriptableObjects/NewGameSettingsData", order = 0)] 
    public class NewGameSettingsData : ScriptableObject
    {
        public string subcontinentName;
        public PlayableCulture playableCulture;
    }
}
