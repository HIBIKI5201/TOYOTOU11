using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class PlayerInitializer : MonoBehaviour
    {
        public PlayerManager Player1 => _player1Manager;
        public PlayerManager Player2 => _player2Manager;

        public void Init(GameManager gm)
        {
            GameState state = gm.State;
            PlayerStatus status1 = state.Player1Status;
            PlayerStatus status2 = state.Player2Status;

            _player1Manager.Init(status1, state.Player1Model);
            _player2Manager.Init(status2, state.Player2Model);

            _resolver.Init(_player1Manager, _player2Manager);
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
        public async ValueTask WaitAnyPlayerDead(CancellationToken token = default)
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


            CancellationTokenRegistration registration = token.Register(task.SetCanceled);
            try
            {
                await task.Task;
            }
            finally
            {
                registration.Dispose();
            }
        }

        [SerializeField] private PlayerManager _player1Manager;
        [SerializeField] private PlayerManager _player2Manager;
        [SerializeField] private ConflictResolver _resolver;
    }
}
