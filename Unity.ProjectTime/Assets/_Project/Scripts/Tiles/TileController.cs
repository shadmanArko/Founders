using ASP.NET.ProjectTime.Models;

namespace _Project.Scripts.Tiles
{
    public class TileController: ITileController
    {
        private readonly TileView _tileView;
        private readonly string _tileId;
        public TileController(TileView tileView, string tileId)
        {
            _tileView = tileView;
            _tileId = tileId;
        }


        public void Config()
        {
            
        }
        
        public string Id => _tileId;

    }
}