using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class PlayerModelSelector : MonoBehaviour
    {
        public PlayerModelController GetSelectedModel() => _characterModels[_index];

        public void Change(int index)
        {
            index = (index + _characterModels.Length) % _characterModels.Length;

            PlayerModelController model = _characterModels[index];
            ActivateModel(model);
            DeactivateModel(_characterModels[_index]);
            _index = index;
        }

        [SerializeField] private PlayerStatus[] _stetuses;
        private PlayerModelController[] _characterModels;

        private int _index;

        private void Start()
        {
            _characterModels = new PlayerModelController[_stetuses.Length];
            for (int i = 0; i < _stetuses.Length; i++)
            {
                PlayerModelController model = Instantiate(_stetuses[i].Model, transform);
                DeactivateModel(model);
                _characterModels[i] = model;
            }

            ActivateModel(_characterModels[0]);
        }

        private void ActivateModel(PlayerModelController model)
        {
            model.gameObject.SetActive(true);
        }

        private void DeactivateModel(PlayerModelController model)
        {
            model.gameObject.SetActive(false);
        }
    }
}
