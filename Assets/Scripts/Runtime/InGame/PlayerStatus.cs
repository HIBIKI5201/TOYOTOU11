using UnityEngine;

namespace TOYOTOU.Runtime
{
    [CreateAssetMenu(fileName = nameof(PlayerStatus), menuName = nameof(PlayerStatus))]
    public class PlayerStatus : ScriptableObject
    {
        public CharacterModelController Model => _model;
        public float MaxHitPoint => _maxHitPoint;
        public float AttackPower => _attackPower;
        public float BounceForce => _bounceForce;
        public float Weight => _weight;
        public float MaxSpeed => _maxSpeed;
        public float Acceleration => _acceleration;

        [SerializeField] private CharacterModelController _model;
        [SerializeField] private float _maxHitPoint = 10;
        [SerializeField] private float _attackPower = 10;
        [SerializeField] private float _bounceForce = 10f;
        [SerializeField] private float _weight = 1f;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _acceleration = 0.5f;
    }
}