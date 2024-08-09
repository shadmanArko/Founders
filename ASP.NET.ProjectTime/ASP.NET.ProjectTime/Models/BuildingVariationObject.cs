using System;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class BuildingVariationObject
    {
        public string VariationObjectAsset;
        public float VariationObjectPositionZ;

        public BuildingVariationObject(string variationObjectAsset, float variationObjectPositionZ)
        {
            VariationObjectAsset = variationObjectAsset;
            VariationObjectPositionZ = variationObjectPositionZ;
        }
    }
}