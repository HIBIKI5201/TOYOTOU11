using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class SkillResurrection: SkillBase
    {
        [SerializeField,Tooltip("ワープ位置")]
        private Vector3 _position;
        [SerializeField, Tooltip("速度減衰率"), Range(0,1)]
        private float _Damping = 0.5f;
    }
}
