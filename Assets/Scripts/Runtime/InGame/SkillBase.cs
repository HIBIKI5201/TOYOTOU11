using TOYOTOU.Runtime;
using UnityEngine;

namespace TOYOTOU
{
    public abstract class SkillBase
    {
        public string Name => _naame;
        public Sprite Icon => _icon;
        public string Explaion => _explain;
        public float CoolTime => _cooltime;

        public abstract void Execute(PlayerManager self, PlayerManager other);

        [SerializeField] private string _naame;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _explain;
        [SerializeField] protected float _cooltime = 5;
    }
}
