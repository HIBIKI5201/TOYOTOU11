using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class PlayerStatus : MonoBehaviour
    {

        [SerializeField] public int hitpoint = 10;
        [SerializeField] public int attack = 10;

        PlayerMoving _player;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _player = GetComponent<PlayerMoving>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}