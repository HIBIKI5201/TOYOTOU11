using SymphonyFrameWork.Attribute;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    [CreateAssetMenu(fileName = nameof(SkillDataRepository), menuName = nameof(SkillDataRepository))]
    public class SkillDataRepository : ScriptableObject
    {
        public SkillBase Skill1 => _skill1;
        public SkillBase Skill2 => _skill2;
        public SkillBase Skill3 => _skill3;
        public SkillBase Skill4 => _skill4;

        [SerializeReference, SubclassSelector] private SkillBase _skill1;
        [Space]
        [SerializeReference, SubclassSelector] private SkillBase _skill2;
        [Space]
        [SerializeReference, SubclassSelector] private SkillBase _skill3;
        [Space]
        [SerializeReference, SubclassSelector] private SkillBase _skill4;
    }
}
