using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class NamingSet : Base
    {
        public string cultureGroup;
        public List<string> tileNamePrefix;
        public List<string> tileNameSuffix;
        public List<string> clanNameEnders;
    }
}