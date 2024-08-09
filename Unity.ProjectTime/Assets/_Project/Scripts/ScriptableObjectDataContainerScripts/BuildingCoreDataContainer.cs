using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "BuildingCoreDataContainer", menuName = "ScriptableObjects/Building/BuildingCoreDataContainer", order = 0)]
    public class BuildingCoreDataContainer : ScriptableObject
    {
        public List<BuildingCoreData> buildingCoreDatas;
    }
}