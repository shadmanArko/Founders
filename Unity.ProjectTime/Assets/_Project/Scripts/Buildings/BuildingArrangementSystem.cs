using System.Collections.Generic;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Buildings
{
    public class BuildingArrangementSystem
    {
        private Vector3 _offsetSmallBuildingTopLeftPosition;
        private Vector3 _offsetSmallBuildingTopRightPosition;
        private Vector3 _offsetSmallBuildingBottomLeftPosition;
        private Vector3 _offsetSmallBuildingBottomRightPosition;
        private Vector3 _offsetMediumBuildingTopPosition;
        private Vector3 _offsetMediumBuildingBottomPosition;
        private Vector3 _offsetLargeBuildingCenterPosition;
        private Vector3 _offsetExtraLargeBuildingCenterPosition; 
        private Vector3 SmallBuildingTopLeftPosition { get; set; }
        private Vector3 SmallBuildingTopRightPosition { get; set; }
        private Vector3 SmallBuildingBottomLeftPosition { get; set; }
        private Vector3 SmallBuildingBottomRightPosition { get; set; }
        private Vector3 MediumBuildingTopPosition { get; set; }
        private Vector3 MediumBuildingBottomPosition { get; set; }
        private Vector3 LargeBuildingCenterPosition { get; set; }
        private Vector3 ExtraLargeBuildingCenterPosition { get; set; }

        private SaveDataScriptableObject _saveDataScriptableObject;
        public BuildingArrangementSystem(SaveDataScriptableObject saveDataScriptableObject)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
        }
        private List<Vector3> SmallBuildingPosition => new() 
        {
            SmallBuildingTopLeftPosition, SmallBuildingTopRightPosition, 
            SmallBuildingBottomLeftPosition, SmallBuildingBottomRightPosition
        };
        private List<Vector3> MediumBuildingPosition => new()
        {
            MediumBuildingTopPosition, MediumBuildingBottomPosition
        };

        private void SetTileSlotPositions(TileCoordinates tileCoordinate, float tileElevation)
        {
            _offsetSmallBuildingTopLeftPosition = new Vector3(0.25f, 0, 0.75f);
            _offsetSmallBuildingTopRightPosition = new Vector3(0.75f, 0, 0.75f);
            _offsetSmallBuildingBottomLeftPosition = new Vector3(0.25f, 0, 0.25f);
            _offsetSmallBuildingBottomRightPosition = new Vector3(0.75f, 0, 0.25f);
            _offsetMediumBuildingTopPosition = new Vector3(0.5f, 0, 0.75f);
            _offsetMediumBuildingBottomPosition = new Vector3(0.5f, 0, 0.25f);
            _offsetLargeBuildingCenterPosition = new Vector3(0.5f, 0, 0.5f);
            _offsetExtraLargeBuildingCenterPosition = new Vector3(0.5f, 0, 0.5f);

            SmallBuildingTopLeftPosition = new Vector3(tileCoordinate.X + _offsetSmallBuildingTopLeftPosition.x,
                tileElevation, tileCoordinate.Y + _offsetSmallBuildingTopLeftPosition.z);
            SmallBuildingTopRightPosition = new Vector3(tileCoordinate.X + _offsetSmallBuildingTopRightPosition.x,
                tileElevation, tileCoordinate.Y + _offsetSmallBuildingTopRightPosition.z);
            SmallBuildingBottomLeftPosition = new Vector3(tileCoordinate.X + _offsetSmallBuildingBottomLeftPosition.x,
                tileElevation, tileCoordinate.Y + _offsetSmallBuildingBottomLeftPosition.z);
            SmallBuildingBottomRightPosition = new Vector3(tileCoordinate.X + _offsetSmallBuildingBottomRightPosition.x,
                tileElevation, tileCoordinate.Y + _offsetSmallBuildingBottomRightPosition.z);
            
            MediumBuildingTopPosition = new Vector3(tileCoordinate.X + _offsetMediumBuildingTopPosition.x,
                tileElevation, tileCoordinate.Y + _offsetMediumBuildingTopPosition.z);
            MediumBuildingBottomPosition = new Vector3(tileCoordinate.X + _offsetMediumBuildingBottomPosition.x,
                tileElevation, tileCoordinate.Y + _offsetMediumBuildingBottomPosition.z);
            
            LargeBuildingCenterPosition = new Vector3(tileCoordinate.X + _offsetLargeBuildingCenterPosition.x,
                tileElevation, tileCoordinate.Y + _offsetLargeBuildingCenterPosition.z);
            
            ExtraLargeBuildingCenterPosition = new Vector3(tileCoordinate.X + _offsetExtraLargeBuildingCenterPosition.x,
                tileElevation, tileCoordinate.Y + _offsetExtraLargeBuildingCenterPosition.z);
        }
        
        
        public bool CheckBuildingSlotAvailable(string BuildingSize, Tile tile)
        {
            var occupiedBuildingSlots = CountOccupiedBuildingSlots(tile.BuildingIds);
            var neededBuildingSlots = CountNeededBuildingSlots(BuildingSize);

            if (!BuildingSize.Contains("ExtraLarge") && tile.NaturalResource is not null)
                occupiedBuildingSlots++;
            
            return 4 - occupiedBuildingSlots >= neededBuildingSlots;
        }

        public Dictionary<string, Vector3> RearrangeBuildings(Tile tile)
        {
            var arrangedBuildings = new Dictionary<string, Vector3>();
            SetTileSlotPositions(tile.TileCoordinates, tile.Elevation);
            
            var smallBuildingCount = 0;
            var mediumBuildingCount = 0;
            var largeBuildingCount = 0;
            var extraLargeBuildingCount = 0;

            var smallBuildingList = new List<Building>();
            var mediumBuildingList = new List<Building>();
            var largeBuildingList = new List<Building>();
            var extraLargeBuildingList = new List<Building>();
        
            foreach (var buildingId in tile.BuildingIds)
            {
                var buildingFromSaveData =
                    _saveDataScriptableObject.Save.Buildings.GetById(building1 => building1.Id, buildingId);
                if(buildingFromSaveData == null) continue;
                switch (buildingFromSaveData.BuildingSize)
                {
                    case BuildingSize.ExtraLarge:
                        extraLargeBuildingCount += 1;
                        extraLargeBuildingList.Add(buildingFromSaveData);
                        break;
                    case BuildingSize.Large:
                        largeBuildingCount += 1;
                        largeBuildingList.Add(buildingFromSaveData);
                        break;
                    case BuildingSize.Medium:
                        mediumBuildingCount += 1;
                        mediumBuildingList.Add(buildingFromSaveData);
                        break;
                    case BuildingSize.Small:
                        smallBuildingCount += 1;
                        smallBuildingList.Add(buildingFromSaveData);
                        break;
                }
            }

            if (extraLargeBuildingCount > 0)
            {
                extraLargeBuildingList[0].BuildingSlot = GetBuildingSlot(ExtraLargeBuildingCenterPosition);
                arrangedBuildings.Add(extraLargeBuildingList[0].Id, ExtraLargeBuildingCenterPosition);
                return arrangedBuildings;
            }
            
            if (largeBuildingCount > 0)
            {
                largeBuildingList[0].BuildingSlot = GetBuildingSlot(LargeBuildingCenterPosition);
                arrangedBuildings.Add(largeBuildingList[0].Id, LargeBuildingCenterPosition);
                if (smallBuildingCount > 0)
                {
                    smallBuildingList[0].BuildingSlot = GetBuildingSlot(SmallBuildingBottomRightPosition);
                    arrangedBuildings.Add(smallBuildingList[0].Id, SmallBuildingBottomRightPosition);
                }
                return arrangedBuildings;
            }

            if (mediumBuildingCount > 0)
            {
                for (var i = 0; i < mediumBuildingCount; i++)
                {
                    mediumBuildingList[i].BuildingSlot = GetBuildingSlot(MediumBuildingPosition[i]);
                    arrangedBuildings.Add(mediumBuildingList[i].Id, MediumBuildingPosition[i]);
                }

                if (smallBuildingCount <= 0) return arrangedBuildings;
                {
                    for (var i = 0; i < smallBuildingCount; i++)
                    {
                        smallBuildingList[i].BuildingSlot = GetBuildingSlot(SmallBuildingPosition[i + 2]);
                        arrangedBuildings.Add(smallBuildingList[i].Id, SmallBuildingPosition[i+2]);
                    }
                }
                return arrangedBuildings;
            }

            if (smallBuildingCount <= 0) return arrangedBuildings;
            {
                for (var i = 0; i < smallBuildingCount; i++)
                {
                    smallBuildingList[i].BuildingSlot = GetBuildingSlot(SmallBuildingPosition[i]);
                    arrangedBuildings.Add(smallBuildingList[i].Id, SmallBuildingPosition[i]);
                }
                return arrangedBuildings;
            }
        }

        #region Utility Methods

        private int CountOccupiedBuildingSlots(List<string> buildingIds)
        {
            var count = 0;

            if (buildingIds.Count <= 0) return 0;
            
            foreach (var tileBuildingId in buildingIds)
            {
                var tileBuilding =
                    _saveDataScriptableObject.Save.Buildings.GetById(building => building.Id, tileBuildingId);
                if (tileBuilding.BuildingSize.ToString().Contains("Small"))
                    count++;
                else if (tileBuilding.BuildingSize.ToString().Contains("Medium"))
                    count += 2;
                else if (tileBuilding.BuildingSize.ToString().Contains("ExtraLarge"))
                    count += 4;
                else
                    count = 3;
            }

            return count;
        }
        
        private int CountNeededBuildingSlots(string BuildingSize)
        {
            if (BuildingSize.Contains("Small"))
                return 1;
            if (BuildingSize.Contains("Medium")) 
                return 2;
            return BuildingSize.Contains("ExtraLarge") ? 4 : 3;
        }

        private BuildingSlot GetBuildingSlot(Vector3 pos)
        {
            if (pos == SmallBuildingTopLeftPosition)
                return BuildingSlot.Small_TopLeft;
            if (pos == SmallBuildingTopRightPosition)
                return BuildingSlot.Small_TopRight;
            if (pos == SmallBuildingBottomLeftPosition)
                return BuildingSlot.Small_BottomLeft;
            if (pos == SmallBuildingBottomRightPosition)
                return BuildingSlot.Small_BottomRight;
            
            if (pos == MediumBuildingTopPosition)
                return BuildingSlot.Medium_Top;
            if (pos == MediumBuildingBottomPosition)
                return BuildingSlot.Medium_Bottom;
            
            if (pos == LargeBuildingCenterPosition)
                return BuildingSlot.Large_Center;
            
            return pos == ExtraLargeBuildingCenterPosition ? BuildingSlot.ExtraLarge_Center : BuildingSlot.Small_TopLeft;
        }

        #endregion
    }
}