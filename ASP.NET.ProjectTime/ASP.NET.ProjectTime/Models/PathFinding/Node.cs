namespace ASP.NET.ProjectTime.Models.PathFinding
{
    [System.Serializable]
    public class Node
    {
        
        //todo Need to connect IsWalkable
        //todo Need to add more values to modify HCost
        public TileCoordinates TileCoordinates;
        public Node Parent;
        public float GCost;
        public float HCost;
        public bool IsWalkable;
        public int MovecostFromPreviousTile;
        public float PathFindingTerrainModifier;
        public float PathFindingFeatureModifier;
        public float PathFindingElevationModifier;
        public float PathFindingRoadModifier;

        public float FCost { get { return GCost + HCost + (PathFindingTerrainModifier + PathFindingFeatureModifier + PathFindingRoadModifier + PathFindingElevationModifier); } }

        public float PathfindingModifiers => (PathFindingTerrainModifier + PathFindingFeatureModifier +
                                              PathFindingRoadModifier + PathFindingElevationModifier);
        
        public Node(TileCoordinates tileCoordinates, Node parent, float gCost, float hCost, bool isWalkable, float pathFindingTerrainModifier, float pathFindingFeatureModifier, float pathFindingElevationModifier, float pathFindingRoadModifier)
        {
            TileCoordinates = tileCoordinates;
            Parent = parent;
            GCost = gCost;
            HCost = hCost;
            IsWalkable = isWalkable;
            MovecostFromPreviousTile = 0;
            PathFindingTerrainModifier = pathFindingTerrainModifier;
            PathFindingFeatureModifier = pathFindingFeatureModifier;
            PathFindingElevationModifier = pathFindingElevationModifier;
            PathFindingRoadModifier = pathFindingRoadModifier;
        }
    }
}