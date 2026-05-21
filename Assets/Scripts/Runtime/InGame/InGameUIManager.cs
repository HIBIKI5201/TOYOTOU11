using DG.Tweening;
using System.Collections.Generic;
using TOYOTOU.Runtime;
using UnityEngine;

namespace TOYOTOU
{
    [RequireComponent(typeof(CanvasGroup))]
    public class InGameUIManager : MonoBehaviour
    {
        public void Init()
        {
            _skillCoolTimeGuageManagerList.ForEach(s => s.Init());
        }

        public void Visible() => _group.DOFade(1, 0.5f);

        [SerializeField] List<SkillCoolTimeGuageManager> _skillCoolTimeGuageManagerList;

        private CanvasGroup _group;
        private void Awake()
        {
            _group = GetComponent<CanvasGroup>();
            _group.alpha = 0;
        }
      
    }
}
