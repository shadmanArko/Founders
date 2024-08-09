using System;
using System.Linq;
using _Project.Scripts.Game_Actions;
using _Project.Scripts.Main_Menu;
using _Project.Scripts.Managers;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.ScriptableObjectDataContainerScripts.Culture;
using _Project.Scripts.Static_Classes;
using ASP.NET.ProjectTime.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Scripts.UI.MainMenu
{
    public class NewGameStartupCanvasController : IDisposable
    {
        private BaseCanvasView _baseCanvasView;
        private NewGameStartupCanvasView _newGameStartupCanvasView;
        private SubcontinentsContainer _subcontinentsContainer;
        private MainMenuSubcontinentPrefab _subcontinentPrefab;
        private NewGameSettingsData _newGameSettingsData;
        private PlayableCultureContainerSo _playableCultureContainerSo;

        public NewGameStartupCanvasController( PlayableCultureContainerSo playableCultureContainerSo, NewGameSettingsData newGameSettingsData, BaseCanvasView baseCanvasView, NewGameStartupCanvasView newGameStartupCanvasView, SubcontinentsContainer subcontinentsContainer, MainMenuSubcontinentPrefab subcontinentPrefab)
        {
            _newGameSettingsData = newGameSettingsData;
            _playableCultureContainerSo = playableCultureContainerSo;
            _baseCanvasView = baseCanvasView;
            _newGameStartupCanvasView = newGameStartupCanvasView;
            _subcontinentsContainer = subcontinentsContainer;
            _subcontinentPrefab = subcontinentPrefab;
            MainMenuActions.OnClickLoadNewGameButton += OnClickLoadNewGameButton;
        }

        private void OnClickLoadNewGameButton()
        {
            if (_newGameSettingsData == null)
            {
                Debug.LogError("Settings data null");
            }
            
            _newGameSettingsData.subcontinentName = _newGameStartupCanvasView.selectedSubcontinentNameText.text;
            var playableCultureDropDown = _newGameStartupCanvasView.playableCultureDropdown.playableCultureDropdown;
            var playableCultureName = playableCultureDropDown.options[playableCultureDropDown.value].text;
            _newGameSettingsData.playableCulture =
                _playableCultureContainerSo.playableCultures.FirstOrDefault(culture =>
                    culture.GetPlayableCultureName() == playableCultureName);
            SceneManager.LoadScene("Map");
        }

        public void SetUp()
        {
            _newGameStartupCanvasView.SetSubcontinentsContainer(_subcontinentsContainer);
            _newGameStartupCanvasView.SetSubcontinentPrefab(_subcontinentPrefab.gameObject);
            _newGameStartupCanvasView.playableCultureDropdown.PopulateDropDown(_playableCultureContainerSo);
        }


        public void Dispose()
        {
            MainMenuActions.OnClickLoadNewGameButton -= OnClickLoadNewGameButton;
        }
    }
    

    
}
