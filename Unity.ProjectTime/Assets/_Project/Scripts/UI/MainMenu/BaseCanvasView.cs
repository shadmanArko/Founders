using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.MainMenu
{
    public class BaseCanvasView : MonoBehaviour
    {
        public Button newGameButton;
        public Button loadGameButton;

        public void Show(GameObject gameObjectToShow)
        {
            gameObjectToShow.SetActive(true);
        }

        public void Hide(GameObject gameObjectToHide)
        {
            gameObjectToHide.SetActive(false);
        }
    }
}
