using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;
using UnityEngine;

namespace _Project.Scripts.MapDataGenerator
{
    public class FeatureAllocationBasedOnTemperatureAndTerrain
    {
        private NewGameDataGenerator _newGameDataGenerator;

        public FeatureAllocationBasedOnTemperatureAndTerrain(NewGameDataGenerator newGameDataGenerator)
        {
            _newGameDataGenerator = newGameDataGenerator;
        }

        public void AllocateFeatures(ChunkTile chunkTile, bool dryLand, int temperature)
        {
            // Debug.Log("Came to Allocate features");
            int probability = Random.Range(0, 100);
            if (dryLand)
            {
                if (temperature == 0)
                {
                    Temperature0AndDryLand(chunkTile, probability);
                }else if (temperature == 1)
                {
                    Temperature1AndDryLand(chunkTile, probability);
                }else if (temperature == 2)
                {
                    Temperature2AndDryLand(chunkTile, probability);
                }else if (temperature == 3)
                {
                    Temperature3AndDryLand(chunkTile, probability);
                }else if (temperature == 4)
                {
                    Temperature4AndDryLand(chunkTile, probability);
                }else if (temperature == 5)
                {
                    Temperature5AndDryLand(chunkTile, probability);
                }
            }
            else
            {
                if (temperature == 0)
                {
                    Temperature0AndNonDryLand(chunkTile, probability);
                }else if (temperature == 1)
                {
                    Temperature1AndNonDryLand(chunkTile, probability);
                }else if (temperature == 2)
                {
                    Temperature2AndNonDryLand(chunkTile, probability);
                }else if (temperature == 3)
                {
                    Temperature3AndNonDryLand(chunkTile, probability);
                }else if (temperature == 4)
                {
                    Temperature4AndNonDryLand(chunkTile, probability);
                }else if (temperature == 5)
                {
                    Temperature5AndNonDryLand(chunkTile, probability);
                }
            }
        }

        #region TerrainAndFeaturesAllocationForDryLand
        private void Temperature0AndDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 20)
            {
                SetTerrainToChunk(chunkTile, TerrainType.snow);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }
            else
            {
                SetTerrainToChunk(chunkTile, TerrainType.snow);
            }
        }
        private void Temperature1AndDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 10)
            {
                SetTerrainToChunk(chunkTile, TerrainType.snow);
            }
            else if (probability < 20)
            {
                SetTerrainToChunk(chunkTile, TerrainType.snow);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else if (probability < 50)
            {
                SetTerrainToChunk(chunkTile, TerrainType.tundra);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else 
            {
                SetTerrainToChunk(chunkTile, TerrainType.tundra);
            }
        }
        private void Temperature2AndDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 10)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }
            else if (probability < 35)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
            }else if (probability < 45)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else if (probability < 70)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
            }else if (probability < 90)
            {
                SetTerrainToChunk(chunkTile, TerrainType.tundra);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else 
            {
                SetTerrainToChunk(chunkTile, TerrainType.tundra);
            }
        }
        private void Temperature3AndDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 25)
            {
                SetTerrainToChunk(chunkTile, TerrainType.desert);
            }
            else if (probability < 45)
            {
                SetTerrainToChunk(chunkTile, TerrainType.dryland);
            }else if (probability < 55)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else if (probability < 80)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
            }else if (probability < 85)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else 
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
            }
        }
        private void Temperature4AndDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 10)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
            }
            else if (probability < 15)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else if (probability < 30)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
            }else if (probability < 65)
            {
                SetTerrainToChunk(chunkTile, TerrainType.dryland);
            }else 
            {
                SetTerrainToChunk(chunkTile, TerrainType.desert);
            }
        }
        private void Temperature5AndDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 5)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
            }
            else if (probability < 20)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
            }else if (probability < 50)
            {
                SetTerrainToChunk(chunkTile, TerrainType.dryland);
            }else 
            {
                SetTerrainToChunk(chunkTile, TerrainType.desert);
            }
        }
        #endregion
    
        #region TerrainAndFeaturesAllocationForNonDryLand
        private void Temperature0AndNonDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 20)
            {
                SetTerrainToChunk(chunkTile, TerrainType.snow);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }
            else
            {
                SetTerrainToChunk(chunkTile, TerrainType.snow);
            }
        }
        private void Temperature1AndNonDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 5)
            {
                SetTerrainToChunk(chunkTile, TerrainType.snow);
            }
            else if (probability < 20)
            {
                SetTerrainToChunk(chunkTile, TerrainType.snow);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else if (probability < 65)
            {
                SetTerrainToChunk(chunkTile, TerrainType.tundra);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else 
            {
                SetTerrainToChunk(chunkTile, TerrainType.tundra);
            }
        }
        private void Temperature2AndNonDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 10)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }
            else if (probability < 25)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
            }else if (probability < 45)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else if (probability < 70)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
            }else if (probability < 90)
            {
                SetTerrainToChunk(chunkTile, TerrainType.tundra);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else 
            {
                SetTerrainToChunk(chunkTile, TerrainType.tundra);
            }
        }
        private void Temperature3AndNonDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 10)
            {
                SetTerrainToChunk(chunkTile, TerrainType.dryland);
            }
            else if (probability < 35)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
            }else if (probability < 55)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
            }else if (probability < 75)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
            }else 
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }
        }
        private void Temperature4AndNonDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 15)
            {
                SetTerrainToChunk(chunkTile, TerrainType.dryland);
            }
            else if (probability < 30)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else if (probability < 45)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
            }else if (probability < 60)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
                SetFeatureToChunk(chunkTile, FeatureType.jungle);
            }else if (probability < 80)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else 
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
            }
        }
        private void Temperature5AndNonDryLand(ChunkTile chunkTile, int probability)
        {
            if (probability < 10)
            {
                SetTerrainToChunk(chunkTile, TerrainType.dryland);
            }
            else if (probability < 15)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else if (probability < 20)
            {
                SetTerrainToChunk(chunkTile, TerrainType.plains);
            }else if (probability < 60)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
                SetFeatureToChunk(chunkTile, FeatureType.jungle);
            }else if (probability < 80)
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
                SetFeatureToChunk(chunkTile, FeatureType.forest);
            }else 
            {
                SetTerrainToChunk(chunkTile, TerrainType.grassland);
            }
        }
        #endregion
        void SetTerrainToChunk(ChunkTile chunkTile, TerrainType terrainType)
        {
            foreach (var tile in chunkTile.tiles)
            {
                if (tile.Terrain == TerrainType.ocean.ToString() || tile.Terrain == TerrainType.coast.ToString())
                {
                    // Debug.Log("Skipping ocean or coast");
                    continue;
                }
                tile.Terrain = terrainType.ToString();
            }
        }
        void SetFeatureToChunk(ChunkTile chunkTile, FeatureType featureType)
        {
            foreach (var tile in chunkTile.tiles)
            {
                if(tile.ElevationType == ElevationType.mountain.ToString() || tile.Terrain == TerrainType.ocean.ToString() || tile.Terrain == TerrainType.coast.ToString() || tile.Feature == FeatureType.lake.ToString()) continue;
                tile.Feature = featureType.ToString();
            }
        }
    }
}