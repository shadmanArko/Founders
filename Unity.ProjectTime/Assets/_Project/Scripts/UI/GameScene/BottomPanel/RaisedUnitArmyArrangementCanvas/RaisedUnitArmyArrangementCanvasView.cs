using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScene.BottomPanel.RaisedUnitArmyArrangementCanvas
{
    public class RaisedUnitArmyArrangementCanvasView : MonoBehaviour
    {
        public TMP_Text unitNameText;
        public Image unitImageHolder;

        public List<Button> firstRowButtons;
        public List<Button> secondRowButtons;
    }
}