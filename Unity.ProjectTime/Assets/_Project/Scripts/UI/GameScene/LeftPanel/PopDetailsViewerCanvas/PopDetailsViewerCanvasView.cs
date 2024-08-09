using System.Collections.Generic;
using _Project.Scripts.UI.GameScene.LeftPanel.PopNavigatorCanvas;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScene.LeftPanel.PopDetailsViewerCanvas
{
    public class PopDetailsViewerCanvasView : MonoBehaviour
    {
        public TMP_Text popNameText;
        public Image popImage;
        public Button popLocatorButton;
        public Button closeButton;

        [FormerlySerializedAs("popStats")] public List<StatCard> popStatCards;

    }
}