using System;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    ///     インゲームの時間制限タイマーの管理クラス。
    /// </summary>
    public class DemoTimer : MonoBehaviour
    {
        /// <summary> 時間制限超過時に実行されるイベント </summary>
        public event Action OnTimeUp;
        /// <summary> 時間制限超過かどうか </summary>
        public bool IsTimeUp => _isTimeUp;

        /// <summary>
        ///     タイマーを開始する。
        /// </summary>
        public void StartTimer()
        {
            _timer = _timeLimit;
            _isTimeUp = false;
        }

        [SerializeField, Tooltip("タイムリミット(秒)")] private float _timeLimit = 30f;

        private float _timer = 0f;
        private bool _isTimeUp = true;

        void Update()
        {
            if (_isTimeUp) { return; } // 時間制限超過後は処理しない。

            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                OnTimeUp?.Invoke();
                _isTimeUp = true;
            }
        }
    }
}