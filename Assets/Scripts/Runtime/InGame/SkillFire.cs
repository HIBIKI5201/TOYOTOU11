using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class SkillFire : SkillBase
    {
        public override async void Execute(PlayerManager self, PlayerManager other)
        {
            Debug.Log($"燃えるスキルを発動");
            GameObject particle = Object.Instantiate(_particle, other.transform.position, Quaternion.identity, other.transform);

            for (int i = 0; i < _count; i++)
            {
                other.TakeDamage(_damage);
                await Awaitable.WaitForSecondsAsync(_duration / _count);
            }

            Object.Destroy(particle);
        }

        [SerializeField] private float _duration;
        [SerializeField] private float _damage;
        [SerializeField] private float _count;

        [SerializeField] private GameObject _particle;
    }
}
