using System;
using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class PlayerInitializer : MonoBehaviour
    {
        public PlayerManager Player1 => _player1Manager;
        public PlayerManager Player2 => _player2Manager;

        public void Init(PlayerStatus status1 = null, PlayerStatus status2 = null)
        {
            if (status1 == null) { status1 = _defaultPlayer1Status; }
            if (status2 == null) { status2 = _defaultPlayer2Status; }

            _player1Manager.Init(status1);
            _player2Manager.Init(status2);
        }

        public void PlayerControlEnable()
        {
            _player1Manager.SetCanControl(true);
            _player2Manager.SetCanControl(true);
        }

        /// <summary>
        ///     プレイヤーのどちらかが倒されるまで終わらないタスク。
        /// </summary>
        /// <returns></returns>
        public async ValueTask WaitAnyPlayerDead()
        {
            TaskCompletionSource<byte> task = new();

            Action onDeadAction = null;
            onDeadAction = () =>
            {
                _player1Manager.OnDead -= onDeadAction;
                _player2Manager.OnDead -= onDeadAction;
                task.SetResult(0);
            };

            _player1Manager.OnDead += onDeadAction;
            _player2Manager.OnDead += onDeadAction;

            await task.Task;
        }

        [SerializeField] private PlayerManager _player1Manager;
        [SerializeField] private PlayerManager _player2Manager;
        [SerializeField] private PlayerStatus _defaultPlayer1Status;
        [SerializeField] private PlayerStatus _defaultPlayer2Status;
    }
}
