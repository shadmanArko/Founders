using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.HelperScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.UI.UiController;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.UI.MiniMap
{
    public class MiniMapController : IDisposable, IUiController
    {
        private const int NumberOfPixelsToShowTileWidth = 10;
        private const int NumberOfPixelsToShowTileHeight = 10; 
        
        private readonly MiniMapView _miniMapView;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly TileMapInitializingDataContainer _tileMapInitializingDataContainer;
        private readonly IDisposable _miniMapDisposable;
        
        public MiniMapController(MiniMapView miniMapView, SaveDataScriptableObject saveDataScriptableObject,TileMapInitializingDataContainer tileMapInitializingDataContainer , IDisposable miniMapDisposable)
        {
            _miniMapView = miniMapView;
            _saveDataScriptableObject = saveDataScriptableObject;
            _tileMapInitializingDataContainer = tileMapInitializingDataContainer;
            _miniMapDisposable = miniMapDisposable;
        }
        
        public void GenerateMiniMap()
        {
            var tiles = GetAllTheTilesOfActiveSubcontinent();
            var miniMapTexture = CreateNewTextureForMiniMapAccordingToGridSize();
            AssignTheColorPixelByPixelToTheMiniMapTextureAccordingToTheTiles(tiles, miniMapTexture);
            AssignTheMiniMapTextureToTheMiniMapImageSprite(miniMapTexture);
        }
        
        private List<Tile> GetAllTheTilesOfActiveSubcontinent()
        {
            var activeSubcontinentId = _saveDataScriptableObject.Save.ActiveSubcontinentTilesId;
            var tiles = _saveDataScriptableObject.Save.AllSubcontinentTiles
                .GetById(subcontinent => subcontinent.Id, activeSubcontinentId).Tiles;
            return tiles;
        }
        
        private Texture2D CreateNewTextureForMiniMapAccordingToGridSize()
        {
            int widthOfTheTexture = _tileMapInitializingDataContainer.gridSizeX * NumberOfPixelsToShowTileWidth;
            int heightOfTheTexture = _tileMapInitializingDataContainer.gridSizeY * NumberOfPixelsToShowTileHeight;
            Texture2D texture = new Texture2D(widthOfTheTexture, heightOfTheTexture);
            return texture;
        }
        
        private void AssignTheColorPixelByPixelToTheMiniMapTextureAccordingToTheTiles(List<Tile> tiles, Texture2D miniMapTexture)
        {
            for (int y = 0; y < _tileMapInitializingDataContainer.gridSizeY; y++)
            {
                for (int x = 0; x < _tileMapInitializingDataContainer.gridSizeX; x++)
                {
                    int startX = x * NumberOfPixelsToShowTileWidth;
                    int startY = y * NumberOfPixelsToShowTileHeight;

                    var tile = tiles.FirstOrDefault(tempTile => tempTile.XPosition == x && tempTile.YPosition == y);
                    var unityColor = tile.GetColor(_tileMapInitializingDataContainer);

                    var r = Mathf.RoundToInt(unityColor.r * 255);
                    var g = Mathf.RoundToInt(unityColor.g * 255);
                    var b = Mathf.RoundToInt(unityColor.b * 255);
                    var a = Mathf.RoundToInt(unityColor.a * 255);

                    var color = new Color32((byte)r, (byte)g, (byte)b, (byte)a);


                    for (int j = 0; j < NumberOfPixelsToShowTileHeight; j++)
                    {
                        for (int i = 0; i < NumberOfPixelsToShowTileWidth; i++)
                        {
                            int texX = startX + i;
                            int texY = startY + j;

                            miniMapTexture.SetPixel(texX, texY, color);
                        }
                    }
                }
            }
            miniMapTexture.Apply();
        }
        
        private void AssignTheMiniMapTextureToTheMiniMapImageSprite(Texture2D miniMapTexture)
        {
            var pivot = AdjustThePivotOfTheTexture();
            var rect = AdjustTheRectOfTheTextureAccordingToTheWidthAndHeightOfTheTexture(miniMapTexture);
            _miniMapView.miniMapImage.sprite = Sprite.Create(miniMapTexture, rect, pivot);
        }
        
        private Vector2 AdjustThePivotOfTheTexture()
        {
            Vector2 newPivot = Vector2.one * 0.5f;
            return newPivot;
        }
        
        private Rect AdjustTheRectOfTheTextureAccordingToTheWidthAndHeightOfTheTexture(Texture2D texture2D)
        {
            Rect newRect = new Rect(0, 0, texture2D.width, texture2D.height);
            return newRect;
        }
        
        public void Dispose()
        {
            _miniMapDisposable?.Dispose();
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}