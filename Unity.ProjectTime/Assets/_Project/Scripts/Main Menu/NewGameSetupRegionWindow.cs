using _Project.Scripts.Managers;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.UI.MainMenu;
using ASP.NET.ProjectTime.Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts.Main_Menu
{
    public class NewGameSetupRegionWindow : MonoBehaviour
    {
        public SubcontinentsContainer subcontinentsContainer;
        public GameObject subContinentPrefab;
        public Transform parentTransform;

        public GameObject selectSubcontinentTextGameObject;
        public GameObject selectedSubcontinentGameObject;
        public TMP_Text selectedSubcontinentNameText;
        private void Start()
        {
            ShowSubcontinents();
        }

        private void ShowSubcontinents()
        {
            foreach (var subcontinent in subcontinentsContainer.subcontinents)
            {
                GameObject newSubcontinentObject = Instantiate(subContinentPrefab, parentTransform);

                Image subcontinentImage = newSubcontinentObject.GetComponent<Image>();

                Vector3 newPosition = subcontinentImage.rectTransform.localPosition;
                newPosition.x = -400 + (3* subcontinent.subcontinentPosition.x);
                newPosition.y = -50 + (3* subcontinent.subcontinentPosition.y);
                newSubcontinentObject.GetComponent<MainMenuSubcontinentPrefab>().Init(subcontinent);
                subcontinentImage.rectTransform.localPosition = newPosition;
            }
        }

        void SelectSubcontinent(Subcontinent subcontinent)
        {
            selectSubcontinentTextGameObject.SetActive(false);
            selectedSubcontinentGameObject.SetActive(true);
            selectedSubcontinentNameText.text = subcontinent.subcontinentName;
        }
        public void LoadNewGame()
        {
            SceneManager.LoadScene("Map");
            
        }

        private void OnEnable()
        {
            MainMenuManager.mainMenuInitiated += ShowSubcontinents;
            MainMenuSubcontinentPrefab.subcontinentButtonClicked += SelectSubcontinent;
        }

        private void OnDisable()
        {
            MainMenuManager.mainMenuInitiated -= ShowSubcontinents;
            MainMenuSubcontinentPrefab.subcontinentButtonClicked -= SelectSubcontinent;

        }
    }
}