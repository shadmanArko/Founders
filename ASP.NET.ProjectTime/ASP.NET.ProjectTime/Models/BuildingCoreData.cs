using System;
using _Project.Scripts.Buildings;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class BuildingCoreData
    {
        public string Label;
        public string Category;
        public float BuildingMaterialCost;
        public float GoldCost;
        public int MinimumPopCapacity;
        public int MaximumPopCapacity;
        public int BuildingTime;
        public BuildingSize BuildingSize;
    }
}