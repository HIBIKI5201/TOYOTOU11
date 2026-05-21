using DG.Tweening;
using System;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// 準備完了ボタンの状態と視覚効果を管理するクラス
    /// </summary>
    public class ReadyButton : MonoBehaviour
    {
        /// <summary>
        /// 準備完了状態が変更されたときに通知されるイベント
        /// </summary>
        public event Action<bool> OnValueChanged;

        /// <summary>
        /// 現在準備完了状態かどうかを取得します
        /// </summary>
        public bool IsReady => _ready;

        /// <summary>
        /// 準備完了状態を切り替え、視覚効果を再生します
        /// </summary>
        public void TriggeredReady()
        {
            _ready = !_ready;
            Effect();
            OnValueChanged?.Invoke(_ready);
        }

        private bool _ready;

        private void Effect()
        {
            if (_ready)
            {
                transform.DOScale(0.8f, 0.3f);
            }
            else
            {
                transform.DOScale(1, 0.2f);
            }
        }
    }
}
