using System.Collections.Generic;
using UnityEngine;

namespace TOYOTOU
{
    public class ResultManager : MonoBehaviour
    {
        public void SetActivePlayer1UI() => _player1WinUI.ForEach(go => go.SetActive(true));
        public void SetActivePlayer2UI() => _player2WinUI.ForEach(go => go.SetActive(true));

        [SerializeField] private List<GameObject> _player1WinUI;
        [SerializeField] private List<GameObject> _player2WinUI;

        private void Start()
        {
            _player1WinUI.ForEach(go => go.SetActive(false));
            _player2WinUI.ForEach(go => go.SetActive(false));
        }
    }
}
