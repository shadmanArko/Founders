using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NET.ProjectTime.DataContext
{
    public abstract class DataContext
    {        
        public SaveData SaveData ;

        public abstract Task Load();
        public abstract Task Save();

        public Save Set<T>()
        {
            if (typeof(T) == typeof(SaveData))
            {
                return SaveData.Save;
            }
            return null;
        }
    }
}