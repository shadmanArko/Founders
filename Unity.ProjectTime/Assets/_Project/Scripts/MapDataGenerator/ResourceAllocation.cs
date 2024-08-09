using System.Collections.Generic;
using _Project.Scripts.HelperScripts;
using _Project.Scripts.Natural_Resources;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.MapDataGenerator
{
    public class ResourceAllocation
    {
        private NewGameDataGenerator _newGameDataGenerator;

        public ResourceAllocation(NewGameDataGenerator newGameDataGenerator)
        {
            _newGameDataGenerator = newGameDataGenerator;
        }

        public void AssignResources(GameObject resourceParent)
        {
            List<string> allResources = new List<string>();
            foreach (var resourceGroup in _newGameDataGenerator.tileMapInitializingDataContainer.resourceGroups)
            {
                foreach (var naturalResource in _newGameDataGenerator.tileMapInitializingDataContainer.resourcesContainer.resources.Shuffle())
                {
                    if(naturalResource.Category != resourceGroup.resourceGroupName) continue;
                    // if(naturalResource.GetResourceIcon() == null) continue;
                    List<Tile> eligibleListOfTiles = new List<Tile>();
                    foreach (var tile in _newGameDataGenerator.listOfTiles.Shuffle())
                    {
                        if(tile.ElevationType == ElevationType.mountain.ToString())continue;
                        if(tile.Feature == FeatureType.lake.ToString()) continue;

                        if(tile.NaturalResource != null)continue;
                        foreach (var resourceLocation in naturalResource.resourceLocationsList)
                        {
                            if (resourceLocation.terrain != "AnyLand")
                            {
                                if(resourceLocation.terrain != tile.Terrain) continue;
                            }
                            else
                            {
                                if(tile.Terrain == TerrainType.ocean.ToString() || tile.Terrain == TerrainType.coast.ToString()) continue;
                            }

                            if (resourceLocation.feature != "Any")
                            {
                                if(!resourceLocation.hasFeature && tile.Feature != null ) continue;
                                if(resourceLocation.hasFeature && tile.Feature != resourceLocation.feature ) continue; 
                            }
                            if(resourceLocation.elevationType != tile.ElevationType) continue;
                            if(eligibleListOfTiles.Contains(tile)) continue;
                            eligibleListOfTiles.Add(tile);
                            if(eligibleListOfTiles.Count >= resourceGroup.numberOfResourceToSpawn) break;
                        }

                        if (eligibleListOfTiles.Count >= resourceGroup.numberOfResourceToSpawn)
                        {
                            break;
                        }
                    }

                    if (eligibleListOfTiles.Count >= resourceGroup.numberOfResourceToSpawn)
                    {
                        if (!resourceGroup.isMandatory)
                        {
                            if (Random.Range(0, 100) > resourceGroup.optionalProbability)
                            {
                                Debug.Log($"At {_newGameDataGenerator.subcontinent.subcontinentName} {resourceGroup.resourceGroupName} Resource {naturalResource.Name} was not spawned for less probability.");
                                break;
                            }
                        }
                        foreach (var eligibleTile in eligibleListOfTiles)
                        {
                            eligibleTile.NaturalResource = naturalResource;
                            // var resourceIcon = Object.Instantiate((Object)_newGameDataGenerator.tileMapInitializingDataContainer.naturalResourceIcon,
                            //     new Vector3(eligibleTile.XPosition, eligibleTile.Elevation, eligibleTile.YPosition), Quaternion.identity, resourceParent.transform);
                            // resourceIcon.GetComponent<NaturalResourceIcon>().Init(naturalResource);
                            // Debug.Log("Resource " + naturalResource.Name + " Location "+ eligibleTile.XPosition +", " + eligibleTile.YPosition );
                           // Debug.Log($"At {_newGameDataGenerator.subcontinent.subcontinentName} {resourceGroup.resourceGroupName} Resource {naturalResource.Name} Location {eligibleTile.XPosition}, {eligibleTile.YPosition}");
                        }
                        allResources.Add(naturalResource.Name);
                        if(!resourceGroup.resourceGroupName.Equals("Must Haves")) break;
                    }
                    else
                    {
                        Debug.Log($"At {_newGameDataGenerator.subcontinent.subcontinentName} {resourceGroup.resourceGroupName} Resource {naturalResource.Name} found locations {eligibleListOfTiles.Count}");
                    }
                }
            }

            _newGameDataGenerator.subcontinentResources.Add(allResources);
        }
    }
}