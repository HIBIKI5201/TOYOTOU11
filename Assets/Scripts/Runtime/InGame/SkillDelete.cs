using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class SkillDelete : SkillBase
    {
        public override async void Execute(PlayerManager self, PlayerManager other)
        {
            int originalExcludeLayers = self.Rigidbody.excludeLayers;
            self.Rigidbody.excludeLayers |= _ignoreLayer;

            await Awaitable.WaitForSecondsAsync(_duration);

            self.Rigidbody.excludeLayers = originalExcludeLayers;
        }

        [SerializeField] private float _duration;
        [SerializeField] private LayerMask _ignoreLayer;
    }
}
