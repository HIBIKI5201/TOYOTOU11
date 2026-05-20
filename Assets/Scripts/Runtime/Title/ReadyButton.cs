using DG.Tweening;
using System;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class ReadyButton : MonoBehaviour
    {
        public event Action<bool> OnValueChanged;
        public bool IsReady => _ready;

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
