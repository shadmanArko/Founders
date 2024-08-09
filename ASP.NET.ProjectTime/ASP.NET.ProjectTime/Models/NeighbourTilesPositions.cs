namespace ASP.NET.ProjectTime.Models
{
    [System.Serializable]
    public class NeighbourTilesPositions
    {
        public TileCoordinates Top;
        public TileCoordinates TopLeft;
        public TileCoordinates TopRight;
        public TileCoordinates Left;
        public TileCoordinates Right;
        public TileCoordinates Bottom;
        public TileCoordinates BottomLeft;
        public TileCoordinates BottomRight;

        public NeighbourTilesPositions(int mainTileXPos, int mainTileYPos)
        {
            Top = new TileCoordinates();
            TopLeft = new TileCoordinates();
            TopRight = new TileCoordinates();
            Left = new TileCoordinates();
            Right = new TileCoordinates();
            Bottom = new TileCoordinates();
            BottomLeft = new TileCoordinates();
            BottomRight = new TileCoordinates();
            

            Top.X = mainTileXPos + 0;
            Top.Y = mainTileYPos + 1;
            
            TopLeft.X = mainTileXPos - 1;
            TopLeft.Y = mainTileYPos + 1;
            
            TopRight.X = mainTileXPos + 1;
            TopRight.Y = mainTileYPos + 1;
            
            Left.X = mainTileXPos - 1;
            Left.Y = mainTileYPos + 0;
            
            Right.X = mainTileXPos + 1;
            Right.Y = mainTileYPos + 0;
            
            Bottom.X = mainTileXPos + 0;
            Bottom.Y = mainTileYPos - 1;
            
            BottomLeft.X = mainTileXPos - 1;
            BottomLeft.Y = mainTileYPos - 1;
            
            BottomRight.X = mainTileXPos + 1;
            BottomRight.Y = mainTileYPos - 1;
        }
    }
}