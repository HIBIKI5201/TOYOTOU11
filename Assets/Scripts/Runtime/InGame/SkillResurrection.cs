using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class SkillResurrection: SkillBase
    {
        public override async void Execute(PlayerManager self, PlayerManager other)
        {
            Debug.Log($"蘇るスキルを発動");

            self.transform.position = _position;
            self.Rigidbody.linearVelocity *= _damping;
            GameObject particle = Object.Instantiate(_particle, _position, Quaternion.identity);
            await Awaitable.WaitForSecondsAsync(4);
            Object.Destroy(particle);
        }

        [SerializeField,Tooltip("ワープ位置")]
        private Vector3 _position;
        [SerializeField, Tooltip("速度減衰率"), Range(0,1)]
        private float _damping = 0.5f;

        [SerializeField, Tooltip("演出")]
        private GameObject _particle;
    }
}
