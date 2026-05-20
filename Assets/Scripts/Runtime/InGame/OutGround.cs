using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class OutGround : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerManager manager))
            {
                manager.TakeDamage(9999);
            }
        }
    }
}
