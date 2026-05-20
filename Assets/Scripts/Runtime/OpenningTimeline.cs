using UnityEngine;
using UnityEngine.InputSystem;

namespace TOYOTOU.Runtime
{
    /// <summary>
    ///     オープニング演出を管理するクラス。
    /// </summary>
    public class OpenningTimeline : MonoBehaviour
    {
        [Header("System")]
        public float BattleTimer = 30;
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
            winner.SetActive(false);
            ready.SetActive(false);
            Gosign.SetActive(false);
            title.SetActive(true);
            BattleTimer = 30f;
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
                // timer.SetActive(true);
                ready.SetActive(true);
                readytime -= Time.deltaTime;

                if (readytime <= 0)
                {
                    // 不正な代入を修正（戻り値に代入できない）
                    readytime = 0;
                    ready.SetActive(false);
                    Gosign.SetActive(true);


                    if (BattleTimer <= 29)
                        Gosign.SetActive(false);

                    BattleTimer -= Time.deltaTime;

                    if (BattleTimer <= 0f)
                    {
                        BattleTimer = 0;
                        Finish();
                    }
                }
            }

            void Finish()
            {
                Debug.Log("time up");
                winner.SetActive(true);

                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    start = !start;
                    Start();
                }
            }
        }
    }
}