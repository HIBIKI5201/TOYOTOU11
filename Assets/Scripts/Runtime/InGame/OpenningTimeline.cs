using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

namespace TOYOTOU.Runtime
{
    /// <summary>
    ///     オープニング演出を管理するクラス。
    /// </summary>
    [RequireComponent(typeof(PlayableDirector))]
    public class OpenningTimeline : MonoBehaviour
    {
        public async ValueTask Play()
        {
            _director.Play();

            try
            {
                await Awaitable.WaitForSecondsAsync((float)_director.duration, destroyCancellationToken);
            }
            catch(OperationCanceledException) { }
        }

        [Header("Text")]
        [SerializeField] private GameObject _ready;
        [SerializeField] private GameObject _gosign;

        private PlayableDirector _director;

        private void Awake()
        {
            _director = GetComponent<PlayableDirector>();

            _ready.SetActive(false);
            _gosign.SetActive(false);
        }
    }
}