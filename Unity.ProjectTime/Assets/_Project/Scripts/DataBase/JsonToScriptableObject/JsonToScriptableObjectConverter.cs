using _Project.Scripts.HelperScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project.Scripts.DataBase.JsonToScriptableObject
{
    public class JsonToScriptableObjectConverter
    {

        private ResourcesScriptableObject _resourcesContainer;
        private ResourcesDescriptiveDataObject _resourcesDescriptiveDataObject;
        private TerrainDescriptiveDataSo _terrainDescriptiveDataSo;
        private ElevationDescriptiveDataSo _elevationDescriptiveDataSo;
        private FeatureDescriptiveDataSo _featureDescriptiveDataSo;
        private GameTextFieldDescriptiveDataSo _popNavigatorPopDetailsViewerDescriptiveDataSo;
        private SubcontinentsContainer _subcontinentsContainer;
        private NamingSetsContainer _namingSetsContainer;
        private StartingConditionsFunctionalDataSo _startingConditionsFunctionalDataSo;
        private SettingsDataContainer _settingsDataContainer;
        private BuildingVariationsContainer _buildingVariationsContainer;
        private BuildingCoreDataContainer _buildingCoreDataContainer;
        public JsonToScriptableObjectConverter(ResourcesScriptableObject resourcesContainer , SubcontinentsContainer subcontinentsContainer,
            NamingSetsContainer namingSetsContainer, ResourcesDescriptiveDataObject resourceDescriptiveDataObject, TerrainDescriptiveDataSo terrainDescriptiveDataSo
            , ElevationDescriptiveDataSo elevationDescriptiveDataSo, FeatureDescriptiveDataSo featureDescriptiveDataSo,
            StartingConditionsFunctionalDataSo startingConditionsFunctionalDataSo,
            SettingsDataContainer settingsDataContainer,
            BuildingVariationsContainer buildingVariationsContainer,
            BuildingCoreDataContainer   buildingCoreDataContainer, GameTextFieldDescriptiveDataSo popNavigatorPopDetailsViewerDescriptiveDataSo
            )
        {
            _resourcesContainer = resourcesContainer;
            _subcontinentsContainer = subcontinentsContainer;
            _namingSetsContainer = namingSetsContainer;
            _resourcesDescriptiveDataObject = resourceDescriptiveDataObject;
            _terrainDescriptiveDataSo = terrainDescriptiveDataSo;
            _featureDescriptiveDataSo = featureDescriptiveDataSo;
            _elevationDescriptiveDataSo = elevationDescriptiveDataSo;
            _startingConditionsFunctionalDataSo = startingConditionsFunctionalDataSo;
            _settingsDataContainer = settingsDataContainer;
            _buildingVariationsContainer = buildingVariationsContainer;
            _buildingCoreDataContainer = buildingCoreDataContainer;
            _popNavigatorPopDetailsViewerDescriptiveDataSo = popNavigatorPopDetailsViewerDescriptiveDataSo;
        }
      
        public async UniTask LoadData()
        {
         
            await DataLoader.GetDataFromJson(_settingsDataContainer,
                $"{Application.persistentDataPath}/CoreData/Settings/InitialSettings.json");
            
            await DataLoader.GetDataFromJson(_resourcesContainer,
                $"{Application.persistentDataPath}/CoreData/FunctionalData/Resources.json");
            await DataLoader.GetDataFromJson(_resourcesDescriptiveDataObject,
                $"{Application.persistentDataPath}/CoreData/{_settingsDataContainer.gameLanguage}/ResourceDescriptiveData.json");
            await DataLoader.GetDataFromJson(_terrainDescriptiveDataSo,
                $"{Application.persistentDataPath}/CoreData/{_settingsDataContainer.gameLanguage}/TerrainDescriptiveData.json");
            await DataLoader.GetDataFromJson(_elevationDescriptiveDataSo,
                $"{Application.persistentDataPath}/CoreData/{_settingsDataContainer.gameLanguage}/ElevationDescriptiveData.json");
            await DataLoader.GetDataFromJson(_featureDescriptiveDataSo,
                $"{Application.persistentDataPath}/CoreData/{_settingsDataContainer.gameLanguage}/FeatureDescriptiveData.json");
            await DataLoader.GetDataFromJson(_popNavigatorPopDetailsViewerDescriptiveDataSo,
                $"{Application.persistentDataPath}/CoreData/{_settingsDataContainer.gameLanguage}/PopNavigatorPanelUI/PopNavigatorPopDetailsViewerDescriptiveData.json");
            
            await DataLoader.GetDataFromJson(_subcontinentsContainer,
                $"{Application.persistentDataPath}/CoreData/FunctionalData/Subcontinents.json");
            await DataLoader.GetDataFromJson(_namingSetsContainer,
                $"{Application.persistentDataPath}/CoreData/FunctionalData/NamingSets.json");
            await DataLoader.GetDataFromJson(_startingConditionsFunctionalDataSo,
                $"{Application.persistentDataPath}/CoreData/Starting Conditions Functional Data/StartingConditionsFunctionalData.json");
                
            await DataLoader.GetDataFromJson(_buildingVariationsContainer,
                $"{Application.persistentDataPath}/CoreData/FunctionalData/BuildingVariationsContainer.json");
            await DataLoader.GetDataFromJson(_buildingCoreDataContainer,
                $"{Application.persistentDataPath}/CoreData/FunctionalData/BuildingCoreDataContainer.json");
        }
    }
}
