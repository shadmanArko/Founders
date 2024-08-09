using System;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.UI.UiController;
using UnityEngine;

namespace _Project.Scripts.UI.GameScene.LeftPanel.MilitaryCanvas
{
    public class MilitaryCanvasController : IDisposable, IUiController
    {
        private readonly MilitaryCanvasView _militaryCanvasView;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly IDisposable _militaryCanvasIDisposable;

        public MilitaryCanvasController(MilitaryCanvasView militaryCanvasView, SaveDataScriptableObject saveDataScriptableObject, IDisposable militaryCanvasIDisposable)
        {
            _militaryCanvasView = militaryCanvasView;
            _saveDataScriptableObject = saveDataScriptableObject;
            _militaryCanvasIDisposable = militaryCanvasIDisposable;
        }

        public void SetViewAsChild(Transform canvasTransform)
        {
            Activate();
            _militaryCanvasView.transform.SetParent(canvasTransform);
            Deactivate();
        }

        private void ShowMilitaryPanel()
        {
            var view = _militaryCanvasView.gameObject;
            
            if(view.activeSelf)
                Deactivate();
            else 
                Activate();
            
            Debug.LogWarning("Show Military Method not implemented yet!!");
        }
        
        public void Dispose()
        {
            _militaryCanvasIDisposable?.Dispose();
        }

        public void Activate()
        {
            _militaryCanvasView.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _militaryCanvasView.gameObject.SetActive(false);
        }

        public void Show()
        {
            ShowMilitaryPanel();
        }
    }
}