using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI.MainMenu
{
    public class MainMenuUi : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
