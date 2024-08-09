using _Project.Scripts.Game_Actions;
using _Project.Scripts.HelperScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.Helpers;
using ASP.NET.ProjectTime.Models;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Buildings.Factory
{
    public class BuildingGenerationFactory : IFactory<Building, IBuildingController>
    {
        private readonly DiContainer _container;
        private readonly BuildingPrefabContainerScriptableObject _buildingPrefabContainerScriptableObject;

        public BuildingGenerationFactory(DiContainer container, BuildingPrefabContainerScriptableObject buildingPrefabContainerScriptableObject)
        {
            _container = container;
            _buildingPrefabContainerScriptableObject = buildingPrefabContainerScriptableObject;
        }

        public IBuildingController Create(Building building)
        {
            GameObject buildingToSpawn;
            BuildingSize newBuildingSize;
            
            var buildingSize = building.BuildingSize.ToString();
            if (buildingSize.Contains("Small"))
            {
                buildingToSpawn = _buildingPrefabContainerScriptableObject.smallBuilding;
                newBuildingSize = BuildingSize.Small;
            }
            else if (buildingSize.Contains("Medium"))
            {
                buildingToSpawn = _buildingPrefabContainerScriptableObject.mediumBuilding;
                newBuildingSize = BuildingSize.Medium;
            }
            else if(buildingSize.Contains("ExtraLarge"))
            {
                buildingToSpawn = _buildingPrefabContainerScriptableObject.extraLargeBuilding;
                newBuildingSize = BuildingSize.ExtraLarge;
            }
            else
            {
                buildingToSpawn = _buildingPrefabContainerScriptableObject.largeBuilding;
                newBuildingSize = BuildingSize.Large;
            }
            
            
            
            var buildingObject = _container.InstantiatePrefab(buildingToSpawn);
            var buildingView = _container.InstantiateComponent<BuildingView>(buildingObject);
            
            // buildingView.LoadSpriteRenderer();
            AssignSprite(buildingView.buildingSpriteRendererContainer.surfaceSpriteRendered, building.BuildingVariation.BuildingSurfacePngFileLocation);
            AssignSprite(buildingView.buildingSpriteRendererContainer.row1SpriteRenderer, building.BuildingVariation.Row1PngFileLocation);
            AssignSprite(buildingView.buildingSpriteRendererContainer.row2SpriteRenderer, building.BuildingVariation.Row2PngFileLocation);
            AssignSprite(buildingView.buildingSpriteRendererContainer.row3SpriteRenderer, building.BuildingVariation.Row3PngFileLocation);
            AssignSprite(buildingView.buildingSpriteRendererContainer.row4SpriteRenderer, building.BuildingVariation.Row4PngFileLocation);
            AssignSprite(buildingView.buildingSpriteRendererContainer.row5SpriteRenderer, building.BuildingVariation.Row5PngFileLocation);
            
            var onClickBuilding = _container.InstantiateComponent<OnClickBuilding>(buildingObject);
            // var buildingModel = new Building(NewIdGenerator.GenerateNewId(),"", 1f, 1f, 5, 10, 20,
            //     null, newBuildingSize, BuildingSlot.Large_Center, building.TileId, null, 0, 0, 0);
            onClickBuilding.buildingId = building.Id;
            buildingView.gameObject.name = $"{buildingSize}";
            BuildingActions.OnBuildingCreated?.Invoke(building.Id);
            return _container.Instantiate<BuildingController>(new object[]
                { building, buildingView, buildingSize, building.TileId }); 
        }

        async void AssignSprite(SpriteRenderer spriteRenderer, PngAndAddressableFileLocation pngAndAddressableFileLocation)
        {
            if (pngAndAddressableFileLocation.IsAddressable)
            {
                spriteRenderer.sprite = await pngAndAddressableFileLocation.LoadSpriteFromAssetBundle();
            }
            else
            {
                spriteRenderer.sprite = await pngAndAddressableFileLocation.LoadSpriteFromPath();
                
            }
        }
    }
}
