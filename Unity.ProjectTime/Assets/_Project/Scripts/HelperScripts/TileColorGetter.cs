using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Services;
using UnityEngine;

namespace _Project.Scripts.HelperScripts
{
    public static class TileColorGetter
    {

        public static Color GetColor(this Tile tile, TileMapInitializingDataContainer _tileMapInitializingDataContainer)
        {
            Color color = Color.blue;
            if (tile.Terrain == TerrainType.grassland.ToString())
            {
                color = _tileMapInitializingDataContainer.grassLandColor;
            }
            else if (tile.Terrain == TerrainType.coast.ToString())
            {
                //coast blue color
                color = _tileMapInitializingDataContainer.coastColor;
            }
            else if (tile.Terrain == TerrainType.tundra.ToString())
            {
                //tundra
                color = _tileMapInitializingDataContainer.tundraColor;
            }
            else if (tile.Terrain == TerrainType.plains.ToString())
            {
                //plains
                color = _tileMapInitializingDataContainer.plainsColor;
            }
            else if (tile.Terrain == TerrainType.desert.ToString())
            {
                //dessert
                color = _tileMapInitializingDataContainer.dessertColor;
            }
            else if (tile.Terrain == TerrainType.dryland.ToString())
            {
                //dryLand
                color = _tileMapInitializingDataContainer.dryLandColor;
            }
            else if (tile.Terrain == TerrainType.snow.ToString())
            {
                color = _tileMapInitializingDataContainer.snowColor;
            }

            if (tile.ElevationType == ElevationType.mountain.ToString())
            {
                //mountain color
                color = _tileMapInitializingDataContainer.mountainColor;
            }

            // if (tile.ElevationType == ElevationType.hill.ToString())
            // {
            //     //hillcolor
            //     color = new Color(0.7f, 0.6f, 0.4f);
            // }
            if (tile.Feature == FeatureType.lake.ToString())
            {
                //lake
                color = _tileMapInitializingDataContainer.lakeColor;
            }

            if (tile.Feature == FeatureType.swamps.ToString())
            {
                //swamps
                color = _tileMapInitializingDataContainer.swampColor;
            }
            // if (tile.Feature == FeatureType.jungle.ToString())
            // {
            //     //jungle
            //     color = tileMapInitializingDataContainer.jungleColor;
            // }
            // if (tile.Feature == FeatureType.forest.ToString())
            // {
            //     //jungle
            //     color = tileMapInitializingDataContainer.forestColor;
            // }

            if (tile.HasRiver)
            {
                //river
                color = Color.blue;
            }

            if (tile.IsRiverOrigin)
            {
                //lake
                color = Color.black;
            }

            if (tile.IsRiverMouth)
            {
                color = Color.magenta;
            }
            if (tile.BranchRiver)
            {
                color = Color.magenta;
            }

            return color;
        }
    }
}