using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "BuildingVariationsContainer", menuName = "ScriptableObjects/Building/BuildingVariationsContainer", order = 0)]
    public class BuildingVariationsContainer : ScriptableObject
    {
        public List<BuildingVariation> buildingVariations;
    }
}