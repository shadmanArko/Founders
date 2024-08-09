using System;
using _Project.Scripts.Natural_Resources.Factory;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime._1._Repositories;
using UnityEngine;

namespace _Project.Scripts.Natural_Resources
{
    public class NaturalResourceManager : IDisposable
    {
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly NaturalResourceFactory _naturalResourceFactory;
        private readonly SubcontinentsContainer _subcontinentsContainer;

        public NaturalResourceManager(SaveDataScriptableObject saveDataScriptableObject, NaturalResourceFactory naturalResourceFactory, SubcontinentsContainer subcontinentsContainer)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
            _naturalResourceFactory = naturalResourceFactory;
            _subcontinentsContainer = subcontinentsContainer;
        }

        public void SpawnAllResources()
        {
            foreach (var subcontinentTileContainer in _saveDataScriptableObject.Save.AllSubcontinentTiles)
            {
                // if(subcontinentTileContainer.Id != _saveDataScriptableObject.Save.ActiveSubcontinentTilesId) continue;
                var subcontinentOffset = _subcontinentsContainer.subcontinents.GetById(
                    subcontinent => subcontinent.subcontinentName, subcontinentTileContainer.SubcontinentName).subcontinentPosition;
                foreach (var tile in subcontinentTileContainer.Tiles)
                {
                    if (tile.NaturalResource != null && tile.NaturalResource.Name.Length > 0)
                    {
                        var spawnPosition = new Vector3(tile.TileCoordinates.X + subcontinentOffset.x, tile.Elevation, tile.TileCoordinates.Y + subcontinentOffset.y);
                        _naturalResourceFactory.Create(tile.Id, tile.NaturalResource, spawnPosition);
                    }
                }
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
