using System;
using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;
using Random = UnityEngine.Random;

namespace _Project.Scripts.HelperScripts
{
    public static class MapTilePeninsulaExtensions 
    {
        public static bool NeighbourTilesTerrainMatchesInDirection(this Tile tile, int numberOfTiles, Directions direction, TerrainType terrainType, List<Tile> tiles)
        {
            var tileFinder = new TileFinder(tiles);
            if (tile == null) return false;
            switch (direction)
            {
                case Directions.Left:
                    for (int i = 0; i < numberOfTiles; i++)
                    {
                        var neighbourTile = tileFinder.GetTileByXAndYPosition(tile.XPosition - 1 - i, tile.YPosition);
                        if (neighbourTile == null) return false;
                        if (neighbourTile.Terrain != terrainType.ToString()) return false;
                    }
                    break;
                case Directions.Right:
                    for (int i = 0; i < numberOfTiles; i++)
                    {
                        var neighbourTile = tileFinder.GetTileByXAndYPosition(tile.XPosition + 1 + i, tile.YPosition);
                        if (neighbourTile == null) return false;
                        if (neighbourTile.Terrain != terrainType.ToString()) return false;
                    }
                    break;
                case Directions.Up:
                    for (int i = 0; i < numberOfTiles; i++)
                    {
                        var neighbourTile = tileFinder.GetTileByXAndYPosition(tile.XPosition, tile.YPosition + 1 + i);
                        if (neighbourTile == null) return false;
                        if (neighbourTile.Terrain != terrainType.ToString()) return false;
                    }
                    break;
                case Directions.Down:
                    for (int i = 0; i < numberOfTiles; i++)
                    {
                        var neighbourTile = tileFinder.GetTileByXAndYPosition(tile.XPosition, tile.YPosition - 1 - i);
                        if (neighbourTile == null) return false;
                        if (neighbourTile.Terrain != terrainType.ToString()) return false;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            return true;
        }
        
        public static void GenerateVerticalPeninsula(this Tile peninsulaRootTile, int maxHeightOfPeninsula, Directions xDirection, Directions yDirection, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            int xIncrement = xDirection == Directions.Right ? 1 : -1;
            int yIncrement = yDirection == Directions.Up ? 1 : -1;
            var lastTile = tileFinder.GetTileByXAndYPosition(peninsulaRootTile.XPosition, peninsulaRootTile.YPosition + yIncrement);
            lastTile.Terrain = TerrainType.grassland.ToString();
            lastTile.ElevationType = ElevationType.flat.ToString();
            Tile grassLandNeighbourOfLastTile = null;
            bool lastNewTileWasOnline = true;
            int wentBackToPreviousGrasslandCount = 0;
            
            for (int i = 1; i < maxHeightOfPeninsula ; i++)
            {
                Tile newTile;
                var probability = Random.Range(0f, 1f);
                if (grassLandNeighbourOfLastTile != null && probability > 0.5f && wentBackToPreviousGrasslandCount < 1)
                {
                    //goes down from one of two grass land tile
                    newTile = tileFinder.GetTileByXAndYPosition(grassLandNeighbourOfLastTile.XPosition, grassLandNeighbourOfLastTile.YPosition + yIncrement);
                    grassLandNeighbourOfLastTile = null;
                    wentBackToPreviousGrasslandCount++;
                }
                else if (lastNewTileWasOnline)
                {
                    if (probability > 0.75f)
                    {
                        //go down
                        newTile = tileFinder.GetTileByXAndYPosition(lastTile.XPosition, lastTile.YPosition + yIncrement);
                    }
                    else
                    {
                        //go right
                        newTile = tileFinder.GetTileByXAndYPosition(lastTile.XPosition + xIncrement, lastTile.YPosition );
                        grassLandNeighbourOfLastTile = lastTile;
                    }
                }
                else
                {
                    if (probability < 0.75f)
                    {
                        //go down
                        newTile = tileFinder.GetTileByXAndYPosition(lastTile.XPosition, lastTile.YPosition + yIncrement);
                    }
                    else
                    {
                        //go left
                        newTile = tileFinder.GetTileByXAndYPosition(lastTile.XPosition - xIncrement, lastTile.YPosition );
                        grassLandNeighbourOfLastTile = lastTile;
                    }
                }
                
                newTile.Terrain = TerrainType.grassland.ToString();
                newTile.ElevationType = ElevationType.flat.ToString();
                lastNewTileWasOnline = (newTile.XPosition == peninsulaRootTile.XPosition);
                lastTile = newTile;
            }
            
        }
        
        public static void GenerateHorizontalPeninsula(this Tile peninsulaRootTile, int maxHeightOfPeninsula, Directions xDirection, Directions yDirection, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            int xIncrement = xDirection == Directions.Right ? 1 : -1;
            int yIncrement = yDirection == Directions.Up ? 1 : -1;
            var lastTile = tileFinder.GetTileByXAndYPosition(peninsulaRootTile.XPosition + xIncrement, peninsulaRootTile.YPosition);
            lastTile.Terrain = TerrainType.grassland.ToString();
            lastTile.ElevationType = ElevationType.flat.ToString();
            Tile grassLandNeighbourOfLastTile = null;
            bool lastNewTileWasOnline = true;
            int wentBackToPreviousGrasslandCount = 0;
            
            for (int i = 1; i < maxHeightOfPeninsula ; i++)
            {
                Tile newTile;
                var probability = Random.Range(0f, 1f);
                if (grassLandNeighbourOfLastTile != null && probability > 0.5f && wentBackToPreviousGrasslandCount < 1)
                {
                    //goes down from one of two grass land tile
                    newTile = tileFinder.GetTileByXAndYPosition(grassLandNeighbourOfLastTile.XPosition + xIncrement, grassLandNeighbourOfLastTile.YPosition );
                    grassLandNeighbourOfLastTile = null;
                    wentBackToPreviousGrasslandCount++;
                }
                else if (lastNewTileWasOnline)
                {
                    if (probability > 0.75f)
                    {
                       
                        //go horizontal
                        newTile = tileFinder.GetTileByXAndYPosition(lastTile.XPosition + xIncrement, lastTile.YPosition );
                        
                        
                    }
                    else
                    {
                        //go vertical
                        newTile = tileFinder.GetTileByXAndYPosition(lastTile.XPosition, lastTile.YPosition + yIncrement);
                        grassLandNeighbourOfLastTile = lastTile;
                        
                    }
                }
                else
                {
                    if (probability < 0.75f)
                    {
                        
                        //go horizontal
                        newTile = tileFinder.GetTileByXAndYPosition(lastTile.XPosition + xIncrement, lastTile.YPosition );
                        
                    }
                    else
                    {
                        //go vertical
                        newTile = tileFinder.GetTileByXAndYPosition(lastTile.XPosition, lastTile.YPosition - yIncrement);
                        grassLandNeighbourOfLastTile = lastTile;
                    }
                }
                
                newTile.Terrain = TerrainType.grassland.ToString();
                newTile.ElevationType = ElevationType.flat.ToString();
                lastNewTileWasOnline = (newTile.YPosition == peninsulaRootTile.YPosition);
                lastTile = newTile;
            }
            
        }

    }
}
