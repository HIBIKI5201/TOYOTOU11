using UnityEngine;
using UnityEngine.InputSystem;

namespace TOYOTOU.Runtime
{
    public class PlayerMoving : MonoBehaviour
    {
        [SerializeField] public int Hitpoint = 10;
        [SerializeField] public int Attack = 10;
        [SerializeField] public float bounceForce = 10f;

        [Header("加速力")]
        [SerializeField] private float _addSpeed = 0.5f;

        [Header("最大速度")]
        [SerializeField, Tooltip("最大速度")] private Vector3 _maxSpeed;

        [SerializeField] private Vector3 _startSpeed;

        private Rigidbody _rb;
        private Vector3 _moveDirection;
        private Vector3 _addVelocity;
        private Vector3 _currentMoveVelocity;

        public GameObject Bey;

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
            _addVelocity = _moveDirection * _addSpeed;
        }

        private void FixedUpdate()
        {
            Move();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (gameObject.CompareTag("Player"))
            {
                PlayerMoving PlayerRotation = collision.gameObject.GetComponent<PlayerMoving>();
                if (PlayerRotation != null)
                {
                    PlayerRotation.TakeDamage(Attack);
                    Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
                    if (otherRb != null)
                    {
                        ContactPoint contact = collision.contacts[0];
                        Vector3 pushDirection = contact.normal;
                        GetComponent<Rigidbody>().AddForce(pushDirection * bounceForce, ForceMode.Impulse);
                        otherRb.AddForce(pushDirection * bounceForce, ForceMode.Impulse);

                    }
                }
            }
        }

        public void TakeDamage(int Attack)
        {
            Hitpoint -= Attack;
            if (Hitpoint < 0)
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