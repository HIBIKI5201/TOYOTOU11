using TMPro;
using TOYOTOU.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TOYOTOU.Runtime
{
    public class SpeedGuage : MonoBehaviour
    {
        [SerializeField] private PlayerManager _player;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            _player.OnSpeedChanged += OnValueChangedHandler;
        }

        private void OnValueChangedHandler(float current, float max)
        {
            _image.fillAmount = current / max;
            _text.text = $"{current.ToString("0.0")} m/s";
        }
    }
}
