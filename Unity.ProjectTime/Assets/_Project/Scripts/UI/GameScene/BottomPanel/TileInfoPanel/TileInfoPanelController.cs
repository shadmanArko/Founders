using System;
using _Project.Scripts.Buildings;
using _Project.Scripts.Game_Actions;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.Static_Classes;
using _Project.Scripts.UI.UiController;
using ASP.NET.ProjectTime._1._Repositories;
using ASP.NET.ProjectTime.Models;

namespace _Project.Scripts.UI.GameScene.BottomPanel.TileInfoPanel
{
    public class TileInfoPanelController :IDisposable, IUiController
    {
        private readonly TileInfoPanelView _tileInfoPanelView;
        private readonly IDisposable _tileInfoPanelViewDisposable;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;

        public TileInfoPanelController(TileInfoPanelView tileInfoPanelView, SaveDataScriptableObject saveDataScriptableObject, IDisposable tileInfoPanelViewDisposable)
        {
            _tileInfoPanelView = tileInfoPanelView;
            TileActions.OnClickShowTileInfo += ToggleButtonBasedOnActiveBuildingSlots;
            _saveDataScriptableObject = saveDataScriptableObject;
            _tileInfoPanelViewDisposable = tileInfoPanelViewDisposable;
        }

        private void ToggleButtonBasedOnActiveBuildingSlots(string tileId)
        {
            var tile = _saveDataScriptableObject.Save.AllSubcontinentTiles.GetById(sub => sub.Id, _saveDataScriptableObject.Save.ActiveSubcontinentTilesId).Tiles.GetById(tile => tile.Id, tileId);
            DeactivateAllButtons();
            
            _tileInfoPanelView.tileNameText.text = tile.Name;
            if (tile.NaturalResource != null)
            {
                var resourceImage = _tileInfoPanelView.resourceImage;
                resourceImage.gameObject.SetActive(true);
                resourceImage.sprite = tile.NaturalResource.GetResourceIcon();
                _tileInfoPanelView.resourceDetails.text = tile.NaturalResource.Name;
            }
            
            var buildingIds = tile.BuildingIds;
            if (buildingIds.Count <= 0)
            {
                ActivateDefaultButtons();
                return;
            }
            
            foreach (var buildingId in buildingIds)
            {
                var buildingFromSave =
                    _saveDataScriptableObject.Save.Buildings.GetById(building => building.Id, buildingId);
                if(buildingFromSave.BuildingSize == BuildingSize.ExtraLarge)
                    ActivateExtraLargeBuildingButton();
                
                if(buildingFromSave.BuildingSize == BuildingSize.Large)
                    ActivateLargeBuildingButton();

                if (buildingFromSave.BuildingSize == BuildingSize.Medium)
                {
                    if(buildingFromSave.BuildingSlot == BuildingSlot.Medium_Top)
                        ActivateMediumTopBuildingButtons();
                    else
                        ActivateMediumBottomBuildingButtons();
                }

                if (buildingFromSave.BuildingSize == BuildingSize.Small)
                {
                    if(buildingFromSave.BuildingSlot == BuildingSlot.Small_TopLeft)
                        ActivateSmallBuildingTopLeftButtons();
                    else if(buildingFromSave.BuildingSlot == BuildingSlot.Small_TopRight)
                        ActivateSmallBuildingTopRightButtons();
                    else if(buildingFromSave.BuildingSlot == BuildingSlot.Small_BottomLeft)
                        ActivateSmallBuildingBottomLeftButtons();
                    else 
                        ActivateSmallBuildingBottomRightButtons();
                }
            }
        }
        
        #region Button Methods

        #region Small Building Buttons

        private void ActivateSmallBuildingTopLeftButtons()
        {
            var button = _tileInfoPanelView.smallBuildingTopLeftButton;
            button.gameObject.SetActive(true);
            //TODO: add method to on click
        }
        
        private void ActivateSmallBuildingTopRightButtons()
        {
            var button = _tileInfoPanelView.smallBuildingTopRightButton;
            button.gameObject.SetActive(true);
            //TODO: add method to on click
        }
        
        private void ActivateSmallBuildingBottomLeftButtons()
        {
            var button = _tileInfoPanelView.smallBuildingBottomLeftButton;
            button.gameObject.SetActive(true);
            //TODO: add method to on click
        }
        
        private void ActivateSmallBuildingBottomRightButtons()
        {
            var button = _tileInfoPanelView.smallBuildingBottomRightButton;
            button.gameObject.SetActive(true);
            //TODO: add method to on click
        }

        #endregion

        #region Medium Building Buttons

        private void ActivateMediumTopBuildingButtons()
        {
            var button = _tileInfoPanelView.mediumBuildingTopButton;
            button.gameObject.SetActive(true);
            //TODO: add method to on click
        }

        private void ActivateMediumBottomBuildingButtons()
        {
            var button = _tileInfoPanelView.mediumBuildingBottomButton;
            button.gameObject.SetActive(true);
            //TODO: add method to on click
        }

        #endregion

        #region Large Building Button

        private void ActivateLargeBuildingButton()
        {
            var button = _tileInfoPanelView.largeBuildingCenterButton;
            button.gameObject.SetActive(true);
            //TODO: add method to on click
        }

        #endregion

        #region Extra Large Building Button

        private void ActivateExtraLargeBuildingButton()
        {
            var button = _tileInfoPanelView.extraLargeBuildingCenterButton;
            button.gameObject.SetActive(true);
            //TODO: add method to on click
        }

        #endregion

        private void DeactivateAllButtons()
        {
            _tileInfoPanelView.smallBuildingTopLeftButton.gameObject.SetActive(false);
            _tileInfoPanelView.smallBuildingTopRightButton.gameObject.SetActive(false);
            _tileInfoPanelView.smallBuildingBottomLeftButton.gameObject.SetActive(false);
            _tileInfoPanelView.smallBuildingBottomRightButton.gameObject.SetActive(false); 

            _tileInfoPanelView.mediumBuildingTopButton.gameObject.SetActive(false);
            _tileInfoPanelView.mediumBuildingBottomButton.gameObject.SetActive(false);
            _tileInfoPanelView.largeBuildingCenterButton.gameObject.SetActive(false);
            _tileInfoPanelView.extraLargeBuildingCenterButton.gameObject.SetActive(false);
            
            _tileInfoPanelView.resourceGameObject.SetActive(false);
        }

        private void ActivateDefaultButtons()
        {
            _tileInfoPanelView.smallBuildingTopLeftButton.gameObject.SetActive(true);
            _tileInfoPanelView.smallBuildingTopRightButton.gameObject.SetActive(true);
            _tileInfoPanelView.smallBuildingBottomLeftButton.gameObject.SetActive(true);
            _tileInfoPanelView.smallBuildingBottomRightButton.gameObject.SetActive(true);
            
            _tileInfoPanelView.smallBuildingTopLeftImage.sprite = default;
            _tileInfoPanelView.smallBuildingTopRightImage.sprite = default;
            _tileInfoPanelView.smallBuildingBottomLeftImage.sprite = default;
            _tileInfoPanelView.smallBuildingBottomRightImage.sprite = default;
        }

        #endregion

        #region Button Sprite Change Methods

        public void SmallBuildingModifySprite(BuildingSlot slot, string spriteId)
        {
            //TODO: set sprite id on the designated building slot button
        }
        
        public void MediumBuildingModifySprite(BuildingSlot slot, string spriteId)
        {
            //TODO: set sprite id on the designated building slot button
        }
        
        public void LargeBuildingModifySprite(BuildingSlot slot, string spriteId)
        {
            //TODO: set sprite id on the designated building slot button
        }
        
        public void ExtraLargeBuildingModifySprite(BuildingSlot slot, string spriteId)
        {
            //TODO: set sprite id on the designated building slot button
        }

        #endregion

        public void Dispose()
        {
            TileActions.OnClickShowTileInfo -= ToggleButtonBasedOnActiveBuildingSlots;
            _tileInfoPanelViewDisposable?.Dispose();
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