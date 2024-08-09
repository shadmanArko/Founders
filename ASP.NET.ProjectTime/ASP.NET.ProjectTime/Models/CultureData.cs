using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models
{
    [System.Serializable]
    public class CultureData : Base
    {
        // public List<TileCoordinates> TileCoordinatesList;
        // public List<Pop> Pops;

        public Culture Culture;

        public CultureData(string id, Culture culture)
        {
            Id = id;
            Culture = culture;
        }
    }
}