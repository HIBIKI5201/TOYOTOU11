using Unity.VisualScripting;
using UnityEngine;

namespace TOYOTOU
{
    public class PlayerModelSelector : MonoBehaviour
    {
        public void Change(int index)
        {
            index = (index + _characterModels.Length) % _characterModels.Length;

            CharacterModelController model = _characterModels[index];
            ActivateModel(model);
            DeactivateModel(_characterModels[_index]);
            _index = index;
        }

        [SerializeField] CharacterModelController[] _characterModels;

        private int _index;

        private void Start()
        {
            for (int i = 0; i < _characterModels.Length; i++)
            {
                CharacterModelController model = Instantiate(_characterModels[i], transform);
                DeactivateModel(model);
                _characterModels[i] = model;
            }

            ActivateModel(_characterModels[0]);
        }

        private void ActivateModel(CharacterModelController model)
        {
            model.gameObject.SetActive(true);
        }

        private void DeactivateModel(CharacterModelController model)
        {
            model.gameObject.SetActive(false);
        }
    }
}
