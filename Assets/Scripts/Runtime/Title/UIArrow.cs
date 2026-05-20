using DG.Tweening;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class UIArrow : MonoBehaviour
    {
        public void Selected()
        {
            DOTween.Sequence()
                .Append(transform.DOScale(0.6f, 0.1f))
                .Append(transform.DOScale(1f, 0.1f))
                .SetLink(gameObject);
        }
    }
}
