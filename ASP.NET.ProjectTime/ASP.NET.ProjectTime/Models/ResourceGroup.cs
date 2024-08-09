using System;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class ResourceGroup
    {
        public string resourceGroupName;
        public int numberOfResourceToSpawn;
        public bool isMandatory;
        public int optionalProbability;
    }
}