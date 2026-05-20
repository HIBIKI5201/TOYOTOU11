using DG.Tweening;
using UnityEngine;

namespace TOYOTOU
{
    public class PlayerModelController : MonoBehaviour
    {
        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
            transform.DOLocalMove(Vector3.zero, 1);
        }
    }
}
