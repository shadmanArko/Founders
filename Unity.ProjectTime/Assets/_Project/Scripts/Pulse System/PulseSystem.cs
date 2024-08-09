using System;
using System.Threading;
using _Project.Scripts.Game_Actions;
using _Project.Scripts.ScriptableObjectDataContainerScripts;
using _Project.Scripts.TIme_System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Pulse_System
{
    public class PulseSystem : IDisposable
    {
        private ReactiveProperty<int> _counter = new ReactiveProperty<int>(0);
        private ReactiveProperty<bool> _isPlaying = new ReactiveProperty<bool>(false);
        private bool _isPulseInProgress = false;
        private IDisposable _pulseSubscription;
        private SemaphoreSlim _pulseSemaphore = new SemaphoreSlim(1, 1); // Initialize with 1 permit
        private readonly GameTimeSystem _gameTimeSystem;
        private int count = 0;
        private readonly SaveDataScriptableObject _saveDataScriptableObject;

        public PulseSystem(GameTimeSystem gameTimeSystem, SaveDataScriptableObject saveDataScriptableObject)
        {
            _saveDataScriptableObject = saveDataScriptableObject;
            _gameTimeSystem = gameTimeSystem;
        }

        public async UniTask Init()
        {
            await UniTask.SwitchToMainThread();

            _pulseSubscription = Observable.Interval(System.TimeSpan.FromSeconds(0.1))
                .WithLatestFrom(_isPlaying, (_, playing) => playing)
                .Where(playing => playing)
                .Subscribe(async _ =>
                {
                    await Pulse(); 
                });

            Application.quitting += Dispose;
        }

        private async UniTask Pulse()
        {
            await UniTask.SwitchToMainThread();

            if (_isPulseInProgress)
            {
                return; // If a pulse is already in progress, do nothing
            }

            _isPulseInProgress = true;

            if (count < _saveDataScriptableObject.Save.PulseSpeed)
            {
                count++;
                _isPulseInProgress = false;
                return;
            }
            else
            {
                count = 0;
            }
            // Perform your pulse operations here

            await OnEveryPulse();

            _isPulseInProgress = false;
        }

        private async UniTask OnEveryPulse()
        {
            //await UniTask.SwitchToThreadPool();
            await UniTask.SwitchToMainThread();
            _gameTimeSystem.IncreaseTime();
            TimeActions.onPulseTicked?.Invoke();
        }

        public void PlayPulse()
        {
            _isPlaying.Value = true;
            _saveDataScriptableObject.Save.PulseSpeed = 10;
        }
        
        public void PlayPulse2X()
        {
            _isPlaying.Value = true;
            _saveDataScriptableObject.Save.PulseSpeed = 5;
        }
        
        public void PlayPulse4X()
        {
            _isPlaying.Value = true;
            _saveDataScriptableObject.Save.PulseSpeed = 1;
        }
        
        public void PlayPulse8X()
        {
            _isPlaying.Value = true;
            _saveDataScriptableObject.Save.PulseSpeed = 0;
        }

        public void PausePulse()
        {
            _isPlaying.Value = false;
        }

        public void Dispose()
        {
            _pulseSubscription?.Dispose();
            Application.quitting -= Dispose;
        }
    }
}