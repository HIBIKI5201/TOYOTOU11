using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class ReadyButton : MonoBehaviour
    {
        public bool IsReady => _ready;

        public void TriggeredReady() => _ready = !_ready;

        private bool _ready;
    }
}
