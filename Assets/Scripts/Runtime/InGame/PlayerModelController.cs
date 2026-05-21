using DG.Tweening;
using UnityEngine;

namespace TOYOTOU
{
    /// <summary>
    /// プレイヤーモデルの移動や親子関係を制御するクラス
    /// </summary>
    public class PlayerModelController : MonoBehaviour
    {
        /// <summary>
        /// 親となるTransformを設定し、その位置まで移動させます
        /// </summary>
        /// <param name="parent">設定する親のTransform</param>
        public void SetParent(Transform parent)
        {
            transform.DOMove(parent.position, 1).SetEase(Ease.Linear);
            transform.SetParent(parent);
        }
    }
}
