using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models.Units.ArmyUnit
{
    public class Army : Base
    {
        public string UnitId;
        public string Name;
        public List<Regiment> Regiments;

        public Army(string id, string unitId, string name)
        {
            Id = id;
            UnitId = unitId;
            Name = name;
            Regiments = new List<Regiment>();
        }
    }
}