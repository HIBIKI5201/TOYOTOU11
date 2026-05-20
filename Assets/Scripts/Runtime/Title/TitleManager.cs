using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gm;
        [SerializeField] private InGameLoopManager _ingameLoop;

        [ContextMenu(nameof(GameStart))]
        public void GameStart()
        {
            _gm.SetActiveIngameObjs(true);
            _ingameLoop.GameStart();
            _gm.SetActiveTitleObjs(false);
        }

        private void OnEnable()
        {
            
        }
    }
}
