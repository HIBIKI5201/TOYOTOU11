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
        /// 現在選択されているスキルを取得
        /// </summary>
        public SkillBase GetSelectedSkill() => _skillBases[_index];

        /// <summary>
        /// 現在選択されているIndex
        /// </summary>
        public int Index => _index;

        /// <summary>
        /// 次/前へ移動
        /// </summary>
        public void Next(float selector)
        {
            int dir = (int)Mathf.Sign(selector);

            if (dir == 0)
                return;

            int nextIndex = _index;

            for (int i = 0; i < _skillBases.Length; i++)
            {
                nextIndex += dir;

                // ループ
                nextIndex = (nextIndex + _skillBases.Length) % _skillBases.Length;

                // 相手と被っていなければ採用。
                if (_other == null || nextIndex != _other.Index)
                {
                    Change(nextIndex);
                    return;
                }
            }
        }

        /// <summary>
        /// 指定Indexへ変更
        /// </summary>
        public void Change(int index)
        {
            index = (index + _skillBases.Length) % _skillBases.Length;

            // 相手と被るなら変更しない
            if (_other != null && index == _other.Index)
                return;

            SkillBase skill = _skillBases[index];

            ApplyUI(skill);

            _index = index;
        }

        [SerializeField] private SkillDataRepository _repo;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;
        [SerializeField] private int _index;
        [SerializeField] private SkillSelector _other;


        private SkillBase[] _skillBases;

        private void Start()
        {
            _skillBases = new SkillBase[]
            {
                _repo.Skill1,
                _repo.Skill2,
                _repo.Skill3,
                _repo.Skill4
            };

            ApplyUI(_skillBases[_index]);
        }

        private void ApplyUI(SkillBase skill)
        {
            _text.text = skill.Explaion;
            _image.sprite = skill.Icon;
        }
    }
}