using System;
using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class InGameLoopManager : MonoBehaviour
    {
        public async void GameStart()
        {
            try
            {
                _playerInit.Init();

                await _timeline.Play();
                _timer.StartTimer();
                _playerInit.PlayerControlEnable();

                Task timerTask = _timer.WaitTimeUp(destroyCancellationToken).AsTask();
                Task playerTask = _playerInit.WaitAnyPlayerDead(destroyCancellationToken).AsTask();
                await Task.WhenAny(timerTask, playerTask);
                _result.JudgeWinner(_playerInit);
            }
            catch (OperationCanceledException) { }
        }

        [SerializeField] private PlayerInitializer _playerInit;
        [SerializeField] private OpenningTimeline _timeline;
        [SerializeField] private InGameTimer _timer;
        [SerializeField] private ResultManager _result;
    }
}
