using System.Collections.Generic;
using _Project.Scripts.HelperScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;
using UnityEngine;

namespace _Project.Scripts.MapDataGenerator
{
    public class IslandGenerator
    {
        private NewGameDataGenerator _newGameDataGenerator;

        public IslandGenerator(NewGameDataGenerator newGameDataGenerator)
        {
            _newGameDataGenerator = newGameDataGenerator;
        }

        public void MakeSmallIslands()
        {
            TileFinder tileFinder = new TileFinder(_newGameDataGenerator.listOfTiles);
            TileMapInitializingDataContainer tileMapInitializingDataContainer =
                _newGameDataGenerator.tileMapInitializingDataContainer;
            var numberOfSmallIsland = _newGameDataGenerator.subcontinent.numberOfSmallIslands.RandomValueInRange();
            for (int islandNumber = 0; islandNumber < numberOfSmallIsland; islandNumber++)
            {
                var minimumDistanceFromLandTiles =
                    _newGameDataGenerator.subcontinent.minDistanceOfSmallIslandsFromLand.RandomValueInRange();
                var islandMaxLandTiles = _newGameDataGenerator.subcontinent.numberOfSmallIslandLandTiles.RandomValueInRange();
                List<Tile> listOfOceanTiles = tileFinder.GetTilesByTerrain(TerrainType.ocean.ToString());
                listOfOceanTiles = listOfOceanTiles.Shuffle();
                foreach (var oceanTile in listOfOceanTiles)
                {
                    if (oceanTile.IsSmallIslandPossible(minimumDistanceFromLandTiles, _newGameDataGenerator.listOfTiles, tileMapInitializingDataContainer.gridSizeX, tileMapInitializingDataContainer.gridSizeY))
                    {
                        Debug.Log("Small Island possible at " + oceanTile.XPosition + ", "+ oceanTile.YPosition);
                        oceanTile.CreateIsland(islandMaxLandTiles, _newGameDataGenerator.listOfTiles, tileMapInitializingDataContainer.gridSizeX, tileMapInitializingDataContainer.gridSizeY);
                        break;
                    }
                
                }
            }
        
        }
    }
}