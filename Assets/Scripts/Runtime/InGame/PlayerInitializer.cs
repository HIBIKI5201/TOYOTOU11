using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// インゲーム開始時にプレイヤーの初期化を担当するクラス
    /// </summary>
    public class PlayerInitializer : MonoBehaviour
    {
        /// <summary>
        /// 1PのPlayerManagerを取得します
        /// </summary>
        public PlayerManager Player1 => _player1Manager;

        /// <summary>
        /// 2PのPlayerManagerを取得します
        /// </summary>
        public PlayerManager Player2 => _player2Manager;

        /// <summary>
        /// 指定されたゲームマネージャーの情報を使用してプレイヤーを初期化します
        /// </summary>
        /// <param name="gm">初期化に使用するゲームマネージャー</param>
        public void Init(GameManager gm)
        {
            GameState state = gm.State;
            PlayerStatus status1 = state.Player1Status;
            PlayerStatus status2 = state.Player2Status;

            _player1Manager.Init(status1, state.Player1Model, _player2Manager, state.Player1Skill1, state.Player1Skill2);
            _player2Manager.Init(status2, state.Player2Model, _player1Manager, state.Player2Skill1, state.Player2Skill2);

            _resolver.Init(_player1Manager, _player2Manager);
        }

        /// <summary>
        /// 両プレイヤーの操作を有効にします
        /// </summary>
        public void PlayerControlEnable()
        {
            _player1Manager.SetCanControl(true);
            _player2Manager.SetCanControl(true);
        }

        /// <summary>
        /// プレイヤーのどちらかが倒されるまで待機する非同期タスクです
        /// </summary>
        /// <param name="token">キャンセルトークン</param>
        /// <returns>タスク</returns>
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

        [Tooltip("プレイヤー1の管理コンポーネント")]
        [SerializeField] private PlayerManager _player1Manager;
        [Tooltip("プレイヤー2の管理コンポーネント")]
        [SerializeField] private PlayerManager _player2Manager;
        [Tooltip("プレイヤー間の衝突解決を管理するコンポーネント")]
        [SerializeField] private ConflictResolver _resolver;
    }
}
