using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// タイトル画面の全体的な管理と、ゲーム開始時の初期設定を行うクラス
    /// </summary>
    public class TitleManager : MonoBehaviour
    {
        [Tooltip("ゲーム全体の状態を管理するGameManager")]
        [SerializeField] private GameManager _gm;
        [Tooltip("インゲームのループ処理を管理するコンポーネント")]
        [SerializeField] private InGameLoopManager _ingameLoop;
        [Tooltip("プレイヤーのステータスなどのデータを保持するリポジトリ")]
        [SerializeField] private DataRepository _dataRepository;
        [Tooltip("ゲーム開始時の演出シーケンスを管理するコンポーネント")]
        [SerializeField] private GameStartSequence _sequence;

        [Tooltip("プレイヤー1の入力アクション設定")]
        [SerializeField] private InputActionKeyConfig _player1;
        [Tooltip("プレイヤー2の入力アクション設定")]
        [SerializeField] private InputActionKeyConfig _player2;

        [Tooltip("プレイヤー1のモデル選択を管理するコンポーネント")]
        [SerializeField] private PlayerModelSelector _selector1;
        [Tooltip("プレイヤー2のモデル選択を管理するコンポーネント")]
        [SerializeField] private PlayerModelSelector _selector2;

        [Tooltip("プレイヤー1のUIナビゲーションを管理するクラス")]
        [SerializeField] private UINavigater _player1Navigater;
        [Tooltip("プレイヤー2のUIナビゲーションを管理するクラス")]
        [SerializeField] private UINavigater _player2Navigater;

        /// <summary>
        /// 選択された情報をゲーム状態に反映し、インゲームを開始します
        /// </summary>
        [ContextMenu(nameof(GameStart))]
        public void GameStart()
        {
            GameState state = _gm.State;
            state.Player1Model = _selector1.GetSelectedModel();
            state.Player2Model = _selector2.GetSelectedModel();
            state.Player1Status = _dataRepository.States[_selector1.Index];
            state.Player2Status = _dataRepository.States[_selector2.Index];

            _gm.SetActiveIngameObjs(true);
            _ingameLoop.GameStart();
            _gm.SetActiveTitleObjs(false);
        }

        private void OnEnable()
        {
            _player1Navigater.Enable(_player1);
            _player2Navigater.Enable(_player2);
            _sequence.OnSequencePlayed += GameStart;
        }

        private void OnDisable()
        {
            _player1Navigater.Disable();
            _player2Navigater.Disable();
        }
    }
}
