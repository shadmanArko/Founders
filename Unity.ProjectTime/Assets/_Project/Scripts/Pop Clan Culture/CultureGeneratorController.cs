using System.Collections.Generic;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.Helpers;
using ASP.NET.ProjectTime.Models;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Pop_Clan_Culture
{
    public class CultureGeneratorController
    {
        private readonly ClanAndPopGeneratorController _clanAndPopGeneratorController;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly NewGameSettingsData _newGameSettingsData;

        public CultureGeneratorController(NewGameSettingsData newGameSettingsData, ClanAndPopGeneratorController clanAndPopGeneratorController, SaveDataScriptableObject saveDataScriptableObject)
        {
            _newGameSettingsData = newGameSettingsData;
            _clanAndPopGeneratorController = clanAndPopGeneratorController;
            _saveDataScriptableObject = saveDataScriptableObject;
        }


        #region Culture Generator for New Game

        public void CultureGenerator_NewGame()
        {
            
        }

        public async UniTask GenerateCulture_OnStartNewGame()
        {
            _saveDataScriptableObject.Save.AllCultures.Clear();
            var cultureData = new CultureData(NewIdGenerator.GenerateNewId(), CreateCultureWithZeroValues());
            //cultureData.Culture = new Culture();
            var (clanList, popList) = await _clanAndPopGeneratorController.GenerateClanAndPopOnStart_NewGame(); 

            foreach (var clan in clanList)
            {
                cultureData.Culture.ClanIds.Add(clan.Id);
            }

            foreach (var pop in popList)
            {
                cultureData.Culture.PopIds.Add(pop.Id);
            }

            cultureData.Culture.BuildingVariationClass = _newGameSettingsData.playableCulture.buildingVariationClass;
            _saveDataScriptableObject.Save.AllCultures.Add(cultureData);
        }
        
        private Culture CreateCultureWithZeroValues()
        {
            // Create a new 'Culture' instance with all float values set to 0
            return new Culture(
                cultureReference: "",
                buildingVariationClass:"",
                cultureController: "",
                yearlyResearchOutput: 0.0f,
                foodStockPile: 0.0f,
                yearlyFoodBalance: 0.0f,
                yearlyFoodIncome: 0.0f,
                yearlyFoodProduction: 0.0f,
                yearlyFoodImport: 0.0f,
                yearlyFoodExpense: 0.0f,
                yearlyPopFoodConsumption: 0.0f,
                yearlyFoodDeterioration: 0.0f,
                yearlyFoodExport: 0.0f,
                goldStockpile: 0.0f,
                yearlyGoldIncome: 0.0f,
                yearlyGoldProduction: 0.0f,
                yearlyTaxIncome: 0.0f,
                yearlyExportIncome: 0.0f,
                yearlyGoldExpense: 0.0f,
                buildingMaterialsStockpile: 0.0f,
                yearlyBuildingMaterialsIncome: 0.0f,
                yearlyBuildingMaterialProduction: 0.0f,
                yearlyBuildingMaterialImport: 0.0f,
                yearlyBuildingMaterialExpense: 0.0f,
                yearlyBuildingMaterialConsumption: 0.0f,
                yearlyBuildingMaterialExport: 0.0f,
                clanIds: new List<string>(),
                popIds: new List<string>(),
                tileIds: new List<string>(),
                tradeGoodXStockpile: 0.0f,
                yearlyTradeGoodXIncome: 0.0f,
                yearlyTradeGoodXProduction: 0.0f,
                yearlyTradeGoodXImport: 0.0f,
                yearlyTradeGoodXExpense: 0.0f,
                yearlyTradeGoodXConsumption: 0.0f,
                yearlyTradeGoodXExport: 0.0f,
                spiritualAuthorityStockpile: 0.0f,
                yearlySpiritualAuthorityIncome: 0.0f,
                yearlySpiritualAuthorityExpense: 0.0f,
                militaryAuthorityStockpile: 0.0f,
                yearlyMilitaryAuthorityIncome: 0.0f,
                yearlyMilitaryAuthorityExpense: 0.0f,
                administrativeAuthorityStockpile: 0.0f,
                yearlyAdministrativeAuthorityIncome: 0.0f,
                yearlyAdministrativeAuthorityExpense: 0.0f,
                unitIds: new List<string>()
                );
        }
        
        

        #endregion
    }
}