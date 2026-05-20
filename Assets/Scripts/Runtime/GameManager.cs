using SymphonyFrameWork.Attribute;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// ゲーム全体の進行状況やオブジェクトの活性状態を管理するクラス
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// 現在のゲーム状態を取得します
        /// </summary>
        public GameState State => _state;

        /// <summary>
        /// タイトルに関連するオブジェクトの活性状態を一括設定します
        /// </summary>
        /// <param name="value">活性化する場合はtrue</param>
        public void SetActiveTitleObjs(bool value) => _titleObjes.ForEach(go => go.SetActive(value));

        /// <summary>
        /// インゲームに関連するオブジェクトの活性状態を一括設定します
        /// </summary>
        /// <param name="value">活性化する場合はtrue</param>
        public void SetActiveIngameObjs(bool value) => _ingameObjs.ForEach(go => go.SetActive(value));

        /// <summary>
        /// シーンをロードし直してゲームをリセットします
        /// </summary>
        public void GameReset() => SceneManager.LoadScene(_sceneName);

        [SerializeField] List<GameObject> _titleObjes;
        [SerializeField] List<GameObject> _ingameObjs;
        [SerializeField, SceneNameSelector] string _sceneName;

        private GameState _state;
        private void Awake()
        {
            _state = GetComponent<GameState>();
        }

        private void Start()
        {
            SetActiveTitleObjs(true);
            SetActiveIngameObjs(false);
        }
    }
}
