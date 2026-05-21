using System.Text;
using TMPro;
using UnityEditor;
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
            UpdateText(_repo.States[index]);
            DeactivateModel(_characterModels[_index]);
            _index = index;
        }

        [Tooltip("プレイヤーのステータスデータを参照するためのリポジトリ")]
        [SerializeField] private DataRepository _repo;
        [SerializeField] private TMP_Text _text;
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
            UpdateText(_repo.States[0]);
        }

        private void ActivateModel(PlayerModelController model)
        {
            model.gameObject.SetActive(true);
        }

        private void DeactivateModel(PlayerModelController model)
        {
            model.gameObject.SetActive(false);
        }

        private void UpdateText(PlayerStatus status)
        {
            StringBuilder sb = new();
            sb.AppendLine($"体力:{status.MaxHitPoint}");
            sb.AppendLine($"攻撃:{status.AttackPower}");
            sb.AppendLine($"最速:{status.MaxSpeed}");
            sb.AppendLine($"加速:{status.Acceleration}");
            sb.AppendLine($"反射:{status.BounceForce}");
            _text.text = sb.ToString();
        }
    }
}
