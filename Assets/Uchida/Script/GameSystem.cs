
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameSystem : MonoBehaviour
{


[Header("Text")]
    public GameObject ready;
    public GameObject Gosign;
    public GameObject timer;
    public GameObject winner;
    public GameObject title;

    [Header("Other")]
    bool start = false;

    [Header("audio")]
    public AudioSource gamewin;
    public AudioSource fancall;
    public AudioSource battlemusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    void Start()
    {
        winner.SetActive(false);
        ready.SetActive(false);
        Gosign.SetActive(false);
        title.SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && !start)
        {
            start = true;
            title.SetActive(false);
            battlemusic.loop = true;
            battlemusic.Play();
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

            time -= Time.deltaTime;
            yield return null;
        
        if(time < 0f)
        Finish();
    }

void Finish()
        {
            Debug.Log("time up");
            gamewin.Play();
            winner.SetActive(true);
            fancall.Play();

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                start = !start;
                Start();
            }
        }
    }
