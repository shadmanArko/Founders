using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using _Project.Scripts.Buildings;
using _Project.Scripts.Game_Actions;
using _Project.Scripts.Map;
using _Project.Scripts.Pathfinding;
using _Project.Scripts.Pop_Clan_Culture;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.Units.Armies;
using _Project.Scripts.Units.Factory;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Models.PathFinding;
using ASP.NET.ProjectTime.Services;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Scripts.Units
{
    public class UnitManager : IDisposable
    {
        public IUnitController SelectedUnitController = null;

        public List<IUnitController> UnitControllers = new List<IUnitController>();
        public List<IUnitController> autoMatedUnitControllers = new List<IUnitController>();

        private readonly UnitFactory _unitFactory;
        private readonly SettlerBestSpawningTileFinder _settlerBestSpawningTileFinder;
        private readonly TileFinderForAutomatedExploration _tileFinderForAutomatedExploration;
        private readonly UnitMovementSystem _unitMovementSystem;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly BuildingManager _buildingManager;
        private readonly PopManager _popManager;
        private readonly ArmyUnitInitializer _armyUnitInitializer;


        private AStarPathfinding _aStarPathfinding;
        private PathfinderGridDataContainer _pathfinderGridDataContainer;
        private UnitPathViewer _unitPathViewer;
        
        private bool _unitSelected = false;
        private bool _unitMovementCompleted;
        

        public UnitManager(AStarPathfinding aStarPathfinding, PathfinderGridDataContainer pathfinderGridDataContainer, UnitPathViewer unitPathViewer,
            TileFinderForAutomatedExploration tileFinderForAutomatedExploration, PopManager popManager, BuildingManager buildingManager, UnitFactory unitFactory, SettlerBestSpawningTileFinder settlerBestSpawningTileFinder, UnitMovementSystem unitMovementSystem, SaveDataScriptableObject saveDataScriptableObject, ArmyUnitInitializer armyUnitInitializer)
        {
            _unitFactory = unitFactory;
            _settlerBestSpawningTileFinder = settlerBestSpawningTileFinder;
            _tileFinderForAutomatedExploration = tileFinderForAutomatedExploration;
            _unitMovementSystem = unitMovementSystem;
            _saveDataScriptableObject = saveDataScriptableObject;
            _buildingManager = buildingManager;
            _popManager = popManager;
            _armyUnitInitializer = armyUnitInitializer;
            _unitSelected = false;
            UnitActions.onSelectedUnit += OnUnitSelect;
            TileActions.onSelectedTile += OnTileSelected;
            UnitActions.OnClickSettleASettlement += SettleASettlement;
            TimeActions.onPulseTicked += ControlAutomatedUnits;

            _aStarPathfinding = aStarPathfinding;
            _pathfinderGridDataContainer = pathfinderGridDataContainer;
            _unitPathViewer = unitPathViewer;
        }

        public void CreateASettlerUnitForFirstTime()
        {
            var tileId = _settlerBestSpawningTileFinder.GetARandomSettlerSpawningTileId();
            var unit = _unitFactory.Create("Settler", tileId);
            _saveDataScriptableObject.Save.Units.Add(unit.Unit);
            _saveDataScriptableObject.Save.AllCultures[0].Culture.UnitIds.Add(unit.Id);
            foreach (var savePop in _saveDataScriptableObject.Save.Pops)
            {
                unit.Unit.PopIds.Add(savePop.Id);
                savePop.UnitId = unit.Id; 
            }
            unit.UnitMovementSystem =
                new UnitMovementSystem(_aStarPathfinding, _pathfinderGridDataContainer, GameObject.Instantiate(_unitPathViewer).GetComponent<UnitPathViewer>() , _saveDataScriptableObject);
            unit.UnitAnimator.ShowRaiseAnimation();
            UnitControllers.Add(unit);
        }

        public void CreateAnArmyUnit()
        {
            
        }
        
        public void CreateAScoutUnit(string tileId)
        {
            var unit = _unitFactory.Create("Scout", tileId);
            _saveDataScriptableObject.Save.Units.Add(unit.Unit);
            _saveDataScriptableObject.Save.AllCultures[0].Culture.UnitIds.Add(unit.Id);
            UnitControllers.Add(unit);
            unit.UnitMovementSystem =
                new UnitMovementSystem(_aStarPathfinding, _pathfinderGridDataContainer, GameObject.Instantiate(_unitPathViewer).GetComponent<UnitPathViewer>() , _saveDataScriptableObject);
            unit.UnitAnimator.ShowRaiseAnimation();
            autoMatedUnitControllers.Add(unit);
            
        }

        public void ControlAutomatedUnits() 
        {
            foreach (var autoMatedUnitController in autoMatedUnitControllers)
            {
                if (!autoMatedUnitController.IsMoving)
                {
                    var destinationTile =
                        _tileFinderForAutomatedExploration.GetAnUndiscoveredTile(autoMatedUnitController.Unit);
                    if(destinationTile != null) autoMatedUnitController.UnitMovementSystem.InitiateMovement(autoMatedUnitController, destinationTile);
                }
            }
        }
        
        

        private void OnTileSelected(string tileId)
        {
            if (_unitSelected && SelectedUnitController != null)
            {
                // _unitMovementSystem.InitiateMovement(SelectedUnitController, _saveDataScriptableObject.Save.AllSubcontinentTiles.GetById(sub => sub.Id, _saveDataScriptableObject.Save.ActiveSubcontinentTilesId).Tiles.GetById(tile => tile.Id, tileId));
                SelectedUnitController.UnitMovementSystem.InitiateMovement(SelectedUnitController, _saveDataScriptableObject.Save.AllSubcontinentTiles.GetById(sub => sub.Id, _saveDataScriptableObject.Save.ActiveSubcontinentTilesId).Tiles.GetById(tile => tile.Id, tileId));
                _unitSelected = false;
            }
        }

        private void SettleASettlement(string buildingType)
        {
            if (_unitSelected && SelectedUnitController != null && SelectedUnitController.Unit.Name == "Settler")
            {
                UnitActions.OnSettlerSettleASettlement?.Invoke(buildingType, SelectedUnitController.Unit.CurrentTileId);
                Debug.Log($"Settled a settlement at {SelectedUnitController.Unit.CurrentTileId}");

                AfterCompletingSettlement();
            }
        }

        private void AfterCompletingSettlement()
        {
            var building = _buildingManager.GetLastCreatedBuilding();
            if (building != null && building.TileId == SelectedUnitController.Unit.CurrentTileId)
            {
                foreach (var unitPopId in SelectedUnitController.Unit.PopIds)
                {
                    _popManager.AssignPopToBuilding(building.Id, unitPopId);
                }
            }

            UnitControllers.Remove(SelectedUnitController);
            SelectedUnitController.Destroy();
            SelectedUnitController = null;
            
            //Temporary
            CreateAScoutUnit(building?.TileId);
            CreateAScoutUnit(building?.TileId);
            CreateAScoutUnit(building?.TileId);
            CreateAScoutUnit(building?.TileId);
            CreateAScoutUnit(building?.TileId);
        }

        private void OnUnitSelect(string unitId)
        {
            _unitSelected = true;
            SelectedUnitController = UnitControllers.GetById(unit=>unit.Id, unitId);
        }
        
        

        public void Dispose()
        {
            UnitActions.onSelectedUnit -= OnUnitSelect;
            TileActions.onSelectedTile -= OnTileSelected;
            UnitActions.OnClickSettleASettlement -= SettleASettlement; 
            TimeActions.onPulseTicked -= ControlAutomatedUnits;
        }
    }
}