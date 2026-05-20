using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    ///     インゲームの時間制限タイマーの管理クラス。
    /// </summary>
    public class InGameTimer : MonoBehaviour
    {
        /// <summary> 時間制限超過時に実行されるイベント </summary>
        public event Action OnTimeUp;
        /// <summary> 時間制限超過かどうか </summary>
        public bool IsTimeUp => _isTimeUp;
        public float RemainTime => _isTimeUp ? 0 : _remainTime;

        /// <summary>
        ///     タイマーを開始する。
        /// </summary>
        public void StartTimer()
        {
            _remainTime = _timeLimit;
            _isTimeUp = false;
        }

        /// <summary>
        ///     タイムアップするまで終わらないタスク。
        /// </summary>
        /// <returns></returns>
        public async ValueTask WaitTimeUp(CancellationToken token = default)
        {
            CancellationTokenSource linkedCts =
                CancellationTokenSource.CreateLinkedTokenSource(
                token, destroyCancellationToken);
            
            while (!_isTimeUp)
            {
                try
                {
                    await Awaitable.NextFrameAsync(linkedCts.Token);
                }
                catch (OperationCanceledException) { }
            }
        }

        [SerializeField, Tooltip("タイムリミット(秒)")] private float _timeLimit = 30f;

        private float _remainTime = 0f;
        private bool _isTimeUp = true;

        private void Update()
        {
            if (_isTimeUp) { return; } // 時間制限超過後は処理しない。

            _remainTime -= Time.deltaTime;
            if (_remainTime <= 0)
            {
                OnTimeUp?.Invoke();
                _isTimeUp = true;
            }
        }
    }
}