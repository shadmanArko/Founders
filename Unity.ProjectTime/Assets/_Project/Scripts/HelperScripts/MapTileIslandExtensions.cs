using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;

namespace _Project.Scripts.HelperScripts
{
    public static class MapTileIslandExtensions
    {
        public static bool IsSmallIslandPossible(this Tile tile, int maxRangeOfIsland, List<Tile> listOfTiles, int mapSizeX, int mapSizeY)
        {
            if (tile.IsBorderTile(mapSizeX, mapSizeY))
            {
                return false;
            }
            var tileFinder = new TileFinder(listOfTiles);
            var tilesInRange = tileFinder.GetTilesInRange(tile, maxRangeOfIsland + 1);
            if (tilesInRange.Count > 0)
            {
                foreach (var tileInRange in tilesInRange)
                {
                    if (tileInRange.Terrain != TerrainType.ocean.ToString())
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }
        
        public static void CreateIsland(this Tile rootTile, int maxNumberOfLandTiles, List<Tile> listOfTiles, int mapSizeX, int mapSizeY)
        {
            var tileFinder = new TileFinder(listOfTiles);
            rootTile.Terrain = TerrainType.grassland.ToString();
            rootTile.ElevationType = ElevationType.flat.ToString();
            rootTile.IsIslandTile = true;
            List<Tile> islandTiles = new List<Tile> { rootTile };
            for (int i = 1; i < maxNumberOfLandTiles; i++)
            {
               islandTiles = ExpandIsland(islandTiles, listOfTiles, mapSizeX, mapSizeY);
            }
            
        }

        private static List<Tile> ExpandIsland(List<Tile> islandTiles, List<Tile> listOfTiles, int mapSizeX, int mapSizeY)
        {
            var tileFinder = new TileFinder(listOfTiles);
            islandTiles = islandTiles.Shuffle();
            foreach (var islandTile in islandTiles)
            {
                var neighborTileCoordinates = islandTile.GetListOfNonDiagonalNeighbourTileCoordinates().Shuffle();
                foreach (var tileCoordinate in neighborTileCoordinates)
                {
                    var tile = tileFinder.GetTileByCoordinates(tileCoordinate);
                    if (tile != null && tile.Terrain != TerrainType.grassland.ToString() && 
                        tile.Terrain != TerrainType.grassland.ToString() && !tile.IsBorderTile(mapSizeX, mapSizeY))
                    {
                        tile.Terrain = TerrainType.grassland.ToString();
                        tile.ElevationType = ElevationType.flat.ToString();
                        tile.IsIslandTile = true;
                        islandTiles.Add(tile);
                        return islandTiles;
                    }
                }
            }

            return islandTiles;
        }
        
        public static bool IsBorderTile(this Tile tileToCheck, int mapSizeX, int mapSizeY)
        {

            if (tileToCheck.XPosition == 0 || tileToCheck.XPosition == mapSizeX - 1 ||
                tileToCheck.YPosition == 0 || tileToCheck.YPosition == mapSizeY - 1)
            {
                return true;
            }

            return false;
        }
    }
}