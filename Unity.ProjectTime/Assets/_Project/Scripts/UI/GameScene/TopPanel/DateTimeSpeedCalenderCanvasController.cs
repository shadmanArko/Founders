using System;
using _Project.Scripts.Pulse_System;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.UI.UiController;
using UniRx;

namespace _Project.Scripts.UI.GameScene.TopPanel
{
    public class DateTimeSpeedCalenderCanvasController : IDisposable, IUiController
    {
        private readonly DateTimeSpeedCalenderCanvasView _dateTimeSpeedCalenderCanvasView;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;
        private readonly IDisposable _dateInWordTextUpdateSubscription;
        private readonly PulseSystem _pulseSystem;

        public DateTimeSpeedCalenderCanvasController(DateTimeSpeedCalenderCanvasView dateTimeSpeedCalenderCanvasView, SaveDataScriptableObject saveDataScriptableObject, PulseSystem pulseSystem)
        {
            _dateTimeSpeedCalenderCanvasView = dateTimeSpeedCalenderCanvasView;
            _saveDataScriptableObject = saveDataScriptableObject;
            _pulseSystem = pulseSystem;
            _dateInWordTextUpdateSubscription = _saveDataScriptableObject.Save.GameTime.ObserveEveryValueChanged(time => time.InWords)
                .Subscribe(UpdateDateInWordsTextField);
            
            AddButtonListeners();
        }

        private void AddButtonListeners()
        {
            _dateTimeSpeedCalenderCanvasView.pauseButton.onClick.AddListener(OnClickPauseButton);
            _dateTimeSpeedCalenderCanvasView.playButton.onClick.AddListener(OnClickPlayButton);
            _dateTimeSpeedCalenderCanvasView.playButton2X.onClick.AddListener(OnClickPlayButton2X);
            _dateTimeSpeedCalenderCanvasView.playButton4X.onClick.AddListener(OnClickPlayButton4X);
            _dateTimeSpeedCalenderCanvasView.playButton8X.onClick.AddListener(OnClickPlayButton8X); 
        }
        
        private void UpdateDateInWordsTextField(string newInWords)
        {
            _dateTimeSpeedCalenderCanvasView.dateInWordsText.text = newInWords;
        }

        private void OnClickPauseButton()
        {
            _pulseSystem.PausePulse();
        }

        private void OnClickPlayButton()
        {
            _pulseSystem.PlayPulse();
        }

        private void OnClickPlayButton2X()
        {
            _pulseSystem.PlayPulse2X();
        }

        private void OnClickPlayButton4X()
        {
            _pulseSystem.PlayPulse4X();
        }

        private void OnClickPlayButton8X()
        {
            _pulseSystem.PlayPulse8X();
        }
        
        public void Dispose()
        {
            _dateInWordTextUpdateSubscription?.Dispose();
        }

        public void Activate()
        {
            
        }

        public void Deactivate()
        {
            
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
