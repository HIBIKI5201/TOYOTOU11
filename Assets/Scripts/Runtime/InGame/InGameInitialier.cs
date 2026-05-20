using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class InGameInitialier : MonoBehaviour
    {
        public void Start()
        {
            GameStart();
        }

        public async void GameStart()
        {
            _playerInit.Init();
            await _timeline.Play();
            _timer.StartTimer();
        }

        [SerializeField] private PlayerInitializer _playerInit;
        [SerializeField] private OpenningTimeline _timeline;
        [SerializeField] private InGameTimer _timer;
    }
}
