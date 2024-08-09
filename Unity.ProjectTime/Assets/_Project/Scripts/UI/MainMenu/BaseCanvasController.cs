using Zenject;

namespace _Project.Scripts.UI.MainMenu
{
    public class BaseCanvasController
    {
        private readonly BaseCanvasView _baseCanvasView;
        private readonly NewGameStartupCanvasView _newGameStartupCanvasView;

        public BaseCanvasController(BaseCanvasView baseCanvasView, NewGameStartupCanvasView newGameStartupCanvasView)
        {
            _baseCanvasView = baseCanvasView;
            _newGameStartupCanvasView = newGameStartupCanvasView;
        }

        [Inject]
        void SetUpBaseCanvasButtons()
        {
            _baseCanvasView.newGameButton.onClick.AddListener(()=> _baseCanvasView.Show(_newGameStartupCanvasView.gameObject));
            _baseCanvasView.newGameButton.onClick.AddListener(()=> _baseCanvasView.Hide(_baseCanvasView.gameObject));
        }
    }
}
