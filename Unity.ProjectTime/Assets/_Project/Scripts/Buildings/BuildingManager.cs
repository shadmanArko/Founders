using System;
using System.Collections.Generic;
using _Project.Scripts.Buildings.Factory;
using _Project.Scripts.Game_Actions;
using _Project.Scripts.HelperScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Helpers;
using ASP.NET.ProjectTime.Models;
using PlasticGui.WorkspaceWindow.Replication;
using UnityEngine;

namespace _Project.Scripts.Buildings
{
    public class BuildingManager : IDisposable
    {
        public IBuildingController BuildingController;
        public List<IBuildingController> BuildingControllers = new();
        private readonly BuildingFactory _buildingFactory;
        private readonly BuildingArrangementSystem _buildingArrangementSystem;

        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly BuildingVariationsContainer _buildingVariationsContainer;
        private readonly BuildingCoreDataContainer _buildingCoreDataContainer;

        public BuildingManager(BuildingFactory buildingFactory, SaveDataScriptableObject saveDataScriptableObject,
            BuildingArrangementSystem buildingArrangementSystem, BuildingVariationsContainer buildingVariationsContainer,
            BuildingCoreDataContainer buildingCoreDataContainer)
        {
            _buildingFactory = buildingFactory;
            _saveDataScriptableObject = saveDataScriptableObject;
            _buildingArrangementSystem = buildingArrangementSystem;
            _buildingCoreDataContainer = buildingCoreDataContainer;
            _buildingVariationsContainer = buildingVariationsContainer;
            UnitActions.OnSettlerSettleASettlement += CreateNewBuilding;
        }

        private void CreateNewBuilding(string label, string tileId)
        {
            var tile = _saveDataScriptableObject.Save.AllSubcontinentTiles.GetById(subcontinent => subcontinent.Id, _saveDataScriptableObject.Save.ActiveSubcontinentTilesId ).Tiles.GetById(tile1 => tile1.Id, tileId);
            var buildingCoreData = _buildingCoreDataContainer.buildingCoreDatas.GetById(data => data.Label, label);
            var checkBuildingSlotAvailability =
                _buildingArrangementSystem.CheckBuildingSlotAvailable(buildingCoreData.BuildingSize.ToString(), tile);

            if (!checkBuildingSlotAvailability)
            {
                Debug.LogWarning("Building Positions not available");
            }
            else
            {
                var buildingVariations = _buildingVariationsContainer.buildingVariations.FindAll(variation =>
                    variation.BuildingName == buildingCoreData.Label && variation.VariationClass ==
                    _saveDataScriptableObject.Save.AllCultures[0].Culture.BuildingVariationClass);
                if(buildingVariations.Count <=0) return;

                
                var buildingModel = new Building(
                    NewIdGenerator.GenerateNewId(),
                    buildingCoreData.Label,
                    buildingCoreData.BuildingMaterialCost,
                    buildingCoreData.GoldCost,
                    buildingCoreData.MinimumPopCapacity,
                    buildingCoreData.MaximumPopCapacity,
                    buildingCoreData.BuildingTime,
                    buildingVariations.Shuffle()[0],
                    buildingCoreData.BuildingSize,
                    BuildingSlot.Small_BottomLeft,
                    tileId,
                    new List<string>(),
                    0,
                    0, 
                    0
                      
                    );
                tile.BuildingIds.Add(buildingModel.Id);
                _saveDataScriptableObject.Save.Buildings.Add(buildingModel);
                var buildingIdAndPositionDictionary = _buildingArrangementSystem.RearrangeBuildings(tile);
                var newBuilding = _buildingFactory.Create(buildingModel);
                BuildingControllers.Add(newBuilding);

                foreach (var buildingIdAndPosition in buildingIdAndPositionDictionary)
                {
                    var buildingController = BuildingControllers.GetById(b => b.Id, buildingIdAndPosition.Key);
                    var buildingModel1 =
                        _saveDataScriptableObject.Save.Buildings.GetById(building => building.Id, buildingController.Id);
                    buildingModel1.XPosition = buildingController.Position.x;
                    buildingModel1.YPosition = buildingController.Position.y;
                    buildingModel1.ZPosition = buildingController.Position.z;
                    buildingController.Position = buildingIdAndPosition.Value;
                    
                }
                
                //newBuilding.Position = new Vector3(tile.TileCoordinates.X + 0.5f, tile.Elevation, tile.TileCoordinates.Y + 0.5f);
                Debug.LogWarning($"Building Created at {tile.Subcontinent} {tile.ElevationType} {tile.TileCoordinates.X} {tile.TileCoordinates.Y}");
            }
        }

        public void CreateNewBuildingPrefabsOnStart()
        {
            BuildingControllers.Clear();
            foreach (var saveBuilding in _saveDataScriptableObject.Save.Buildings)
            {
                var buildingController = _buildingFactory.Create(saveBuilding);
                buildingController.Position =
                    new Vector3(saveBuilding.XPosition, saveBuilding.YPosition, saveBuilding.ZPosition);
                BuildingControllers.Add(buildingController);
            }
        }

        public Building GetLastCreatedBuilding()
        {
            if (BuildingControllers.Count > 0) return BuildingControllers[^1].Building;
            else
            {
                return null;
            }
        }

        public void Dispose()
        {
            UnitActions.OnSettlerSettleASettlement -= CreateNewBuilding;

        }
    }
}
