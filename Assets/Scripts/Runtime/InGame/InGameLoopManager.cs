using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class InGameLoopManager : MonoBehaviour
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
            _playerInit.PlayerControlEnable();

            Task timerTask = _timer.WaitTimeUp().AsTask();
            Task playerTask = _playerInit.WaitAnyPlayerDead().AsTask();
            await Task.WhenAny(timerTask, playerTask);

        }

        [SerializeField] private PlayerInitializer _playerInit;
        [SerializeField] private OpenningTimeline _timeline;
        [SerializeField] private InGameTimer _timer;
    }
}
