using UnityEngine;
using UnityEngine.Events;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// プレイヤー間の衝突（コンフリクト）を解決し、ダメージ計算やノックバック処理を行うクラス。
    /// </summary>
    public class ConflictResolver : MonoBehaviour
    {
        /// <summary>
        /// プレイヤー同士が衝突した際に発行されるイベント。
        /// </summary>
        public UnityEvent OnConflicted;

        /// <summary>
        /// 衝突解決クラスを初期化し、プレイヤーを登録します。
        /// </summary>
        /// <param name="player1">プレイヤー1のマネージャー</param>
        /// <param name="player2">プレイヤー2のマネージャー</param>
        public void Init(PlayerManager player1, PlayerManager player2)
        {
            _player1 = player1;
            _player2 = player2;

            _player1.OnConflicted += ConflictHandler;
        }

        /// <summary>
        /// 衝突時の具体的な処理（ダメージ計算、ヒットストップ、ノックバック）を実行します。
        /// </summary>
        /// <param name="alpha">衝突したプレイヤーA</param>
        /// <param name="beta">衝突したプレイヤーB</param>
        public async void ConflictHandler(PlayerManager alpha, PlayerManager beta)
        {
            float powerAlpha = alpha.PreviousVelocity * alpha.AttackPower;
            float powerBeta = beta.PreviousVelocity * beta.AttackPower;
            alpha.TakeDamage(powerBeta);
            beta.TakeDamage(powerAlpha);

            Vector3 alpha2Beta = (beta.transform.position - alpha.transform.position).normalized;
            alpha2Beta.y = 0;
            alpha2Beta.Normalize();

            OnConflicted?.Invoke();

            alpha.Sleep();
            beta.Sleep();
            await Awaitable.WaitForSecondsAsync(_hitStopTime);
            alpha.WakeUp();
            beta.WakeUp();

            ResolveRigidBody(alpha.Rigidbody, -alpha2Beta, alpha.BounceForce);
            ResolveRigidBody(beta.Rigidbody, alpha2Beta, beta.BounceForce);
        }

        [SerializeField, Tooltip("衝突時のヒットストップ時間（秒）")] private float _hitStopTime = 0.2f;

        private PlayerManager _player1;
        private PlayerManager _player2;

        private void ResolveRigidBody(Rigidbody rb, Vector3 dir, float force)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}
