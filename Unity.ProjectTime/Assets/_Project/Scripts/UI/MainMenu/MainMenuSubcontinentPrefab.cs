using System;
using ASP.NET.ProjectTime.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.MainMenu
{
    public class MainMenuSubcontinentPrefab : MonoBehaviour
    {
        public TextMeshProUGUI subContinentName;
        public Button subcontinentButton;
        private Subcontinent _subcontinent;
        public static Action<Subcontinent> subcontinentButtonClicked;
        public void Init(Subcontinent subcontinent)
        {
            _subcontinent = subcontinent;
            subContinentName.text = subcontinent.subcontinentName;
            subcontinentButton.onClick.AddListener(OnClickSubcontinentButton);
        }

        void OnClickSubcontinentButton()
        {
            subcontinentButtonClicked?.Invoke(_subcontinent);
        }
    }
}
