
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class GameSystem : MonoBehaviour
{
    [Header("System")]
    public float BattleTimer = 30;
    private float readytime = 2f;

    [Header("Other")]
    public GameObject ready;
    public GameObject Gosign;
    public GameObject timer;
    public GameObject winner;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winner.SetActive(false);
        ready.SetActive(true);
        Gosign.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (winner != null)
        {
           // timer.SetActive(true);
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

                if (timer != null)
                {
                    BattleTimer -= Time.deltaTime;
                    Debug.Log("timer起動中");
                }

                if (Mathf.Ceil(BattleTimer) / 5.00f == 0 && BattleTimer <= 30f)
                {
                    Debug.Log("5秒経過したので衝突が発生した");
                }
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
        }
    }
} 