using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;

namespace _Project.Scripts.HelperScripts
{
    public static class MapTileMountainExtensions
    {
        public static bool IsSecondaryMountainPossible(this Tile tile, int maxLengthOfMountain, List<Tile> listOfTiles, int mapSizeX, int mapSizeY)
        {
            if (tile.IsBorderTile(mapSizeX, mapSizeY))
            {
                return false;
            }
            var tileFinder = new TileFinder(listOfTiles);
            var tilesInRange = tileFinder.GetTilesInRange(tile, maxLengthOfMountain + 1);
            
            foreach (var tileInRange in tilesInRange)
            {
                if (tileInRange.ElevationType == ElevationType.mountain.ToString())
                {
                    return false;
                }
            }
            

            return true;
        }
        public static void CreateSecondaryMountain(this Tile rootTile, int maxLengthOfMountain, List<Tile> listOfTiles, int mapSizeX, int mapSizeY)
        {
            var tileFinder = new TileFinder(listOfTiles);
            rootTile.ElevationType = ElevationType.mountain.ToString();
            Tile lastMountainTile = rootTile;
            for (int i = 1; i < maxLengthOfMountain; i++)
            {
                if(lastMountainTile != null) lastMountainTile = ExpandMountainRange(lastMountainTile, listOfTiles);
            }
            
        }

        public static Tile ExpandMountainRange(this Tile mountainTile, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            var neighbourTileCoordinates = mountainTile.GetListOfNeighbourTileCoordinates();
            foreach (var tileCoordinates in neighbourTileCoordinates.Shuffle())
            {
                var neighbourTile = tileFinder.GetTileByCoordinates(tileCoordinates);
                if (neighbourTile!= null && neighbourTile.Terrain == TerrainType.grassland.ToString() && 
                    neighbourTile.NeighbourOfElevationCount(ElevationType.mountain, listOfTiles) < 2)
                {
                    neighbourTile.ElevationType = ElevationType.mountain.ToString();
                    return neighbourTile;
                }
                
            }

            return null;
        }
        public static int NeighbourOfElevationCount(this Tile tile, ElevationType elevationType, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            var neighbourTileCoordinates = tile.GetListOfNeighbourTileCoordinates();
            int count = 0;
            foreach (var tileCoordinates in neighbourTileCoordinates)
            {
                var neighbourTile = tileFinder.GetTileByCoordinates(tileCoordinates);
                if (neighbourTile!= null && neighbourTile.ElevationType == elevationType.ToString())  
                {
                    count++;
                }
            }

            return count;
        }
    }
    
}
