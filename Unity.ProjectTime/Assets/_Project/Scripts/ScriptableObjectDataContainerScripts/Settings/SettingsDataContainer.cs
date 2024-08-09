using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts.Settings
{
    [CreateAssetMenu(fileName = "SettingsDataContainer", menuName = "ScriptableObjects/SettingsDataContainer", order = 0)]
    public class SettingsDataContainer : ScriptableObject
    {
        public string gameLanguage = "English";
    }
}