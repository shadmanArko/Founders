
using System;
using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class NaturalResource : Base
    {
        public string Name;
        public string Category;
        public string IconName;
        public List<ResourceLocations> resourceLocationsList;
        public string ExtractionBuilding;
        public bool IsDepletable;
        public bool GivesWorkAnimalModifier;
        public List<string> TradeGoods;

    }
}