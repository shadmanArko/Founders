using _Project.Scripts.Managers;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Main_Menu
{
    public class UseDescriptiveDataForTextField : MonoBehaviour
    {
        public string label;
        public GameTextFieldDescriptiveDataSo gameTextFieldDescriptiveDataSo;

        private void Start()
        {
            UpdateTextField();
        }

        void UpdateTextField()
        {
            foreach (var gameTextFieldDescriptiveData in gameTextFieldDescriptiveDataSo.gameTextFieldDescriptiveDatas)  
            {
                if (gameTextFieldDescriptiveData.label == label)
                {
                    GetComponent<TMP_Text>().text = gameTextFieldDescriptiveData.text;
                }
            }
        }
        private void OnEnable()
        {
            MainMenuManager.mainMenuInitiated += UpdateTextField;
        }

        private void OnDisable()
        {
            MainMenuManager.mainMenuInitiated -= UpdateTextField;
        }
    }
}
