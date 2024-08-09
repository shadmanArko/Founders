using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;

namespace ASP.NET.ProjectTime.DataContext
{
    [System.Serializable]
    public class SaveData
    {
        public Save Save;

        public SaveData(Save save)
        {
            Save = save;
        }
    }
}