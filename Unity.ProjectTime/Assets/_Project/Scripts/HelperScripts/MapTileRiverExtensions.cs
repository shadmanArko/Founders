using System;
using System.Collections.Generic;
using System.Linq;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.HelperScripts
{
    public static class MapTileRiverExtensions
    {
        private static bool _stopRiver = false;
        public static bool CanBeRiverOrigin(this Tile tile, int riverOriginDistanceFromCoast, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var tilesInRange = tileFinder.GetTilesInRange(tile, riverOriginDistanceFromCoast);
            foreach (var tileInRange in tilesInRange)
            {
                if (tileInRange.Terrain == TerrainType.coast.ToString())
                {
                    return false;
                }else if (tileInRange.IsRiverOrigin) return false;
            }

            return true;
        }
        public static bool CanBeBranchRiverOrigin(this Tile tile, int riverOriginDistanceFromCoast, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var tilesInRange = tileFinder.GetTilesInRange(tile, riverOriginDistanceFromCoast);
            foreach (var tileInRange in tilesInRange)
            {
                if (tileInRange.Terrain == TerrainType.coast.ToString())
                {
                    return false;
                }else if (tileInRange.IsRiverOrigin) return false;
            }

            return true;
        }
        public static Tile GetRiverMouth(this Tile riverOriginTile, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var coastTiles = tileFinder.GetTilesByTerrain(TerrainType.coast.ToString());
            var filteredCoastTiles = coastTiles.ToList();
            if (!riverOriginTile.IsIslandTile)
            {
                foreach (var coastTile in coastTiles)
                {
                    var tilesInRange = tileFinder.GetTilesInRange(coastTile, 2);
                    var hasRiverMouthInClose = false;
                    foreach (var tileInRange in tilesInRange)
                    {
                        if (tileInRange.IsRiverMouth)
                        {
                            hasRiverMouthInClose = true;
                            break;
                        }
                    }
                    if(hasRiverMouthInClose) filteredCoastTiles.Remove(coastTile);
                    if (Math.Abs(coastTile.XPosition - riverOriginTile.XPosition) < 3) filteredCoastTiles.Remove(coastTile);
                    if (  Math.Abs(coastTile.YPosition - riverOriginTile.YPosition) < 5) filteredCoastTiles.Remove(coastTile);
                    var coastTileNeighboursCoordinates = coastTile.GetListOfNeighbourTileCoordinates();
                    foreach (var coastTileNeighboursCoordinate in coastTileNeighboursCoordinates)
                    {
                        var coastTileNeighbour = tileFinder.GetTileByCoordinates(coastTileNeighboursCoordinate);
                        if (coastTileNeighbour != null && coastTileNeighbour.IsIslandTile)
                            filteredCoastTiles.Remove(coastTile);
                    }
                }
            }
            var closestCoastTile = new Tile();
            
            int minimumDistance = 99999;
            var sortedList = filteredCoastTiles.OrderBy(riverOriginTile.DistanceTo).ToList();
            foreach (var coastTile in filteredCoastTiles.Shuffle())
            {
                if (!coastTile.IsRiverMouth && riverOriginTile.DistanceTo(coastTile) < minimumDistance)
                {
                    closestCoastTile = coastTile;
                    minimumDistance = riverOriginTile.DistanceTo(coastTile);
                }
            }

            // if (sortedList.Count > 3)
            // {
            //     return sortedList[Random.Range(0, 3)];
            // }
            // else
            // {
            //     return sortedList[0];
            // }
            return closestCoastTile;
        }
        public static Tile GetRiverMouthFromDirection(this Tile riverOriginTile, Directions directions, List<Tile> allTiles)
        {
            
            TileFinder tileFinder = new TileFinder(allTiles);
            var coastTiles = tileFinder.GetTilesByTerrain(TerrainType.coast.ToString());
            var filteredCoastTiles = coastTiles.ToList();
            if (!riverOriginTile.IsIslandTile)
            {
                foreach (var coastTile in coastTiles)
                {
                    var tilesInRange = tileFinder.GetTilesInRange(coastTile, 2);
                    var hasRiverMouthInClose = false;
                    foreach (var tileInRange in tilesInRange)
                    {
                        if (tileInRange.IsRiverMouth)
                        {
                            hasRiverMouthInClose = true;
                            break;
                        }
                    }
                    if(hasRiverMouthInClose) filteredCoastTiles.Remove(coastTile);
                    if (Math.Abs(coastTile.XPosition - riverOriginTile.XPosition) < 3) filteredCoastTiles.Remove(coastTile);
                    if (  Math.Abs(coastTile.YPosition - riverOriginTile.YPosition) < 5) filteredCoastTiles.Remove(coastTile);
                    var coastTileNeighboursCoordinates = coastTile.GetListOfNeighbourTileCoordinates();
                    if(directions == Directions.Right && coastTile.XPosition < riverOriginTile.XPosition) filteredCoastTiles.Remove(coastTile);
                    if(directions == Directions.Left && coastTile.XPosition > riverOriginTile.XPosition) filteredCoastTiles.Remove(coastTile);
                    foreach (var coastTileNeighboursCoordinate in coastTileNeighboursCoordinates)
                    {
                        var coastTileNeighbour = tileFinder.GetTileByCoordinates(coastTileNeighboursCoordinate);
                        if (coastTileNeighbour != null && coastTileNeighbour.IsIslandTile)
                            filteredCoastTiles.Remove(coastTile);
                    }
                }
            }
            var closestCoastTile = new Tile();
            
            int minimumDistance = 99999;
            var sortedList = filteredCoastTiles.OrderBy(riverOriginTile.DistanceTo).ToList();
            foreach (var coastTile in filteredCoastTiles.Shuffle())
            {
                if (!coastTile.IsRiverMouth && riverOriginTile.DistanceTo(coastTile) < minimumDistance)
                {
                    closestCoastTile = coastTile;
                    minimumDistance = riverOriginTile.DistanceTo(coastTile);
                }
            }

            // if (sortedList.Count > 3)
            // {
            //     return sortedList[Random.Range(0, 3)];
            // }
            // else
            // {
            //     return sortedList[0];
            // }
            return closestCoastTile;
        }
        public static Tile GetBranchRiverMouth(this Tile riverOriginTile, Directions direction, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var coastTiles = tileFinder.GetTilesByTerrain(TerrainType.coast.ToString());
            var filteredCoastTiles = coastTiles.ToList();
            if (!riverOriginTile.IsIslandTile)
            {
                foreach (var coastTile in coastTiles)
                {
                    var tilesInRange = tileFinder.GetTilesInRange(coastTile, 2);
                    var hasRiverMouthInClose = false;
                    foreach (var tileInRange in tilesInRange)
                    {
                        if (tileInRange.IsRiverMouth)
                        {
                            hasRiverMouthInClose = true;
                            break;
                        }
                    }
                    if(hasRiverMouthInClose) filteredCoastTiles.Remove(coastTile);
                    if(direction == Directions.Right && coastTile.XPosition <= riverOriginTile.XPosition) filteredCoastTiles.Remove(coastTile); 
                    if(direction == Directions.Left && coastTile.XPosition >= riverOriginTile.XPosition) filteredCoastTiles.Remove(coastTile); 
                    if (Math.Abs(coastTile.XPosition - riverOriginTile.XPosition) < 3) filteredCoastTiles.Remove(coastTile);
                    if (  Math.Abs(coastTile.YPosition - riverOriginTile.YPosition) < 3) filteredCoastTiles.Remove(coastTile);
                    var coastTileNeighboursCoordinates = coastTile.GetListOfNeighbourTileCoordinates();
                    foreach (var coastTileNeighboursCoordinate in coastTileNeighboursCoordinates)
                    {
                        var coastTileNeighbour = tileFinder.GetTileByCoordinates(coastTileNeighboursCoordinate);
                        if (coastTileNeighbour != null && coastTileNeighbour.IsIslandTile)
                            filteredCoastTiles.Remove(coastTile);
                    }
                }
            }
            var closestCoastTile = new Tile();
            
            int minimumDistance = 99999;
            foreach (var coastTile in filteredCoastTiles.Shuffle())
            {
                if (!coastTile.IsRiverMouth && riverOriginTile.DistanceTo(coastTile) < minimumDistance)
                {
                    closestCoastTile = coastTile;
                    minimumDistance = riverOriginTile.DistanceTo(coastTile);
                }
            }

            return closestCoastTile;
        }
        
        public static void CreateRiver(this Tile riverOriginTile, Tile riverMouthTile, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var lastRiverTile = riverOriginTile;
            riverOriginTile.IsRiverOrigin = true;
            riverOriginTile.HasRiver = true;
            riverMouthTile.IsRiverMouth = true;
            riverMouthTile.HasRiver = true;
            Tile newRiverTile = null;
            if (riverMouthTile.YPosition < riverOriginTile.YPosition)
            {
                newRiverTile = tileFinder.GetTileByXAndYPosition(lastRiverTile.XPosition, lastRiverTile.YPosition - 1);
            }
            var secondLastRiverTile = newRiverTile;
            var mouthTileCoordinates = riverMouthTile.TileCoordinates;
            if(lastRiverTile == null || secondLastRiverTile == null){Debug.Log("River Failed for origin " + riverOriginTile.XPosition +", " + riverOriginTile.YPosition); return;}
            MakeCommonEdgesRiver(lastRiverTile, secondLastRiverTile);
            for (int i = 0; i < 100; i++)
            {
                if (newRiverTile!= null && !newRiverTile.IsRiverMouth)
                { 
                    // if(newRiverTile.HasRiver) break;
                    newRiverTile.HasRiver = true;
                    var probability = Random.Range(0, 100);
                    if (probability < 100)
                    {
                        newRiverTile = ClosestCommonNeighbourTileToRiverMouth(lastRiverTile, secondLastRiverTile, riverMouthTile, allTiles);
                        if (newRiverTile == null)
                        {
                            Debug.Log("River failed after tile " + lastRiverTile.XPosition +", "+ lastRiverTile.YPosition);
                            break;
                        }
                        lastRiverTile = secondLastRiverTile;
                        secondLastRiverTile = newRiverTile;
                        
                    }
                    else
                    {
                        newRiverTile = FarthestCommonNeighbourTileToRiverMouth(lastRiverTile, secondLastRiverTile, riverMouthTile, allTiles) ??
                                       ClosestCommonNeighbourTileToRiverMouth(lastRiverTile, secondLastRiverTile, riverMouthTile, allTiles);

                        lastRiverTile = secondLastRiverTile;
                        secondLastRiverTile = newRiverTile;
                    }
                    if(lastRiverTile!= null && secondLastRiverTile!= null){ MakeCommonEdgesRiver(lastRiverTile, secondLastRiverTile);}
                    if (i > 10 && riverMouthTile.DistanceTo(lastRiverTile) > 4)
                    {
                        Debug.LogWarning($"Came for the branching river");
                        if (lastRiverTile != null)
                        {
                            var leftRiverOrigin = tileFinder.GetTileByCoordinates(new TileCoordinates()
                                {X = lastRiverTile.TileCoordinates.X - 1, Y = lastRiverTile.TileCoordinates.Y});
                            var rightRiverOrigin = tileFinder.GetTileByCoordinates(new TileCoordinates()
                                {X = lastRiverTile.TileCoordinates.X + 1, Y = lastRiverTile.TileCoordinates.Y});
                            var leftRiverMouth = new Tile();
                            var rightRiverMouth = new Tile();
                            if(leftRiverOrigin != null) leftRiverMouth =GetRiverMouthFromDirection(leftRiverOrigin, Directions.Left, allTiles);
                            if(rightRiverOrigin != null) rightRiverMouth = GetRiverMouthFromDirection(rightRiverOrigin, Directions.Right, allTiles);
                            if(leftRiverMouth.Id!= null) CreateBranchRiver(leftRiverOrigin, leftRiverMouth, allTiles);
                            if(rightRiverMouth.Id!= null)CreateBranchRiver(rightRiverOrigin, rightRiverMouth, allTiles);
                            Debug.Log($"Branch River created At {newRiverTile.XPosition}, {newRiverTile.YPosition} at {newRiverTile.Subcontinent} ");
                            break;
                        }

                    }
                }else if (newRiverTile != null && (newRiverTile.IsRiverMouth || newRiverTile.GetListOfNeighbourTileCoordinates().Contains(mouthTileCoordinates)) || _stopRiver)
                {
                    break;
                }
            }
            

        }
        public static void CreateRiverInReverse(this Tile riverOriginTile, Tile riverMouthTile, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var lastRiverTile = riverOriginTile;
            Tile newRiverTile = null;
            if (riverMouthTile.YPosition < riverOriginTile.YPosition)
            {
                newRiverTile = tileFinder.GetTileByXAndYPosition(lastRiverTile.XPosition, lastRiverTile.YPosition - 1);
            }
            var secondLastRiverTile = newRiverTile;
            var mouthTileCoordinates = riverMouthTile.TileCoordinates;
            if(lastRiverTile == null || secondLastRiverTile == null){Debug.Log("Reverse River Failed for origin " + riverOriginTile.XPosition +", " + riverOriginTile.YPosition); return;}
            MakeCommonEdgesRiver(lastRiverTile, secondLastRiverTile);
            for (int i = 0; i < 100; i++)
            {
                if (newRiverTile!= null && !newRiverTile.IsRiverMouth)
                { 
                    // if(newRiverTile.HasRiver) break;
                    newRiverTile.HasRiver = true;
                    var probability = Random.Range(0, 100);
                    if (probability < 100)
                    {
                        newRiverTile = ClosestCommonNeighbourTileToRiverMouthForReverseRiver(lastRiverTile, secondLastRiverTile, riverMouthTile, allTiles);
                        if (newRiverTile == null)
                        {
                            Debug.Log("Reverse River failed after tile " + lastRiverTile.XPosition +", "+ lastRiverTile.YPosition);
                            break;
                        }
                        lastRiverTile = secondLastRiverTile;
                        secondLastRiverTile = newRiverTile;
                        
                    }
                    else
                    {
                        newRiverTile = FarthestCommonNeighbourTileToRiverMouth(lastRiverTile, secondLastRiverTile, riverMouthTile, allTiles) ??
                                       ClosestCommonNeighbourTileToRiverMouth(lastRiverTile, secondLastRiverTile, riverMouthTile, allTiles);

                        lastRiverTile = secondLastRiverTile;
                        secondLastRiverTile = newRiverTile;
                    }
                    if(lastRiverTile!= null && secondLastRiverTile!= null) MakeCommonEdgesRiver(lastRiverTile, secondLastRiverTile);
                }else if (newRiverTile != null && (newRiverTile.IsRiverMouth || newRiverTile.GetListOfNeighbourTileCoordinates().Contains(mouthTileCoordinates) || _stopRiver))
                {
                    break;
                }
            }
            
        }
        public static void CreateBranchRiver(this Tile riverOriginTile, Tile riverMouthTile, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var lastRiverTile = riverOriginTile;
            riverOriginTile.IsRiverOrigin = true;
            riverOriginTile.HasRiver = true;
            riverMouthTile.IsRiverMouth = true;
            riverMouthTile.HasRiver = true;
            Tile newRiverTile = null;
            if (riverMouthTile.YPosition < riverOriginTile.YPosition)
            {
                newRiverTile = tileFinder.GetTileByXAndYPosition(lastRiverTile.XPosition, lastRiverTile.YPosition - 1);
            }
            var secondLastRiverTile = newRiverTile;
            var mouthTileCoordinates = riverMouthTile.TileCoordinates;
            if(lastRiverTile == null || secondLastRiverTile == null){Debug.Log("River Failed for origin " + riverOriginTile.XPosition +", " + riverOriginTile.YPosition); return;}
            MakeCommonEdgesRiver(lastRiverTile, secondLastRiverTile);
            for (int i = 0; i < 100; i++)
            {
                if (newRiverTile!= null && !newRiverTile.IsRiverMouth)
                { 
                    // if(newRiverTile.HasRiver) break;
                    newRiverTile.HasRiver = true;
                    newRiverTile.BranchRiver = true;
                    var probability = Random.Range(0, 100);
                    if (probability < 100)
                    {
                        newRiverTile = ClosestCommonNeighbourTileToRiverMouth(lastRiverTile, secondLastRiverTile, riverMouthTile, allTiles);
                        if (newRiverTile == null)
                        {
                            Debug.Log("River failed after tile " + lastRiverTile.XPosition +", "+ lastRiverTile.YPosition);
                            break;
                        }
                        lastRiverTile = secondLastRiverTile;
                        secondLastRiverTile = newRiverTile;
                        
                    }
                    else
                    {
                        newRiverTile = FarthestCommonNeighbourTileToRiverMouth(lastRiverTile, secondLastRiverTile, riverMouthTile, allTiles) ??
                                       ClosestCommonNeighbourTileToRiverMouth(lastRiverTile, secondLastRiverTile, riverMouthTile, allTiles);

                        lastRiverTile = secondLastRiverTile;
                        secondLastRiverTile = newRiverTile;
                    }
                    if(lastRiverTile!= null && secondLastRiverTile!= null){ MakeCommonEdgesRiver(lastRiverTile, secondLastRiverTile);}
                }else if (newRiverTile != null && (newRiverTile.IsRiverMouth || newRiverTile.GetListOfNeighbourTileCoordinates().Contains(mouthTileCoordinates)) || _stopRiver)
                {
                    break;
                }
            }
            

        }
        private static void MakeCommonEdgesRiver(Tile lastRiverTile, Tile newRiverTile)
        {
            _stopRiver = false;
            TileCoordinates lastRiverTileDownNeighbourCoordinates= new TileCoordinates();
            TileCoordinates lastRiverTileUpNeighbourCoordinates= new TileCoordinates();
            TileCoordinates lastRiverTileLeftNeighbourCoordinates= new TileCoordinates();
            TileCoordinates lastRiverTileRightNeighbourCoordinates= new TileCoordinates();
            if (lastRiverTile.TileCoordinates != null )
            {
                lastRiverTileDownNeighbourCoordinates = lastRiverTile.GetNeighbourTileCoordinatesInDirection(Directions.Down);
                lastRiverTileUpNeighbourCoordinates = lastRiverTile.GetNeighbourTileCoordinatesInDirection(Directions.Up);
                lastRiverTileLeftNeighbourCoordinates = lastRiverTile.GetNeighbourTileCoordinatesInDirection(Directions.Left);
                lastRiverTileRightNeighbourCoordinates = lastRiverTile.GetNeighbourTileCoordinatesInDirection(Directions.Right);
            }
            else
            {
                Debug.LogWarning($"no coordinates found on tile {lastRiverTile.XPosition}, {lastRiverTile.YPosition}");
            }
            
            if (newRiverTile.XPosition == lastRiverTileDownNeighbourCoordinates.X &&
                newRiverTile.YPosition == lastRiverTileDownNeighbourCoordinates.Y)
            {
                //last river bottom side river and newTile top side river
                if (newRiverTile.RiverTop) _stopRiver = true;
                lastRiverTile.RiverBottom = true;
                newRiverTile.RiverTop = true;
            }else if (newRiverTile.XPosition == lastRiverTileUpNeighbourCoordinates.X &&
                      newRiverTile.YPosition == lastRiverTileUpNeighbourCoordinates.Y)
            {
                //last river top side river and newTile bottom side river
                if (newRiverTile.RiverBottom) _stopRiver = true;
                lastRiverTile.RiverTop = true;
                newRiverTile.RiverBottom = true;
            }else if (newRiverTile.XPosition == lastRiverTileLeftNeighbourCoordinates.X &&
                      newRiverTile.YPosition == lastRiverTileLeftNeighbourCoordinates.Y)
            {
                //last river left side river and newTile right side river
                if (newRiverTile.RiverRight) _stopRiver = true;
                lastRiverTile.RiverLeft = true;
                newRiverTile.RiverRight = true;
            }else if (newRiverTile.XPosition == lastRiverTileRightNeighbourCoordinates.X &&
                      newRiverTile.YPosition == lastRiverTileRightNeighbourCoordinates.Y)
            {
                //last river right side river and newTile left side river
                if (newRiverTile.RiverLeft) _stopRiver = true;
                lastRiverTile.RiverRight = true;
                newRiverTile.RiverLeft = true;
            }
        }
        
        

        private static Tile ClosestCommonNeighbourTileToRiverMouth(Tile lastRiverTile, Tile newLastRiverTile, Tile riverMouthTile, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var lastRiverTileNeighbourCoordinates = lastRiverTile.GetListOfNeighbourTileCoordinates();
            var newLastRiverTileNeighbourCoordinates = newLastRiverTile.GetListOfNeighbourTileCoordinates();
            var commonNeighboursCoordinates = tileFinder.GetCommonTileLists(lastRiverTileNeighbourCoordinates,
                newLastRiverTileNeighbourCoordinates);
            
            Tile closestTileToRiverMouth = null;
            Tile tileInCaseNoValidTileFound = new Tile();
            int closestTileDistance = 99999;
            // Debug.Log("last river coordinates " + lastRiverTileNeighbourCoordinates.Count + " new last river coordinates " + newLastRiverTileNeighbourCoordinates.Count);
            foreach (var tileCoordinates in commonNeighboursCoordinates.Shuffle())
            {
                // Debug.Log("Common coordinate " + tileCoordinates.X +" "+ tileCoordinates.Y);
                var neighbourTile = tileFinder.GetTileByCoordinates(tileCoordinates);
                if(neighbourTile == null) continue;
                if (neighbourTile.IsRiverMouth) return neighbourTile;
                //dont take tile that are in upper side when river mouth is in down side
                // if(neighbourTile.YPosition > newLastRiverTile.YPosition && newLastRiverTile.YPosition > riverMouthTile.YPosition)continue;
                //if last river tile have river in right and new river tile have river in top => dont go right
                if(lastRiverTile.RiverRight && newLastRiverTile.RiverTop && neighbourTile.XPosition > newLastRiverTile.XPosition){ tileInCaseNoValidTileFound = neighbourTile; continue;}
                if(neighbourTile.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverRight){ tileInCaseNoValidTileFound = neighbourTile; continue;}
                if(neighbourTile.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverLeft){ tileInCaseNoValidTileFound = neighbourTile; continue;}
                //if last river tile have river in left and new river tile have river in top => dont go left
                if(lastRiverTile.RiverLeft && newLastRiverTile.RiverTop && neighbourTile.XPosition < newLastRiverTile.XPosition) { tileInCaseNoValidTileFound = neighbourTile; continue;}
                if((neighbourTile.Terrain == TerrainType.ocean.ToString() || neighbourTile.Terrain == TerrainType.coast.ToString()) && !neighbourTile.IsRiverMouth) return null;
                if (riverMouthTile.DistanceTo(neighbourTile) <= closestTileDistance)
                {
                    if (riverMouthTile.DistanceTo(neighbourTile) == closestTileDistance)
                    {
                        if (riverMouthTile.MinimumInX(neighbourTile) <=
                            riverMouthTile.MinimumInX(closestTileToRiverMouth)  || riverMouthTile.MinimumInY(neighbourTile) <=
                            riverMouthTile.MinimumInY(closestTileToRiverMouth))
                        {
                            closestTileToRiverMouth = neighbourTile;
                            closestTileDistance = riverMouthTile.DistanceTo(neighbourTile);
                        }
                    }
                    else
                    {
                        closestTileToRiverMouth = neighbourTile;
                        closestTileDistance = riverMouthTile.DistanceTo(neighbourTile);
                    }
                    
                }
            }

            if (closestTileToRiverMouth == null)
            {
                if (tileInCaseNoValidTileFound.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverRight)
                {
                    newLastRiverTile.RiverRight = false;
                    lastRiverTile.RiverLeft = false;
                    Debug.Log("Fixed river issue");
                }else if (tileInCaseNoValidTileFound.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverLeft)
                {
                    newLastRiverTile.RiverLeft = false;
                    lastRiverTile.RiverRight = false;
                    Debug.Log("Fixed river issue");
                }
                Debug.Log("Fixed river issue: returning tile in case");
                return tileInCaseNoValidTileFound;
            }
            return closestTileToRiverMouth;

        }
        private static Tile ClosestCommonNeighbourTileToRiverMouthForReverseRiver(Tile lastRiverTile, Tile newLastRiverTile, Tile riverMouthTile, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var lastRiverTileNeighbourCoordinates = lastRiverTile.GetListOfNeighbourTileCoordinates();
            var newLastRiverTileNeighbourCoordinates = newLastRiverTile.GetListOfNeighbourTileCoordinates();
            var commonNeighboursCoordinates = tileFinder.GetCommonTileLists(lastRiverTileNeighbourCoordinates,
                newLastRiverTileNeighbourCoordinates);
            
            Tile closestTileToRiverMouth = null;
            Tile tileInCaseNoValidTileFound = new Tile();
            int closestTileDistance = 99999;
            // Debug.Log("last river coordinates " + lastRiverTileNeighbourCoordinates.Count + " new last river coordinates " + newLastRiverTileNeighbourCoordinates.Count);
            foreach (var tileCoordinates in commonNeighboursCoordinates.Shuffle())
            {
                // Debug.Log("Common coordinate " + tileCoordinates.X +" "+ tileCoordinates.Y);
                var neighbourTile = tileFinder.GetTileByCoordinates(tileCoordinates);
                if(neighbourTile == null) continue;
                if (neighbourTile.IsRiverMouth) return neighbourTile;
                //dont take tile that are in upper side when river mouth is in down side
                // if(neighbourTile.YPosition > newLastRiverTile.YPosition && newLastRiverTile.YPosition > riverMouthTile.YPosition)continue;
                //if last river tile have river in right and new river tile have river in top => dont go right
                if(lastRiverTile.RiverRight && newLastRiverTile.RiverTop && neighbourTile.XPosition > newLastRiverTile.XPosition){ tileInCaseNoValidTileFound = neighbourTile; continue;}
                if(neighbourTile.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverRight){ tileInCaseNoValidTileFound = neighbourTile; continue;}
                if(neighbourTile.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverLeft){ tileInCaseNoValidTileFound = neighbourTile; continue;}
                //if last river tile have river in left and new river tile have river in top => dont go left
                if(lastRiverTile.RiverLeft && newLastRiverTile.RiverTop && neighbourTile.XPosition < newLastRiverTile.XPosition) { tileInCaseNoValidTileFound = neighbourTile; continue;}
                if((neighbourTile.Terrain == TerrainType.ocean.ToString() || neighbourTile.Terrain == TerrainType.coast.ToString()) && !neighbourTile.IsRiverOrigin) continue;
                if (riverMouthTile.DistanceTo(neighbourTile) <= closestTileDistance)
                {
                    if (riverMouthTile.DistanceTo(neighbourTile) == closestTileDistance)
                    {
                        if (riverMouthTile.MinimumInX(neighbourTile) <=
                            riverMouthTile.MinimumInX(closestTileToRiverMouth)  || riverMouthTile.MinimumInY(neighbourTile) <=
                            riverMouthTile.MinimumInY(closestTileToRiverMouth))
                        {
                            closestTileToRiverMouth = neighbourTile;
                            closestTileDistance = riverMouthTile.DistanceTo(neighbourTile);
                        }
                    }
                    else
                    {
                        closestTileToRiverMouth = neighbourTile;
                        closestTileDistance = riverMouthTile.DistanceTo(neighbourTile);
                    }
                    
                }
            }

            if (closestTileToRiverMouth == null)
            {
                if (tileInCaseNoValidTileFound.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverRight)
                {
                    newLastRiverTile.RiverRight = false;
                    lastRiverTile.RiverLeft = false;
                    Debug.Log("Fixed river issue");
                }else if (tileInCaseNoValidTileFound.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverLeft)
                {
                    newLastRiverTile.RiverLeft = false;
                    lastRiverTile.RiverRight = false;
                    Debug.Log("Fixed river issue");
                }
                Debug.Log("Fixed river issue: returning tile in case");
                return tileInCaseNoValidTileFound;
            }
            return closestTileToRiverMouth;

        }

        private static Tile FarthestCommonNeighbourTileToRiverMouth(Tile lastRiverTile, Tile newLastRiverTile, Tile riverMouthTile, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var lastRiverTileNeighbourCoordinates = lastRiverTile.GetListOfNeighbourTileCoordinates();
            var newLastRiverTileNeighbourCoordinates = newLastRiverTile.GetListOfNeighbourTileCoordinates();
            var commonNeighboursCoordinates = tileFinder.GetCommonTileLists(lastRiverTileNeighbourCoordinates,
                newLastRiverTileNeighbourCoordinates);
            
            Tile closestTileToRiverMouth = null;
            Tile tileInCaseNoValidTileFound = new Tile();
            int closestTileDistance = 0;
            // Debug.Log("last river coordinates " + lastRiverTileNeighbourCoordinates.Count + " new last river coordinates " + newLastRiverTileNeighbourCoordinates.Count);
            foreach (var tileCoordinates in commonNeighboursCoordinates.Shuffle())
            {
                // Debug.Log("Common coordinate " + tileCoordinates.X +" "+ tileCoordinates.Y);
                var neighbourTile = tileFinder.GetTileByCoordinates(tileCoordinates);
                if(neighbourTile == null) continue;
                if (neighbourTile.IsRiverMouth) return neighbourTile;
                //dont take tile that are in upper side when river mouth is in down side
                // if(neighbourTile.YPosition > newLastRiverTile.YPosition && newLastRiverTile.YPosition > riverMouthTile.YPosition)continue;
                //if last river tile have river in right and new river tile have river in top => dont go right
                if(lastRiverTile.RiverRight && newLastRiverTile.RiverTop && neighbourTile.XPosition > newLastRiverTile.XPosition){ tileInCaseNoValidTileFound = neighbourTile; continue;}
                if(neighbourTile.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverRight){ tileInCaseNoValidTileFound = neighbourTile; continue;}
                if(neighbourTile.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverLeft){ tileInCaseNoValidTileFound = neighbourTile; continue;}
                //if last river tile have river in left and new river tile have river in top => dont go left
                if(lastRiverTile.RiverLeft && newLastRiverTile.RiverTop && neighbourTile.XPosition < newLastRiverTile.XPosition) { tileInCaseNoValidTileFound = neighbourTile; continue;}
                // if((neighbourTile.Terrain == TerrainType.ocean.ToString() || neighbourTile.Terrain == TerrainType.coast.ToString()) && !neighbourTile.IsRiverMouth) return null;
                if (riverMouthTile.DistanceTo(neighbourTile) >= closestTileDistance)
                {
                    if (riverMouthTile.DistanceTo(neighbourTile) == closestTileDistance)
                    {
                        if (riverMouthTile.MinimumInX(neighbourTile) >=
                            riverMouthTile.MinimumInX(closestTileToRiverMouth)  || riverMouthTile.MinimumInY(neighbourTile) >=
                            riverMouthTile.MinimumInY(closestTileToRiverMouth))
                        {
                            closestTileToRiverMouth = neighbourTile;
                            closestTileDistance = riverMouthTile.DistanceTo(neighbourTile);
                        }
                    }
                    else
                    {
                        closestTileToRiverMouth = neighbourTile;
                        closestTileDistance = riverMouthTile.DistanceTo(neighbourTile);
                    }
                    
                }
            }

            if (closestTileToRiverMouth == null)
            {
                if (tileInCaseNoValidTileFound.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverRight)
                {
                    newLastRiverTile.RiverRight = false;
                    lastRiverTile.RiverLeft = false;
                    Debug.Log("Fixed river issue");
                }else if (tileInCaseNoValidTileFound.YPosition > newLastRiverTile.YPosition && newLastRiverTile.RiverLeft)
                {
                    newLastRiverTile.RiverLeft = false;
                    lastRiverTile.RiverRight = false;
                    Debug.Log("Fixed river issue");
                }
                Debug.Log("Fixed river issue: returning tile in case");
                return tileInCaseNoValidTileFound;
            }
            return closestTileToRiverMouth;

        }

        public static Tile NeighbourNonRiverTileAwayFromRiverMouth(this Tile riverTile, Tile riverMouthTile, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var listOfNonDiagonalNeighbourTileCoordinates = riverTile.GetListOfNonDiagonalNeighbourTileCoordinates();
            
            var farthestTileToRiverMouth = new Tile();
            int farthestTileDistance = 0;
            foreach (var tileCoordinates in listOfNonDiagonalNeighbourTileCoordinates.Shuffle())
            {
                var neighbourTile = tileFinder.GetTileByCoordinates(tileCoordinates);
                if(neighbourTile == null) continue;
                if (riverTile.IsRiverMouth) return neighbourTile;
                //dont take tile that are in upper side when river mouth is in down side
                if(neighbourTile.YPosition > riverTile.YPosition && riverTile.YPosition > riverMouthTile.YPosition || neighbourTile.HasRiver) continue;
                if (riverMouthTile.DistanceTo(neighbourTile) > farthestTileDistance)
                {
                    farthestTileToRiverMouth = neighbourTile;
                    farthestTileDistance = riverMouthTile.DistanceTo(neighbourTile);
                }
            }

            return farthestTileToRiverMouth;
        }
        public static Tile ClosestNeighbourTileToRiverMouth(this Tile riverTile, Tile riverMouthTile, List<Tile> allTiles)
        {
            TileFinder tileFinder = new TileFinder(allTiles);
            var listOfNonDiagonalNeighbourTileCoordinates = riverTile.GetListOfNonDiagonalNeighbourTileCoordinates();
            
            var closestTileToRiverMouth = new Tile();
            int closestTileDistance = 99999;
            foreach (var tileCoordinates in listOfNonDiagonalNeighbourTileCoordinates.Shuffle())
            {
                var neighbourTile = tileFinder.GetTileByCoordinates(tileCoordinates);
                if (neighbourTile!= null && riverMouthTile.DistanceTo(neighbourTile) < closestTileDistance)
                {
                    closestTileToRiverMouth = neighbourTile;
                    closestTileDistance = riverMouthTile.DistanceTo(neighbourTile);
                }
            }

            return closestTileToRiverMouth;
        }
    }
}