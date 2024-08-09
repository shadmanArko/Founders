using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime._1._Repositories;
  
namespace _Project.Scripts.Pop_Clan_Culture
{
    public class PopManager
    {
        private readonly SaveDataScriptableObject _saveDataScriptableObject;

        public PopManager(SaveDataScriptableObject saveDataScriptableObject)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
        }

        public void AssignPopToBuilding(string buildingId, string popId)
        {
            var building = _saveDataScriptableObject.Save.Buildings.GetById(building1 => building1.Id, buildingId);
            var pop = _saveDataScriptableObject.Save.Pops.GetById(pop1 => pop1.Id, popId);
            building.PopIds.Add(popId);
            pop.BuildingId = buildingId;
        }
        public void RemovePopFromBuilding(string buildingId, string popId)
        {
            var building = _saveDataScriptableObject.Save.Buildings.GetById(building1 => building1.Id, buildingId);
            var pop = _saveDataScriptableObject.Save.Pops.GetById(pop1 => pop1.Id, popId);
            // building.PopId = null;
            pop.BuildingId = null;
        }
        public void AssignPopToInstitution(string institutionId, string popId)
        {
            var institution = _saveDataScriptableObject.Save.Buildings.GetById(building1 => building1.Id, institutionId);
            var pop = _saveDataScriptableObject.Save.Pops.GetById(pop1 => pop1.Id, popId);
            // institution.PopId = popId;
            pop.BuildingId = institutionId;
        }
        public void RemovePopFromInstitution(string institutionId, string popId)
        {
            var institution = _saveDataScriptableObject.Save.Buildings.GetById(building1 => building1.Id, institutionId);
            var pop = _saveDataScriptableObject.Save.Pops.GetById(pop1 => pop1.Id, popId);
            // institution.PopId = null;
            pop.BuildingId = null;
        }
        public void AssignPopToArmy(string regimentId, string popId)
        {
            var army = _saveDataScriptableObject.Save.Units.GetById(unit => unit.Id, regimentId);
            var pop = _saveDataScriptableObject.Save.Pops.GetById(pop1 => pop1.Id, popId);
            army.PopIds.Add(popId);
            pop.UnitId = regimentId;
        }
        public void RemovePopFromArmy(string regimentId, string popId)
        {
            var army = _saveDataScriptableObject.Save.Units.GetById(unit => unit.Id, regimentId);
            var pop = _saveDataScriptableObject.Save.Pops.GetById(pop1 => pop1.Id, popId);
            army.PopIds.Remove(popId);
            pop.UnitId = null;
        }
        public void AssignPopToScout(string scoutId, string popId)
        {
            var scout = _saveDataScriptableObject.Save.Units.GetById(unit => unit.Id, scoutId);
            var pop = _saveDataScriptableObject.Save.Pops.GetById(pop1 => pop1.Id, popId);
            scout.PopIds.Add(popId);
            pop.UnitId = scoutId;
        }
        public void RemovePopFromScout(string scoutId, string popId)
        {
            var scout = _saveDataScriptableObject.Save.Units.GetById(unit => unit.Id, scoutId);
            var pop = _saveDataScriptableObject.Save.Pops.GetById(pop1 => pop1.Id, popId);
            scout.PopIds.Remove(popId);
            pop.UnitId = null;
        }
        public string CheckPop(string popId)
        {
            return popId;
        }
    }
}