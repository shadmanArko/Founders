using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models.Units.ArmyUnit
{
    public class Regiment : Base
    {
        public string Name;
        public string Type;
        public List<string> PopIds;

        public Regiment(string id, string name, string type, List<string> popIds)
        {
            Id = id;
            Name = name;
            Type = type;
            PopIds = popIds;
        }
    }
}