using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Tiles
{
    public class TileInfo : MonoBehaviour
    {
        public string tileId;
        public TileCoordinates tileCoordinates;
        public Tile tile;
        [SerializeField] private BoxCollider tileBoxCollider;

        public void Init(Tile tile)
        {
            tileId = tile.Id;
            this.tile = tile;
            tileCoordinates = tile.TileCoordinates;
            var tempCenter = new Vector3(tileCoordinates.X + 0.5f, tile.Elevation/2, tileCoordinates.Y + 0.5f);
            tileBoxCollider.center = tempCenter;
            var size = tileBoxCollider.size;
            size = new Vector3(size.x, tile.Elevation, size.z);
            tileBoxCollider.size = size;
        }
    }
}
