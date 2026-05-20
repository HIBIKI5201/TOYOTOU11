using Unity.Collections;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class ConflictResolver : MonoBehaviour
    {
        public void ConflictHandler(PlayerManager alpha, PlayerManager beta)
        {
            Rigidbody rbA = alpha.GetComponent<Rigidbody>();
            Rigidbody rbB = beta.GetComponent<Rigidbody>();


            /*
                    float power = collision.relativeVelocity.magnitude * _attackPower;
                    OtherManager.TakeDamage(power);

                    if (!collision.gameObject.TryGetComponent(out Rigidbody otherRb)) { return; }

                    ContactPoint contact = collision.contacts[0];
                    Vector3 pushDirection = contact.normal;
                    _rb.AddForce(pushDirection * _bounceForce, ForceMode.Impulse);
                    otherRb.AddForce(pushDirection * _bounceForce, ForceMode.Impulse);
            */
        }

        [SerializeField] private PlayerManager _player1;
        [SerializeField] private PlayerManager _player2;

        private void Start()
        {
            _player1.OnConflicted += ConflictHandler;
        }
    }
}
