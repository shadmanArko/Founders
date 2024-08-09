using _Project.Scripts.HelperScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts.Culture;
using _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.DataBase.JsonToScriptableObject
{
    public class MainMenuJsonToScriptableObjectConverter
    {
        private SettingsDataContainer _settingsDataContainer;
        private PlayableCultureContainerSo _playableCultureContainerSo;
        private PlayableCultureDescriptiveDataSo _playableCultureDescriptiveDataSo;
        private GameTextFieldDescriptiveDataSo _newGameSetupDescriptiveData;
        private SubcontinentsContainer _subcontinentsContainer;

        
        public MainMenuJsonToScriptableObjectConverter(PlayableCultureContainerSo playableCultureContainerSo, 
            PlayableCultureDescriptiveDataSo playableCultureDescriptiveDataSo,
            SettingsDataContainer settingsDataContainer,
            GameTextFieldDescriptiveDataSo newGameSetupDescriptiveData,
            SubcontinentsContainer subcontinentsContainer
            )
        {
            _playableCultureContainerSo = playableCultureContainerSo;
            _playableCultureDescriptiveDataSo = playableCultureDescriptiveDataSo;
            _settingsDataContainer = settingsDataContainer;
            _newGameSetupDescriptiveData = newGameSetupDescriptiveData;
            _subcontinentsContainer = subcontinentsContainer;
        }
        public async UniTask LoadData()
        {
            await DataLoader.GetDataFromJson(_settingsDataContainer,
                $"{Application.dataPath}/CoreData/Settings/FunctionalData/InitialSettings.json");
            
            await DataLoader.GetDataFromJson(_subcontinentsContainer,
                $"{Application.dataPath}/CoreData/FunctionalData/Subcontinents.json");
            
            await DataLoader.GetDataFromJson(_newGameSetupDescriptiveData,
                $"{Application.dataPath}/CoreData/{_settingsDataContainer.gameLanguage}/NewGameSetUpDescriptiveData.json");
            await DataLoader.GetDataFromJson(_playableCultureContainerSo,
                $"{Application.dataPath}/CoreData/FunctionalData/PlayableCultures.json");
            await DataLoader.GetDataFromJson(_playableCultureDescriptiveDataSo,
                $"{Application.dataPath}/CoreData/{_settingsDataContainer.gameLanguage}/PlayableCultureDescriptiveData.json");
        }
    }
}
