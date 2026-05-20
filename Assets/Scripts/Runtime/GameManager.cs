using SymphonyFrameWork.Attribute;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TOYOTOU.Runtime
{
    public class GameManager : MonoBehaviour
    {
        public void SetActiveTitleObjs(bool value) => _titleObjes.ForEach(go => go.SetActive(value));
        public void SetActiveIngameObjs(bool value) => _ingameObjs.ForEach(go => go.SetActive(value));
        public void GameReset() => SceneManager.LoadScene(_sceneName);

        [SerializeField] List<GameObject> _titleObjes;
        [SerializeField] List<GameObject> _ingameObjs;
        [SerializeField, SceneNameSelector] string _sceneName;

        private void Start()
        {
            SetActiveTitleObjs(true);
            SetActiveIngameObjs(false);
        }
    }
}
