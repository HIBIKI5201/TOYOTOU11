using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class SkillResurrection: SkillBase
    {
        public override void Execute(PlayerManager self, PlayerManager other)
        {
            self.transform.position = _position;
            self.Rigidbody.linearVelocity *= _damping;
            if (_particle) { Object.Instantiate(_particle, _position, Quaternion.identity); }
        }

        [SerializeField,Tooltip("ワープ位置")]
        private Vector3 _position;
        [SerializeField, Tooltip("速度減衰率"), Range(0,1)]
        private float _damping = 0.5f;

        [SerializeField, Tooltip("演出")]
        private GameObject _particle;
    }
}
