using System;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.UI.GameScene.LeftPanel.MilitaryCanvas;
using _Project.Scripts.UI.GameScene.LeftPanel.PopDetailsViewerCanvas;
using _Project.Scripts.UI.GameScene.LeftPanel.PopNavigatorCanvas;
using _Project.Scripts.UI.UiController;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScene.LeftPanel
{
    public class LeftPanelCanvasController : IDisposable, IUiController
    {
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly LeftPanelCanvasView _leftPanelCanvasView;
        
        private readonly PopNavigatorCanvasController _popNavigatorCanvasController;
        private readonly PopDetailsViewerCanvasController _popDetailsViewerCanvasController;
        private readonly MilitaryCanvasController _militaryCanvasController;
        private readonly IDisposable _leftPanelCanvasControllerDisposable;

        public LeftPanelCanvasController(SaveDataScriptableObject saveDataScriptableObject, LeftPanelCanvasView leftPanelCanvasView, IDisposable leftPanelCanvasControllerDisposable, PopNavigatorCanvasController popNavigatorCanvasController, PopDetailsViewerCanvasController popDetailsViewerCanvasController, MilitaryCanvasController militaryCanvasController)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
            _leftPanelCanvasView = leftPanelCanvasView;
            _popNavigatorCanvasController = popNavigatorCanvasController;
            _popDetailsViewerCanvasController = popDetailsViewerCanvasController;
            _militaryCanvasController = militaryCanvasController;
            _leftPanelCanvasControllerDisposable = leftPanelCanvasControllerDisposable;
            
            SetViewAsChild();
            AssignButtons(_leftPanelCanvasView.popNavigatorPanelButton, _popNavigatorCanvasController);
            AssignButtons(_leftPanelCanvasView.militaryPanelButton, _militaryCanvasController);
        }

        

        private void AssignButtons(Button button, IUiController controller)
        {
            button.onClick.AddListener(Deactivate);
            button.onClick.AddListener(controller.Show);
        }
        
        private void SetViewAsChild()
        {
            _popNavigatorCanvasController.SetViewAsChild(_leftPanelCanvasView.leftPanel);
            _popDetailsViewerCanvasController.SetViewAsChild(_leftPanelCanvasView.leftPanel);
            _militaryCanvasController.SetViewAsChild(_leftPanelCanvasView.leftPanel);
        }
        
        public void Dispose()
        {
            _leftPanelCanvasControllerDisposable?.Dispose();
        }

        public void Activate()
        {
            _leftPanelCanvasView.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _popNavigatorCanvasController.Deactivate();
            _militaryCanvasController.Deactivate();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}