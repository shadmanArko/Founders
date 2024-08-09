using System;
using _Project.Scripts.DataBase.Initializer;
using _Project.Scripts.UI.MainMenu;
using Project.Scripts.DataBase.JsonToScriptableObject;
using SandBox.Arko.Scripts.UnitOfWork;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Managers
{
    public class MainMenuManager
    {
        private readonly MainMenuJsonToScriptableObjectConverter _mainMenuJsonToScriptableObjectConverter;
        private readonly Initializer _initializer;
        private readonly NewGameStartupCanvasController _newGameStartupCanvasController;
        public static Action mainMenuInitiated;
        public MainMenuManager(NewGameStartupCanvasController newGameStartupCanvasController, Initializer initializer, MainMenuJsonToScriptableObjectConverter mainMenuJsonToScriptableObjectConverter)
        {
            _newGameStartupCanvasController = newGameStartupCanvasController;
            _initializer = initializer;
            _mainMenuJsonToScriptableObjectConverter = mainMenuJsonToScriptableObjectConverter;
        }


        [Inject]
        public async void Init()
        {
            _initializer.LoadCoreDataFile();
            await _mainMenuJsonToScriptableObjectConverter.LoadData();
            _newGameStartupCanvasController.SetUp();
            mainMenuInitiated?.Invoke();
        }
    }
}