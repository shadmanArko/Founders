using System;
using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class SubcontinentTiles : Base
    {
        public string SubcontinentName;
        public List<Tile> Tiles;

    }
}