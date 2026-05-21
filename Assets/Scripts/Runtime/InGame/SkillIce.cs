using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class SkillIce : SkillBase
    {
        public override async void Execute(PlayerManager self, PlayerManager other)
        {
            Collider[] colliders = other.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                float origin = collider.material.dynamicFriction;
                collider.material.dynamicFriction *= _damping;

                await Awaitable.WaitForSecondsAsync(_duration);

                collider.material.dynamicFriction = origin;
            }
        }

        [SerializeField]
        private float _duration = 5;
        [SerializeField]
        private float _damping = 0.5f;
    }
}
