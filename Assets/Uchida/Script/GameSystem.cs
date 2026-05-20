using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameSystem : MonoBehaviour
{
    [Header("Text")]
    public GameObject ready;
    public GameObject Gosign;
    public GameObject winner;
    public GameObject title;

    [Header("Other")]
    bool start = false;
    public float battletime = 30f;

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
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && !start)
        {
            start = true;
            bgm.Stop();           
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

        bgm.clip = battlemusic;
        bgm.loop = true;
        bgm.Play();

        yield return new WaitForSeconds(battletime);

        Finish();
    }

    void Finish()
    {
        Debug.Log("time up");

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
