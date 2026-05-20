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
            OnValueChanged?.Invoke(_ready);
        }

        private bool _ready;
    }
}
