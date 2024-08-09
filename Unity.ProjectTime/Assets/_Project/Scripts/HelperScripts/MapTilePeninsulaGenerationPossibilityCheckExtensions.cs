using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;

namespace _Project.Scripts.HelperScripts
{
    public static class MapTilePeninsulaGenerationPossibilityCheckExtensions 
    {
    
        public static bool IsDownRightPeninsulaPossible(this Tile tile, int maxHeightOfPeninsula, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            //check if upper 4 tiles are ocean and each of those ocean has 2 ocean tiles in their left and 1 ocean tile in right
            // if(tile.GetListOfNeighbourTileCoordinates())
            if (tile.NeighbourTilesTerrainMatchesInDirection( maxHeightOfPeninsula+1, Directions.Down, TerrainType.ocean, listOfTiles))
            {
                //check if all these tiles have at least 1 ocean tile in left and 2 in right
                for (int i = 0; i < maxHeightOfPeninsula + 1; i++)
                {
                    var newTile = tileFinder.GetTileByXAndYPosition(tile.XPosition, tile.YPosition - i - 1);
                
                    if (!newTile.NeighbourTilesTerrainMatchesInDirection(1, Directions.Left, TerrainType.ocean,
                            listOfTiles) ||
                        !newTile.NeighbourTilesTerrainMatchesInDirection(2, Directions.Right, TerrainType.ocean,
                            listOfTiles))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        
        }
        public static bool IsDownLeftPeninsulaPossible(this Tile tile, int maxHeightOfPeninsula, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            //check if upper 4 tiles are ocean and each of those ocean has 2 ocean tiles in their left and 1 ocean tile in right
            // if(tile.GetListOfNeighbourTileCoordinates())
            if (tile.NeighbourTilesTerrainMatchesInDirection( maxHeightOfPeninsula+1, Directions.Down, TerrainType.ocean, listOfTiles))
            {
                //check if all these tiles have at least 1 ocean tile in left and 2 in right
                for (int i = 0; i < maxHeightOfPeninsula + 1; i++)
                {
                    var newTile = tileFinder.GetTileByXAndYPosition(tile.XPosition, tile.YPosition - i - 1);
                
                    if (!newTile.NeighbourTilesTerrainMatchesInDirection(2, Directions.Left, TerrainType.ocean,
                            listOfTiles) ||
                        !newTile.NeighbourTilesTerrainMatchesInDirection(1, Directions.Right, TerrainType.ocean,
                            listOfTiles))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        
        }
        public static bool IsUpRightPeninsulaPossible(this Tile tile, int maxHeightOfPeninsula, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            //check if upper 4 tiles are ocean and each of those ocean has 2 ocean tiles in their left and 1 ocean tile in right
            // if(tile.GetListOfNeighbourTileCoordinates())
            if (tile.NeighbourTilesTerrainMatchesInDirection( maxHeightOfPeninsula+1, Directions.Down, TerrainType.ocean, listOfTiles))
            {
                //check if all these tiles have at least 1 ocean tile in left and 2 in right
                for (int i = 0; i < maxHeightOfPeninsula + 1; i++)
                {
                    var newTile = tileFinder.GetTileByXAndYPosition(tile.XPosition, tile.YPosition + i + 1);
                
                    if (!newTile.NeighbourTilesTerrainMatchesInDirection(1, Directions.Left, TerrainType.ocean,
                            listOfTiles) ||
                        !newTile.NeighbourTilesTerrainMatchesInDirection(2, Directions.Right, TerrainType.ocean,
                            listOfTiles))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        
        }
        public static bool IsUpLeftPeninsulaPossible(this Tile tile, int maxHeightOfPeninsula, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            //check if upper 4 tiles are ocean and each of those ocean has 2 ocean tiles in their left and 1 ocean tile in right
            // if(tile.GetListOfNeighbourTileCoordinates())
            if (tile.NeighbourTilesTerrainMatchesInDirection( maxHeightOfPeninsula+1, Directions.Down, TerrainType.ocean, listOfTiles))
            {
                //check if all these tiles have at least 1 ocean tile in left and 2 in right
                for (int i = 0; i < maxHeightOfPeninsula + 1; i++)
                {
                    var newTile = tileFinder.GetTileByXAndYPosition(tile.XPosition, tile.YPosition + i + 1);
                
                    if (!newTile.NeighbourTilesTerrainMatchesInDirection(2, Directions.Left, TerrainType.ocean,
                            listOfTiles) ||
                        !newTile.NeighbourTilesTerrainMatchesInDirection(1, Directions.Right, TerrainType.ocean,
                            listOfTiles))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        
        }
        
        public static bool IsLeftDownPeninsulaPossible(this Tile tile, int maxHeightOfPeninsula, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            //check if upper 4 tiles are ocean and each of those ocean has 2 ocean tiles in their left and 1 ocean tile in right
            // if(tile.GetListOfNeighbourTileCoordinates())
            if (tile.NeighbourTilesTerrainMatchesInDirection( maxHeightOfPeninsula+1, Directions.Down, TerrainType.ocean, listOfTiles))
            {
                //check if all these tiles have at least 1 ocean tile in left and 2 in right
                for (int i = 0; i < maxHeightOfPeninsula + 1; i++)
                {
                    var newTile = tileFinder.GetTileByXAndYPosition(tile.XPosition- i - 1, tile.YPosition );
                
                    if (!newTile.NeighbourTilesTerrainMatchesInDirection(1, Directions.Up, TerrainType.ocean,
                            listOfTiles) ||
                        !newTile.NeighbourTilesTerrainMatchesInDirection(2, Directions.Down, TerrainType.ocean,
                            listOfTiles))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        
        }
        public static bool IsLeftUpPeninsulaPossible(this Tile tile, int maxHeightOfPeninsula, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            //check if upper 4 tiles are ocean and each of those ocean has 2 ocean tiles in their left and 1 ocean tile in right
            // if(tile.GetListOfNeighbourTileCoordinates())
            if (tile.NeighbourTilesTerrainMatchesInDirection(maxHeightOfPeninsula+1, Directions.Down, TerrainType.ocean, listOfTiles))
            {
                //check if all these tiles have at least 1 ocean tile in left and 2 in right
                for (int i = 0; i < maxHeightOfPeninsula + 1; i++)
                {
                    var newTile = tileFinder.GetTileByXAndYPosition(tile.XPosition- i - 1, tile.YPosition );
                
                    if (!newTile.NeighbourTilesTerrainMatchesInDirection(2, Directions.Up, TerrainType.ocean,
                            listOfTiles) ||
                        !newTile.NeighbourTilesTerrainMatchesInDirection(1, Directions.Down, TerrainType.ocean,
                            listOfTiles))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        
        }
        public static bool IsRightDownPeninsulaPossible(this Tile tile, int maxHeightOfPeninsula, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            //check if upper 4 tiles are ocean and each of those ocean has 2 ocean tiles in their left and 1 ocean tile in right
            // if(tile.GetListOfNeighbourTileCoordinates())
            if (tile.NeighbourTilesTerrainMatchesInDirection( maxHeightOfPeninsula+1, Directions.Down, TerrainType.ocean, listOfTiles))
            {
                //check if all these tiles have at least 1 ocean tile in left and 2 in right
                for (int i = 0; i < maxHeightOfPeninsula + 1; i++)
                {
                    var newTile = tileFinder.GetTileByXAndYPosition(tile.XPosition+ i + 1, tile.YPosition );
                
                    if (!newTile.NeighbourTilesTerrainMatchesInDirection(1, Directions.Up, TerrainType.ocean,
                            listOfTiles) ||
                        !newTile.NeighbourTilesTerrainMatchesInDirection(2, Directions.Down, TerrainType.ocean,
                            listOfTiles))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        
        }
        public static bool IsRightUpPeninsulaPossible(this Tile tile, int maxHeightOfPeninsula, List<Tile> listOfTiles)
        {
            var tileFinder = new TileFinder(listOfTiles);
            //check if upper 4 tiles are ocean and each of those ocean has 2 ocean tiles in their left and 1 ocean tile in right
            // if(tile.GetListOfNeighbourTileCoordinates())
            if (tile.NeighbourTilesTerrainMatchesInDirection(maxHeightOfPeninsula+1, Directions.Down, TerrainType.ocean, listOfTiles))
            {
                //check if all these tiles have at least 1 ocean tile in left and 2 in right
                for (int i = 0; i < maxHeightOfPeninsula + 1; i++)
                {
                    var newTile = tileFinder.GetTileByXAndYPosition(tile.XPosition+ i + 1, tile.YPosition );
                
                    if (!newTile.NeighbourTilesTerrainMatchesInDirection(2, Directions.Up, TerrainType.ocean,
                            listOfTiles) ||
                        !newTile.NeighbourTilesTerrainMatchesInDirection(1, Directions.Down, TerrainType.ocean,
                            listOfTiles))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        
        }

    }
}
