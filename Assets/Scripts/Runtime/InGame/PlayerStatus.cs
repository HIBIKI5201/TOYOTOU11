using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// プレイヤーの基本パラメータを定義するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = nameof(PlayerStatus), menuName = nameof(PlayerStatus))]
    public class PlayerStatus : ScriptableObject
    {
        /// <summary>
        /// プレイヤーのモデルプリセット
        /// </summary>
        public PlayerModelController Model => _model;

        /// <summary>
        /// 最大体力
        /// </summary>
        public float MaxHitPoint => _maxHitPoint;

        /// <summary>
        /// 攻撃力
        /// </summary>
        public float AttackPower => _attackPower;

        /// <summary>
        /// 衝突時の跳ね返り係数
        /// </summary>
        public float BounceForce => _bounceForce;

        /// <summary>
        /// プレイヤーの重量（物理演算に影響）
        /// </summary>
        public float Weight => _weight;

        /// <summary>
        /// 最大移動速度
        /// </summary>
        public float MaxSpeed => _maxSpeed;

        /// <summary>
        /// 移動時の加速度
        /// </summary>
        public float Acceleration => _acceleration;

        [SerializeField] private PlayerModelController _model;
        [SerializeField] private float _maxHitPoint = 10;
        [SerializeField] private float _attackPower = 10;
        [SerializeField] private float _bounceForce = 10f;
        [SerializeField] private float _weight = 1f;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _acceleration = 0.5f;
    }
}