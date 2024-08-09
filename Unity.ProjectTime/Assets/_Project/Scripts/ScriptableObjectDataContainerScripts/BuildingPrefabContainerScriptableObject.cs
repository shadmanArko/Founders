using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "BuildingPrefabContainer", menuName = "ScriptableObjects/BuildingPrefabContainer", order = 0)]
    public class BuildingPrefabContainerScriptableObject : ScriptableObject
    {
        public GameObject smallBuilding;
        public GameObject mediumBuilding;
        public GameObject largeBuilding;
        public GameObject extraLargeBuilding;
    }
}