using System;
using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class Unit
    {
        public string Id;
        public string Name;
        public string CurrentTileId;
        public List<string> PopIds;

        public Unit(string id, string name, List<string> popIds)
        {
            Id = id;
            Name = name;
            PopIds = popIds;
        }
    }
}
