using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gm;
        [SerializeField] private InGameLoopManager _ingameLoop;

        [SerializeField] private InputActionKeyConfig _player1;
        [SerializeField] private InputActionKeyConfig _player2;

        [SerializeField] private PlayerModelSelector _selector1;
        [SerializeField] private PlayerModelSelector _selector2;

        [SerializeField] private UINavigater _player1Navigater;
        [SerializeField] private UINavigater _player2Navigater;

        [ContextMenu(nameof(GameStart))]
        public void GameStart()
        {
            _gm.State.Player1Model = _selector1.GetSelectedModel();
            _gm.State.Player2Model = _selector2.GetSelectedModel();

            _gm.SetActiveIngameObjs(true);
            _ingameLoop.GameStart();
            _gm.SetActiveTitleObjs(false);
        }

        private void OnEnable()
        {
            _player1Navigater.Enable(_player1);
            _player2Navigater.Enable(_player2);
        }

        private void OnDisable()
        {
            _player1Navigater.Disable();
            _player2Navigater.Disable();
        }
    }
}
