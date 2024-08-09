using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Models.Units.ArmyUnit;

namespace ASP.NET.ProjectTime.DataContext
{
    [System.Serializable]
    public class Save
    {
        public float PulseSpeed;
        public string SaveName;
        public GameTime GameTime;
        public string ActiveSubcontinentTilesId;
        public List<SubcontinentTiles> AllSubcontinentTiles;
        public List<CultureData> AllCultures;
        public List<Clan> Clans;
        public List<Pop> Pops;
        public List<Building> Buildings;
        public List<Unit> Units;
        public List<Army> Armies;

        public Save(float pulseSpeed, List<CultureData> allCultures, List<Clan> clans, string activeSubcontinentTilesId, List<SubcontinentTiles> allSubcontinentTiles, List<Pop> pops, string saveName, GameTime gameTime, List<Building> buildings, List<Unit> units)
        {
            PulseSpeed = pulseSpeed;
            AllCultures = allCultures;
            ActiveSubcontinentTilesId = activeSubcontinentTilesId;
            AllSubcontinentTiles = allSubcontinentTiles;
            Pops = pops;
            SaveName = saveName;
            GameTime = gameTime;
            Buildings = buildings;
            Units = units;
            Clans = clans;
        }
    }
}