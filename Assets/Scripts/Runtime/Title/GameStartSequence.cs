using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace TOYOTOU.Runtime
{
    [RequireComponent(typeof(PlayableDirector))]
    public class GameStartSequence : MonoBehaviour
    {
        public event Action OnSequencePlayed;

        [SerializeField] private PlayableDirector _director;
        [SerializeField] private ReadyButton _button1;
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
