using _Project.Scripts.DataBase.Initializer;
using _Project.Scripts.MapDataGenerator;
using _Project.Scripts.Natural_Resources;
using _Project.Scripts.Pathfinding;
using _Project.Scripts.Pop_Clan_Culture;
using _Project.Scripts.Pulse_System;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.Tiles;
using _Project.Scripts.Units;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Models.PathFinding;
using Project.Scripts.DataBase.JsonToScriptableObject;
using SandBox.Arko.Scripts.UnitOfWork;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Managers
{
    public class GameManager
    {
        private readonly JsonToScriptableObjectConverter _jsonToScriptableObjectConverter;
        private readonly NewGameDataGenerator _newGameDataGenerator;
        private readonly CultureGeneratorController _cultureGeneratorController;
        private readonly PathfinderGridDataContainer _pathfinderGridDataContainer;
        private readonly TileMapInitializingDataContainer _tileMapInitializingDataContainer;
        // private readonly UnitFactory _unitFactory;
        private readonly UnitManager _unitManager;
        private readonly TileManager _tileManager;
        private readonly NaturalResourceManager _naturalResourceManager;

        // private readonly ClanAndPopGeneratorController _clanAndPopGeneratorController;
        private readonly UnitOfWork _unitOfWork;
        private readonly Initializer _initializer;
        private readonly PulseSystem _pulseSystem;


        public GameManager(JsonToScriptableObjectConverter jsonToScriptableObjectConverter, NewGameDataGenerator newGameDataGenerator, CultureGeneratorController cultureGeneratorController, PathfinderGridDataContainer pathfinderGridDataContainer, TileMapInitializingDataContainer tileMapInitializingDataContainer, UnitManager unitManager, TileManager tileManager, NaturalResourceManager naturalResourceManager, UnitOfWork unitOfWork, Initializer initializer, PulseSystem pulseSystem)
        {
            _jsonToScriptableObjectConverter = jsonToScriptableObjectConverter;
            _newGameDataGenerator = newGameDataGenerator;
            _cultureGeneratorController = cultureGeneratorController;
            _pathfinderGridDataContainer = pathfinderGridDataContainer;
            _tileMapInitializingDataContainer = tileMapInitializingDataContainer;
            _unitManager = unitManager;
            _tileManager = tileManager;
            _naturalResourceManager = naturalResourceManager;
            _unitOfWork = unitOfWork;
            _initializer = initializer;
            _pulseSystem = pulseSystem;
        }


        [Inject]
        public async void Init()
        {
            _initializer.LoadCoreDataFile();
            await _jsonToScriptableObjectConverter.LoadData();
            _newGameDataGenerator.Initialize();
            _tileManager.GenerateMapTileMesh();
            _naturalResourceManager.SpawnAllResources();
            // Unit unit = _unitFactory.Create();
            GeneratePathFindingGrids(_tileMapInitializingDataContainer.subcontinentsContainer.subcontinents[1].subcontinentName);
            await _cultureGeneratorController.GenerateCulture_OnStartNewGame();
            // await _clanAndPopGeneratorController.GenerateClanAndPopOnStart_NewGame();
            await _pulseSystem.Init();
            //_popNavigatorCanvasController.AssignPopCardToPopNavigationPanel();
            //_bottomPanelCanvasController.ActivateTileViewerPanel();
            _unitManager.CreateASettlerUnitForFirstTime();
        }
        
        private void GeneratePathFindingGrids(string subcontinentName)
        {
            _pathfinderGridDataContainer.Grid = new Node[_tileMapInitializingDataContainer.gridSizeX, _tileMapInitializingDataContainer.gridSizeY];
            Debug.Log($"grid created with length {_pathfinderGridDataContainer.Grid.Length}");

            // var tempNode = new Node();
            foreach (var tile in _tileMapInitializingDataContainer.saveDataScriptableObject.Save.AllSubcontinentTiles.GetById(subcontinent => subcontinent.Id, _tileMapInitializingDataContainer.saveDataScriptableObject.Save.ActiveSubcontinentTilesId ).Tiles)
            {
                if (subcontinentName != tile.Subcontinent) continue;
                var tempNode = new Node(tile.TileCoordinates, null, float.MaxValue, float.MaxValue, tile.IsWalkable, tile.PathFindingTerrainModifier, tile.PathFindingFeatureModifier, tile.PathFindingElevationModifier, tile.PathFindingRoadModifier);
                _pathfinderGridDataContainer.Grid[tile.XPosition, tile.YPosition] = tempNode; //todo change is walkable
        
            }
        }
    }
}