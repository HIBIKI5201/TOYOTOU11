using UnityEngine;
using UnityEngine.InputSystem;

namespace TOYOTOU.Runtime
{
    public class PlayerManager : MonoBehaviour
    {
        public void Init(PlayerStatus playerStatus)
        {
            _maxHitPoint = playerStatus.MaxHitPoint;
            _attackPower = playerStatus.AttackPower;
            _bounceForce = playerStatus.BounceForce;
            _weight = playerStatus.Weight;
            _maxSpeed = new Vector3(playerStatus.MaxSpeed, 0, playerStatus.MaxSpeed);
            _acceleration = playerStatus.Acceleration;
        }

        private float _maxHitPoint;
        private float _attackPower;
        private float _bounceForce;
        private float _weight;
        private float _acceleration = 0.5f;
        private Vector3 _maxSpeed;

        [SerializeField] private Vector3 _startSpeed;

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
            float x = 0;
            float z = 0;

            Keyboard keyboard = Keyboard.current;

            if (keyboard == null) return;

            var wKey = keyboard.wKey;
            var aKey = keyboard.aKey;
            var dKey = keyboard.dKey;
            var sKey = keyboard.sKey;

            if (wKey.IsPressed())// 前s
            {
                x = 1;
            }
            else if (sKey.IsPressed()) // 後ろ
            {
                x = -1;
            }

            if (dKey.IsPressed())// 右
            {
                z = 1;
            }
            else if (aKey.IsPressed())// 左
            {
                z = -1;
            }

            _moveDirection = new Vector3(x, 0, z);
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
                    PlayerRotation.TakeDamage(_attackPower);
                    Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
                    if (otherRb != null)
                    {
                        ContactPoint contact = collision.contacts[0];
                        Vector3 pushDirection = contact.normal;
                        GetComponent<Rigidbody>().AddForce(pushDirection * _bounceForce, ForceMode.Impulse);
                        otherRb.AddForce(pushDirection * _bounceForce, ForceMode.Impulse);

                    }
                }
            }
        }

        public void TakeDamage(float Attack)
        {
            _maxHitPoint -= Attack;
            if (_maxHitPoint < 0)
            {
                Destroy(this);
            }
        }

        private void Move()
        {
            _currentMoveVelocity += _addVelocity;
            _rb.linearVelocity = new Vector3(_currentMoveVelocity.x, _rb.linearVelocity.y, _currentMoveVelocity.z);
        }
    }
}