using UnityEngine;

namespace TOYOTOU.Runtime
{
    public class PlayerModelSelector : MonoBehaviour
    {
        public PlayerModelController GetSelectedModel() => _characterModels[_index];
        public int Index => _index;

        public void Next(float selector)
        {
            int dir = (int)Mathf.Sign(selector);
            Change(_index + dir);
        }

        public void Change(int index)
        {
            index = (index + _characterModels.Length) % _characterModels.Length;

            PlayerModelController model = _characterModels[index];
            ActivateModel(model);
            DeactivateModel(_characterModels[_index]);
            _index = index;
        }

        [SerializeField] private DataRepository _repo;
        private PlayerModelController[] _characterModels;

        private int _index;

        private void Start()
        {
            PlayerStatus[] statuses = _repo.States;
            _characterModels = new PlayerModelController[statuses.Length];
            for (int i = 0; i < statuses.Length; i++)
            {
                PlayerModelController model = Instantiate(statuses[i].Model, transform);
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
