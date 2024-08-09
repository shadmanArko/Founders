using System;
using _Project.Scripts.Game_Actions;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime._1._Repositories;
using UnityEngine;

namespace _Project.Scripts.UI.GameScene.BottomPanel.RaisedUnitSettlerArrangementCanvas
{
    public class RaisedUnitSettlerArrangementCanvasController : IDisposable
    {
        private readonly RaisedUnitSettlerArrangementCanvasView _raisedUnitSettlerArrangementCanvasView;
        private readonly IDisposable _raisedUnitSettlerArrangementCanvasDisposal;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;

        public RaisedUnitSettlerArrangementCanvasController(RaisedUnitSettlerArrangementCanvasView raisedUnitSettlerArrangementCanvasView, IDisposable raisedUnitSettlerArrangementCanvasDisposal, SaveDataScriptableObject saveDataScriptableObject)
        {
            _raisedUnitSettlerArrangementCanvasView = raisedUnitSettlerArrangementCanvasView;
            _raisedUnitSettlerArrangementCanvasDisposal = raisedUnitSettlerArrangementCanvasDisposal;
            _saveDataScriptableObject = saveDataScriptableObject;

            UnitActions.onSelectedUnit += ShowUnitDetails;
            
            Init();
        }

        private void Init()
        {
            _raisedUnitSettlerArrangementCanvasView.villageCenterButton.onClick.AddListener(OnVillageCenterButtonClicked);
        }

        private static void OnVillageCenterButtonClicked()
        {
            Debug.Log("Building Village Center");
            UnitActions.OnClickSettleASettlement?.Invoke("VillageCenter");
        }

        private void ShowUnitDetails(string unitId)
        {
            var unit = _saveDataScriptableObject.Save.Units.GetById(unit => unit.Id, unitId);
            if (unit is null)
            {
                Debug.Log("Uni");
                return;
            }

            Debug.LogWarning("Showing Unit Details");
            _raisedUnitSettlerArrangementCanvasView.unitNameText.text = unit.Name;
        }
        
        

        public void Dispose()
        {
            UnitActions.onSelectedUnit += ShowUnitDetails;
            _raisedUnitSettlerArrangementCanvasDisposal?.Dispose();
        }
    }
}