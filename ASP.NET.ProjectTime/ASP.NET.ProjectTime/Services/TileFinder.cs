using System;
using System.Collections.Generic;
using System.Linq;
using ASP.NET.ProjectTime.Models;

namespace ASP.NET.ProjectTime.Services
{
    public class TileFinder
    {
        private List<Tile> _tiles;

        public List<Tile> Tiles
        {
            get => _tiles;
            set => _tiles = value;
        }

        public TileFinder(List<Tile> tiles)
        {
            _tiles = tiles;
        }

        public Tile GetTileByXAndYPosition(int xPosition, int yPosition)
        {
            try
            {
                return _tiles.FirstOrDefault(tile => tile.XPosition == xPosition && tile.YPosition == yPosition);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public Tile GetTileByCoordinates(TileCoordinates tileCoordinates)
        {
            try
            {
                return _tiles.FirstOrDefault(tile =>
                    tile.XPosition == tileCoordinates.X && tile.YPosition == tileCoordinates.Y);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
        public List<TileCoordinates>  GetCommonTileLists(List<TileCoordinates> tileCoordinatesList1, List<TileCoordinates> tileCoordinatesList2)
        {
            var commonValueList = new List<TileCoordinates>();
            foreach (var tileCoordinate1 in tileCoordinatesList1)
            {
                foreach (var tileCoordinate2 in tileCoordinatesList2)
                {
                    if(tileCoordinate1.X == tileCoordinate2.X && tileCoordinate1.Y == tileCoordinate2.Y) commonValueList.Add(tileCoordinate1);
                }
            }

            return commonValueList;
        }
        public List<Tile> GetTilesBySubcontinent(string subcontinent)
        {
            var tiles = _tiles.Where(tile => tile.Subcontinent == subcontinent).ToList();
            return tiles;
        }
        public List<Tile> GetTilesByTerrain(string terrain)
        {
            var tiles = _tiles.Where(tile => tile.Terrain == terrain).ToList();
            return tiles;
        }
        public List<Tile> GetTilesByFeature(FeatureType feature)
        {
            var tiles = _tiles.Where(tile => tile.Feature == feature.ToString()).ToList();
            return tiles;
        }
        
        public List<Tile> GetTilesByElevation(ElevationType elevationType)
        {
            var tiles = _tiles.Where(tile =>  elevationType.ToString() == tile.ElevationType).ToList();
            return tiles;
        }
        public List<Tile> GetRiverTilesWithoutOriginAndMouth()
        {
            var tiles = _tiles.Where(tile =>  tile.HasRiver && !tile.IsRiverOrigin && !tile.IsRiverMouth).ToList();
            return tiles;
        }

        public List<Tile> GetTilesInRange(Tile tile, int radius)
        {
            List<Tile> tilesInRadius = new List<Tile>();

            int startX = tile.XPosition - radius;
            int endX = tile.XPosition + radius;
            int startY = tile.YPosition - radius;
            int endY = tile.YPosition + radius;

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile currentTile = GetTileByXAndYPosition(x, y);
                    if (currentTile != null)
                    {
                        int distance = CalculateDistance(tile, currentTile);
                        if (distance <= radius)
                        {
                            tilesInRadius.Add(currentTile);
                        }
                    }
                }
            }

            return tilesInRadius;
        }

        private int CalculateDistance(Tile tile1, Tile tile2)
        {
            int dx = Math.Abs(tile1.XPosition - tile2.XPosition);
            int dy = Math.Abs(tile1.YPosition - tile2.YPosition);
            return Math.Max(dx, dy);
        }

    }
}