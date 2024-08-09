using System.Collections.Generic;
using _Project.Scripts.Game_Actions;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Helpers;
using ASP.NET.ProjectTime.Models;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Units.Factory
{
    public class UnitGenerationFactory : IFactory<string, string, IUnitController>
    {
        private readonly DiContainer _container;
        private readonly RaisedUnitPrefabsSo _raisedUnitPrefabsSo;
        private readonly SaveDataScriptableObject _saveDataScriptableObject; 

        public UnitGenerationFactory(DiContainer container, RaisedUnitPrefabsSo raisedUnitPrefabsSo, SaveDataScriptableObject saveDataScriptableObject)
        {
            _container = container;
            _raisedUnitPrefabsSo = raisedUnitPrefabsSo;
            _saveDataScriptableObject = saveDataScriptableObject;
        }

        public IUnitController Create(string unitType, string spawnTileId)
        {
            var unitObject = _container.InstantiatePrefab(_raisedUnitPrefabsSo.settlerGameObject);
            var unitView = _container.InstantiateComponent<UnitView>(unitObject);
            var unitAnimator = _container.InstantiateComponent<UnitAnimator>(unitObject);
            var spawnTile = _saveDataScriptableObject.Save.AllSubcontinentTiles.GetById(subcontinent => subcontinent.Id, _saveDataScriptableObject.Save.ActiveSubcontinentTilesId ).Tiles.GetById(tile =>tile.Id,  spawnTileId);
            var unitModel = new Unit(NewIdGenerator.GenerateNewId(), unitType, new List<string>());
            unitModel.CurrentTileId = spawnTileId;
            var onClickUnit = _container.InstantiateComponent<OnclickUnit>(unitObject);
            onClickUnit.unitId = unitModel.Id;
            unitView.gameObject.name = $"{unitType}";
            var spawnPosition = new Vector3(spawnTile.TileCoordinates.X + 0.5f, spawnTile.Elevation,
                spawnTile.TileCoordinates.Y + 0.5f);
            return _container.Instantiate<UnitController>(new object[] {unitModel, unitView, unitAnimator, unitType, spawnPosition});
        }
       
    }
    
    
    
    
    
    // public class UnitGenerationFactory : IFactory<string, IUnitController>
    // {
    //     private readonly DiContainer _container;
    //     private readonly RaisedUnitPrefabsSo _raisedUnitPrefabsSo;
    //     private readonly AsyncReference<GameObject> _unitPrefabReference;
    //
    //     public UnitGenerationFactory(DiContainer container, RaisedUnitPrefabsSo raisedUnitPrefabsSo)
    //     {
    //         _container = container;
    //         _raisedUnitPrefabsSo = raisedUnitPrefabsSo;
    //         _unitPrefabReference = Addressables.LoadAssetAsync<GameObject>("path/to/your/unit/prefab");
    //     }
    //
    //     public async IUnitController Create(string param)
    //     {
    //         var unitObject = await _unitPrefabReference.Task;
    //         var unitView = _container.InstantiateComponent<UnitView>(unitObject);
    //         var unitModel = new Unit(NewIdGenerator.GenerateNewId(), param);
    //         unitView.gameObject.name = $"{param}";
    //         return _container.Instantiate<UnitController>(new object[] {unitModel, unitView, param});
    //     }
    // }
}