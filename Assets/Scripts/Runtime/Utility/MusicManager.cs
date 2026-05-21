using DG.Tweening;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class MusicManager : MonoBehaviour
    {
        public async void PlayBGM(AudioClip clip)
        {
            _source.Stop();
            _source.clip = clip;
            _source.Play();
        }

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private AudioSource _source;
    }
}
