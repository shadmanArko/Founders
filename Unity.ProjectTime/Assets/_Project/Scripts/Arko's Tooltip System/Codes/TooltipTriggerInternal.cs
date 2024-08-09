using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Arko_s_Tooltip_System.Codes
{
    public class TooltipTriggerInternal : MonoBehaviour
    {
        [TextArea]
        public string content;
        private TooltipUI _tooltipUI;

        private UniTask _tooltipTask;
        private CancellationTokenSource _cancellationTokenSource;
        public float tooltipDelay = 0.5f;

        private void Start()
        {
            _tooltipUI = GameObject.Find("Tooltip Canvas").GetComponent<ToolTipCanvas>().tooltipUI;
        }

        private void OnMouseEnter()
        {
            if (_tooltipTask.Status != UniTaskStatus.Pending)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _tooltipTask = ShowTooltipDelayed(_cancellationTokenSource.Token);
            }
        }

        private void OnMouseExit()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }

            _tooltipUI.Hide();
        }

        private async UniTask ShowTooltipDelayed(CancellationToken cancellationToken)
        {
            var delayTask = UniTask.Delay((int)(tooltipDelay * 1000), cancellationToken: cancellationToken).SuppressCancellationThrow();
            var result = await UniTask.WhenAny(delayTask);

            if ( !cancellationToken.IsCancellationRequested)
            {
                _tooltipUI.Show(content);
            }
        }

        private void OnDisable()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }

            if (_tooltipUI != null)
            {
                _tooltipUI.Hide();
            }
        }
    }
}