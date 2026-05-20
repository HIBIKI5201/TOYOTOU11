
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameSystem : MonoBehaviour
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created



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
        if (Keyboard.current.fKey.wasPressedThisFrame && !start)
        {
            start = true;
            title.SetActive(false);
            StartCoroutine(BattleFlow());
        }
    }

    IEnumerator BattleFlow()
    {
        ready.SetActive(true);
        yield return new WaitForSeconds(2f);
        ready.SetActive(false);

        Gosign.SetActive(true);
        yield return new WaitForSeconds(1f);
        Gosign.SetActive(false);

        float time = 30f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        Finish();
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
