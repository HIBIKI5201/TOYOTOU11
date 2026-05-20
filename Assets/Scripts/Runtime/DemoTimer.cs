using UnityEngine;

public class DemoTimer : MonoBehaviour
{

    [SerializeField] public float time = 30f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        time -= Time.deltaTime;
        if(time / 5 == 0)
        {
            Debug.Log("objectは中心に集まった");
        }
    }
}
