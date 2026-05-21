using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// プレイヤーが場外に落下した際の判定と処理（即死ダメージの適用）を行うクラス。
    /// </summary>
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
