using System;
using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// インゲームのメインループ（初期化から終了まで）を制御するクラス
    /// </summary>
    public class InGameLoopManager : MonoBehaviour
    {
        /// <summary>
        /// ゲームを開始し、終了条件が満たされるまで待機して結果表示を行います
        /// </summary>
        public async void GameStart()
        {
            try
            {
                _playerInit.Init(_gm);
                _uiManager.Init();

                await _timeline.Play();
                _uiManager.Visible();
                _timer.StartTimer();
                _playerInit.PlayerControlEnable();

                Task timerTask = _timer.WaitTimeUp(destroyCancellationToken).AsTask();
                Task playerTask = _playerInit.WaitAnyPlayerDead(destroyCancellationToken).AsTask();
                await Task.WhenAny(timerTask, playerTask);
                _result.JudgeWinner(_playerInit);

                await Awaitable.WaitForSecondsAsync(5, destroyCancellationToken);
                _gm.GameReset();
            }
            catch (OperationCanceledException) { }
        }

        [SerializeField] private GameManager _gm;
        [SerializeField] private PlayerInitializer _playerInit;
        [SerializeField] private OpenningTimeline _timeline;
        [SerializeField] private InGameTimer _timer;
        [SerializeField] private ResultManager _result;
        [SerializeField] private InGameUIManager _uiManager;
    }
}
