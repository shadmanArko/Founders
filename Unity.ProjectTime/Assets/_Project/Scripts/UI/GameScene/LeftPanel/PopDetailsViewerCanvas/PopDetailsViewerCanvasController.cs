using System;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.UI.UiController;
using ASP.NET.ProjectTime._1._Repositories;
using UnityEngine;

namespace _Project.Scripts.UI.GameScene.LeftPanel.PopDetailsViewerCanvas
{
    public class PopDetailsViewerCanvasController : IDisposable, IUiController
    {
        private SaveDataScriptableObject _saveDataScriptableObject;
        private readonly PopDetailsViewerCanvasView _popDetailsViewerCanvasView;
        private readonly IDisposable _popDetailsViewerCanvasDisposable;

        public PopDetailsViewerCanvasController(SaveDataScriptableObject saveDataScriptableObject, PopDetailsViewerCanvasView popDetailsViewerCanvasView, IDisposable popDetailsViewerCanvasDisposable)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
            _popDetailsViewerCanvasView = popDetailsViewerCanvasView;
            _popDetailsViewerCanvasDisposable = popDetailsViewerCanvasDisposable;
            
            popDetailsViewerCanvasView.closeButton.onClick.AddListener(Deactivate);
        }

        public void SetViewAsChild(Transform canvasTransform)
        {
            Activate();
            _popDetailsViewerCanvasView.transform.SetParent(canvasTransform);
            Deactivate();
        }

        public void ShowPopViewer(string popId)
        {  
            Activate();
            var pop = _saveDataScriptableObject.Save.Pops.GetById(pop1 => pop1.Id ,popId);
            _popDetailsViewerCanvasView.popNameText.text = pop.Reference;
            _popDetailsViewerCanvasView.popStatCards[0].statValue.text = $"{pop.PhysicalProwess}";
            _popDetailsViewerCanvasView.popStatCards[1].statValue.text = $"{pop.EducationalTradition}";
            _popDetailsViewerCanvasView.popStatCards[2].statValue.text = $"{pop.SpiritualKnowledge}";
            _popDetailsViewerCanvasView.popStatCards[3].statValue.text = $"{pop.PopSize}"; 
        }

        public void Dispose()
        {
            _popDetailsViewerCanvasDisposable?.Dispose();
        }

        public void Activate()
        {
            _popDetailsViewerCanvasView.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _popDetailsViewerCanvasView.gameObject.SetActive(false);
        }

        public void Show()
        {
            
        }
    }
}