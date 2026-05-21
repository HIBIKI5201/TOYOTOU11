using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    ///     自機を回転させるクラス。
    /// </summary>
    public class RotatePlayer : MonoBehaviour
    {
        /// <summary>
        /// 回転速度を設定する。
        /// </summary>
        /// <param name="value"></param>
        public void SetSpeed(float value) => _speed = value;

        /// <summary>
        /// 回転処理を開始（再開）させます。
        /// </summary>
        public void WakeUp() => _isRotate = true;

        /// <summary>
        /// 回転処理を停止させます。
        /// </summary>
        public void Sleep() => _isRotate = false;

        [SerializeField, Tooltip("回転軸の方向")] private Vector3 _rotateDirection = new Vector3(0, 1, 0);
        [SerializeField, Tooltip("回転速度")] private float _speed = 10f;

        private bool _isRotate = true;

        private void Start()
        {
            _rotateDirection.Normalize();
            _isRotate = true;
        }

        private void Update()
        {
            if (!_isRotate) { return; }
            transform.Rotate(_rotateDirection * _speed * Time.deltaTime);
        }
    }
}
