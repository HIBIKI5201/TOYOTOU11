using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// ゲーム終了後の結果判定とリザルトUIの表示を管理するクラス。
    /// </summary>
    public class ResultManager : MonoBehaviour
    {
        /// <summary>
        /// ゲーム終了アクション（ボタン押下など）がトリガーされた時に発行されるイベント。
        /// </summary>
        public event Action OnGameEndTriggered;

        /// <summary>
        /// プレイヤーの残りHPを基に勝敗を判定し、対応するUIを表示します。
        /// </summary>
        /// <param name="playerInitializer">プレイヤー情報を持つ初期化クラス</param>
        public void JudgeWinner(PlayerInitializer playerInitializer)
        {
            PlayerManager player1 = playerInitializer.Player1;
            PlayerManager player2 = playerInitializer.Player2;
            float player1HP = player1.RemainHitPoint;
            float player2HP = player2.RemainHitPoint;

            bool isPlayer1Dead = player1HP <= 0;
            bool isPlayer2Dead = player2HP <= 0;

            if (isPlayer1Dead && isPlayer2Dead)
            {
                SetActiveDrawUI();
                return;
            }

            if (isPlayer1Dead)
            {
                SetActivePlayer2UI();
                return;
            }

            if (isPlayer2Dead)
            {
                SetActivePlayer1UI();
                return;
            }

            if (player1HP == player2HP)
            {
                SetActiveDrawUI();
            }
            else
            {
                if (player1HP > player2HP)
                {
                    SetActivePlayer1UI();
                }
                else
                {
                    SetActivePlayer2UI();
                }
            }
        }

        /// <summary>
        /// プレイヤー1勝利時のUIを活性化します。
        /// </summary>
        public void SetActivePlayer1UI() => _player1WinUI.ForEach(go => go.SetActive(true));

        /// <summary>
        /// プレイヤー2勝利時のUIを活性化します。
        /// </summary>
        public void SetActivePlayer2UI() => _player2WinUI.ForEach(go => go.SetActive(true));

        /// <summary>
        /// 引き分け時のUIを活性化します。
        /// </summary>
        public void SetActiveDrawUI() => _drawUI.ForEach(go => go.SetActive(true));

        /// <summary>
        /// ゲーム終了トリガーが引かれるまで待機する非同期タスク。
        /// </summary>
        /// <returns></returns>
        public async ValueTask WaitPressedGameEnd()
        {
            TaskCompletionSource<byte> task = new();

            Action onTriggerdAction = null;
            onTriggerdAction = () =>
            {
                OnGameEndTriggered -= onTriggerdAction;
                task.SetResult(0);
            };

            OnGameEndTriggered += onTriggerdAction;

            await task.Task;
        }

        [SerializeField, Tooltip("プレイヤー1勝利時に表示するUIオブジェクトのリスト")] private List<GameObject> _player1WinUI;
        [SerializeField, Tooltip("プレイヤー2勝利時に表示するUIオブジェクトのリスト")] private List<GameObject> _player2WinUI;
        [SerializeField, Tooltip("引き分け時に表示するUIオブジェクトのリスト")] private List<GameObject> _drawUI;

        [ContextMenu("Inactive UI")]
        private void Awake()
        {
            _player1WinUI.ForEach(go => go.SetActive(false));
            _player2WinUI.ForEach(go => go.SetActive(false));
            _drawUI.ForEach(go => go.SetActive(false));
        }
    }
}
