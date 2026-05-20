using Unity.Collections;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class ConflictResolver : MonoBehaviour
    {
        public void ConflictHandler(PlayerManager alpha, PlayerManager beta)
        {            
            float powerAlpha = alpha.PreviousVelocity * alpha.AttackPower;
            float powerBeta = beta.PreviousVelocity * beta.AttackPower;
            alpha.TakeDamage(powerBeta);
            beta.TakeDamage(powerAlpha);

            Vector3 alpha2Beta = (beta.transform.position - alpha.transform.position).normalized;
            ResolveRigidBody(alpha.Rigidbody, -alpha2Beta, alpha.BounceForce);
            ResolveRigidBody(beta.Rigidbody, alpha2Beta, beta.BounceForce);
        }

        [SerializeField] private PlayerManager _player1;
        [SerializeField] private PlayerManager _player2;

        private void Start()
        {
            _player1.OnConflicted += ConflictHandler;
        }

        private void ResolveRigidBody(Rigidbody rb, Vector3 dir, float force)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}
