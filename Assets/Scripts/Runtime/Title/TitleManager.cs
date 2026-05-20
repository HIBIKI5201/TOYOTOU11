using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gm;
        [SerializeField] private InGameLoopManager _ingameLoop;

        public void GameStart()
        {
            _gm.SetActiveIngameObjs(true);
            _ingameLoop.GameStart();
            _gm.SetActiveTitleObjs(false);
        }
    }
}
