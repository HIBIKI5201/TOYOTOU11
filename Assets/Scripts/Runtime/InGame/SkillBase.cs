using TOYOTOU.Runtime;
using UnityEngine;

namespace TOYOTOU
{
    public abstract class SkillBase
    {
        public float CoolTime => _cooltime;

        public abstract void Execute(PlayerManager self, PlayerManager other);

        [SerializeField]
        protected float _cooltime;
    }
}
