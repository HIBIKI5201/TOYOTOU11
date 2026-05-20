using UnityEngine;

namespace TOYOTOU.Runtime
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

        [SerializeField] private PlayerStatus[] _stetuses;
        private CharacterModelController[] _characterModels;

        private int _index;

        private void Start()
        {
            for (int i = 0; i < _stetuses.Length; i++)
            {
                CharacterModelController model = Instantiate(_stetuses[i].Model, transform);
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
