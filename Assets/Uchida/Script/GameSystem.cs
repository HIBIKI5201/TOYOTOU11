using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameSystem : MonoBehaviour
{
    [Header("Text")]
    public GameObject ready;
    public GameObject Gosign;
    public GameObject winner;
    public GameObject title;
    public GameObject timetext;

    [Header("Other")]
    bool start = false;
    public float battletime = 30f;
    public TMP_Text timer;

    [Header("Audio Clips")]
    public AudioClip titleclip;
    public AudioClip gamewin;
    public AudioClip fancall;
    public AudioClip rappa;
    public AudioClip battlemusic;

    [Header("Audio Sources")]
    public AudioSource bgm;
    public AudioSource sound;

    void Start()
    {
        winner.SetActive(false);
        ready.SetActive(false);
        Gosign.SetActive(false);
        title.SetActive(true);
        bgm.PlayOneShot(titleclip);
        timetext.SetActive(false);
        
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && !start)
        {
            start = true;
            bgm.Stop();           
            title.SetActive(false);

            StartCoroutine(BattleStart());
        }
    }

    IEnumerator BattleStart()
    {
        ready.SetActive(true);
        yield return new WaitForSeconds(2f);

        ready.SetActive(false);
        Gosign.SetActive(true);
        yield return new WaitForSeconds(1f);

        Gosign.SetActive(false);

        bgm.clip = battlemusic;
        bgm.loop = true;
        bgm.Play();

        timetext.SetActive(true);

        float time = battletime;

        while (time > 0f)
        {
            time -= Time.deltaTime;
            timer.text = time.ToString("F0");
            yield return null; 
        }

        Finish();
    }

    void Finish()
    {
        Debug.Log("time up");
        timetext.SetActive(!timetext);
        bgm.Stop();

        sound.PlayOneShot(rappa);
        sound.PlayOneShot(fancall);
        bgm.clip = gamewin;
        bgm.loop = false;
        bgm.Play();

        winner.SetActive(true);

        start = false;
    }

}
