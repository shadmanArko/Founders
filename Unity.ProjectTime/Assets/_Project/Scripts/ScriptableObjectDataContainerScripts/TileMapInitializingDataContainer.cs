using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.ScriptableObjectDataContainerScripts
{
    [CreateAssetMenu(fileName = "TilemapInitializingDataContainer", menuName = "ProceduralMapGeneration/TilemapInitializingDataContainer", order = 0)]
    public class TileMapInitializingDataContainer : ScriptableObject
    {
        public int gridSizeX;
        public int gridSizeY;
        public int chunkTileSize;
        public int meshDivisions;
        public int numberOfSlots;
        public GameObject tileMeshRendererPrefab;
        [FormerlySerializedAs("saveDataDataScriptableObject")] [FormerlySerializedAs("playerDataDataScriptableObject")] public SaveDataScriptableObject saveDataScriptableObject;
        public TileNameGeneratorDataContainerScriptableObject tileNameGeneratorDataContainerScriptableObject;
        
        [Header("Tile Heights")]
        public float landHeight;
        public float coastHeight;
        public float riverHeight;
        public float lakeHeight;
        public float mountainHeight;
        public float hillHeight;
        public float oceanHeight;
        
        [Header("Tile identification color")]
        public Color grassLandColor;
        public Color tundraColor;
        public Color dessertColor;
        public Color plainsColor;
        public Color dryLandColor;
        public Color forestColor;
        public Color jungleColor;
        public Color snowColor;
        public Color coastColor;
        public Color riverColor;
        public Color lakeColor;
        public Color mountainColor;
        public Color hillColor;
        public Color oceanColor;
        public Color swampColor;

        [Header("Trees")] 
        public List<GameObject> treePrefabs;
        public List<GameObject> jungleTreePrefabs;
        public List<GameObject> forestTreePrefabs;

        public int numberOfTreesPerTile = 15;

        [Header("Resources")] 
        public ResourcesScriptableObject resourcesContainer;
        public List<ResourceGroup> resourceGroups;
        // public NaturalResourceIcon naturalResourceIcon;
        
        [Header("Tile Materials")] 
        public List<Material> desertMaterials;
        public List<Material> dryLandMaterials;
        public List<Material> grassLandMaterials;
        public List<Material> plainsMaterials;
        public List<Material> snowMaterials;
        public List<Material> tundraMaterials;
        public List<Material> mountainMaterials;

        public SubcontinentsContainer subcontinentsContainer;
        
    }
}