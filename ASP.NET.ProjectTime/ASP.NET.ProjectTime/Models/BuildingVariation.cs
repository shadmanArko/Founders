using System;
using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class BuildingVariation
    {
        public string BuildingName;
        public string VariationLabel;
        public string VariationClass;
        public string BuildingCategory;
        public PngAndAddressableFileLocation BuildingSurfacePngFileLocation;
        public PngAndAddressableFileLocation Row1PngFileLocation;
        public PngAndAddressableFileLocation Row2PngFileLocation;
        public PngAndAddressableFileLocation Row3PngFileLocation;
        public PngAndAddressableFileLocation Row4PngFileLocation;
        public PngAndAddressableFileLocation Row5PngFileLocation;

        public BuildingVariation(string buildingName, string variationLabel, string variationClass, string buildingCategory, PngAndAddressableFileLocation buildingSurfacePngFileLocation, PngAndAddressableFileLocation row1PngFileLocation, PngAndAddressableFileLocation row2PngFileLocation, PngAndAddressableFileLocation row3PngFileLocation, PngAndAddressableFileLocation row4PngFileLocation, PngAndAddressableFileLocation row5PngFileLocation)
        {
            BuildingName = buildingName;
            VariationLabel = variationLabel;
            VariationClass = variationClass;
            BuildingCategory = buildingCategory;
            BuildingSurfacePngFileLocation = buildingSurfacePngFileLocation;
            Row1PngFileLocation = row1PngFileLocation;
            Row2PngFileLocation = row2PngFileLocation;
            Row3PngFileLocation = row3PngFileLocation;
            Row4PngFileLocation = row4PngFileLocation;
            Row5PngFileLocation = row5PngFileLocation;
        }
    }
}