using Third_Party.Recyclable_Scroll_Rect.Main.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScene
{
    public class PopCardView : MonoBehaviour, ICell
    {
        public TMP_Text popName;
        public Button popDetailsButton;
    }
}
