using UnityEngine;
using UnityEngine.UI;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// 体力バーの管理クラス
    /// </summary>
    public class HitPointBarManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;

        [SerializeField] private Image _guage;
        private void Start()
        {
            _playerManager.OnHitPointChanged += ValueChangeHandler;
        }

        private void ValueChangeHandler(float current, float max)
        {
            _guage.fillAmount = current / max;
        }
    }
}
