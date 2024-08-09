using System.Collections.Generic;
using _Project.Scripts.Arko_s_Tooltip_System.Codes;
using _Project.Scripts.HelperScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.Static_Classes;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Tiles.Factory
{
    public class TileGenerationFactory: IFactory<Tile, int[], Transform, ITileController>
    {
        private readonly DiContainer _container;
        private readonly TileMapInitializingDataContainer _tileMapInitializingDataContainer;
        public TileGenerationFactory(DiContainer container, TileMapInitializingDataContainer tileMapInitializingDataContainer)
        {
            _container = container;
            _tileMapInitializingDataContainer = tileMapInitializingDataContainer;
        }


        public ITileController Create(Tile tile, int[] triangles, Transform tileParent)
        {
            var tileObject = _container.InstantiatePrefab(_tileMapInitializingDataContainer.tileMeshRendererPrefab, tileParent);
            tileObject.name = "Tile " + tile.XPosition + ", " + tile.YPosition;
            var tileView = _container.InstantiateComponent<TileView>(tileObject);
            var onclickTile = _container.InstantiateComponent<OnClickTile>(tileObject);
            onclickTile.tileId = tile.Id;
            onclickTile.tile = tile;
            
            TooltipAssignment(tile, tileObject);
            TileMeshAndMaterial(tile, triangles, tileView, tileObject);
            TileColliderAssignment(tile, tileView);

            return _container.Instantiate<TileController>(new object[]{tileView, tile.Id});
        }

        private static void TileColliderAssignment(Tile tile, TileView tileView)
        {
            var tempCenter = new Vector3(tile.TileCoordinates.X + 0.5f, tile.Elevation / 2, tile.TileCoordinates.Y + 0.5f);
            tileView.tileBoxCollider.center = tempCenter;
            var size = tileView.tileBoxCollider.size;
            size = new Vector3(size.x, tile.Elevation, size.z);
            tileView.tileBoxCollider.size = size;
        }

        private void TileMeshAndMaterial(Tile tile, int[] triangles, TileView tileView, GameObject tileObject)
        {
            var mesh = new Mesh();
            var tileMesh = tileView.meshFilter.mesh = mesh;

            List<Vector3> listOfTileVertices = new List<Vector3>();
            List<Color> tileColors = new List<Color>();
            //tileMesh.vertices = new Vector3[tile.vertices.Length];
            for (int i = 0; i < tile.vertices.Length; i++)
            {
                Vector3 tempVector3 = new Vector3(tile.vertices[i].x, tile.vertices[i].y, tile.vertices[i].z);
                listOfTileVertices.Add(tempVector3);
                
                tileColors.Add(tile.GetColor(_tileMapInitializingDataContainer));
            }

            tileMesh.vertices = listOfTileVertices.ToArray();
            //tileMesh.vertices = tile.vertices;
            tileMesh.triangles = triangles;
            AssignMaterial(tile, tileObject);
            tileMesh.RecalculateNormals();
            tileMesh.colors = tileColors.ToArray();
        }

        private void TooltipAssignment(Tile tile, GameObject tileObject)
        {
            var toolTipTriggerInternal = _container.InstantiateComponent<TooltipTriggerInternal>(tileObject);
            toolTipTriggerInternal.content = $"{tileObject.name}\n" +
                                             $"{tile.Terrain.GetTerrainName()}\n" +
                                             $"{tile.ElevationType.GetElevationName()}\n{tile.Feature.GetFeatureName()}";
        }

        private void AssignMaterial(Tile tile, GameObject tileGameObject)
        {
            if ( tile.Feature != FeatureType.lake.ToString() && tile.Feature != FeatureType.swamps.ToString() /* && (tile.Feature != FeatureType.jungle.ToString() && tile.Feature!= FeatureType.forest.ToString())*/)
            {
                // if(tile.IsRiverMouth||tile.IsRiverOrigin)return;
                if (tile.Terrain == TerrainType.grassland.ToString())
                {
                    tileGameObject.GetComponent<MeshRenderer>().material = GetRandomMaterials(_tileMapInitializingDataContainer.grassLandMaterials);
                }else if (tile.Terrain == TerrainType.dryland.ToString())
                {
                    tileGameObject.GetComponent<MeshRenderer>().material = GetRandomMaterials(_tileMapInitializingDataContainer.dryLandMaterials);
                }else if (tile.Terrain == TerrainType.desert.ToString())
                {
                    tileGameObject.GetComponent<MeshRenderer>().material = GetRandomMaterials(_tileMapInitializingDataContainer.desertMaterials);
                }else if (tile.Terrain == TerrainType.plains.ToString())
                {
                    tileGameObject.GetComponent<MeshRenderer>().material = GetRandomMaterials(_tileMapInitializingDataContainer.plainsMaterials);
                }else if (tile.Terrain == TerrainType.snow.ToString())
                {
                    tileGameObject.GetComponent<MeshRenderer>().material = GetRandomMaterials(_tileMapInitializingDataContainer.snowMaterials);
                }else if (tile.Terrain == TerrainType.tundra.ToString())
                {
                    tileGameObject.GetComponent<MeshRenderer>().material = GetRandomMaterials(_tileMapInitializingDataContainer.tundraMaterials);
                }
                
                if (tile.ElevationType == ElevationType.mountain.ToString())
                {
                    tileGameObject.GetComponent<MeshRenderer>().material = GetRandomMaterials(_tileMapInitializingDataContainer.mountainMaterials);
                }
            }
        }

        private Material GetRandomMaterials(List<Material> materialsList)
        {
            return materialsList[Random.Range(0, materialsList.Count)];
        }
    }
}