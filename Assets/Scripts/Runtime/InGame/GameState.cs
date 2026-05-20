using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// ゲームの動的な状態（プレイヤーの選択情報など）を保持するクラス
    /// </summary>
    public class GameState : MonoBehaviour
    {
        /// <summary>
        /// 1Pのプレイヤーモデル
        /// </summary>
        public PlayerModelController Player1Model;

        /// <summary>
        /// 2Pのプレイヤーモデル
        /// </summary>
        public PlayerModelController Player2Model;

        /// <summary>
        /// 1Pのプレイヤーステータス
        /// </summary>
        public PlayerStatus Player1Status;

        /// <summary>
        /// 2Pのプレイヤーステータス
        /// </summary>
        public PlayerStatus Player2Status;
    }
}
