using System;
using _Project.Scripts.Game_Actions;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Units
{
    public class UnitController : IUnitController, IDisposable
    {
        private Unit _unit;
        private readonly UnitView _unitView;
        private UnitAnimator _unitAnimator;
        private string _unitName;
        private Vector3 _unitSpawnPosition;
        private SaveDataScriptableObject _saveDataScriptableObject;
        public UnitController(SaveDataScriptableObject saveDataScriptableObject, Unit unit, UnitView unitView, UnitAnimator unitAnimator, string unitName, Vector3 spawnPosition)
        {
            _unit = unit;
            _unitView = unitView;
            _unitAnimator = unitAnimator;
            _unitName = unitName;
            _unitSpawnPosition = spawnPosition;
            _saveDataScriptableObject = saveDataScriptableObject;
            UnitActions.OnSelectedTileToMoveInDirection += ControlUnitsMovementAnimation;
            UnitActions.onUnitMovedToTile += OnUnitMovedToTile;
            Config();
        }

        public string Id => _unit.Id;
        public Vector3 Position
        {
            get => _unitView.Position;
            set => _unitView.Position = value;
        }

        public Unit Unit => _unit;
        public bool IsMoving { get; set; }
        public UnitMovementSystem UnitMovementSystem { get; set; }
        public UnitAnimator UnitAnimator
        {
            get => _unitAnimator;
            set => _unitAnimator = value;
        }

        public void Config()
        {
            Position = _unitSpawnPosition;
        }
        private void OnUnitMovedToTile(string movedUnitControllerId, string tileId)
        {
            
                if (Id == movedUnitControllerId)
                {
                    Debug.Log("Updated Unit Id");
                    Unit.CurrentTileId = tileId;
                    UnitAnimator.ShowIdleAnimation();
                }
            
        }
        public void ControlUnitsMovementAnimation(string unitId, Directions direction) 
        {
            
            if (Id == unitId)
            {
                if (direction == Directions.Down)
                {
                    UnitAnimator.ShowFrontWalkAnimation();
                }else if (direction == Directions.Right)
                {
                    UnitAnimator.ShowRightWalkAnimation();
                }else if (direction == Directions.Left)
                {
                    UnitAnimator.ShowLeftWalkAnimation();
                }
                else if (direction == Directions.Up)
                {
                    UnitAnimator.ShowBackWalkAnimation();
                }
            }
        }
        public void Destroy()
        {
            _saveDataScriptableObject.Save.Units.Remove(_unit);
            Object.Destroy(_unitView.gameObject);
        }

        public void Dispose()
        {
            UnitActions.onUnitMovedToTile -= OnUnitMovedToTile;
            UnitActions.OnSelectedTileToMoveInDirection -= ControlUnitsMovementAnimation;
        }
    }
}