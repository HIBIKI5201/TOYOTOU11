using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gm;
        [SerializeField] private InGameLoopManager _ingameLoop;

        [SerializeField] private PlayerModelSelector _selector1;
        [SerializeField] private PlayerModelSelector _selector2;

        [ContextMenu(nameof(GameStart))]
        public void GameStart()
        {
            _gm.State.Player1Model = _selector1.GetSelectedModel();
            _gm.State.Player2Model = _selector2.GetSelectedModel();

            _gm.SetActiveIngameObjs(true);
            _ingameLoop.GameStart();
            _gm.SetActiveTitleObjs(false);
        }
    }
}
