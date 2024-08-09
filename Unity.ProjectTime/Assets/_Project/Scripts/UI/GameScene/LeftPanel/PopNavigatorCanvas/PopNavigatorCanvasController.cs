using System;
using System.Collections.Generic;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.UI.GameScene.LeftPanel.PopDetailsViewerCanvas;
using _Project.Scripts.UI.UiController;
using ASP.NET.ProjectTime._1._Repositories;
using Third_Party.Recyclable_Scroll_Rect.Main.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.UI.GameScene.LeftPanel.PopNavigatorCanvas
{
    public class PopNavigatorCanvasController : IDisposable, IRecyclableScrollRectDataSource, IUiController
    {
        private List<string> _popListIds;
        
        private readonly PopNavigatorCanvasView _popNavigatorCanvasView;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly PopDetailsViewerCanvasController _popDetailsViewerCanvasController;
        private readonly IDisposable _popNavigatorPanelDisposable;
        private readonly PopCardView _popCardView;

        public PopNavigatorCanvasController(PopNavigatorCanvasView popNavigatorCanvasView, PopDetailsViewerCanvasController popDetailsViewerCanvasController, SaveDataScriptableObject saveDataScriptableObject, PopCardView popCardView)
        {
            _popNavigatorCanvasView = popNavigatorCanvasView;
            _popNavigatorCanvasView.recyclableScrollRect.PrototypeCell = popCardView.gameObject.GetComponent<RectTransform>();
            _saveDataScriptableObject = saveDataScriptableObject;
            _popDetailsViewerCanvasController = popDetailsViewerCanvasController; 
            _popListIds = new List<string>(); 
            // Your other setup code
        }

        public void SetViewAsChild(Transform canvasTransform)
        {
            Activate();
            _popNavigatorCanvasView.transform.SetParent(canvasTransform);
            Deactivate();
        }

        private void AssignPopCardToPopNavigationPanel()
        {
            _popListIds = _saveDataScriptableObject.Save.AllCultures[0].Culture.PopIds;
            if (_popListIds.Count > 0)
            {
                if (_popNavigatorCanvasView.recyclableScrollRect.DataSource == null)
                {
                    _popNavigatorCanvasView.recyclableScrollRect.DataSource = this;
                    _popNavigatorCanvasView.recyclableScrollRect.Initialize(this);
                }
                else
                {
                    ReloadPopData();
                }
            }
        }

        private void ShowPopList()
        {
            var view = _popNavigatorCanvasView.gameObject;
            if(view.activeSelf)
                Deactivate();
            else 
                Activate();
            
            if(view.activeSelf) AssignPopCardToPopNavigationPanel();
        }
        
        
        #region Recyclable Scroll Rect Methods

        public int GetItemCount()
        {
            return _popListIds.Count;
        }

        public void SetCell(ICell cell, int index)
        {
            var pop = _saveDataScriptableObject.Save.Pops.GetById(pop1 => pop1.Id , _popListIds[index]);
            var item = cell as PopCardView;

            if (item != null)
            {
                item.popName.text = pop.Reference;
                item.popDetailsButton.onClick.AddListener(() =>
                { 
                    _popDetailsViewerCanvasController.ShowPopViewer(pop.Id);
                });
            } 
        }

        private void ReloadPopData()
        {
            _popNavigatorCanvasView.recyclableScrollRect.ReloadData(this);
        }

        #endregion
        
        public void Dispose()
        {
            _popNavigatorPanelDisposable?.Dispose();
        }

        public void Activate()
        {
            _popNavigatorCanvasView.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _popDetailsViewerCanvasController.Deactivate();
            _popNavigatorCanvasView.gameObject.SetActive(false);
        }

        public void Show()
        {
            ShowPopList();
        }
    }
}