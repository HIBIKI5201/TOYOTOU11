using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class GameSystem : MonoBehaviour
{
    [Header("System")]
    public float floattime = 1f;
    public float BattleTimer = 30;

    [Header("Other")]
    public TMP_Text timer;
    TMP_Text winner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BattleTimer -= floattime * Time.deltaTime;

        if (timer != null)
        {
            BattleTimer -= floattime * Time.deltaTime;
            Debug.Log("timer起動中");
        }
          if(BattleTimer <= 0f)
        {
            
            Finish();
        }
    }

    void Finish()
    {
        Debug.Log("time up"); 
    }
}
     
