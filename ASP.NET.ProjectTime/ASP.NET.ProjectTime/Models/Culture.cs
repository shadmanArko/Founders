using System;
using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class Culture
    {
        public string CultureReference;
        public string BuildingVariationClass;
        public string CultureController;
        public float YearlyResearchOutput;
        public float FoodStockPile;
        public float YearlyFoodBalance;
        public float YearlyFoodIncome;
        public float YearlyFoodProduction;
        public float YearlyFoodImport;
        public float YearlyFoodExpense;
        public float YearlyPopFoodConsumption;
        public float YearlyFoodDeterioration;
        public float YearlyFoodExport;
        public float GoldStockpile;
        public float YearlyGoldIncome;
        public float YearlyGoldProduction;
        public float YearlyTaxIncome;
        public float YearlyExportIncome;
        public float YearlyGoldExpense;
        public float BuildingMaterialsStockpile;
        public float YearlyBuildingMaterialsIncome;
        public float YearlyBuildingMaterialProduction;
        public float YearlyBuildingMaterialImport;
        public float YearlyBuildingMaterialExpense;
        public float YearlyBuildingMaterialConsumption;
        public float YearlyBuildingMaterialExport;
        public List<string> ClanIds;
        public List<string> PopIds;
        public List<string> TileIds;
        public float TradeGoodXStockpile;
        public float YearlyTradeGoodXIncome;
        public float YearlyTradeGoodXProduction;
        public float YearlyTradeGoodXImport;
        public float YearlyTradeGoodXExpense;
        public float YearlyTradeGoodXConsumption;
        public float YearlyTradeGoodXExport;
        public float SpiritualAuthorityStockpile;
        public float YearlySpiritualAuthorityIncome;
        public float YearlySpiritualAuthorityExpense;
        public float MilitaryAuthorityStockpile;
        public float YearlyMilitaryAuthorityIncome;
        public float YearlyMilitaryAuthorityExpense;
        public float AdministrativeAuthorityStockpile;
        public float YearlyAdministrativeAuthorityIncome;
        public float YearlyAdministrativeAuthorityExpense;
        public List<string> UnitIds;


        public Culture(string cultureReference, string cultureController, float yearlyResearchOutput, float foodStockPile, float yearlyFoodBalance, float yearlyFoodIncome, float yearlyFoodProduction, float yearlyFoodImport, float yearlyFoodExpense, float yearlyPopFoodConsumption, float yearlyFoodDeterioration, float yearlyFoodExport, float goldStockpile, float yearlyGoldIncome, float yearlyGoldProduction, float yearlyTaxIncome, float yearlyExportIncome, float yearlyGoldExpense, float buildingMaterialsStockpile, float yearlyBuildingMaterialsIncome, float yearlyBuildingMaterialProduction, float yearlyBuildingMaterialImport, float yearlyBuildingMaterialExpense, float yearlyBuildingMaterialConsumption, float yearlyBuildingMaterialExport, List<string> clanIds, List<string> popIds, List<string> tileIds, float tradeGoodXStockpile, float yearlyTradeGoodXIncome, float yearlyTradeGoodXProduction, float yearlyTradeGoodXImport, float yearlyTradeGoodXExpense, float yearlyTradeGoodXConsumption, float yearlyTradeGoodXExport, float spiritualAuthorityStockpile, float yearlySpiritualAuthorityIncome, float yearlySpiritualAuthorityExpense, float militaryAuthorityStockpile, float yearlyMilitaryAuthorityIncome, float yearlyMilitaryAuthorityExpense, float administrativeAuthorityStockpile, float yearlyAdministrativeAuthorityIncome, float yearlyAdministrativeAuthorityExpense, List<string> unitIds, string buildingVariationClass)
        {
            CultureReference = cultureReference;
            CultureController = cultureController;
            YearlyResearchOutput = yearlyResearchOutput;
            FoodStockPile = foodStockPile;
            YearlyFoodBalance = yearlyFoodBalance;
            YearlyFoodIncome = yearlyFoodIncome;
            YearlyFoodProduction = yearlyFoodProduction;
            YearlyFoodImport = yearlyFoodImport;
            YearlyFoodExpense = yearlyFoodExpense;
            YearlyPopFoodConsumption = yearlyPopFoodConsumption;
            YearlyFoodDeterioration = yearlyFoodDeterioration;
            YearlyFoodExport = yearlyFoodExport;
            GoldStockpile = goldStockpile;
            YearlyGoldIncome = yearlyGoldIncome;
            YearlyGoldProduction = yearlyGoldProduction;
            YearlyTaxIncome = yearlyTaxIncome;
            YearlyExportIncome = yearlyExportIncome;
            YearlyGoldExpense = yearlyGoldExpense;
            BuildingMaterialsStockpile = buildingMaterialsStockpile;
            YearlyBuildingMaterialsIncome = yearlyBuildingMaterialsIncome;
            YearlyBuildingMaterialProduction = yearlyBuildingMaterialProduction;
            YearlyBuildingMaterialImport = yearlyBuildingMaterialImport;
            YearlyBuildingMaterialExpense = yearlyBuildingMaterialExpense;
            YearlyBuildingMaterialConsumption = yearlyBuildingMaterialConsumption;
            YearlyBuildingMaterialExport = yearlyBuildingMaterialExport;
            ClanIds = clanIds;
            PopIds = popIds;
            TileIds = tileIds;
            TradeGoodXStockpile = tradeGoodXStockpile;
            YearlyTradeGoodXIncome = yearlyTradeGoodXIncome;
            YearlyTradeGoodXProduction = yearlyTradeGoodXProduction;
            YearlyTradeGoodXImport = yearlyTradeGoodXImport;
            YearlyTradeGoodXExpense = yearlyTradeGoodXExpense;
            YearlyTradeGoodXConsumption = yearlyTradeGoodXConsumption;
            YearlyTradeGoodXExport = yearlyTradeGoodXExport;
            SpiritualAuthorityStockpile = spiritualAuthorityStockpile;
            YearlySpiritualAuthorityIncome = yearlySpiritualAuthorityIncome;
            YearlySpiritualAuthorityExpense = yearlySpiritualAuthorityExpense;
            MilitaryAuthorityStockpile = militaryAuthorityStockpile;
            YearlyMilitaryAuthorityIncome = yearlyMilitaryAuthorityIncome;
            YearlyMilitaryAuthorityExpense = yearlyMilitaryAuthorityExpense;
            AdministrativeAuthorityStockpile = administrativeAuthorityStockpile;
            YearlyAdministrativeAuthorityIncome = yearlyAdministrativeAuthorityIncome;
            YearlyAdministrativeAuthorityExpense = yearlyAdministrativeAuthorityExpense;
            UnitIds = unitIds;
            BuildingVariationClass = buildingVariationClass;
        }
    }
}