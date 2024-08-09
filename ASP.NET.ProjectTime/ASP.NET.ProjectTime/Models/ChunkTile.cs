namespace ASP.NET.ProjectTime.Models
{
    [System.Serializable]
    public class ChunkTile
    {
        public Tile[] tiles;
        public int x;
        public int y;

        public ChunkTile(Tile[] tiles, int x, int y)
        {
            this.tiles = tiles;
            this.x = x;
            this.y = y;
        }
    }
}