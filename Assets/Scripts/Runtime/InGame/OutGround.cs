using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class OutGround : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody.TryGetComponent(out PlayerManager manager))
            {
                manager.TakeDamage(9999);
            }
        }
    }
}
