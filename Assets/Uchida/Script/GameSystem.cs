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


    /// <summary>
    /// もし、enterキーが押されたら以下の操作を行う
    /// soundaudioの停止
    /// bgmaudioの停止
    /// コルーチン操作の起動(timer処理の一種)
    /// </summary>
    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame && !start && winner)
        {
            start = true;
            bgm.Stop();
            sound.Stop();
            title.SetActive(false);
            winner.SetActive(false);

            StartCoroutine(BattleStart());
        }
    }


    /// <summary>
    /// 最初の2秒(2f)は、readyobjectをtrueに
    ///次の１秒(1f)は、Gosignobjectをtrueにする
    ///またその次の30秒間、timetextobjectのtrue
    ///そのobject内のtimerテキストコンポーネントのテキストの更新をwhileで行う
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// 30秒が経過したらFinishメソッドを呼び出し、
    /// またenterキーが押されるまで、bgmを再生する
    /// </summary>
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
