using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    ///     自機を回転させるクラス。
    /// </summary>
    public class RotatePlayer : MonoBehaviour
    {
        /// <summary>
        ///     回転速度を設定する。
        /// </summary>
        /// <param name="value"></param>
        public void SetSpeed(float value) => _speed = value;

        public void WakeUp() => _isRotate = true;
        public void Sleep() => _isRotate = false;

        [SerializeField] private Vector3 _rotateDirection = new Vector3(0, 1, 0);
        [SerializeField] private float _speed = 10f;

        private bool _isRotate = true;

        private void Start()
        {
            _rotateDirection.Normalize();
        }

        private void Update()
        {
            if (!_isRotate) { return; }
            transform.Rotate(_rotateDirection * _speed * Time.deltaTime);
        }
    }
}
