using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TOYOTOU.Runtime
{
    /// <summary>
    ///     オープニング演出を管理するクラス。
    /// </summary>
    public class OpenningTimeline : MonoBehaviour
    {
        public async ValueTask Play()
        {

        }

        private float readytime = 2f;

        [Header("Text")]
        public GameObject ready;
        public GameObject Gosign;
        public GameObject timer;
        public GameObject winner;
        public GameObject title;

        [Header("Other")]
        bool start = false;

        void Start()
        {
            ready.SetActive(false);
            Gosign.SetActive(false);
            readytime = 2f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                start = !start;
                title.SetActive(false);
            }

            if (winner != null && start)
            {
                ready.SetActive(true);
                readytime -= Time.deltaTime;

                if (readytime <= 0)
                {
                    // 不正な代入を修正（戻り値に代入できない）
                    readytime = 0;
                    ready.SetActive(false);
                    Gosign.SetActive(true);
                }
            }
        }
    }
}