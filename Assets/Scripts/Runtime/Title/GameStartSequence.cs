using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// ゲーム開始時の演出シークエンスを制御するクラス
    /// </summary>
    [RequireComponent(typeof(PlayableDirector))]
    public class GameStartSequence : MonoBehaviour
    {
        /// <summary>
        /// シークエンスの再生が完了したときに通知されるイベント
        /// </summary>
        public event Action OnSequencePlayed;

        [Tooltip("演出を再生するためのPlayableDirector")]
        [SerializeField] private PlayableDirector _director;
        [Tooltip("プレイヤー1の準備完了ボタン")]
        [SerializeField] private ReadyButton _button1;
        [Tooltip("プレイヤー2の準備完了ボタン")]
        [SerializeField] private ReadyButton _button2;

        private CancellationTokenSource _cts;

        private void Start()
        {
            _button1.OnValueChanged += OnButtonTriggered;
            _button2.OnValueChanged += OnButtonTriggered;
        }

        private void OnButtonTriggered(bool value)
        {
            if (!value)
            {
                if (_cts != null)
                {
                    _cts.Cancel();
                    _cts.Dispose();
                    _cts = null;
                }
                return;
            }

            if (_button1.IsReady && _button2.IsReady)
            {
                _cts = new();
                SequencePlay(_cts.Token);
            }
        }

        private async void SequencePlay(CancellationToken token)
        {
            _director.Play();
            TaskCompletionSource<PlayableDirector> tcs = new();
            _director.stopped += tcs.SetResult;

            CancellationTokenRegistration registration = token.Register(() =>
            {
                _director.Stop();
                tcs.TrySetCanceled();
            });

            try
            {
                await tcs.Task;
            }
            catch (OperationCanceledException) { return; }
            finally
            {
                registration.Dispose();
                _director.stopped -= tcs.SetResult;
            }

            OnSequencePlayed?.Invoke();
        }
    }
}
