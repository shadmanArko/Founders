using _Project.Scripts.Buildings;
using _Project.Scripts.Buildings.Factory;
using _Project.Scripts.DataBase.Initializer;
using _Project.Scripts.Managers;
using _Project.Scripts.Map;
using _Project.Scripts.MapDataGenerator;
using _Project.Scripts.Natural_Resources;
using _Project.Scripts.Natural_Resources.Factory;
using _Project.Scripts.Pathfinding;
using _Project.Scripts.Pop_Clan_Culture;
using _Project.Scripts.Pulse_System;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts.DescriptiveDataScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts.Settings;
using _Project.Scripts.Tiles;
using _Project.Scripts.Tiles.Factory;
using _Project.Scripts.TIme_System;
using _Project.Scripts.Units;
using _Project.Scripts.Units.Armies;
using _Project.Scripts.Units.Factory;
using ASP.NET.ProjectTime.DataContext;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Models.PathFinding;
using Project.Scripts.DataBase.JsonToScriptableObject;
using SandBox.Arko.Scripts;
using SandBox.Arko.Scripts.UnitOfWork;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Installers
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        [SerializeField] private TileMapInitializingDataContainer tileMapInitializingDataContainer;
        [SerializeField] private PathfinderGridDataContainer pathfinderGridDataContainer;
        [SerializeField] private VariablesForMapSeed variablesForMapSeed;
        [FormerlySerializedAs("pathViewer")] [SerializeField] private UnitPathViewer unitPathViewer;
        [SerializeField] private ResourcesScriptableObject resourcesContainer;
        [SerializeField] private ResourcesDescriptiveDataObject resourcesDescriptiveDataContainer;
        [SerializeField] private TerrainDescriptiveDataSo terrainDescriptiveDataSo;
        [SerializeField] private ElevationDescriptiveDataSo elevationDescriptiveDataSo;
        [SerializeField] private FeatureDescriptiveDataSo featureDescriptiveDataSo;
        [SerializeField] private GameTextFieldDescriptiveDataSo popNavigatorPopDetailsViewerDescriptiveDataSo;
        [SerializeField] private SubcontinentsContainer subcontinentsContainer;
        [SerializeField] private NamingSetsContainer namingSetsContainer;
        [SerializeField] private StartingConditionsFunctionalDataSo startingConditionsFunctionalDataSo;
        [SerializeField] private SettingsDataContainer settingsDataContainer;
        [SerializeField] private ClanCoreNameGeneratorDataContainerScriptableObject clanCoreNameGeneratorDataContainerScriptableObject;
        [SerializeField] private SaveDataScriptableObject saveDataScriptableObject;
        [SerializeField] private RaisedUnitPrefabsSo raisedUnitPrefabsSo;
        [SerializeField] private BuildingPrefabContainerScriptableObject buildingPrefabContainerScriptableObject;
        [SerializeField] private BuildingVariationsContainer buildingVariationsContainer;
        [SerializeField] private BuildingCoreDataContainer buildingCoreDataContainer;
        [SerializeField] private ResourcePrefabsContainerSo resourcePrefabsContainerSo;
        [SerializeField] private NewGameSettingsData newGameSettingsData;

        //UI
        
    
        public override void InstallBindings()
        {
            Container.Bind<SaveDataScriptableObject>().FromInstance(saveDataScriptableObject).AsSingle();
            Container.Bind<DataContext>().To<JsonDataContext>().AsSingle();
            Container.Bind<UnitOfWork>().AsSingle();
            Container.Bind<Initializer>().AsSingle();
            Container.Bind<PulseSystem>().AsSingle();
            Container.Bind<GameTimeSystem>().AsSingle();
            Container.Bind<SettlerBestSpawningTileFinder>().AsSingle();
            Container.Bind<TileFinderForAutomatedExploration>().AsSingle();
            Container.Bind<UnitMovementSystem>().AsSingle();
            Container.Bind<BuildingArrangementSystem>().AsSingle();
            Container.Bind<TileShapeGenerator>().AsSingle();
            Container.Bind<ArmyUnitInitializer>().AsSingle();
            
            Container.Bind<PathfinderGridDataContainer>().FromInstance(pathfinderGridDataContainer).AsSingle();
            Container.Bind<TileMapInitializingDataContainer>().FromInstance(tileMapInitializingDataContainer).AsSingle();
            Container.Bind<VariablesForMapSeed>().FromInstance(variablesForMapSeed).AsSingle();
            Container.Bind<ResourcesScriptableObject>().FromInstance(resourcesContainer).AsSingle();
            Container.Bind<ResourcesDescriptiveDataObject>().FromInstance(resourcesDescriptiveDataContainer).AsSingle();
            Container.Bind<TerrainDescriptiveDataSo>().FromInstance(terrainDescriptiveDataSo).AsSingle();
            Container.Bind<FeatureDescriptiveDataSo>().FromInstance(featureDescriptiveDataSo).AsSingle();
            Container.Bind<GameTextFieldDescriptiveDataSo>().FromInstance(popNavigatorPopDetailsViewerDescriptiveDataSo)
                .AsSingle();
            Container.Bind<ElevationDescriptiveDataSo>().FromInstance(elevationDescriptiveDataSo).AsSingle();
            Container.Bind<SubcontinentsContainer>().FromInstance(subcontinentsContainer).AsSingle();
            Container.Bind<NamingSetsContainer>().FromInstance(namingSetsContainer).AsSingle();
            Container.Bind<StartingConditionsFunctionalDataSo>().FromInstance(startingConditionsFunctionalDataSo).AsSingle();
            Container.Bind<ClanCoreNameGeneratorDataContainerScriptableObject>().FromInstance(clanCoreNameGeneratorDataContainerScriptableObject).AsSingle();
            Container.Bind<SettingsDataContainer>().FromInstance(settingsDataContainer).AsSingle();
            Container.Bind<RaisedUnitPrefabsSo>().FromInstance(raisedUnitPrefabsSo).AsSingle();
            Container.Bind<BuildingPrefabContainerScriptableObject>().FromInstance(buildingPrefabContainerScriptableObject).AsSingle();
            Container.Bind<BuildingVariationsContainer>().FromInstance(buildingVariationsContainer).AsSingle();
            Container.Bind<BuildingCoreDataContainer>().FromInstance(buildingCoreDataContainer).AsSingle();
            Container.Bind<ResourcePrefabsContainerSo>().FromInstance(resourcePrefabsContainerSo).AsSingle();
            Container.Bind<NewGameSettingsData>().FromInstance(newGameSettingsData).AsSingle();
        
        
            Container.Bind<JsonToScriptableObjectConverter>().AsSingle();
            Container.Bind<AStarPathfinding>().AsSingle().WithArguments(tileMapInitializingDataContainer.gridSizeX,tileMapInitializingDataContainer.gridSizeY);
            Container.Bind<RandomSeedController>().AsSingle();
            Container.Bind<NewGameDataGenerator>().AsSingle();
            Container.Bind<UnitPathViewer>().FromComponentInNewPrefab(unitPathViewer).AsSingle();
       
            Container.Bind<ClanAndPopGeneratorController>().AsSingle();
            Container.Bind<CultureGeneratorController>().AsSingle();

            //Factory
          
            Container.BindFactory<string, string, IUnitController, UnitFactory>().FromFactory<UnitGenerationFactory>();
            Container.Bind<UnitManager>().AsSingle();
        
            Container.BindFactory<Building, IBuildingController, BuildingFactory>().FromFactory<BuildingGenerationFactory>();
            Container.Bind<BuildingManager>().AsSingle().NonLazy();

            Container.BindFactory<Tile, int[], Transform, ITileController, TileFactory>().FromFactory<TileGenerationFactory>();
            Container.Bind<TileManager>().AsSingle();
        
            Container.BindFactory<string, NaturalResource, Vector3, INaturalResourceController, NaturalResourceFactory>().FromFactory<NaturalResourceGenerationFactory>();
            Container.Bind<NaturalResourceManager>().AsSingle();
            
            Container.Bind<PopManager>().AsSingle();

        
            Container.Bind<GameManager>().AsSingle().NonLazy();
        }
    }
}