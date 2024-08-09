using System;
using _Project.Scripts.Game_Actions;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.Static_Classes;
using _Project.Scripts.Tiles;
using _Project.Scripts.UI.UiController;

namespace _Project.Scripts.UI.GameScene
{
    public class BottomPanelCanvasController : IDisposable, IUiController
    {
        private readonly BottomPanelCanvasView _bottomPanelCanvasView;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly IDisposable _bottomPanelTileViewerPanelDisposable;

        public BottomPanelCanvasController(BottomPanelCanvasView bottomPanelCanvasView, SaveDataScriptableObject saveDataScriptableObject, IDisposable bottomPanelTileViewerPanelDisposable)
        {
            _bottomPanelCanvasView = bottomPanelCanvasView;
            _saveDataScriptableObject = saveDataScriptableObject;
            _bottomPanelTileViewerPanelDisposable = bottomPanelTileViewerPanelDisposable;
            //TileActions.onSelectedTile += ActivateTileViewerPanel;
        }
        
        

        private void ActivateTileViewerPanel(TileInfo tileInfo)
        {
            if(_bottomPanelCanvasView.tileViewerPanel == null) return;
            _bottomPanelCanvasView.tileViewerPanel.SetActive(true);
            var tileViewer = _bottomPanelCanvasView.tileInfoPanelView;
            tileViewer.tileNameText.text = tileInfo.tile.Name;

            var tile = tileInfo.tile;
            if (tile.NaturalResource == null)
            {
                // tileViewer.resourceImage.gameObject.SetActive(false);
                return;
            }
            tileViewer.resourceImage.gameObject.SetActive(true);
            tileViewer.resourceImage.sprite = tile.NaturalResource.GetResourceIcon();
        }

        public void Dispose()
        {
            //TileActions.onSelectedTile -= ActivateTileViewerPanel;
            _bottomPanelTileViewerPanelDisposable?.Dispose();
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