using System;
using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class MandatoryRiver
    {
        public List<Position2> riverOriginChunks;
        public List<Position2> riverMouthChunks;
    }
}