using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class ResultManager : MonoBehaviour
    {
        public event Action OnGameEndTriggered;

        public void JudgeWinner(PlayerInitializer playerInitializer)
        {
            PlayerManager player1 = playerInitializer.Player1;
            PlayerManager player2 = playerInitializer.Player2;
            float player1HP = player1.RemainHitPoint;
            float player2HP = player2.RemainHitPoint;

            bool isPlayer1Dead = player1HP <= 0;
            bool isPlayer2Dead = player2HP <= 0;

            if (isPlayer1Dead && isPlayer2Dead)
            {
                SetActiveDrawUI();
                return;
            }

            if (isPlayer1Dead)
            {
                SetActivePlayer2UI();
                return;
            }

            if (isPlayer2Dead)
            {
                SetActivePlayer1UI();
                return;
            }

            if (player1HP == player2HP)
            {
                SetActiveDrawUI();
            }
            else
            {
                if (player1HP > player2HP)
                {
                    SetActivePlayer1UI();
                }
                else
                {
                    SetActivePlayer2UI();
                }
            }
        }

        public void SetActivePlayer1UI() => _player1WinUI.ForEach(go => go.SetActive(true));
        public void SetActivePlayer2UI() => _player2WinUI.ForEach(go => go.SetActive(true));
        public void SetActiveDrawUI() => _drawUI.ForEach(go => go.SetActive(true));

        public async ValueTask WaitPressedGameEnd()
        {
            TaskCompletionSource<byte> task = new();

            Action onTriggerdAction = null;
            onTriggerdAction = () =>
            {
                OnGameEndTriggered -= onTriggerdAction;
                task.SetResult(0);
            };

            OnGameEndTriggered += onTriggerdAction;

            await task.Task;
        }

        [SerializeField] private List<GameObject> _player1WinUI;
        [SerializeField] private List<GameObject> _player2WinUI;
        [SerializeField] private List<GameObject> _drawUI;

        [ContextMenu("Inactive UI")]
        private void Awake()
        {
            _player1WinUI.ForEach(go => go.SetActive(false));
            _player2WinUI.ForEach(go => go.SetActive(false));
            _drawUI.ForEach(go => go.SetActive(false));
        }
    }
}
