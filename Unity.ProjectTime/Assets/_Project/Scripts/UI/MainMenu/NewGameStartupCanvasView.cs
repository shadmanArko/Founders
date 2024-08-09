using _Project.Scripts.Game_Actions;
using _Project.Scripts.Main_Menu;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts.UI.MainMenu
{
    public class NewGameStartupCanvasView : MonoBehaviour
    {
        public Transform parentTransform;

        public GameObject selectSubcontinentTextGameObject;
        public GameObject selectedSubcontinentGameObject;
        public TMP_Text selectedSubcontinentNameText;
        public PlayableCultureDropdown playableCultureDropdown;

        private SubcontinentsContainer _subcontinentsContainer;
        private GameObject _subcontinentPrefab;

        private void Start()
        {
            ShowSubcontinents();
        }

        public void SetSubcontinentsContainer(SubcontinentsContainer subcontinentsContainer)
        {
            _subcontinentsContainer = subcontinentsContainer;
        }

        public void SetSubcontinentPrefab(GameObject prefab)
        {
            _subcontinentPrefab = prefab;
        }
        public void ShowSubcontinents()
        {
            foreach (var subcontinent in _subcontinentsContainer.subcontinents)
            {
                GameObject newSubcontinentObject = Instantiate(_subcontinentPrefab, parentTransform);

                Image subcontinentImage = newSubcontinentObject.GetComponent<Image>();

                Vector3 newPosition = subcontinentImage.rectTransform.localPosition;
                newPosition.x = -200 + (3* subcontinent.subcontinentPosition.x);
                newPosition.y = -50 + (3* subcontinent.subcontinentPosition.y);
                newSubcontinentObject.GetComponent<MainMenuSubcontinentPrefab>().Init(subcontinent);
                subcontinentImage.rectTransform.localPosition = newPosition;
                
            }
        }

        public void SelectSubcontinent(Subcontinent subcontinent)
        {
            selectSubcontinentTextGameObject.SetActive(false);
            selectedSubcontinentGameObject.SetActive(true);
            selectedSubcontinentNameText.text = subcontinent.subcontinentName;
        }
        public void LoadScene(string scene)
        {
            // SceneManager.LoadScene(scene);
            MainMenuActions.OnClickLoadNewGameButton?.Invoke();
        }

        private void OnEnable()
        {
            MainMenuSubcontinentPrefab.subcontinentButtonClicked += SelectSubcontinent;
        }
        private void OnDisable()
        {
            MainMenuSubcontinentPrefab.subcontinentButtonClicked -= SelectSubcontinent;
        }
    }
}
