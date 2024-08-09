using _Project.Scripts.Game_Actions;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Tiles
{
    public class OnClickTile : MonoBehaviour
    {
        private GameObject _tileSelectionShaderPrefab;
        public string tileId;
        public Tile tile;
        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                if (_tileSelectionShaderPrefab == null) _tileSelectionShaderPrefab = GameObject.Find("TileSelectionShaderPrefab");
                Debug.Log(gameObject.name);
                var tempPos = GetComponent<BoxCollider>().center;
                _tileSelectionShaderPrefab.transform.position = new Vector3(tile.SubcontinentOffset.x + tempPos.x, (tempPos.y * 2) - 0.01f, tile.SubcontinentOffset.y + tempPos.z);
                //Actions.OnClickTileInfo?.Invoke(tileInfo);
                TileActions.OnClickShowTileInfo?.Invoke(tileId);
            }

            if (Input.GetMouseButtonDown(1)) 
            {
                TileActions.onSelectedTile?.Invoke(tileId);
            }
        }
    }
}