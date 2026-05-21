using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class SkillDelete : SkillBase
    {
        public override async void Execute(PlayerManager self, PlayerManager other)
        {
            Debug.Log($"消えるスキルを発動");

            int originalExcludeLayers = self.Rigidbody.excludeLayers;
            self.Rigidbody.excludeLayers |= _ignoreLayer;
            Renderer[] renderers = self.GetComponentsInChildren<Renderer>();
            Material[] origin = Transparent(renderers);

            await Awaitable.WaitForSecondsAsync(_duration);

            Resume(renderers, origin);
            self.Rigidbody.excludeLayers = originalExcludeLayers;
        }

        [SerializeField] private float _duration;
        [SerializeField] private LayerMask _ignoreLayer;
        [SerializeField] private float alpha = 0.5f;
        private Material transparentMaterial;
        private Material[] Transparent(Renderer[] renderers)
        {
            // URP Lit を使用
            Shader shader = Shader.Find("Universal Render Pipeline/Lit");

            transparentMaterial = new Material(shader);

            // Surface Type = Transparent
            transparentMaterial.SetFloat("_Surface", 1);

            // Alpha Blend
            transparentMaterial.SetFloat("_Blend", 0);

            // ZWrite Off
            transparentMaterial.SetFloat("_ZWrite", 0);

            // Render Queue
            transparentMaterial.renderQueue = 3000;

            Color color = Color.white;
            color.a = alpha;

            transparentMaterial.SetColor("_BaseColor", color);

            Material[] origin = new Material[renderers.Length];
            for (int i = 0; i < renderers.Length; i++)
            {
                Renderer r = renderers[i];
                origin[i] = r.material;
                r.material = transparentMaterial;
            }

            return origin;
        }

        private void Resume(Renderer[] renderers, Material[] material)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material = material[i];
            }
        }
    }
}
