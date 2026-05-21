using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// スキルの選択を管理するクラス
    /// </summary>
    public class SkillSelector : MonoBehaviour
    {
        /// <summary>
        /// 現在選択されているプレイヤーモデルのコントローラーを取得します
        /// </summary>
        /// <returns>選択中のモデルコントローラー</returns>
        public SkillBase GetSelectedSkill() => _skillBases[_index];

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
            index = (index + _skillBases.Length) % _skillBases.Length;

            SkillBase model = _skillBases[index];
            ApplyUI(model);
            _index = index;
        }

        [SerializeField] private SkillDataRepository _repo;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;
        [SerializeField] private int _index;

        private SkillBase[] _skillBases;

        private void Start()
        {
            _skillBases = new SkillBase[] { _repo.Skill1, _repo.Skill2, _repo.Skill3, _repo.Skill4 };

            ApplyUI(_skillBases[_index]);
        }

        private void ApplyUI(SkillBase skill)
        {
            _text.text = skill.Explaion;
            _image.sprite = skill.Icon;
        }
    }
}
