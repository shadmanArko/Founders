using System;
using System.Collections.Generic;
using _Project.Scripts.Buildings;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class Building : Base
    {
        public string Label;
        public string Category;
        public float BuildingMaterialCost;
        public float GoldCost;
        public int MinimumPopCapacity;
        public int MaximumPopCapacity;
        public int BuildingTime;    //TODO: MUST BE CHANGED TO WEEK
        // public string BuildingSize;
        public BuildingVariation BuildingVariation;
        public BuildingSize BuildingSize;
        public BuildingSlot BuildingSlot;
        public string TileId;
        public List<string> PopIds;
        public float XPosition;
        public float YPosition;
        public float ZPosition;

        public Building(string id, string label, float buildingMaterialCost, float goldCost, int minimumPopCapacity, int maximumPopCapacity, int buildingTime, BuildingVariation buildingVariation, BuildingSize buildingSize, BuildingSlot buildingSlot, string tileId, List<string> popIds, float xPosition, float yPosition, float zPosition)
        {
            Id = id;
            Label = label;
            BuildingMaterialCost = buildingMaterialCost;
            GoldCost = goldCost;
            MinimumPopCapacity = minimumPopCapacity;
            MaximumPopCapacity = maximumPopCapacity;
            BuildingTime = buildingTime;
            BuildingVariation = buildingVariation;
            BuildingSize = buildingSize;
            BuildingSlot = buildingSlot;
            TileId = tileId;
            PopIds = popIds;
            XPosition = xPosition;
            YPosition = yPosition;
            ZPosition = zPosition;
        }
    }
}