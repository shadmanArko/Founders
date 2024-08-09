using Zenject;

namespace _Project.Scripts.zzz_Testing.Tilemap_Testing__Depricated_
{
    public class TileMapController
    {
        private readonly TileMapGenerator _tileMapGenerator;

        public TileMapController(TileMapGenerator tileMapGenerator)
        {
            _tileMapGenerator = tileMapGenerator;
        }

        [Inject]
        public void Initialize()
        {
            _tileMapGenerator.GenerateContinentMap();
        }
    }
}