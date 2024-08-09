using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI.GameScene.LeftPanel
{
    public class LeftPanelCanvasView : MonoBehaviour
    {
        public Button popNavigatorPanelButton;
        public Button militaryPanelButton;
        
        public Transform leftPanel;

        public Transform primaryPanel;
        public Transform secondaryPanel;
    }
}