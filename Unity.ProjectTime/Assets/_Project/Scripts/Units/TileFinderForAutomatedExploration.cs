using _Project.Scripts.HelperScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;

namespace _Project.Scripts.Units
{
    public class TileFinderForAutomatedExploration
    {
        private readonly SaveDataScriptableObject _saveDataScriptableObject;

        public TileFinderForAutomatedExploration(SaveDataScriptableObject saveDataScriptableObject)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
        }

        public Tile GetAnUndiscoveredTile(Unit unit)
        {
            var activeSubcontinentTiles = _saveDataScriptableObject.Save.AllSubcontinentTiles.GetById(sub => sub.Id,
                _saveDataScriptableObject.Save.ActiveSubcontinentTilesId);

            TileFinder tileFinder = new TileFinder(activeSubcontinentTiles.Tiles);
            var tilesInRange = tileFinder.GetTilesInRange(activeSubcontinentTiles.Tiles.GetById(tile => tile.Id, unit.CurrentTileId), 3);
            foreach (var tile in tilesInRange.Shuffle())
            {
                if (tile.IsWalkable) return tile;
            }

            return null;
        }
    }
}