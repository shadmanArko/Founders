using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Project.Scripts.HelperScripts;
using _Project.Scripts.Pathfinding;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.DataContext;
using ASP.NET.ProjectTime.Helpers;
using ASP.NET.ProjectTime.Models;
using ASP.NET.ProjectTime.Models.PathFinding;
using ASP.NET.ProjectTime.Services;
using ASP.NET.ProjectTime.Services.ClanService;
using ASP.NET.ProjectTime.Services.Tiles;
using SandBox.Arko.Scripts.UnitOfWork;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Project.Scripts.MapDataGenerator
{
    public class NewGameDataGenerator
    {
    
        public Vertex[] vertices;

        public int[] triangles;
    
        private PathfinderGridDataContainer _pathfinderGridDataContainer;
        public TileMapInitializingDataContainer tileMapInitializingDataContainer;
        private AStarPathfinding _aStarPathfinding;
        private RandomSeedController _randomSeedController;
        public Subcontinent subcontinent;

        private List<ChunkTile> _chunkTiles;
        public List<Tile> edgeTiles;
        public List<GameObject> tileObjects;
        public List<GameObject> treeObjects;
        private GameObject _environmentalObjectsParent;
        private List<GameObject> _objectsToDestroyBeforeRegeneration;

        public readonly List<List<string>> subcontinentResources = new List<List<string>>();
    

        public List<Tile> listOfTiles;
        // private List<Tile> _listOfAllSubcontinentTiles;
        private List<SubcontinentTiles> _listOfAllSubcontinentTilesList;
        private string _activeSubcontinentTilesId;

        private readonly PeninsulaGenerator _peninsulaGenerator;
        private readonly IslandGenerator _islandGenerator;
        private readonly FeatureAllocationBasedOnTemperatureAndTerrain _featureAllocation;
        private static readonly int WorldScale = Shader.PropertyToID("_WorldScale");
        private ClanNameGenerator _clanNameGenerator;
        private TileNameGenerator _tileNameGenerator;
        private UnitOfWork _unitOfWork;
        private readonly ResourceAllocation _resourceAllocation;
        private TileShapeGenerator _tileShapeGenerator;
        public NewGameDataGenerator(TileShapeGenerator tileShapeGenerator, PathfinderGridDataContainer pathfinderGridDataContainer, TileMapInitializingDataContainer tileMapInitializingDataContainer, AStarPathfinding aStarPathfinding, RandomSeedController randomSeedController, UnitOfWork unitOfWork)
        {
       
            _pathfinderGridDataContainer = pathfinderGridDataContainer;
            this.tileMapInitializingDataContainer = tileMapInitializingDataContainer;
            _tileShapeGenerator = tileShapeGenerator;
            _aStarPathfinding = aStarPathfinding;
            _randomSeedController = randomSeedController;
            _peninsulaGenerator = new PeninsulaGenerator(this);
            _islandGenerator = new IslandGenerator(this);
            _featureAllocation = new FeatureAllocationBasedOnTemperatureAndTerrain(this);
            _clanNameGenerator = new ClanNameGenerator(Random.Range(0, 100000).ToString());
            _tileNameGenerator = new TileNameGenerator(Random.Range(0, 100000).ToString());
            _resourceAllocation = new ResourceAllocation(this);
            _unitOfWork = unitOfWork;
            // Initialize();
        }

        #region Initialization
    
        public void Initialize()
        {
            _listOfAllSubcontinentTilesList = new List<SubcontinentTiles>();
            if (_objectsToDestroyBeforeRegeneration != null && _objectsToDestroyBeforeRegeneration.Count > 0)
            {
                foreach (var gameObject in _objectsToDestroyBeforeRegeneration)
                {
                    Object.Destroy(gameObject);
                }
                _objectsToDestroyBeforeRegeneration.Clear();
            }
            else
            {
                _objectsToDestroyBeforeRegeneration = new List<GameObject>();
            }
            if (tileObjects != null && tileObjects.Count > 0)
            {
                foreach (var tileObject in tileObjects)
                {
                    Object.Destroy(tileObject);
                }
                listOfTiles.Clear();
                _listOfAllSubcontinentTilesList.Clear();
            
            }
            else
            {
                tileObjects = new List<GameObject>();
            }
            if (treeObjects != null && treeObjects.Count > 0)
            {
                foreach (var treeObject in treeObjects)
                {
                    Object.Destroy(treeObject);
                }

                treeObjects.Clear();
            }
            else
            {
                treeObjects = new List<GameObject>();
                _environmentalObjectsParent = new GameObject(name:"Environmental Elements");
            }
            _randomSeedController.InitiateSeed();
            int subContinentCount = 0;
            foreach (var containerSubcontinent in tileMapInitializingDataContainer.subcontinentsContainer.subcontinents)
            {

                if (/*containerSubcontinent.subcontinentPosition.x == 0*/true)
                {
                    subcontinent = containerSubcontinent;
                    _environmentalObjectsParent = new GameObject(subcontinent.subcontinentName + "Environment");
                    _objectsToDestroyBeforeRegeneration.Add(_environmentalObjectsParent);
            
                    GenerateMap();
                    _environmentalObjectsParent.transform.position = new Vector3(subcontinent.subcontinentPosition.x, 0, subcontinent.subcontinentPosition.y);
                    DebugMapStatus();
                    subContinentCount++;
                }
            }
#if UNITY_EDITOR
            // DebugResources();
#endif
            //SaveData();
        }
        private void GenerateMap()
        {
            listOfTiles = new List<Tile>();
            edgeTiles = new List<Tile>();
            // mesh = new Mesh();
            // <MeshFilter>().mesh = mesh;
            (vertices, triangles) = _tileShapeGenerator.CreateShape();
            // UpdateMesh();
            InitializeTileVerticesAndTileData();
            AssignTileSlotsVertices();
            // CreateChunkTiles();
            GetEdgeTiles();
            SmoothenOutEdges();
            _peninsulaGenerator.MakePeninsula();
            _islandGenerator.MakeSmallIslands();
            GenerateCoast();
            GenerateMountains();
            GenerateSecondaryMountains();
            GenerateHills();
            GenerateLakes();
            GenerateMandatoryRivers();
            GenerateRivers();
            CreateBranchRiver();
            TerrainFeatureAllocation();
            BlendTerrainAndFeature();
            AllocateSwamps();
            // BlendFeatures();
            NameAllLandTiles();
            // GeneratePathFindingGrids();
            //TestPathFinding();
            GenerateTileHeight(ElevationType.mountain.ToString(), tileMapInitializingDataContainer.mountainHeight);
            AddPerlinNoiseToHeight(ElevationType.mountain.ToString());
            GenerateTileHeight(ElevationType.hill.ToString(), tileMapInitializingDataContainer.hillHeight);
            GenerateTileHeight(TerrainType.ocean.ToString(), tileMapInitializingDataContainer.oceanHeight);
            AssignHeightToLand(tileMapInitializingDataContainer.landHeight);
            GenerateTileHeight(TerrainType.coast.ToString(), tileMapInitializingDataContainer.coastHeight);
            GenerateTileHeight(FeatureType.lake.ToString(), tileMapInitializingDataContainer.lakeHeight);
            AdjustHeightOfTileEdgeRows();
            
            SetTileWalkableStatus();
            PlantTrees();
            TileVerticesBendingForRiver();
            _resourceAllocation.AssignResources(_environmentalObjectsParent);
            //FixMaterialTextureSize();
            TileMovementModifiers();
            // GenerateBigTileMesh();
        }

    

        #endregion

        #region PathFindings

        private void TileMovementModifiers()
        {
            foreach (var tile in listOfTiles)
            {
                tile.PathFindingTerrainModifier = 0;
                tile.PathFindingFeatureModifier = 0;
                tile.PathFindingElevationModifier = 0;
            
                var terrainType = tile.Terrain;
                var featureType = tile.Feature;
                var elevationType = tile.ElevationType;
            
                //terrain
                if (terrainType == TerrainType.tundra.ToString() || terrainType == TerrainType.dryland.ToString())
                {
                    tile.PathFindingTerrainModifier = 1;
                }else if (terrainType == TerrainType.desert.ToString())
                {
                    tile.PathFindingTerrainModifier = 2;
                }else if (terrainType == TerrainType.snow.ToString())
                {
                    tile.PathFindingTerrainModifier = 3;
                }

                //feature
                if (featureType == FeatureType.jungle.ToString() || featureType == FeatureType.swamps.ToString())
                {
                    tile.PathFindingFeatureModifier = 3;
                }else if (featureType == FeatureType.forest.ToString())
                {
                    tile.PathFindingTerrainModifier = 2;
                }
            
                //elevation
                if (elevationType == ElevationType.hill.ToString())
                {
                    tile.PathFindingFeatureModifier = 2;
                }
            }
        }

        private void SetTileWalkableStatus()
        {
            foreach (var tile in listOfTiles)
            {
                if(tile.Terrain == TerrainType.coast.ToString() || tile.Terrain == TerrainType.ocean.ToString() || tile.ElevationType == ElevationType.mountain.ToString() || tile.Feature == FeatureType.lake.ToString())
                {
                    tile.IsWalkable = false;
                }
                else
                {
                    tile.IsWalkable = true;
                }
            }
        }

        #endregion
    
        #region ForDebugging

        private void DebugResources()
        {       
            int subContinentCount = 0;
            foreach (var containerSubcontinent in tileMapInitializingDataContainer.subcontinentsContainer.subcontinents)
            {
                Debug.Log($"Total resources at {containerSubcontinent.subcontinentName}: {subcontinentResources[subContinentCount].Count}");
                foreach (var resource in subcontinentResources[subContinentCount])
                {
                    Debug.Log(resource);
                }
            
                subContinentCount++;
            }
        }
        private void DebugMapStatus()
        {
            var landTilesCount = 0;
            var waterTilesCount = 0;
            foreach (var tile in listOfTiles)
            {
                if (tile.Terrain == TerrainType.ocean.ToString() || tile.Terrain == TerrainType.coast.ToString())
                {
                    waterTilesCount++;
                }
                else
                {
                    landTilesCount++;
                }
            }
            Debug.Log($"At {subcontinent.subcontinentName} Total Land Tiles: {landTilesCount} and Total Water Tiles: {waterTilesCount}");
        }

        #endregion

        #region FeatureAndTrees

        private void PlantTrees()
        {
            foreach (var tile in listOfTiles)
            {
                if (tile.Feature == FeatureType.forest.ToString())
                {
                    PlantTreesOnTile(tile, tileMapInitializingDataContainer.forestTreePrefabs);
                }else if (tile.Feature == FeatureType.jungle.ToString())
                {
                    PlantTreesOnTile(tile, tileMapInitializingDataContainer.jungleTreePrefabs);

                }
            
            }
        }

        private void PlantTreesOnTile(Tile tile, List<GameObject> treePrefabs)
        {
            var numberOfTrees = tileMapInitializingDataContainer.numberOfTreesPerTile;
            var treeCount = 0;
            var tileSlotCoordinates = GetAvailableSlotForTreePlantation(tile);
            var tileHeight = tile.Elevation;
            foreach (var slotCoordinate in tileSlotCoordinates.Shuffle())
            {
                GameObject tree = Object.Instantiate(treePrefabs.Shuffle()[0],
                    new Vector3(slotCoordinate.x, tileHeight,
                        slotCoordinate.z), Quaternion.identity, _environmentalObjectsParent.transform);
                treeObjects.Add(tree);
                treeCount++;
                if (treeCount >= numberOfTrees) break;
            }
        }

        private List<Vertex> GetAvailableSlotForTreePlantation(Tile tile)
        {
            List<Vertex> listOfSlots = new List<Vertex>();
            listOfSlots = tile.SlotCoordinates;
            var numberOfSlots = tileMapInitializingDataContainer.numberOfSlots;
            TileFinder tileFinder = new TileFinder(listOfTiles);
            var rightNeighbourTile = tileFinder.GetTileByCoordinates(tile.GetNeighbourTileCoordinatesInDirection(Directions.Right)); 
            var leftNeighbourTile = tileFinder.GetTileByCoordinates(tile.GetNeighbourTileCoordinatesInDirection(Directions.Left)); 
            var topNeighbourTile = tileFinder.GetTileByCoordinates(tile.GetNeighbourTileCoordinatesInDirection(Directions.Up)); 
            var bottomNeighbourTile = tileFinder.GetTileByCoordinates(tile.GetNeighbourTileCoordinatesInDirection(Directions.Down));
            var removeRightSlots = false;
            var removeLeftSlots = false;
            var removeTopSlots = false;
            var removeBottomSlots = false;
            float numberOfSlotLinesToRemove = 4;
            if (tile.RiverLeft || ( leftNeighbourTile != null && (leftNeighbourTile.Terrain == TerrainType.coast.ToString() || leftNeighbourTile.Feature == FeatureType.lake.ToString())))
            {
                removeLeftSlots = true;
            }
            if (tile.RiverRight || (rightNeighbourTile != null && (rightNeighbourTile.Terrain == TerrainType.coast.ToString() || rightNeighbourTile.Feature == FeatureType.lake.ToString())))
            {
                removeRightSlots = true;
            }
            if (tile.RiverTop || (topNeighbourTile != null && (topNeighbourTile.Terrain == TerrainType.coast.ToString() || topNeighbourTile.Feature == FeatureType.lake.ToString())))
            {
                removeTopSlots = true;
            }
            if (tile.RiverBottom || (bottomNeighbourTile != null && (bottomNeighbourTile.Terrain == TerrainType.coast.ToString() || bottomNeighbourTile.Feature == FeatureType.lake.ToString())))
            {
                removeBottomSlots = true;
            }
            foreach (var slot in listOfSlots.ToList())
            {
                if (removeLeftSlots && slot.x <= tile.XPosition + numberOfSlotLinesToRemove/numberOfSlots)
                {
                    listOfSlots.Remove(slot);
                }
                if (removeRightSlots && slot.x > tile.XPosition + (numberOfSlots - numberOfSlotLinesToRemove)/numberOfSlots)
                {
                    listOfSlots.Remove(slot);
                }
                if (removeBottomSlots && slot.z <= tile.YPosition + numberOfSlotLinesToRemove/numberOfSlots)
                {
                    listOfSlots.Remove(slot);
                }
                if (removeTopSlots && slot.z > tile.YPosition + (numberOfSlots - numberOfSlotLinesToRemove)/numberOfSlots)
                {
                    listOfSlots.Remove(slot);
                }
                
            }
            return listOfSlots;
        }
        private void BlendTerrainAndFeature()
        {
            TileFinder tileFinder = new TileFinder(listOfTiles);
            // var landTiles = tileFinder.GetTilesByTerrain()
            foreach (var tile in listOfTiles)
            {
                if (tile.Terrain != TerrainType.coast.ToString() && tile.Terrain != TerrainType.ocean.ToString() && tile.Feature != FeatureType.lake.ToString())
                {
                    var probability = Random.Range(0, 100);
                    if (probability < 40)
                    {
                        foreach (var neighbourTile in tileFinder.GetTilesInRange(tile, 1).Shuffle())
                        {
                            if (neighbourTile.Terrain != tile.Terrain &&
                                neighbourTile.Terrain != TerrainType.coast.ToString() &&
                                neighbourTile.Terrain != TerrainType.ocean.ToString() &&
                                neighbourTile.Feature != FeatureType.lake.ToString())
                            {
                                tile.Terrain = neighbourTile.Terrain;
                                tile.Feature = neighbourTile.Feature;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void TerrainFeatureAllocation()
        {
            var dryChunkListIncludingChunkNeighbours = GetChunksIncludingTheNeighbourChunks(subcontinent.dryChunkList);
            foreach (var mapTemperatureInChunkColumn in subcontinent.mapTemperatureInChunkRows)
            {
                for (int i = 0; i < tileMapInitializingDataContainer.gridSizeX; i++)
                {
                    var chunkAddress = new Position2(i, mapTemperatureInChunkColumn.chunkRowIndex);
                    bool dryRegionChunk = false;
                    foreach (var dryChunkAddress in dryChunkListIncludingChunkNeighbours)
                    {
                        if (dryChunkAddress.Equals(chunkAddress))
                        {
                            dryRegionChunk = true;
                        }
                    }
                    AllocateChunkFeaturesAccordingToTemperatureAndTerrain(chunkAddress, mapTemperatureInChunkColumn.temperatureStrength, dryRegionChunk);
                }
            }
        }
        private void GenerateLakes()
        {
            TileFinder tileFinder = new TileFinder(listOfTiles);
            var flatTiles = tileFinder.GetTilesByElevation(ElevationType.flat);
            int maxNumberOfLakes = subcontinent.numberOfLakes.RandomValueInRange();
            int lakeCount = 0;
            foreach (var flatTile in flatTiles.Shuffle())
            {
                //if no coast tile in neighbour make it lake
                if(flatTile.Terrain == TerrainType.ocean.ToString())continue;
                bool noOceanTileInNeighbour = true;
                var neighbourTileCoordinates = flatTile.GetListOfNeighbourTileCoordinates();
                foreach (var tileCoordinates in neighbourTileCoordinates)
                {
                    var neighbourTile = tileFinder.GetTileByCoordinates(tileCoordinates);
                    if (neighbourTile != null && (neighbourTile.Terrain == TerrainType.coast.ToString() || neighbourTile.ElevationType != ElevationType.flat.ToString() && !neighbourTile.HasRiver))
                    {
                        noOceanTileInNeighbour = false;
                        break;
                    }
                }

                if (noOceanTileInNeighbour)
                {
                    flatTile.Feature = FeatureType.lake.ToString();
                    lakeCount++;
                }
                if(lakeCount>= maxNumberOfLakes) break;
            }
        }
        private void AllocateSwamps()
        {
            var numberOfSwamps = subcontinent.numberOfSwamps.RandomValueInRange();
            int swampCount = 0;
            Debug.Log("Swamps number " + numberOfSwamps);
            foreach (var tile in listOfTiles.Shuffle())
            {
                if(tile.ElevationType != ElevationType.flat.ToString())continue;
                if (tile.Terrain == TerrainType.grassland.ToString() || tile.Terrain == TerrainType.plains.ToString() ||
                    tile.Terrain == TerrainType.tundra.ToString())
                {
                    tile.Feature = FeatureType.swamps.ToString();
                    swampCount++;
                    if(swampCount >= numberOfSwamps) break;
                }
            }
        }
        #endregion

        #region Naming

        private void NameAllLandTiles()
        {
            TileFinder tileFinder = new TileFinder(listOfTiles);
            // var landTiles = tileFinder.GetTilesByTerrain()
            List<string> tileNames = new List<string>();
            foreach (var tile in listOfTiles)
            {
                if (tile.Terrain != TerrainType.coast.ToString() && tile.Terrain != TerrainType.ocean.ToString())
                {
                    var tileNameGeneratorDataContainer =
                        tileMapInitializingDataContainer.tileNameGeneratorDataContainerScriptableObject;
                    var name = _tileNameGenerator.GenerateRandomName(tileNameGeneratorDataContainer.prefixIndian,
                        tileNameGeneratorDataContainer.suffixIndian);
                    while (tileNames.Contains(name))
                    {
                        name = _tileNameGenerator.GenerateRandomName(tileNameGeneratorDataContainer.prefixIndian,
                            tileNameGeneratorDataContainer.suffixIndian);
                    }
                    tile.Name = name;
                    tileNames.Add(name);
                    // Debug.Log(tile.Name);
                }
            }
        }

        #endregion
    
        #region RiverAndCoasts

        private void GenerateRivers()
        {
            TileFinder tileFinder = new TileFinder(listOfTiles);
            var mountainTiles = tileFinder.GetTilesByElevation(ElevationType.mountain);
            var lakeTiles = tileFinder.GetTilesByFeature(FeatureType.lake);
            var possibleRiverTiles = mountainTiles.Concat(lakeTiles).ToList();
            int numberOfRivers = subcontinent.numberOfRivers.RandomValueInRange();
            for (int i = 0; i < numberOfRivers; i++)
            {
                int riverOriginDistanceFromCoast = subcontinent.riverOriginDistanceFromCoast.RandomValueInRange();
                foreach (var possibleRiverOrigin in possibleRiverTiles.Shuffle())
                {
                    if (possibleRiverOrigin.CanBeRiverOrigin(riverOriginDistanceFromCoast, listOfTiles))
                    {
                        var mouthTile = possibleRiverOrigin.GetRiverMouth(listOfTiles);
                        // GeneratePathFindingGrids();
                        // TestPathFinding(mountainTile.TileCoordinates, mouthTile.TileCoordinates);
                        CreateRiver(possibleRiverOrigin, mouthTile);
                        break;
                    }
                }
            }
        }

        private void CreateRiver(Tile possibleRiverOrigin, Tile mouthTile)
        {
            possibleRiverOrigin.HasRiver = true;
            mouthTile.HasRiver = true;
            possibleRiverOrigin.IsRiverOrigin = true;
            mouthTile.IsRiverMouth = true;
            if (mouthTile.YPosition > possibleRiverOrigin.YPosition)
            {
                possibleRiverOrigin.IsRiverMouth = true;
                possibleRiverOrigin.IsRiverOrigin = false;
                mouthTile.IsRiverOrigin = true;
                mouthTile.IsRiverMouth = false;
                mouthTile.CreateRiverInReverse(possibleRiverOrigin, listOfTiles);
            }
            else
            {
                // possibleRiverOrigin.IsRiverOrigin = true;
                // mouthTile.IsRiverMouth = true;
                possibleRiverOrigin.CreateRiver(mouthTile, listOfTiles);
            }

            possibleRiverOrigin.IsRiverOrigin = true;
            possibleRiverOrigin.IsRiverMouth = false;
            mouthTile.IsRiverMouth = true;
            mouthTile.IsRiverOrigin = false;
        }

        private void GenerateMandatoryRivers()
        {
            TileFinder tileFinder = new TileFinder(listOfTiles);
            foreach (var mandatoryRiver in subcontinent.mandatoryRivers)
            {
                var originTiles = GetChunkTiles(mandatoryRiver.riverOriginChunks);
                var mouthTiles = GetChunkTiles(mandatoryRiver.riverMouthChunks);
                var selectedOriginTile = new Tile();
                var selectedMouthTile = new Tile();
                foreach (var originTile in originTiles.Shuffle())
                {
                    var neighbourTileCoordinates = originTile.GetListOfNeighbourTileCoordinates();
                    var canBeOrigin = true;
                    foreach (var neighbourTileCoordinate in neighbourTileCoordinates)
                    {
                        var neighbourTile = tileFinder.GetTileByCoordinates(neighbourTileCoordinate);
                        if (neighbourTile != null && neighbourTile.IsWaterTile())
                        {
                            canBeOrigin = false;
                        }
                    }

                    if (canBeOrigin)
                    {
                        selectedOriginTile = originTile;
                        break;
                    }
                }
                foreach (var mouthTile in mouthTiles.Shuffle())
                {
                    if(!mouthTile.IsWaterTile()) continue;
                    var neighbourTileCoordinates = mouthTile.GetListOfNeighbourTileCoordinates();
                    var canBeMouth = false;
                    foreach (var neighbourTileCoordinate in neighbourTileCoordinates)
                    {
                        var neighbourTile = tileFinder.GetTileByCoordinates(neighbourTileCoordinate);
                        if (neighbourTile != null && !neighbourTile.IsWaterTile())
                        {
                            canBeMouth = true;
                        }
                    }

                    if (canBeMouth)
                    {
                        selectedMouthTile = mouthTile;
                        break;
                    }
                }

            
                if (selectedOriginTile != null && selectedMouthTile != null)
                {
                    if(selectedOriginTile.ElevationType != ElevationType.mountain.ToString()) selectedOriginTile.Feature = FeatureType.lake.ToString();
                    CreateRiver(selectedOriginTile, selectedMouthTile);
                    Debug.Log($"Created Mandatory River");
                }
            }
        }
        private void CreateBranchRiver()
        {
            TileFinder tileFinder = new TileFinder(listOfTiles);
        
            int numberOfRiverBranches = subcontinent.numberOfRiverBranches.RandomValueInRange();
            for (int i = 0; i < numberOfRiverBranches; i++)
            {
                var tilesWithoutOriginAndMouth = tileFinder.GetRiverTilesWithoutOriginAndMouth();
                int riverBranchOriginDistanceFromCoast = subcontinent.riverBranchOriginDistanceFromCoast.RandomValueInRange();
                foreach (var riverTile in tilesWithoutOriginAndMouth.Shuffle())
                {
                    if (riverTile.CanBeRiverOrigin(riverBranchOriginDistanceFromCoast, listOfTiles))
                    {
                        if (riverTile.RiverTop && riverTile.RiverLeft && !riverTile.RiverRight)
                        {
                            var mouthTile = riverTile.GetBranchRiverMouth( Directions.Right, listOfTiles);
                            riverTile.CreateRiver(mouthTile, listOfTiles);
                            break;
                        }else if (riverTile.RiverTop && riverTile.RiverRight && !riverTile.RiverLeft)
                        {
                            var mouthTile = riverTile.GetBranchRiverMouth( Directions.Left, listOfTiles);
                            riverTile.CreateRiver(mouthTile, listOfTiles);
                            break;
                        }
                        continue;
                        // GeneratePathFindingGrids();
                        // TestPathFinding(mountainTile.TileCoordinates, mouthTile.TileCoordinates);
                    }
                }
            }
        }
        private void TileVerticesBendingForRiver()
        {
            foreach (var tile in listOfTiles)
            {
            
                if (tile.RiverLeft)
                {
                    for (int i = 0; i < tileMapInitializingDataContainer.meshDivisions + 1; i++)
                    {
                        tile.LeftVertices[i].y = tileMapInitializingDataContainer.riverHeight;
                    }
                }

                if (tile.RiverRight)
                {
                    for (int i = 0; i < tileMapInitializingDataContainer.meshDivisions + 1; i++)
                    {
                        tile.RightVertices[i].y = tileMapInitializingDataContainer.riverHeight;
                    }
                }

                if (tile.RiverTop)
                {
                    for (int i = 0; i < tileMapInitializingDataContainer.meshDivisions + 1; i++)
                    {
                        tile.TopVertices[i].y = tileMapInitializingDataContainer.riverHeight;
                    }
                    // Debug.Log("top vertices height reduced");

                }

                if (tile.RiverBottom)
                {
                    for (int i = 0; i < tileMapInitializingDataContainer.meshDivisions + 1; i++)
                    {
                        tile.BottomVertices[i].y = tileMapInitializingDataContainer.riverHeight;
                    }
                    // Debug.Log("bottom vertices height reduced");
                }
            }
    
    
        }

        private void GenerateCoast()
        {
            TileFinder tileFinder = new TileFinder(listOfTiles);
            foreach (var tile in tileFinder.GetTilesByTerrain(TerrainType.ocean.ToString()))
            {
                var coordinatesOfNeighbours = tile.GetListOfNeighbourTileCoordinates();
                var hasLandInNeighbour = false;
                var hasOceanInNeighbour = false;
                foreach (var coordinatesOfNeighbour in coordinatesOfNeighbours)
                {
                
                    var neighbourTile = tileFinder.GetTileByCoordinates(coordinatesOfNeighbour);
                    if (neighbourTile != null && neighbourTile.Terrain == TerrainType.grassland.ToString())
                    {
                        hasLandInNeighbour = true;
                    }
                    if (neighbourTile != null && (neighbourTile.Terrain == TerrainType.ocean.ToString() || neighbourTile.Terrain == TerrainType.coast.ToString()))
                    {
                        hasOceanInNeighbour = true;
                        // Debug.Log("Assigned has ocean in neighbour");
                    }
                }

                if (!hasLandInNeighbour) continue;
                if (!hasOceanInNeighbour)
                {
                    // Debug.Log("Tile " + tile.XPosition + " ," + tile.YPosition +" has no ocean in neighbour");
                    tile.Terrain = TerrainType.grassland.ToString();
                    tile.ElevationType = ElevationType.flat.ToString();
                    tile.Feature = FeatureType.lake.ToString();
                }
                else
                {
                    tile.Terrain = TerrainType.coast.ToString();
                }
            }
        }

        #endregion
    
        #region HillsAndMountains

        private void GenerateHills()
        {
            TileFinder tileFinder = new TileFinder(listOfTiles);
            var mountainTileLists = tileFinder.GetTilesByElevation(ElevationType.mountain);
            int probabilityValue;
            foreach (var mountainTile in mountainTileLists)
            {
                probabilityValue = Random.Range(0, 100);
                if (probabilityValue < subcontinent.hillCreationProbabilityAtMountainTile)
                {
                    mountainTile.ElevationType = ElevationType.hill.ToString();
                }

                //Hills beside mountains
                var neighbourTileCoordinates = mountainTile.GetListOfNeighbourTileCoordinates();
                foreach (var neighbourTileCoordinate in neighbourTileCoordinates)
                {
                    var tile = tileFinder.GetTileByCoordinates(neighbourTileCoordinate);
                    if (tile!= null && tile.ElevationType != ElevationType.mountain.ToString() &&
                        tile.Terrain == TerrainType.grassland.ToString())
                    {
                        probabilityValue = Random.Range(0, 100);
                        if (probabilityValue < subcontinent.hillCreationProbabilityBesideMountainTile)
                        {
                            tile.ElevationType = ElevationType.hill.ToString();
                        }
                    }
                }
            }
        }

        private void GenerateMountains()
        {
            ConvertChunkToTerrain(subcontinent.mountainChunkList, TerrainType.grassland);
            ConvertChunkElevation(subcontinent.mountainChunkList, ElevationType.mountain);
        
        
        }
        private void GenerateSecondaryMountains()
        {
            var numberOfSecondaryMountain = subcontinent.numberOfSecondaryMountainRange.RandomValueInRange();
            TileFinder tileFinder = new TileFinder(listOfTiles);
            for (int i = 0; i < numberOfSecondaryMountain; i++)
            {
                var maxLengthOfMountain = subcontinent.lengthOfSecondaryMountainRange.RandomValueInRange();
                var landTiles = tileFinder.GetTilesByTerrain(TerrainType.grassland.ToString());
                foreach (var landTile in landTiles.Shuffle())
                {
                    var secondaryMountainPossible = landTile.IsSecondaryMountainPossible(maxLengthOfMountain, listOfTiles, tileMapInitializingDataContainer.gridSizeX,
                        tileMapInitializingDataContainer.gridSizeY);
                    if (secondaryMountainPossible)
                    {
                        landTile.CreateSecondaryMountain(maxLengthOfMountain, listOfTiles, tileMapInitializingDataContainer.gridSizeX, tileMapInitializingDataContainer.gridSizeY);
                        break;
                    }
                }
            }
        }
        private void AddPerlinNoiseToHeight(string elevation)
        {
            foreach (var tile in listOfTiles)
            {

                int numberOfMountainPeaks = Random.Range(3, 8);
                int mountainPeaksCountInTile = 0;
                if (tile.ElevationType == elevation)
                {
                    foreach (var vertex in tile.vertices)
                    {
                        
                        if ( vertex.x > tile.XPosition + 0.3f && vertex.z > tile.YPosition + 0.3f )
                        {
                            if(vertex.x < tile.XPosition + 0.7f && vertex.z < tile.YPosition + 0.7f)
                            {
                                if (mountainPeaksCountInTile < numberOfMountainPeaks)
                                {
                                    MountainYValueDividedBy(vertex, tile, 2);
                                    // vertex.y += 1; 
                                    mountainPeaksCountInTile++;
                                    Debug.Log("Mountain height changed with perlin noise");
                                }
                            }
                            else
                            {
                                MountainYValueDividedBy(vertex, tile, 4);
                            }
                        }
                        else
                        {
                            MountainYValueDividedBy(vertex, tile, 4);
                        }
                    }
                }


            }
        }

        private void MountainYValueDividedBy(Vertex vertex, Tile tile , int divideBy)
        {
            var height = tile.Elevation +
                         GetPerlinNoiseValue(tile.XPosition + (int) vertex.x, tile.YPosition + (int) vertex.y) /
                         divideBy;
            vertex.y = height;
        }

        #endregion

        #region TileHeightAdjustment

        private void AdjustHeightOfTileEdgeRows()
        {
            TileFinder tileFinder = new TileFinder(listOfTiles);
            foreach (var tile in listOfTiles)
            {
                var leftTile = tileFinder.GetTileByCoordinates(tile.GetNeighbourTileCoordinatesInDirection(Directions.Left));
                var rightTile = tileFinder.GetTileByCoordinates(tile.GetNeighbourTileCoordinatesInDirection(Directions.Right));
                var downTile = tileFinder.GetTileByCoordinates(tile.GetNeighbourTileCoordinatesInDirection(Directions.Down));
                var upTile = tileFinder.GetTileByCoordinates(tile.GetNeighbourTileCoordinatesInDirection(Directions.Up));

                if (tile.IsWaterTile())
                {
                    if (leftTile != null && !leftTile.IsWaterTile() && leftTile.Elevation > tile.Elevation && !tile.RiverLeft)
                    {
                        SetTheHeightOfTheTileEdge(tile, MathF.Min( leftTile.Elevation, tileMapInitializingDataContainer.landHeight), Directions.Left);
                        SetTheHeightOfTheTileEdge(leftTile, tileMapInitializingDataContainer.landHeight, Directions.Right);
                    }
                    if (rightTile != null && !rightTile.IsWaterTile() && rightTile.Elevation > tile.Elevation && !tile.RiverRight)
                    {
                        SetTheHeightOfTheTileEdge(tile, MathF.Min( rightTile.Elevation, tileMapInitializingDataContainer.landHeight), Directions.Right);
                        SetTheHeightOfTheTileEdge(rightTile, tileMapInitializingDataContainer.landHeight, Directions.Left);
                    }
                    if (downTile != null && !downTile.IsWaterTile() && downTile.Elevation > tile.Elevation && !tile.RiverBottom)
                    {
                        SetTheHeightOfTheTileEdge(tile, MathF.Min( downTile.Elevation, tileMapInitializingDataContainer.landHeight), Directions.Down);
                        SetTheHeightOfTheTileEdge(downTile, tileMapInitializingDataContainer.landHeight, Directions.Up);
                    }
                    if (upTile != null && !upTile.IsWaterTile() && upTile.Elevation > tile.Elevation && !tile.RiverTop)
                    {
                        SetTheHeightOfTheTileEdge(tile, MathF.Min( upTile.Elevation, tileMapInitializingDataContainer.landHeight), Directions.Up);
                        SetTheHeightOfTheTileEdge(upTile, tileMapInitializingDataContainer.landHeight, Directions.Down);
                    }
                }
                else
                {
                    if (leftTile != null && leftTile.Elevation > tile.Elevation)
                    {
                        if(leftTile.IsWaterTile()) SetTheHeightOfTheTileEdge(leftTile,  tileMapInitializingDataContainer.landHeight, Directions.Right);

                        SetTheHeightOfTheTileEdge(leftTile, MathF.Max( tile.Elevation, tileMapInitializingDataContainer.landHeight), Directions.Right);
                    }
                    if (rightTile != null && rightTile.Elevation > tile.Elevation)
                    {
                        if(rightTile.IsWaterTile()) SetTheHeightOfTheTileEdge(rightTile,  tileMapInitializingDataContainer.landHeight, Directions.Left);

                        SetTheHeightOfTheTileEdge(rightTile, MathF.Max( tile.Elevation, tileMapInitializingDataContainer.landHeight), Directions.Left);
                    }
                    if (downTile != null && downTile.Elevation > tile.Elevation)
                    {
                        if(downTile.IsWaterTile()) SetTheHeightOfTheTileEdge(downTile,  tileMapInitializingDataContainer.landHeight, Directions.Up);

                        SetTheHeightOfTheTileEdge(downTile, MathF.Max( tile.Elevation, tileMapInitializingDataContainer.landHeight), Directions.Up);
                    }
                    if (upTile != null && upTile.Elevation > tile.Elevation)
                    {
                        if(upTile.IsWaterTile()) SetTheHeightOfTheTileEdge(upTile,  tileMapInitializingDataContainer.landHeight, Directions.Down);

                        SetTheHeightOfTheTileEdge(upTile, MathF.Max( tile.Elevation, tileMapInitializingDataContainer.landHeight), Directions.Down);
                    }
                }
                
            }  
        }

        private void SetTheHeightOfTheTileEdge(Tile tile, float elevation, Directions directions)
        {
            float edgeOffset = 0.2f;
            if (directions == Directions.Right)
            {
                foreach (var tileVertex in tile.vertices)
                {
                    if (tileVertex.x >= tile.XPosition + 1 - edgeOffset)
                    {
                        tileVertex.y = tile.IsWaterTile()? elevation : Mathf.Min(tileVertex.y, elevation);
                    }
                }
            }else if (directions == Directions.Left)
            {
                foreach (var tileVertex in tile.vertices)
                {
                    if (tileVertex.x <= tile.XPosition + edgeOffset)
                    {
                        tileVertex.y = tile.IsWaterTile()? elevation : Mathf.Min(tileVertex.y, elevation);
                    }
                }
            }else if (directions == Directions.Up)
            {
                foreach (var tileVertex in tile.vertices)
                {
                    if (tileVertex.z >= tile.YPosition + 1 - edgeOffset)
                    {
                        tileVertex.y = tile.IsWaterTile()? elevation : Mathf.Min(tileVertex.y, elevation);
                    }
                }
            }else if (directions == Directions.Down)
            {
                foreach (var tileVertex in tile.vertices)
                {
                    if (tileVertex.z <= tile.YPosition + edgeOffset)
                    {
                        tileVertex.y = tile.IsWaterTile()? elevation : Mathf.Min(tileVertex.y, elevation);
                    }
                }
            }
        }

        #endregion
        #region ManipulatingMaps
        float GetPerlinNoiseValue(int x, int y)
        {
            float noiseValue = 0f;
            var noiseScale = 20f;
            float xOffset = Random.Range(0, 99999f);
            float zOffset = Random.Range(0, 99999f);
            float xCoOrdinate = ( float) x / tileMapInitializingDataContainer.gridSizeX * noiseScale + xOffset;
            float yCoOrdinate = ( float) y / tileMapInitializingDataContainer.gridSizeY * noiseScale + zOffset;
            noiseValue = Mathf.PerlinNoise(xCoOrdinate, yCoOrdinate);
            // Debug.Log("Noise value " + noiseValue);
            return noiseValue;
        }

        private void SmoothenOutEdges()
        {
            foreach (var edgeTile in edgeTiles)
            {
                var perlinNoiseValue = GetPerlinNoiseValue(edgeTile.XPosition, edgeTile.YPosition);
                if ( perlinNoiseValue > 0.7)
                {
                
                    edgeTile.Terrain = TerrainType.ocean.ToString();
                }
                else
                {
                    var neighborTiles = GetNeighborTiles(edgeTile);
                    var neighborOceanTiles = new List<Tile>();
                    foreach (var neighborTile in neighborTiles)
                    {
                        if (neighborTile.Terrain.Contains(TerrainType.ocean.ToString())) neighborOceanTiles.Add(neighborTile);
                    }

                    if (neighborOceanTiles.Count > 0)
                    {
                        var tile = neighborOceanTiles[Random.Range(0, neighborOceanTiles.Count)];
                        tile.Terrain = TerrainType.grassland.ToString();
                        tile.ElevationType = ElevationType.flat.ToString();
                    }
                }
            }
        }
    
        private List<Tile> GetNeighborTiles(Tile tile)
        {
            var tileFinder = new TileFinder(listOfTiles);
            var neighborTilesCoordinates = tile.GetListOfNeighbourTileCoordinates();
            var listOfNeighborTiles = new List<Tile>();
            foreach (var neighborTilesCoordinate in neighborTilesCoordinates)
            {
                var neighbourTile = tileFinder.GetTileByCoordinates(neighborTilesCoordinate);
                if (neighbourTile != null) listOfNeighborTiles.Add(neighbourTile);
            }

            return listOfNeighborTiles;
        }

        public void GetEdgeTiles()
        {            
            var tileFinder = new TileFinder(listOfTiles);
            var grassLandTiles = tileFinder.GetTilesByTerrain(TerrainType.grassland.ToString());
            foreach (var tile in grassLandTiles)
            {
                var listOfNeighborTiles = GetNeighborTiles(tile);

                foreach (var neighborTile in listOfNeighborTiles)
                {
                    if (neighborTile.Terrain.Contains(TerrainType.ocean.ToString()))
                    {
                        edgeTiles.Add(tile); 
                        break;
                    }
                }
            }
        }
        private void GenerateTileHeight(string tileTerrainElevationOrFeature, float height)
        {
            foreach (var tile in listOfTiles)
            {
                if (tile.Terrain == tileTerrainElevationOrFeature)
                {
                    foreach (var vertex in tile.vertices)
                    {
                        vertex.y = height;
                    }
                    tile.Elevation = height;
                }
                else if (tile.ElevationType == tileTerrainElevationOrFeature )
                {
                    foreach (var vertex in tile.vertices)
                    {
                        vertex.y = height;
                    }
                    tile.Elevation = height;
                }
                else if (tile.Feature == tileTerrainElevationOrFeature )
                {
                    foreach (var vertex in tile.vertices)
                    {
                        vertex.y = height;
                    }
                    tile.Elevation = height;
                }

            
            }
        
        
        }
        private void AssignHeightToLand(float height)
        {
            foreach (var tile in listOfTiles)
            {
                if (tile.Terrain == TerrainType.ocean.ToString() || tile.Terrain == TerrainType.coast.ToString() || tile.Feature == FeatureType.lake.ToString() || tile.ElevationType == ElevationType.mountain.ToString() || tile.ElevationType == ElevationType.hill.ToString())
                {
                    continue;
                }

                tile.Elevation = height;
                foreach (var vertex in tile.vertices)
                {
                    vertex.y = height;
                }


            }
        
        
        }

        #endregion
    
        #region Mesh
    

        #endregion

        #region Slots

    

        private void AssignTileSlotsVertices()
        {
            List<Vertex> tileSlotCoordinatesList = new List<Vertex>();
        
            for (int i = 0, z = 0; z <= tileMapInitializingDataContainer.gridSizeY* tileMapInitializingDataContainer.numberOfSlots; z++)
            {
                for (int x = 0; x <= tileMapInitializingDataContainer.gridSizeX* tileMapInitializingDataContainer.numberOfSlots; x++)
                {
                    //float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) ;
                    float y = 0;
                    //vertices[i] = new Vertex(x, y, z);
                    var divisor = 1.0f / ((float)tileMapInitializingDataContainer.numberOfSlots);
                    var tempSlotPos = new Vertex(x * divisor, y, z*divisor);
                    tileSlotCoordinatesList.Add(tempSlotPos);
                    i++;
                }
            }
        
            for (int i = 0; i < listOfTiles.Count; i++)
            {
                //listOfBigTiles[i].vertices = new Vertex[25];
                List<Vertex> tempListOfVertices = new List<Vertex>();
                for (int j = 0; j <  (tileMapInitializingDataContainer.numberOfSlots+1)*(tileMapInitializingDataContainer.numberOfSlots+1) ; j++)
                {
                
                    // var tempVertex = 
                    //     (vertices[(((j/5) * (4*tileMapInitializingDataContainer.gridSizeX+1)) + (j%5)+ i*4 + ((4*4+((tileMapInitializingDataContainer.gridSizeX-1)*12)) * (i/tileMapInitializingDataContainer.gridSizeX)))]);
                
                    var tempVertex = 
                        (tileSlotCoordinatesList[(((j/(tileMapInitializingDataContainer.numberOfSlots+1)) * // x/(n+1) *
                                                   (tileMapInitializingDataContainer.numberOfSlots*tileMapInitializingDataContainer.gridSizeX+1)) + // n*GridSizeX+1 +
                                                  (j%(tileMapInitializingDataContainer.numberOfSlots+1))+ // x%(n+1) +
                                                  i*tileMapInitializingDataContainer.numberOfSlots + // y*n +
                                                  ((tileMapInitializingDataContainer.numberOfSlots*tileMapInitializingDataContainer.numberOfSlots+((tileMapInitializingDataContainer.gridSizeX-1) * // (n*n+(GridSizeX-1)) *
                                                       ((tileMapInitializingDataContainer.numberOfSlots-1)*(tileMapInitializingDataContainer.numberOfSlots-2)+((tileMapInitializingDataContainer.numberOfSlots-1)*2)))) * // (n-1)*(n-2)+((n-1)*2) *
                                                   (i/tileMapInitializingDataContainer.gridSizeX)))]); // y/GridSizeX
                    tempListOfVertices.Add(tempVertex);
                 
                }

                listOfTiles[i].SlotCoordinates = tempListOfVertices;
            
                listOfTiles[i].BottomLeftBuildingSlotlotCoordinates = GetBuildingSlots(listOfTiles[i], 4, 4, 4,
                    tileMapInitializingDataContainer.numberOfSlots);
                listOfTiles[i].BottomRightBuildingSlotlotCoordinates = GetBuildingSlots(listOfTiles[i], 4, 9, 4,
                    tileMapInitializingDataContainer.numberOfSlots);
                listOfTiles[i].UpperLeftBuildingSlotlotCoordinates = GetBuildingSlots(listOfTiles[i], 4, 4, 9,
                    tileMapInitializingDataContainer.numberOfSlots);
                listOfTiles[i].UpperRightBuildingSlotlotCoordinates = GetBuildingSlots(listOfTiles[i], 4, 9, 9,
                    tileMapInitializingDataContainer.numberOfSlots);
            
            }
        
        
        }

        List<Vertex> GetBuildingSlots( Tile tile, int numberOfVertexInAnAxis, int startXOffset, int startYOffset, int totalNumberOfSlots)
        {
            var vertexList = new List<Vertex>();
            for (float i = 0; i < numberOfVertexInAnAxis; i++)
            {
                for (float j = 0; j < numberOfVertexInAnAxis; j++)
                {
                    var vertex = new Vertex(tile.XPosition + (startXOffset + i) / totalNumberOfSlots, 0,
                        tile.YPosition + (startYOffset + j) / totalNumberOfSlots);
                    vertexList.Add(vertex);
                
                }
            }
            return vertexList;
        }
        #endregion
    
        #region WorksWithChunks
    
        private void InitializeTileVerticesAndTileData()
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < tileMapInitializingDataContainer.gridSizeX*tileMapInitializingDataContainer.gridSizeY; i++)
            {
            
                var tile = new Tile();
                tile.XPosition = x;
                tile.YPosition = y;
                tile.TileCoordinates = new TileCoordinates
                {
                    X = x,
                    Y = y
                };
                tile.Terrain = TerrainType.ocean.ToString();
                tile.ElevationType = ElevationType.flat.ToString();
                tile.Feature = null;
                tile.Id = NewIdGenerator.GenerateNewId();
                tile.Subcontinent = subcontinent.subcontinentName;
                tile.SubcontinentOffset = subcontinent.subcontinentPosition;
                x++;
                if (x==tileMapInitializingDataContainer.gridSizeX)
                {
                    y++;
                    x = 0;
                }

                listOfTiles.Add(tile);
            }

            var subcontinentTiles = new SubcontinentTiles()
                {Id = NewIdGenerator.GenerateNewId(), SubcontinentName = subcontinent.subcontinentName, Tiles = listOfTiles };
            _listOfAllSubcontinentTilesList.Add(subcontinentTiles);
            if (subcontinent.subcontinentName.Contains("Middle East Subcontinent"))
                _activeSubcontinentTilesId = subcontinentTiles.Id;
            CreateChunkTiles();

            for (int i = 0; i < listOfTiles.Count; i++)
            {
                //listOfBigTiles[i].vertices = new Vertex[25];
                List<Vertex> tempListOfVertices = new List<Vertex>();
                for (int j = 0; j <  (tileMapInitializingDataContainer.meshDivisions+1)*(tileMapInitializingDataContainer.meshDivisions+1) ; j++)
                {
                
                    // var tempVertex = 
                    //     (vertices[(((j/5) * (4*tileMapInitializingDataContainer.gridSizeX+1)) + (j%5)+ i*4 + ((4*4+((tileMapInitializingDataContainer.gridSizeX-1)*12)) * (i/tileMapInitializingDataContainer.gridSizeX)))]);
                
                    var tempVertex = 
                        (vertices[(((j/(tileMapInitializingDataContainer.meshDivisions+1)) * // x/(n+1) *
                                    (tileMapInitializingDataContainer.meshDivisions*tileMapInitializingDataContainer.gridSizeX+1)) + // n*GridSizeX+1 +
                                   (j%(tileMapInitializingDataContainer.meshDivisions+1))+ // x%(n+1) +
                                   i*tileMapInitializingDataContainer.meshDivisions + // y*n +
                                   ((tileMapInitializingDataContainer.meshDivisions*tileMapInitializingDataContainer.meshDivisions+((tileMapInitializingDataContainer.gridSizeX-1) * // (n*n+(GridSizeX-1)) *
                                        ((tileMapInitializingDataContainer.meshDivisions-1)*(tileMapInitializingDataContainer.meshDivisions-2)+((tileMapInitializingDataContainer.meshDivisions-1)*2)))) * // (n-1)*(n-2)+((n-1)*2) *
                                    (i/tileMapInitializingDataContainer.gridSizeX)))]); // y/GridSizeX
                    tempListOfVertices.Add(tempVertex);
                 
                }

                listOfTiles[i].vertices = tempListOfVertices.ToArray();
                AssignEdgeVertices(listOfTiles[i]);
            }

            // listOfBigTiles.Last().vertices[12].y = 5;
            //todo for height or hill
        
            ConvertChunkToTerrain(subcontinent.landChunkList, TerrainType.grassland);
            ConvertChunkElevation(subcontinent.landChunkList, ElevationType.flat);
        

            SaveInformation();
        }
        private void AssignEdgeVertices(Tile tile)
        {
            var tempVertices = new List<Vertex>();
            var n = tileMapInitializingDataContainer.meshDivisions;
            //left
            for (int i = 0; i <= tileMapInitializingDataContainer.meshDivisions; i++)
            {
                // {(n+1)*i}

                var vertex = tile.vertices[(n + 1) * i];
                tempVertices.Add(vertex);
            }

            tile.LeftVertices = tempVertices.ToArray();
            tempVertices.Clear();
            //Right
            for (int i = 0; i <= tileMapInitializingDataContainer.meshDivisions; i++)
            {
                // n+{(n+1)*i}
                var vertex = tile.vertices[n+((n+1)*i)];
                tempVertices.Add(vertex);
            }
            tile.RightVertices = tempVertices.ToArray();
            tempVertices.Clear();
            //Top
            for (int i = 0; i <= tileMapInitializingDataContainer.meshDivisions; i++)
            {
                // {n*(n+1)}+i
                var vertex = tile.vertices[(n*(n+1))+i];
                tempVertices.Add(vertex);
            }
            tile.TopVertices = tempVertices.ToArray();
            tempVertices.Clear();
            //Bottom
            for (int i = 0; i <= tileMapInitializingDataContainer.meshDivisions; i++)
            {
                // i
                var vertex = tile.vertices[i];
                tempVertices.Add(vertex);
            }
            tile.BottomVertices = tempVertices.ToArray();
            tempVertices.Clear();
        }
        private void AllocateChunkFeaturesAccordingToTemperatureAndTerrain(Position2 chunkAddress, int temperatureStrength, bool dryRegion)
        {
            if(chunkAddress.x < 0 || chunkAddress.x > tileMapInitializingDataContainer.gridSizeX-1 
                                  || chunkAddress.y < 0 || chunkAddress.y > tileMapInitializingDataContainer.gridSizeY-1) return;
        
            var tileChunk =
                _chunkTiles.FirstOrDefault(tile =>
                    Math.Abs(tile.x - chunkAddress.x) < 0.3 && Math.Abs(tile.y - chunkAddress.y) < 0.3);
        
        
            if (tileChunk != null)
            {
                _featureAllocation.AllocateFeatures(tileChunk, dryRegion, temperatureStrength);
            }
        
        }
        private List<Tile> GetChunkTiles(List<Position2> chunkAddressList)
        {
            var allChunkTiles = new List<Tile>();
            foreach (var chunkAddress in chunkAddressList)
            {
                var chunkTile =
                    _chunkTiles.FirstOrDefault(tile =>
                        Math.Abs(tile.x - chunkAddress.x) < 0.3 && Math.Abs(tile.y - chunkAddress.y) < 0.3);


                if (chunkTile != null)
                {
                    foreach (var tile in chunkTile.tiles)
                    {
                        allChunkTiles.Add(tile);
                    }
                }
            }

            return allChunkTiles;
        }
        private void ConvertChunkToTerrain(List<Position2> chunkAddressList, TerrainType terrainType)
        {
            foreach (var chunkAddress in chunkAddressList)
            {
                var chunkTile =
                    _chunkTiles.FirstOrDefault(tile =>
                        Math.Abs(tile.x - chunkAddress.x) < 0.3 && Math.Abs(tile.y - chunkAddress.y) < 0.3);


                if (chunkTile != null)
                {
                    foreach (var tile in chunkTile.tiles)
                    {
                        for (int i = 0; i < tile.vertices.Length; i++)
                        {
                            // float xCoord = (float)Random.Range(0.0f, 1.0f)  * noiseScale;
                            // float zCoord = (float)Random.Range(0.0f, 1.0f)  * noiseScale;
                            // float noiseValue = Mathf.PerlinNoise(xCoord, zCoord);
                            // float height = noiseValue * hillMaxHeight;
                            //var tempVertex = new Vertex(listOfBigTiles[k].vertices[i].x, height, listOfBigTiles[k].vertices[i].z);
                            //tile.vertices[i].y = landHeight;
                            tile.Terrain = terrainType.ToString();
                            // if(terrainType == TerrainType.grassland) tile.
                        }
                    }
                }
            }
        }
        private void ConvertChunkElevation(List<Position2> chunkAddressList, ElevationType elevation)
        {
            foreach (var chunkAddress in chunkAddressList)
            {
                var chunkTile =
                    _chunkTiles.FirstOrDefault(tile =>
                        Math.Abs(tile.x - chunkAddress.x) < 0.3 && Math.Abs(tile.y - chunkAddress.y) < 0.3);


                if (chunkTile != null)
                {
                    foreach (var tile in chunkTile.tiles)
                    {
                        for (int i = 0; i < tile.vertices.Length; i++)
                        {
                            // float xCoord = (float)Random.Range(0.0f, 1.0f)  * noiseScale;
                            // float zCoord = (float)Random.Range(0.0f, 1.0f)  * noiseScale;
                            // float noiseValue = Mathf.PerlinNoise(xCoord, zCoord);
                            // float height = noiseValue * hillMaxHeight;
                            //var tempVertex = new Vertex(listOfBigTiles[k].vertices[i].x, height, listOfBigTiles[k].vertices[i].z);
                            //tile.vertices[i].y = landHeight;
                            tile.ElevationType = elevation.ToString();
                        }
                    }
                }
            }
        }
        private void CreateChunkTiles()
        {
            int x;
            int y;
            _chunkTiles = new List<ChunkTile>();

            x = 0;
            y = 0;

            for (int i = 0; i < (tileMapInitializingDataContainer.gridSizeX/tileMapInitializingDataContainer.chunkTileSize) * (tileMapInitializingDataContainer.gridSizeY/tileMapInitializingDataContainer.chunkTileSize); i++)
            {
                List<Tile> tempTiles = new List<Tile>();
                tempTiles.Clear();
                // var zero = listOfTiles.FirstOrDefault(tile => tile.XPosition == x * 2 && tile.YPosition == y * 2);
                // var one = listOfTiles.FirstOrDefault(tile => tile.XPosition == x * 2  && tile.YPosition == y * 2 + 1);
                // var two = listOfTiles.FirstOrDefault(tile => tile.XPosition == x * 2 + 1 && tile.YPosition == y * 2  );
                // var three = listOfTiles.FirstOrDefault(tile => tile.XPosition == x * 2 + 1 && tile.YPosition == y * 2 + 1);
                // tempTiles.Add(zero);
                // tempTiles.Add(one);
                // tempTiles.Add(two);
                // tempTiles.Add(three);
            
                for (int row = 0; row < tileMapInitializingDataContainer.chunkTileSize; row++)
                {
                    for (int column = 0; column < tileMapInitializingDataContainer.chunkTileSize; column++)
                    {

                        var gg = listOfTiles.FirstOrDefault(tile => tile.XPosition ==  x * tileMapInitializingDataContainer.chunkTileSize + column && tile.YPosition == y * tileMapInitializingDataContainer.chunkTileSize + row);
                        tempTiles.Add(gg);
                    
                    }
                }
            
                var tempChunkTile = new ChunkTile(tempTiles.ToArray(), x, y);
                _chunkTiles.Add(tempChunkTile);
                x++;
                if (x == tileMapInitializingDataContainer.gridSizeX / tileMapInitializingDataContainer.chunkTileSize)
                {
                    y++;
                    x = 0;
                }
            }
        }
        private List<Position2> GetChunksIncludingTheNeighbourChunks(List<Position2> subcontinentDryChunkList)
        {
            List<Position2> listOfChunksIncludingNeighbourChunks = new List<Position2>();
            foreach (var dryChunk in subcontinentDryChunkList)
            {
                listOfChunksIncludingNeighbourChunks.Add(dryChunk);
            
                listOfChunksIncludingNeighbourChunks.Add(new Position2(dryChunk.x, dryChunk.y + 1));
                listOfChunksIncludingNeighbourChunks.Add(new Position2(dryChunk.x, dryChunk.y - 1));
            
                listOfChunksIncludingNeighbourChunks.Add(new Position2(dryChunk.x+1, dryChunk.y));
                listOfChunksIncludingNeighbourChunks.Add(new Position2(dryChunk.x-1, dryChunk.y));

                listOfChunksIncludingNeighbourChunks.Add(new Position2(dryChunk.x+1, dryChunk.y+1));
                listOfChunksIncludingNeighbourChunks.Add(new Position2(dryChunk.x+1, dryChunk.y-1));
            
                listOfChunksIncludingNeighbourChunks.Add(new Position2(dryChunk.x-1, dryChunk.y+1));
                listOfChunksIncludingNeighbourChunks.Add(new Position2(dryChunk.x-1, dryChunk.y-1));
            
            }

            return listOfChunksIncludingNeighbourChunks;
        }
        #endregion

        #region Saving
        private void SaveInformation()
        {
            // tileMapInitializingDataContainer.saveDataScriptableObject.Save.Tiles.GetById()
            // var allCultures = tileMapInitializingDataContainer.saveDataScriptableObject.Save.AllCultures;
            // if (allCultures is { Count: 0 })
            // {
            //     AllCultures player = new AllCultures()
            //     {
            //         Id = NewIdGenerator.GenerateNewId(),
            //         Culture = new Culture
            //         {
            //             CultureReference = "self",
            //             TileIds = new List<string>() { listOfTiles[Random.Range(0, listOfTiles.Count)].Id }
            //         }
            //     };
            //
            //     tileMapInitializingDataContainer.saveDataScriptableObject.Save.AllCultures.Add(player);
            // }

            tileMapInitializingDataContainer.saveDataScriptableObject.Save.AllSubcontinentTiles = _listOfAllSubcontinentTilesList;
            tileMapInitializingDataContainer.saveDataScriptableObject.Save.ActiveSubcontinentTilesId = _activeSubcontinentTilesId;
        
        }

    
        private async void SaveData()
        {
            SaveData saveData = new SaveData(tileMapInitializingDataContainer.saveDataScriptableObject.Save);
            var json = JsonUtility.ToJson(saveData);

            string savesFolderPath = Path.Combine(Application.persistentDataPath, "Saves");
            Directory.CreateDirectory(savesFolderPath);

            string filePath = Path.Combine(savesFolderPath, "save01.json");

            await using (var writer = new StreamWriter(filePath))
            {
                await writer.WriteAsync(json);
            }

            Debug.Log(Application.persistentDataPath);
        }
        #endregion
    
        #region Rendering
    
    
    

        #endregion

        #region Unused
        private void OnDrawGizmos()
        {
            if (vertices == null)
            {
                return;
            }
            foreach (var t in vertices)
            {
                Vector3 sp = new Vector3(t.x, t.y, t.z);
                Gizmos.DrawSphere(sp, .1f);
            }
        }

        private void GeneratePathFindingGrids()
        {
            _pathfinderGridDataContainer.Grid = new Node[tileMapInitializingDataContainer.gridSizeX, tileMapInitializingDataContainer.gridSizeY];
            Debug.Log($"grid created with length {_pathfinderGridDataContainer.Grid.Length}");

            // var tempNode = new Node();
            foreach (var tile in listOfTiles)
            {   var tempNode = new Node(tile.TileCoordinates, null, float.MaxValue, float.MaxValue, true, tile.PathFindingTerrainModifier, tile.PathFindingFeatureModifier,  tile.PathFindingElevationModifier, tile.PathFindingRoadModifier);
                _pathfinderGridDataContainer.Grid[tile.XPosition, tile.YPosition] = tempNode; //todo change is walkable
        
            }
        }

        private void TestPathFinding(TileCoordinates startTileCoordinates, TileCoordinates targetTileCoordinates)
        {
            TileFinder tileFinder = new TileFinder(listOfTiles);
            var startCoord = new TileCoordinates
            {
                X = 0,
                Y = 0
            };
            var goalCoord = new TileCoordinates
            {
                X = 8,
                Y = 6
            };
            List<TileCoordinates> coordsOfPath = new List<TileCoordinates>();
            coordsOfPath = _aStarPathfinding.FindPath(startTileCoordinates, targetTileCoordinates, _pathfinderGridDataContainer.Grid);
    
            foreach (var coord in coordsOfPath)
            {
                var tile = tileFinder.GetTileByCoordinates(coord);
                // tile.Terrain = 
                tile.HasRiver = true;
            }
        }

        #endregion

    }
}
