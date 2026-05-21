using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class SkillFire : SkillBase
    {
        public override async void Execute(PlayerManager self, PlayerManager other)
        {
            for (int i = 0; i < _count; i++)
            {
                other.TakeDamage(_damage);
                await Awaitable.WaitForSecondsAsync(_duration / _count);
            }
        }

        [SerializeField] private float _duration;
        [SerializeField] private float _damage;
        [SerializeField] private float _count;
    }
}
