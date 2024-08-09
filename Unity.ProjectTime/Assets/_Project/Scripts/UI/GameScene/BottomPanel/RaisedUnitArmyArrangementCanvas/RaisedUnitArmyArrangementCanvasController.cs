using System;
using _Project.Scripts.ScriptableObjectDataContainerScripts;

namespace _Project.Scripts.UI.GameScene.BottomPanel.RaisedUnitArmyArrangementCanvas
{
    public class RaisedUnitArmyArrangementCanvasController : IDisposable
    {
        private readonly RaisedUnitArmyArrangementCanvasView _raisedUnitArmyArrangementCanvasView;
        private readonly IDisposable _raisedUnitCanvasDisposable;
        private SaveDataScriptableObject _saveDataScriptableObject;

        public RaisedUnitArmyArrangementCanvasController(RaisedUnitArmyArrangementCanvasView raisedUnitArmyArrangementCanvasView, IDisposable raisedUnitCanvasDisposable, SaveDataScriptableObject saveDataScriptableObject)
        {
            _raisedUnitArmyArrangementCanvasView = raisedUnitArmyArrangementCanvasView;
            _raisedUnitCanvasDisposable = raisedUnitCanvasDisposable;
            _saveDataScriptableObject = saveDataScriptableObject;
        }
        
        public void Dispose()
        {
            _raisedUnitCanvasDisposable?.Dispose();
        }
    }
}