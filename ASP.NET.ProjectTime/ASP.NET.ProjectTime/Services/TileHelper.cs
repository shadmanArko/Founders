using System;
using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;

namespace ASP.NET.ProjectTime.Services
{
    public static class TileHelper
    {
        public static List<TileCoordinates> GetListOfNeighbourTileCoordinates(this Tile tile)
        {
            var listOfNeighbourTileCoordinates = new List<TileCoordinates>();
            TileCoordinates top = new TileCoordinates();
            TileCoordinates topLeft = new TileCoordinates();
            TileCoordinates topRight = new TileCoordinates();
            TileCoordinates left = new TileCoordinates();
            TileCoordinates right = new TileCoordinates();
            TileCoordinates bottom = new TileCoordinates();
            TileCoordinates bottomLeft = new TileCoordinates();
            TileCoordinates bottomRight = new TileCoordinates();
            

            top.X = tile.XPosition + 0;
            top.Y = tile.YPosition + 1;
            listOfNeighbourTileCoordinates.Add(top);

            topLeft.X = tile.XPosition - 1;
            topLeft.Y = tile.YPosition + 1;
            listOfNeighbourTileCoordinates.Add(topLeft);
            
            topRight.X = tile.XPosition + 1;
            topRight.Y = tile.YPosition + 1;
            listOfNeighbourTileCoordinates.Add(topRight);
            
            left.X = tile.XPosition - 1;
            left.Y = tile.YPosition + 0;
            listOfNeighbourTileCoordinates.Add(left);
            
            right.X = tile.XPosition + 1;
            right.Y = tile.YPosition + 0;
            listOfNeighbourTileCoordinates.Add(right);
            
            bottom.X = tile.XPosition + 0;
            bottom.Y = tile.YPosition - 1;
            listOfNeighbourTileCoordinates.Add(bottom);
            
            bottomLeft.X = tile.XPosition - 1;
            bottomLeft.Y = tile.YPosition - 1;
            listOfNeighbourTileCoordinates.Add(bottomLeft);
            
            bottomRight.X = tile.XPosition + 1;
            bottomRight.Y = tile.YPosition - 1;
            listOfNeighbourTileCoordinates.Add(bottomRight);

            return listOfNeighbourTileCoordinates;
        }
        public static List<TileCoordinates> GetListOfNonDiagonalNeighbourTileCoordinates(this Tile tile)
        {
            var listOfNeighbourTileCoordinates = new List<TileCoordinates>();
            TileCoordinates top = new TileCoordinates();
            TileCoordinates left = new TileCoordinates();
            TileCoordinates right = new TileCoordinates();
            TileCoordinates bottom = new TileCoordinates();


            top.X = tile.XPosition + 0;
            top.Y = tile.YPosition + 1;
            listOfNeighbourTileCoordinates.Add(top);

            left.X = tile.XPosition - 1;
            left.Y = tile.YPosition + 0;
            listOfNeighbourTileCoordinates.Add(left);
            
            right.X = tile.XPosition + 1;
            right.Y = tile.YPosition + 0;
            listOfNeighbourTileCoordinates.Add(right);
            
            bottom.X = tile.XPosition + 0;
            bottom.Y = tile.YPosition - 1;
            listOfNeighbourTileCoordinates.Add(bottom);
            
            

            return listOfNeighbourTileCoordinates;
        }

        public static TileCoordinates GetNeighbourTileCoordinatesInDirection(this Tile tile, Directions directions)
        {
            var neighbourTileCoordinates = new TileCoordinates
            {
                X = tile.TileCoordinates.X,
                Y = tile.TileCoordinates.Y
            };
            switch (directions)
            {
                case Directions.Left:
                    neighbourTileCoordinates.X -= 1;
                    break;
                case Directions.Right:
                    neighbourTileCoordinates.X +=  1;
                    break;
                case Directions.Up:
                    neighbourTileCoordinates.Y +=  1;
                    break;
                case Directions.Down:
                    neighbourTileCoordinates.Y -=  1;
                    break;

                default:
                    return neighbourTileCoordinates;
            }

            return neighbourTileCoordinates;
        }

        public static bool IsWaterTile(this Tile tile)
        {
            if (tile.Terrain == TerrainType.coast.ToString() || tile.Terrain == TerrainType.ocean.ToString() ||
                tile.Feature == FeatureType.lake.ToString())
            {
                return true;
            }

            return false;
        }
        
        public static int DistanceTo(this Tile tile1, Tile tile2)
        {
            int dx = Math.Abs(tile1.XPosition - tile2.XPosition);
            int dy = Math.Abs(tile1.YPosition - tile2.YPosition);
            return Math.Max(dx, dy);
        }
        public static int MinimumInX(this Tile tile1, Tile tile2)
        {
            int dx = Math.Abs(tile1.XPosition - tile2.XPosition);
            return dx;
        }
        public static int MinimumInY(this Tile tile1, Tile tile2)
        {
            int dy = Math.Abs(tile1.YPosition - tile2.YPosition);
            return dy;
        }
    }
}