using DG.Tweening;
using UnityEngine;

namespace TOYOTOU
{
    public class PlayerModelController : MonoBehaviour
    {
        public void SetParent(Transform parent)
        {
            transform.DOMove(parent.position, 1).SetEase(Ease.Linear);
            transform.SetParent(parent);
        }
    }
}
