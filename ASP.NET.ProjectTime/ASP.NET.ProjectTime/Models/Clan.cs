using System;
using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class Clan : Base
    {
        public string Name;
        public int CreatedPopCounter;

        public Clan(string id ,string name, int createdPopCounter)
        {
            Id = id;
            Name = name;
            CreatedPopCounter = createdPopCounter;
        }
    }
}