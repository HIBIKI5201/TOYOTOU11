using System.Collections.Generic;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class GameManager : MonoBehaviour
    {
        public void SetActiveTitleObjs() => _titleObjes.ForEach(go => go.SetActive(true));
        public void SetActiveIngameObjs() => _ingameObjs.ForEach(go => go.SetActive(true));

        [SerializeField] List<GameObject> _titleObjes;
        [SerializeField] List<GameObject> _ingameObjs;
    }
}
