using UnityEngine;
using UnityEngine.Events;

namespace TOYOTOU.Runtime
{
    public class ConflictResolver : MonoBehaviour
    {
        public UnityEvent OnConflicted;

        public void Init(PlayerManager player1, PlayerManager player2)
        {
            _player1 = player1;
            _player2 = player2;

            _player1.OnConflicted += ConflictHandler;
        }

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

        [SerializeField] private float _hitStopTime = 0.2f;
        [SerializeField] private float _hitJump = 5;

        private PlayerManager _player1;
        private PlayerManager _player2;

        private void ResolveRigidBody(Rigidbody rb, Vector3 dir, float force)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(dir * force + Vector3.up * _hitJump, ForceMode.Impulse);
        }
    }
}
