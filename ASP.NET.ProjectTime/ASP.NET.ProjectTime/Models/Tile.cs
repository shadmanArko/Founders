using System;
using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class Tile : Base
    {
        public int XPosition;
        public int YPosition;
        public TileCoordinates TileCoordinates;
        public string Subcontinent;
        public Position2 SubcontinentOffset;
        public string Owner;
        public bool Conquered;
        public string Name;
        public string Terrain;
        public float Elevation;
        public string ElevationType;
        public string Feature;
        public NaturalResource NaturalResource;
        public int TemperatureStrength;
        public bool DepletableResource;
        public float InitialResourceQuantity;
        public float CurrentResourceQuantity;
        public bool RiverTop;
        public bool RiverBottom;
        public bool RiverLeft;
        public bool RiverRight;
        public bool IsRiverOrigin;
        public bool IsRiverMouth;
        public bool HasRiver;
        public bool BranchRiver;
        public bool IsIslandTile;
        public bool landTile;
        public List<Pop> Populations;
        public List<string> BuildingIds;
        public float SupplyLimit;
        public bool IsWalkable;
        public float PathFindingTerrainModifier = 1;
        public float PathFindingFeatureModifier;
        public float PathFindingElevationModifier;
        public float PathFindingRoadModifier;
        
        public Vertex[] vertices;
        public Vertex[] TopVertices;
        public Vertex[] BottomVertices;
        public Vertex[] LeftVertices;
        public Vertex[] RightVertices;
        public List<Vertex> SlotCoordinates;
        public List<Vertex> BottomLeftBuildingSlotlotCoordinates;
        public List<Vertex> BottomRightBuildingSlotlotCoordinates;
        public List<Vertex> UpperLeftBuildingSlotlotCoordinates;
        public List<Vertex> UpperRightBuildingSlotlotCoordinates;


        public Tile()
        {
            BuildingIds = new List<string>();
        }
        
    }
}