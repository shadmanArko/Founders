using System.Collections.Generic;
using _Project.Scripts.MapDataGenerator;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.Tiles.Factory;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Tiles
{
    public class TileManager
    {
        public ITileController SelectedTileController = null;
        public List<ITileController> TileControllers = new List<ITileController>();
        
        private Vertex[] _vertices;
        private int[] _triangles;
        
        private readonly TileMapInitializingDataContainer _tileMapInitializingDataContainer;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly TileShapeGenerator _tileShapeGenerator;
        private readonly SubcontinentsContainer _subcontinentsContainer;
        private readonly TileFactory _tileFactory;
        
        public TileManager(TileFactory tileFactory, TileMapInitializingDataContainer tileMapInitializingDataContainer, SaveDataScriptableObject saveDataScriptableObject, TileShapeGenerator tileShapeGenerator, SubcontinentsContainer subcontinentsContainer)
        {
            _tileMapInitializingDataContainer = tileMapInitializingDataContainer;
            _saveDataScriptableObject = saveDataScriptableObject;
            _tileShapeGenerator = tileShapeGenerator;
            _tileFactory = tileFactory;
            _subcontinentsContainer = subcontinentsContainer;
            (_vertices, _triangles) = _tileShapeGenerator.CreateShape();
        }

        public void GenerateMapTileMesh()
        {
            foreach (var subcontinentTile in _saveDataScriptableObject.Save.AllSubcontinentTiles)
            {
                var tileParent = new GameObject(subcontinentTile.SubcontinentName);
                foreach (var bigTile in subcontinentTile.Tiles)
                {
                    var newTile = _tileFactory.Create(bigTile, _triangles, tileParent.transform);
                    TileControllers.Add(newTile);
                }

                var subcontinent = _subcontinentsContainer.subcontinents.GetById(
                    subcontinent1 => subcontinent1.subcontinentName, subcontinentTile.SubcontinentName);
                tileParent.transform.position = new Vector3(subcontinent.subcontinentPosition.x, 0,
                    subcontinent.subcontinentPosition.y);
                // if(_saveDataScriptableObject.Save.ActiveSubcontinentTilesId != subcontinentTile.Id) tileParent.SetActive(false);
            }
        }
    }
}