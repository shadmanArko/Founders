using _Project.Scripts.UI.GameScene;
using _Project.Scripts.UI.GameScene.BottomPanel.RaisedUnitArmyArrangementCanvas;
using _Project.Scripts.UI.GameScene.BottomPanel.RaisedUnitSettlerArrangementCanvas;
using _Project.Scripts.UI.GameScene.BottomPanel.TileInfoPanel;
using _Project.Scripts.UI.GameScene.LeftPanel;
using _Project.Scripts.UI.GameScene.LeftPanel.MilitaryCanvas;
using _Project.Scripts.UI.GameScene.LeftPanel.PopDetailsViewerCanvas;
using _Project.Scripts.UI.GameScene.LeftPanel.PopNavigatorCanvas;
using _Project.Scripts.UI.GameScene.TopPanel;
using _Project.Scripts.UI.MiniMap;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    [CreateAssetMenu(fileName = "UiInstaller", menuName = "Installers/UiInstaller")]
    public class UiInstaller : ScriptableObjectInstaller<UiInstaller>
    {
        [SerializeField] private DateTimeSpeedCalenderCanvasView dateTimeSpeedCalenderCanvasView;
        [SerializeField] private LeftPanelCanvasView leftPanelCanvasView;
        [SerializeField] private PopNavigatorCanvasView popNavigatorCanvasView;
        [SerializeField] private PopDetailsViewerCanvasView popDetailsViewerCanvasView;
        [SerializeField] private MilitaryCanvasView militaryCanvasView;
        [SerializeField] private BottomPanelCanvasView bottomPanelCanvasView;
        [SerializeField] private RaisedUnitSettlerArrangementCanvasView raisedUnitSettlerArrangementCanvasView;
        [SerializeField] private RaisedUnitArmyArrangementCanvasView raisedUnitArmyArrangementCanvasView;
        [SerializeField] private TileInfoPanelView tileInfoPanelView;
        [SerializeField] private MiniMapView miniMapView;
        [SerializeField] private PopCardView popCardView;
        
        public override void InstallBindings()
        {
            Container.Bind<DateTimeSpeedCalenderCanvasView>().FromComponentInNewPrefab(dateTimeSpeedCalenderCanvasView).AsSingle();
            Container.Bind<DateTimeSpeedCalenderCanvasController>().AsSingle().NonLazy();

            Container.Bind<PopCardView>().FromComponentInNewPrefab(popCardView).AsCached();

            Container.Bind<PopDetailsViewerCanvasView>().FromComponentInNewPrefab(popDetailsViewerCanvasView).AsSingle();
            Container.Bind<PopDetailsViewerCanvasController>().AsSingle();
            
            Container.Bind<PopNavigatorCanvasView>().FromComponentInNewPrefab(popNavigatorCanvasView).AsSingle();
            Container.Bind<PopNavigatorCanvasController>().AsSingle();

            Container.Bind<MilitaryCanvasView>().FromComponentInNewPrefab(militaryCanvasView).AsSingle();
            Container.Bind<MilitaryCanvasController>().AsSingle();
            
            Container.Bind<LeftPanelCanvasView>().FromComponentInNewPrefab(leftPanelCanvasView).AsSingle();
            Container.Bind<LeftPanelCanvasController>().AsSingle().NonLazy(); 
        
            Container.Bind<BottomPanelCanvasView>().FromComponentInNewPrefab(bottomPanelCanvasView).AsSingle();
            Container.Bind<BottomPanelCanvasController>().AsSingle();

            Container.Bind<TileInfoPanelView>().FromComponentInNewPrefab(tileInfoPanelView).AsSingle();
            Container.Bind<TileInfoPanelController>().AsSingle()/*.NonLazy()*/;
        
            Container.Bind<MiniMapView>().FromComponentInNewPrefab(miniMapView).AsSingle();
            Container.Bind<MiniMapController>().AsSingle().NonLazy();
            
            Container.Bind<RaisedUnitSettlerArrangementCanvasView>().FromComponentInNewPrefab(raisedUnitSettlerArrangementCanvasView).AsSingle()/*.NonLazy()*/;
            Container.Bind<RaisedUnitSettlerArrangementCanvasController>().AsSingle().NonLazy();
        
            Container.Bind<RaisedUnitArmyArrangementCanvasView>().FromComponentInNewPrefab(raisedUnitArmyArrangementCanvasView).AsSingle();
            Container.Bind<RaisedUnitArmyArrangementCanvasController>().AsSingle();
        }
    }
}