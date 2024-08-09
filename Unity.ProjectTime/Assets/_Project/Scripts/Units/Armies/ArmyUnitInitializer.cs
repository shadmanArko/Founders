using System.Collections.Generic;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Helpers;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Models.Units.ArmyUnit;

namespace _Project.Scripts.Units.Armies
{
    public class ArmyUnitInitializer
    {
        private readonly SaveDataScriptableObject _saveDataScriptableObject;

        public ArmyUnitInitializer(SaveDataScriptableObject saveDataScriptableObject)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
        }

        public void InitializeGameObject()
        {
            
        }

        public void CreateNewRegiment(string type, List<string> popIds, string armyId)
        {
            var regiment = new Regiment(NewIdGenerator.GenerateNewId(), "", type, popIds);
            var army = _saveDataScriptableObject.Save.Armies.GetById(army => army.Id, armyId);
            army.Regiments.Add(regiment);
        }

        public void CreateNewArmyUnit(string unitId)
        {
            //TODO: Take the pop list that are set in regiments from the military panel
            var army = new Army(NewIdGenerator.GenerateNewId(), unitId, "");
        }
        
    }
}