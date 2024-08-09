using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Game_Actions;
using _Project.Scripts.Map;
using _Project.Scripts.Pathfinding;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Models.PathFinding;
using ASP.NET.ProjectTime.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Units
{
    public class UnitMovementSystem: IDisposable
    {
        private readonly AStarPathfinding _aStarPathfinding;
        private readonly PathfinderGridDataContainer _pathfinderGridDataContainer;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly UnitPathViewer _unitPathViewer;


        private TileCoordinates _lastPosition;

        private bool _hasValidPath;
        private int _coveredPathCost;
        private int _playerAlreadyMovedPathCost;
        private int _totalPathCost;
        private int _nextNodeIndexToMove;
        private int _movementCountBetweenTwoNode;
        private float _pathCostTillNextNode;
        private float _nextNodeCost;
        private float _lockPathAtWeek;
        private List<TileCoordinates> _path;
        private TileCoordinates _destinationCoordinates;
        private Vector3 _destinationPosition;

        private Tile _finalDestinationTile;
        private bool _lockMovement = false;
        private bool _workingOnLockedPath = false;
        private bool _reInitiatedMovement = false;
        private TileCoordinates _coordinateToVisitBeforeTakingNewPath;

        private IUnitController _unitController;

        public UnitMovementSystem( AStarPathfinding aStarPathfinding, PathfinderGridDataContainer pathfinderGridDataContainer, UnitPathViewer unitPathViewer, SaveDataScriptableObject saveDataScriptableObject )
        {
            _aStarPathfinding = aStarPathfinding;
            _pathfinderGridDataContainer = pathfinderGridDataContainer;
            _unitPathViewer = unitPathViewer;
            _saveDataScriptableObject = saveDataScriptableObject; 
            TimeActions.onPulseTicked += OnTimeChangeUpdatePosition;
        }
        public void InitiateMovement(IUnitController unitController,  Tile tile )
        {
            _unitController = unitController;
            _unitController.IsMoving = true;
            _lastPosition = new TileCoordinates()
                { X = (int)(unitController.Position.x - 0.5f), Y = (int)(unitController.Position.z - 0.5f) };
            _finalDestinationTile = tile;
            if (!_lockMovement)
            {
                _path = _aStarPathfinding.FindPath(_lastPosition, tile.TileCoordinates, _pathfinderGridDataContainer.Grid);
    
                if(_path == null || _path.Count ==0) return;
                _workingOnLockedPath = false;
                _unitPathViewer.ShowPath(_lastPosition, _path, 0);

                _hasValidPath = true;
                _totalPathCost = GetTotalWeeksToMove();
                _coveredPathCost = 0;
                _nextNodeIndexToMove = 0;
                _playerAlreadyMovedPathCost = 0;
                _movementCountBetweenTwoNode = 0;
                UpdateNextNodeCost();
                UpdateNextPathLockWeek();
                _unitPathViewer.ChangeColorUpToPercentage(_coveredPathCost, _totalPathCost, Color.blue);
                UpdateDestination(tile);
                UnitActions.OnSelectedTileToMoveInDirection?.Invoke(_unitController.Id, GetNextTileDirection(_path[_nextNodeIndexToMove]));
            }
            else
            {
                var newPath = _aStarPathfinding.FindPath(_coordinateToVisitBeforeTakingNewPath, tile.TileCoordinates, _pathfinderGridDataContainer.Grid);
                if(newPath == null || newPath.Count ==0) return;
                _workingOnLockedPath = true;
                _path = newPath;
                var extraCostForFirstNode = (int)_nextNodeCost;
                _totalPathCost = GetTotalWeeksToMove() + extraCostForFirstNode;
                Debug.Log($"locked Next node cost was {extraCostForFirstNode}");
                _path.Insert(0, _coordinateToVisitBeforeTakingNewPath);
                UpdateNextNodeCost();
                _pathCostTillNextNode = _nextNodeCost;
                _nextNodeIndexToMove = 0;
                _unitPathViewer.ShowPath(_lastPosition, _path, 0);
                _coveredPathCost = _movementCountBetweenTwoNode;
                _unitPathViewer.ChangeColorUpToPercentage(_coveredPathCost, _totalPathCost, Color.red);
                UpdateDestination(tile);
                UnitActions.OnSelectedTileToMoveInDirection?.Invoke(_unitController.Id, GetNextTileDirection(_path[_nextNodeIndexToMove]));
            }
        }

        private void UpdateDestination(Tile tile)
        {
            _destinationCoordinates = _path[^1];
            _destinationPosition =
                new Vector3(_destinationCoordinates.X + 0.5f, tile.Elevation, _destinationCoordinates.Y + 0.5f);
        }

        private int GetTotalWeeksToMove()
        {
            float totalWeeksToMove = 0;
            float fCost = _pathfinderGridDataContainer.Grid[_path.Last().X, _path.Last().Y].FCost;
            //Counting and adding all the all the PathFindingModifier without the last tile
            for (int i = 0; i < _path.Count - 1; i++)
            {
                totalWeeksToMove += _pathfinderGridDataContainer.Grid[_path[i].X, _path[i].Y].PathfindingModifiers;
            }

            //Adding last time fCost with other tile's PathFindingModifier
            totalWeeksToMove += fCost;
            Debug.Log($"total week to Move {totalWeeksToMove}");
            return (int)totalWeeksToMove;
        }

        private void OnTimeChangeUpdatePosition()
        {
            
            if (_hasValidPath )
            {
                _coveredPathCost++;
                _movementCountBetweenTwoNode++;
                
                if (_coveredPathCost == _totalPathCost)
                {
                    EndMovement();
                }
                else if (_nextNodeIndexToMove < _path.Count-1 && _coveredPathCost >= _pathCostTillNextNode)
                {
                    UpdateUnitControllerPosition(new Vector3(_path[_nextNodeIndexToMove].X + 0.5f, 0.5f, _path[_nextNodeIndexToMove].Y + 0.5f));
                    if (_reInitiatedMovement)
                    {
                        _reInitiatedMovement = false;
                        return;
                    }
                    _lastPosition = _path[_nextNodeIndexToMove];
                    _playerAlreadyMovedPathCost = _coveredPathCost;
                    _nextNodeIndexToMove++;
                    UnitActions.OnSelectedTileToMoveInDirection?.Invoke(_unitController.Id, GetNextTileDirection(_path[_nextNodeIndexToMove]));
                    _unitPathViewer.ShowPath(_lastPosition, _path, _nextNodeIndexToMove);
                    UpdateNextPathLockWeek();
                }
                UpdateNextNodeCost();

                // Debug.Log($"fcost of next to move tile {_pathCostTillNextNode}");
                if (_coveredPathCost  >= _lockPathAtWeek && _coveredPathCost < _pathCostTillNextNode)
                {
                    _lockMovement = true;
                    _coordinateToVisitBeforeTakingNewPath = _path[_nextNodeIndexToMove];
                    _unitPathViewer.ChangeColorUpToPercentage(_coveredPathCost - _playerAlreadyMovedPathCost, _totalPathCost -_playerAlreadyMovedPathCost, Color.red);
                }
                else
                {
                    _lockMovement = false;
                    _unitPathViewer.ChangeColorUpToPercentage(_coveredPathCost - _playerAlreadyMovedPathCost, _totalPathCost -_playerAlreadyMovedPathCost, Color.blue);

                }
                
            }
            else
            {
                if(_unitController != null) _unitController.IsMoving = false;
            }
        }

        private Directions GetNextTileDirection(TileCoordinates tileCoordinates)
        {
            var allTiles = _saveDataScriptableObject.Save.AllSubcontinentTiles.GetById(subcontinent => subcontinent.Id,
                _saveDataScriptableObject.Save.ActiveSubcontinentTilesId).Tiles;
            var lastTileCoordinates = allTiles.GetById(tile => tile.Id, _unitController.Unit.CurrentTileId).TileCoordinates;
            if (tileCoordinates.X > lastTileCoordinates.X)
            {
                return Directions.Right;
                // if (tileCoordinates.Y == lastTileCoordinates.Y) return Directions.Right;
                // if (tileCoordinates.Y > lastTileCoordinates.Y) return Directions.UpperRight;
                // if (tileCoordinates.Y < lastTileCoordinates.Y) return Directions.DownRight;
            }else if (tileCoordinates.X < lastTileCoordinates.X)
            {
                return Directions.Left;
                // if (tileCoordinates.Y == lastTileCoordinates.Y) return Directions.Left;
                // if (tileCoordinates.Y > lastTileCoordinates.Y) return Directions.UpperLeft;
                // if (tileCoordinates.Y < lastTileCoordinates.Y) return Directions.DownLeft;
            }else if (tileCoordinates.Y > lastTileCoordinates.Y)
            {
                return Directions.Up;
            }else if (tileCoordinates.Y < lastTileCoordinates.Y)
            {
                return Directions.Down;
            }

            Debug.Log($"No directions found for unit animation");
            return Directions.Right;
        }

        private void EndMovement()
        {
            _hasValidPath = false;
            _unitController.IsMoving = false; 
            UpdateUnitControllerPosition(_destinationPosition);
            _lastPosition = _destinationCoordinates;
            _unitPathViewer.ClearLine();
            _lockMovement = false;
        }

        private void UpdateUnitControllerPosition(Vector3 targetPosition)
        {
            _unitController.Position = targetPosition;
            var targetTileCoordinates = new TileCoordinates()
                { X = (int)(targetPosition.x - 0.5f), Y = (int)(targetPosition.z - 0.5f) };
            TileFinder tileFinder = new TileFinder(_saveDataScriptableObject.Save.AllSubcontinentTiles.GetById(subcontinent => subcontinent.Id, _saveDataScriptableObject.Save.ActiveSubcontinentTilesId ).Tiles);
            var targetTile =
                tileFinder.GetTileByCoordinates(targetTileCoordinates);
            _movementCountBetweenTwoNode = 0;
            UnitActions.onUnitMovedToTile?.Invoke(_unitController.Id, targetTile.Id);
            if (_workingOnLockedPath && _hasValidPath)
            {
                _reInitiatedMovement = true;
                _lockMovement = false;
                InitiateMovement(_unitController, _finalDestinationTile);
            }
        }

        private void UpdateNextNodeCost()
        {
            _pathCostTillNextNode = 0;
            float pathCostTillPreviousNode = 0;
            for (int i = 0; i <= _nextNodeIndexToMove; i++)
            {
                _pathCostTillNextNode += _pathfinderGridDataContainer
                                    .Grid[_path[i].X, _path[i].Y].PathfindingModifiers +
                                _pathfinderGridDataContainer
                                    .Grid[_path[i].X, _path[i].Y].MovecostFromPreviousTile;
                
                if (_workingOnLockedPath && i == 0) _pathCostTillNextNode = _nextNodeCost;
                if (i < _nextNodeIndexToMove) pathCostTillPreviousNode = _pathCostTillNextNode;
            }

            

            if (_nextNodeIndexToMove >= _path.Count - 1) _pathCostTillNextNode = _totalPathCost;
            _nextNodeCost = _pathCostTillNextNode - pathCostTillPreviousNode;
        }

        private void UpdateNextPathLockWeek()
        {
            var costToMoveToNextNode = (_pathfinderGridDataContainer.Grid[_path[_nextNodeIndexToMove].X, _path[_nextNodeIndexToMove].Y].PathfindingModifiers 
                                        + _pathfinderGridDataContainer.Grid[_path[_nextNodeIndexToMove].X, _path[_nextNodeIndexToMove].Y].MovecostFromPreviousTile);
            if (_path.Count == 1 ) costToMoveToNextNode = _totalPathCost;
            if( _path.Count == 2 && _workingOnLockedPath ) costToMoveToNextNode = _totalPathCost - _nextNodeCost;
            _lockPathAtWeek = _coveredPathCost + (costToMoveToNextNode / 2f);
            if (_nextNodeIndexToMove >= _path.Count - 1)
            {
                _lockPathAtWeek = _coveredPathCost + ((_totalPathCost - _coveredPathCost) / 2f);
                Debug.Log($"Lock path cost {_lockPathAtWeek } for last node with total cost {_totalPathCost}");
                
            }
            Debug.Log($"Lock path cost {_lockPathAtWeek } for next node cost {_coveredPathCost+ costToMoveToNextNode}");

        }


        public void Dispose()
        {
            TimeActions.onPulseTicked -= OnTimeChangeUpdatePosition;
        }
    }
}