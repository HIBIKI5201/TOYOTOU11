using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class PlayerInitializer : MonoBehaviour
    {
        public PlayerStatus _player1Status;
        public PlayerStatus _player2Status;

        public void Init()
        {
            _player1Manager.Init(_player1Status);
            _player2Manager.Init(_player2Status);
        }

        public void PlayerControlEnable()
        {
            _player1Manager.SetCanControl(true);
            _player2Manager.SetCanControl(true);
        }

        [SerializeField] private PlayerManager _player1Manager;
        [SerializeField] private PlayerManager _player2Manager;
    }
}
