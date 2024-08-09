using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "CulturalClanNameEndersDataContainer", menuName = "ScriptableObjects/CulturalClanNameEnders", order = 0)]
    public class CulturalClanNameEnders : ScriptableObject
    {
        public List<string> culturalClanNameEnders;
    }
}