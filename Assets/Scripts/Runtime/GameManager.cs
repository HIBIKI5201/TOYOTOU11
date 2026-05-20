using System.Collections.Generic;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class GameManager : MonoBehaviour
    {
        public void SetActiveTitleObjs(bool value) => _titleObjes.ForEach(go => go.SetActive(value));
        public void SetActiveIngameObjs(bool value) => _ingameObjs.ForEach(go => go.SetActive(value));

        [SerializeField] List<GameObject> _titleObjes;
        [SerializeField] List<GameObject> _ingameObjs;

        private void Start()
        {
            SetActiveTitleObjs(true);
            SetActiveIngameObjs(false);
        }
    }
}
