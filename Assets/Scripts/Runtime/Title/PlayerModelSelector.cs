using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// プレイヤーキャラクターの表示モデルを選択・切り替えを管理するクラス
    /// </summary>
    public class PlayerModelSelector : MonoBehaviour
    {
        /// <summary>
        /// 現在選択されているプレイヤーモデルのコントローラーを取得します
        /// </summary>
        /// <returns>選択中のモデルコントローラー</returns>
        public PlayerModelController GetSelectedModel() => _characterModels[_index];

        /// <summary>
        /// 現在選択されているインデックスを取得します
        /// </summary>
        public int Index => _index;

        /// <summary>
        /// 指定された方向にモデルを切り替えます
        /// </summary>
        /// <param name="selector">切り替え方向（正で次、負で前）</param>
        public void Next(float selector)
        {
            int dir = (int)Mathf.Sign(selector);
            Change(_index + dir);
        }

        /// <summary>
        /// 指定されたインデックスのモデルに切り替えます
        /// </summary>
        /// <param name="index">切り替え先のインデックス</param>
        public void Change(int index)
        {
            index = (index + _characterModels.Length) % _characterModels.Length;

            PlayerModelController model = _characterModels[index];
            ActivateModel(model);
            DeactivateModel(_characterModels[_index]);
            _index = index;
        }

        [SerializeField] private DataRepository _repo;
        private PlayerModelController[] _characterModels;

        private int _index;

        private void Start()
        {
            PlayerStatus[] statuses = _repo.States;
            _characterModels = new PlayerModelController[statuses.Length];
            for (int i = 0; i < statuses.Length; i++)
            {
                PlayerModelController model = Instantiate(statuses[i].Model, transform);
                DeactivateModel(model);
                _characterModels[i] = model;
            }

            ActivateModel(_characterModels[0]);
        }

        private void ActivateModel(PlayerModelController model)
        {
            model.gameObject.SetActive(true);
        }

        private void DeactivateModel(PlayerModelController model)
        {
            model.gameObject.SetActive(false);
        }
    }
}
