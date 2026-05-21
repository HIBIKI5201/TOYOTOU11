using UnityEngine;
using UnityEngine.UI;

namespace TOYOTOU.Runtime
{
    public class SkillCoolTimeGuageManager : MonoBehaviour
    {
        public void Init()
        {
            if (_kind == Kind.Skill1)
            {
                _manager.OnSkill1CoolTimeChanged += OnValueChanged;
                _icon.sprite = _manager.Skill1.Icon;
            }
            else
            {
                _manager.OnSkill2CoolTimeChanged += OnValueChanged;
                _icon.sprite = _manager.Skill2.Icon;
            }
        }

        [SerializeField] private PlayerManager _manager;
        [SerializeField] private Kind _kind;
        [SerializeField] private Image _guage;
        [SerializeField] private Image _icon;


        private void OnValueChanged(float current, float max)
        {
            _guage.fillAmount = current / max;
        }

        private enum Kind
        {
            Skill1,
            Skill2,
        }
    }
}
