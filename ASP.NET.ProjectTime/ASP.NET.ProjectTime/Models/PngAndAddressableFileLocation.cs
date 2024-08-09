using System;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class PngAndAddressableFileLocation
    {
        public bool IsAddressable;
        public string BundleName;
        public string BundleLocation;
        public string PngFileLocation;
    }
}