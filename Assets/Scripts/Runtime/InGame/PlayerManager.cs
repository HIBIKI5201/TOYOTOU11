using SymphonyFrameWork.Attribute;
using System;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    ///     インゲームのプレイヤー全体を管理するクラス。
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        public event Action OnDead;
        public event Action<PlayerManager, PlayerManager> OnConflicted;

        public Rigidbody Rigidbody => _rb;
        public float PreviousVelocity => _previousVelocity;
        public float AttackPower => _attackPower;
        public float BounceForce => _bounceForce;
        public float RemainHitPoint => _remainHitPoint;

        /// <summary>
        ///     初期化する。
        /// </summary>
        /// <param name="playerStatus"></param>
        public void Init(PlayerStatus playerStatus)
        {
            _maxHitPoint = playerStatus.MaxHitPoint;
            _attackPower = playerStatus.AttackPower;
            _bounceForce = playerStatus.BounceForce;
            _weight = playerStatus.Weight;
            _maxSpeed = playerStatus.MaxSpeed;
            _acceleration = playerStatus.Acceleration;

            _remainHitPoint = _maxHitPoint;
            _rb.mass = _weight;
        }

        /// <summary>
        ///     操作可能状態を変更する。
        /// </summary>
        /// <param name="value"></param>
        public void SetCanControl(bool value) => _canControl = value;

        public void Sleep()
        {
            _rb.Sleep();
        }

        public void WakeUp()
        {
            _rb.WakeUp();
        }

        /// <summary>
        ///     ダメージを受ける。
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(float damage)
        {
            _remainHitPoint -= damage;
            Debug.Log($"{name}が{damage}ダメージ食らった");
            if (_remainHitPoint < 0)
            {
                OnDead?.Invoke();
                Debug.Log($"{name}が死亡");
            }
        }

        [SerializeField] private InputActionKeyConfig _keyConfig;
        [SerializeField, TagSelector] private string _playerTag;

        private float _maxHitPoint;
        private float _remainHitPoint;
        private float _attackPower;
        private float _bounceForce;
        private float _weight;
        private float _acceleration = 0.5f;
        private float _maxSpeed;

        private Rigidbody _rb;
        private Vector3 _addVelocity;
        private float _previousVelocity;
        private bool _canControl;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _keyConfig.Enable();
        }

        private void OnDisable()
        {
            _keyConfig.Disable();
        }

        private void Update()
        {
            if (!_canControl) { return; }
            if (_keyConfig == null) { return; }

            Vector2 input = _keyConfig.GetMoveValue();
            Vector3 moveDirection = new Vector3(input.x, 0, input.y);
            _addVelocity = moveDirection * _acceleration;
        }

        private void FixedUpdate()
        {
            Move(Time.fixedDeltaTime);
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(_playerTag))
            {
                PlayerManager other = collision.gameObject.GetComponent<PlayerManager>();
                OnConflicted?.Invoke(this, other);
            }
        }

        private void Move(float delta)
        {
            Vector3 velocity = _rb.linearVelocity;
            float originY = velocity.y;
            velocity += _addVelocity * delta;

            float magnitude = velocity.magnitude;
            magnitude = Mathf.Abs(magnitude) < 0.0001f ? 0 : magnitude; //ほぼ0なら0にする。
            if (_maxSpeed < magnitude) // 最大速度を超えないようにする。
            {
                velocity *= _maxSpeed / magnitude;
                magnitude = _maxSpeed;
            }
            _previousVelocity = magnitude; // 最後の速度を記録する。

            velocity = new(velocity.x, originY, velocity.z);
            _rb.linearVelocity = velocity;
        }
    }
}