using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    ///     インゲームのプレイヤー全体を管理するクラス。
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
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
        }

        /// <summary>
        ///     ダメージを受ける。
        /// </summary>
        /// <param name="Attack"></param>
        public void TakeDamage(float Attack)
        {
            _maxHitPoint -= Attack;
            if (_maxHitPoint < 0)
            {
                Destroy(this);
            }
        }

        private float _maxHitPoint;
        private float _attackPower;
        private float _bounceForce;
        private float _weight;
        private float _acceleration = 0.5f;
        private float _maxSpeed;

        [SerializeField] private Vector3 _startSpeed;
        [SerializeField] private InputActionKeyConfig _keyConfig;

        private Rigidbody _rb;
        private Vector3 _moveDirection;
        private Vector3 _addVelocity;
        private Vector3 _currentMoveVelocity;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _currentMoveVelocity = new Vector3(_startSpeed.x, 0, _startSpeed.z);
        }

        private void Update()
        {
            if (_keyConfig == null) { return; }

            Vector2 input = _keyConfig.GetMoveValue();
            _moveDirection = new Vector3(input.x, 0, input.y);
            _addVelocity = _moveDirection * _acceleration;
        }

        private void FixedUpdate()
        {
            Move();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (gameObject.CompareTag("Player"))
            {
                PlayerManager PlayerRotation = collision.gameObject.GetComponent<PlayerManager>();
                if (PlayerRotation != null)
                {
                    float power = _rb.linearVelocity.sqrMagnitude * _attackPower;
                    PlayerRotation.TakeDamage(power);

                    if (!collision.gameObject.TryGetComponent(out Rigidbody otherRb)) { return; }

                    ContactPoint contact = collision.contacts[0];
                    Vector3 pushDirection = contact.normal;
                    _rb.AddForce(pushDirection * _bounceForce, ForceMode.Impulse);
                    otherRb.AddForce(pushDirection * _bounceForce, ForceMode.Impulse);
                }
            }
        }

        private void Move()
        {
            _currentMoveVelocity += _addVelocity;

            float magnitude = _currentMoveVelocity.magnitude;
            if (_maxSpeed < magnitude) // 最大速度を超えないようにする。
            {
                _currentMoveVelocity *= _maxSpeed / magnitude;
            }

            Vector3 velocity = new(_currentMoveVelocity.x, _rb.linearVelocity.y, _currentMoveVelocity.z);
            _rb.linearVelocity = velocity;
        }
    }
}