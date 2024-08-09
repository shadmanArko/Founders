using _Project.Scripts.DataBase.Initializer;
using _Project.Scripts.Managers;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts.Culture;
using _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts.Settings;
using _Project.Scripts.UI.MainMenu;
using Project.Scripts.DataBase.JsonToScriptableObject;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    [CreateAssetMenu(fileName = "MainMenuInstaller", menuName = "Installers/MainMenuInstaller")]
    public class MainMenuInstaller : ScriptableObjectInstaller<MainMenuInstaller>
    {
        [SerializeField] private PlayableCultureContainerSo playableCultureContainerSo;
        [SerializeField] private PlayableCultureDescriptiveDataSo playableCultureDescriptiveDataSo;
        [SerializeField] private SettingsDataContainer settingsDataContainer;
        [SerializeField] private SubcontinentsContainer subcontinentsContainer;
        [SerializeField] private GameTextFieldDescriptiveDataSo newGameSetupDescriptiveData;
        [SerializeField] private BaseCanvasView baseCanvasView;
        [SerializeField] private NewGameStartupCanvasView newGameStartupCanvasView;
        [SerializeField] private MainMenuSubcontinentPrefab subcontinentPrefab;
        [SerializeField] private NewGameSettingsData newGameSettingsData;
        public override void InstallBindings()
        {        
            
            Container.Bind<Initializer>().AsSingle();
            Container.Bind<NewGameSettingsData>().FromInstance(newGameSettingsData).AsSingle();
            Container.Bind<PlayableCultureContainerSo>().FromInstance(playableCultureContainerSo).AsSingle();
            Container.Bind<PlayableCultureDescriptiveDataSo>().FromInstance(playableCultureDescriptiveDataSo).AsSingle();
            Container.Bind<SettingsDataContainer>().FromInstance(settingsDataContainer).AsSingle();
            Container.Bind<GameTextFieldDescriptiveDataSo>().FromInstance(newGameSetupDescriptiveData).AsSingle();
            Container.Bind<SubcontinentsContainer>().FromInstance(subcontinentsContainer).AsSingle();
            Container.Bind<BaseCanvasView>().FromComponentInNewPrefab(baseCanvasView).AsSingle();
            Container.Bind<NewGameStartupCanvasView>().FromComponentInNewPrefab(newGameStartupCanvasView).AsSingle();
            Container.Bind<MainMenuSubcontinentPrefab>().FromComponentInNewPrefab(subcontinentPrefab).AsSingle();
            

            Container.Bind<BaseCanvasController>().AsSingle().NonLazy();
            Container.Bind<NewGameStartupCanvasController>().AsSingle().NonLazy();
            Container.Bind<MainMenuJsonToScriptableObjectConverter>().AsSingle();
            Container.Bind<MainMenuManager>().AsSingle().NonLazy();
        }
    }
}