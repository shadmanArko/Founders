using System.Collections.Generic;
using _Project.Scripts.HelperScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;
using UnityEngine;

namespace _Project.Scripts.Units
{
    public class SettlerBestSpawningTileFinder
    {
        private readonly SaveDataScriptableObject _saveDataScriptableObject;

        public SettlerBestSpawningTileFinder(SaveDataScriptableObject saveDataScriptableObject)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
        }
        public string GetARandomSettlerSpawningTileId( )
        {
            var tiles = _saveDataScriptableObject.Save.AllSubcontinentTiles.GetById(subcontinent => subcontinent.Id, _saveDataScriptableObject.Save.ActiveSubcontinentTilesId ).Tiles;
            TileFinder tileFinder = new TileFinder(tiles);
            tiles = tileFinder.GetTilesBySubcontinent("Middle East Subcontinent");
            tileFinder = new TileFinder(tiles);
            List<Tile> eligibleTilesForSpawning = new List<Tile>();
            foreach (var tile in tiles)
            {
                if(tile.ElevationType == ElevationType.mountain.ToString() || tile.IsWaterTile()) continue;
                var edibleAnimalsCount = 0;
                var stapleResourceCount = 0;
                var waterGenericsResourceCount = 0;
                var otherResourceCount = 0;
            
                var listOfNeighbourCoordinates = tile.GetListOfNeighbourTileCoordinates();
                foreach (var neighbourCoordinate in listOfNeighbourCoordinates)
                {
                    var neighbourTile = tileFinder.GetTileByCoordinates(neighbourCoordinate);
                    if (neighbourTile != null && neighbourTile.NaturalResource!= null &&  neighbourTile.NaturalResource.Category != null) 
                    {
                        var resourceCategory = neighbourTile.NaturalResource.Category;
                        if (resourceCategory.Contains("Staple")) stapleResourceCount++;
                        else if (resourceCategory.Contains("Edible Animals")) edibleAnimalsCount++;
                        else if (resourceCategory.Contains("Water Generics")) waterGenericsResourceCount++;
                        else otherResourceCount++;
                    }
                }

                if ((edibleAnimalsCount > 0 || stapleResourceCount > 0 || waterGenericsResourceCount > 0) &&
                    otherResourceCount > 0)
                {
                    eligibleTilesForSpawning.Add(tile);
                }
            }

        
            var selectedTile = GetBestTileToSpawn(eligibleTilesForSpawning);
            Vector3 selectedTilePosition = new Vector3(selectedTile.TileCoordinates.X + 0.5f, selectedTile.Elevation,
                selectedTile.TileCoordinates.Y + 0.5f);
            return selectedTile.Id;
        }
        private static Tile GetBestTileToSpawn(List<Tile> eligibleTilesForSpawning)
        {
            List<Tile> grasLandOrPlainsTiles = new List<Tile>();
            List<Tile> dryLandOrTundraTiles = new List<Tile>();
            List<Tile> desertOrSnowTiles = new List<Tile>();

            foreach (var tile in eligibleTilesForSpawning.Shuffle())  
            {
                if (tile.Terrain == TerrainType.grassland.ToString() || tile.Terrain == TerrainType.plains.ToString())
                {
                    grasLandOrPlainsTiles.Add(tile);
                }else if (tile.Terrain == TerrainType.dryland.ToString() || tile.Terrain == TerrainType.tundra.ToString())
                {
                    dryLandOrTundraTiles.Add(tile);
                }else if (tile.Terrain == TerrainType.desert.ToString() || tile.Terrain == TerrainType.snow.ToString())
                {
                    desertOrSnowTiles.Add(tile);
                }
            }

            if (grasLandOrPlainsTiles.Count > 0) return grasLandOrPlainsTiles[0];
            if (dryLandOrTundraTiles.Count > 0) return dryLandOrTundraTiles[0];
            if (desertOrSnowTiles.Count > 0) return desertOrSnowTiles[0];

            return eligibleTilesForSpawning[0];
        }

    }
}