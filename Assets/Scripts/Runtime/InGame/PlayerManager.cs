using SymphonyFrameWork.Attribute;
using System;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// インゲームのプレイヤー個体を管理するクラス
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        /// <summary>
        /// プレイヤーが死亡した際に通知されるイベント
        /// </summary>
        public event Action OnDead;

        /// <summary>
        /// プレイヤー同士が衝突した際に通知されるイベント
        /// </summary>
        public event Action<PlayerManager, PlayerManager> OnConflicted;

        /// <summary>
        /// プレイヤーの体力が変化した時に通知されるイベント
        /// 第一引数が現在値、第二引数が最大値
        /// </summary>
        public event Action<float, float> OnHitPointChanged;

        public event Action<float, float> OnSpeedChanged;

        /// <summary>
        /// 物理演算用のRigidbodyを取得します
        /// </summary>
        public Rigidbody Rigidbody => _rb;

        /// <summary>
        /// 直前のフレームでの速度を取得します
        /// </summary>
        public float PreviousVelocity => _previousVelocity;

        /// <summary>
        /// プレイヤーの攻撃力を取得します
        /// </summary>
        public float AttackPower => _attackPower;

        /// <summary>
        /// プレイヤーの跳ね返り係数を取得します
        /// </summary>
        public float BounceForce => _bounceForce;

        /// <summary>
        /// 残りの体力を取得します
        /// </summary>
        public float RemainHitPoint => _remainHitPoint;

        /// <summary>
        /// プレイヤーを初期化します
        /// </summary>
        /// <param name="playerStatus">プレイヤーの基本ステータス</param>
        /// <param name="model">プレイヤーの表示モデル制御</param>
        public void Init(PlayerStatus playerStatus, PlayerModelController model, SkillBase skill1, SkillBase skill2)
        {
            _model = model;
            _maxHitPoint = playerStatus.MaxHitPoint;
            _attackPower = playerStatus.AttackPower;
            _bounceForce = playerStatus.BounceForce;
            _weight = playerStatus.Weight;
            _maxSpeed = playerStatus.MaxSpeed;
            _acceleration = playerStatus.Acceleration;

            _remainHitPoint = _maxHitPoint;
            _rb.mass = _weight;

            _skill1 = skill1;
            _skill2 = skill2;
            model.SetParent(_rotater.transform);
        }

        /// <summary>
        /// 操作可能状態を変更します
        /// </summary>
        /// <param name="value">trueで操作可能、falseで操作不能</param>
        public void SetCanControl(bool value) => _canControl = value;

        /// <summary>
        /// 物理演算と回転処理を一時停止（スリープ）させます
        /// </summary>
        public void Sleep()
        {
            _preSleepVelocity = _rb.linearVelocity;
            _rb.Sleep();
            _rotater.Sleep();
        }

        /// <summary>
        /// 一時停止状態から復帰（ウェイクアップ）させます
        /// </summary>
        public void WakeUp()
        {
            _rotater.WakeUp();
            _rb.WakeUp();
            _rb.linearVelocity = _preSleepVelocity;
        }

        /// <summary>
        /// 指定されたダメージを適用します
        /// </summary>
        /// <param name="damage">受けるダメージ量</param>
        public void TakeDamage(float damage)
        {
            _remainHitPoint -= damage;
            OnHitPointChanged?.Invoke(_remainHitPoint, _maxHitPoint);
            Debug.Log($"{name}が{damage}ダメージ食らった");
            if (_remainHitPoint < 0)
            {
                OnDead?.Invoke();
                Debug.Log($"{name}が死亡");
            }
        }

        [Tooltip("プレイヤーの入力アクション設定")]
        [SerializeField] private InputActionKeyConfig _keyConfig;
        [Tooltip("プレイヤーを識別するためのタグ")]
        [SerializeField, TagSelector] private string _playerTag;
        [Tooltip("プレイヤーの回転制御を管理するコンポーネント")]
        [SerializeField] private RotatePlayer _rotater;

        private float _maxHitPoint;
        private float _remainHitPoint;
        private float _attackPower;
        private float _bounceForce;
        private float _weight;
        private float _acceleration = 0.5f;
        private float _maxSpeed;

        private PlayerModelController _model;
        private Rigidbody _rb;
        private bool _canControl;
        private Vector3 _addVelocity;
        private float _previousVelocity;
        private Vector3 _preSleepVelocity;
        private SkillBase _skill1;
        private SkillBase _skill2;

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
            OnSpeedChanged?.Invoke(magnitude, _maxSpeed);

            velocity = new(velocity.x, originY, velocity.z);
            _rb.linearVelocity = velocity;
        }
    }
}