using ASP.NET.ProjectTime.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.zzz_Testing.Tilemap_Testing__Depricated_
{
    public class DataTesing : MonoBehaviour
    {
        public int x;
        public int y;
        
        private  TileFinder _tileFinder;

        [Inject]
        public void Initialize(TileFinder tileFinder)
        {
            _tileFinder = tileFinder;
        }
        

        [ContextMenu("Get File Name")]
        public void GetTileName()
        {
            Debug.Log(_tileFinder.GetTileByXAndYPosition(x, y).Name);
            
        }
    }
}