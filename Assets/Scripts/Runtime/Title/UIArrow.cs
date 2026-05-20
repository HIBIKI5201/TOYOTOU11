using DG.Tweening;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// UIの矢印（選択指示器）の振る舞いを管理するクラス
    /// </summary>
    public class UIArrow : MonoBehaviour
    {
        /// <summary>
        /// 矢印が選択された際のアニメーションを再生します
        /// </summary>
        public void Selected()
        {
            DOTween.Sequence()
                .Append(transform.DOScale(0.6f, 0.1f))
                .Append(transform.DOScale(1f, 0.1f))
                .SetLink(gameObject);
        }
    }
}
