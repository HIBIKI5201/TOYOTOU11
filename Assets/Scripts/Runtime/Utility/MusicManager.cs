using DG.Tweening;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class MusicManager : MonoBehaviour
    {
        public async void PlayBGM(AudioClip clip)
        {
            DOTween.Sequence()
                .Append(_source.DOFade(0, 1))
                .OnComplete(() =>
                {
                    _source.Stop();
                    _source.clip = clip;
                    _source.Play();
                })
                .Append(_source.DOFade(1, 1));
        }

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private AudioSource _source;
    }
}
