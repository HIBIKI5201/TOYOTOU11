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

        [SerializeField] private Vector3 _rotateDirection = new Vector3(0, 1, 0);

        private float _speed = 10f;

        private void Start()
        {
            _rotateDirection.Normalize();
        }

        private void Update()
        {
            transform.Rotate(_rotateDirection * _speed * Time.deltaTime);
        }
    }
}
