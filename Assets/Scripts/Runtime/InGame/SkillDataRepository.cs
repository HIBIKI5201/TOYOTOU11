using SymphonyFrameWork.Attribute;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    [CreateAssetMenu(fileName = nameof(SkillDataRepository), menuName = nameof(SkillDataRepository))]
    public class SkillDataRepository : ScriptableObject
    {
        public string Skill1Name => _skill1Name;
        public float Skill1Cooldown => _skill1Cooldown;
        public string Skill2Name => _skill2Name;
        public float Skill2Cooldown => _skill2Cooldown;
        public string Skill3Name => _skill3Name;
        public float Skill3Cooldown => _skill3Cooldown;
        public string Skill4Name => _skill4Name;
        public float Skill4Cooldown => _skill4Cooldown;

        [SerializeReference, SubclassSelector] private SkillBase _skill1;
        [SerializeField] private string _skill1Name;
        [SerializeField] private Texture2D _skill1Icon;
        [SerializeField] private string _skill1Explain;
        [SerializeField] private float _skill1Cooldown;
        [Space]
        [SerializeReference, SubclassSelector] private SkillBase _skill2;
        [SerializeField] private string _skill2Name;
        [SerializeField] private Texture2D _skill2Icon;
        [SerializeField] private string _skill2Explain;
        [SerializeField] private float _skill2Cooldown;
        [Space]
        [SerializeReference, SubclassSelector] private SkillBase _skill3;
        [SerializeField] private string _skill3Name;
        [SerializeField] private Texture2D _skill3Icon;
        [SerializeField] private string _skill3Explain;
        [SerializeField] private float _skill3Cooldown;
        [Space]
        [SerializeReference, SubclassSelector] private SkillBase _skill4;
        [SerializeField] private string _skill4Name;
        [SerializeField] private Texture2D _skill4Icon;
        [SerializeField] private string _skill4Explain;
        [SerializeField] private float _skill4Cooldown;
    }
}
